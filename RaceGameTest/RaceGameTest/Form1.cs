﻿//#define __DEBUG_MODE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaceGameTest.Keyboard;

using System.Drawing.Drawing2D;
using RaceGameTest.Q_Engine;
using RaceGameTest.Objects;
namespace RaceGameTest
{
    partial class Form1 : Form
    {
        public Game game;

        private Graphics graphics;//Have to draw something. We use gdi+
        public Form1(Game _game)//I'm lazy to redo all
        {
            InitializeComponent();
            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;
            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

            this.KeyPreview = true;
            graphics = this.CreateGraphics();
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            game = _game;
            //Start update shit.
            game.InitializeGame();
            //Listen to OnDrawGameObject event
            game.OnDrawGameObject += game_OnDrawGameObject;
            game.DrawObjects();
            //Listen to Update position and rotatione events.(Could be one event :| maybe for later)
            game.OnUpdatePosition += game_OnUpdatePosition;
            game.OnUpdateRotation += game_OnUpdateRotation;
            //Update UI
            game.OnUpdateUI += game_OnUpdateUI;
            //Send out that the game has ended!
            game.OnGameEnd += game_OnGameEnd;
            //Make the endscreen invisible.
            EndGameScreen.Visible = false;
            EndGameScreen.BackgroundImage = Image.FromFile("_Images\\winner.bmp");
            //Controls.Add(pictureBox);

            //Add sounds ( It gets played in form1 )
            jSound.AddSound("3", "_Sounds\\3.wav", 1f);
            jSound.AddSound("2", "_Sounds\\2.wav", 1f);
            jSound.AddSound("1", "_Sounds\\1.wav", 1f);
            jSound.AddSound("go", "_Sounds\\go.wav", 1f);

            jSound.AddSound("Bgm", "_Sounds\\gameBGM.wav", 0.3f);
        }
        //If rotation event is fired. Rotate the object!
        void game_OnUpdateRotation(GameObject gameObject, float angle)
        {
            gameObject.angle = angle;
            gameObject.Update();
            Invalidate();//Redraw. Calls the onpaint function
        }
        //Same as above but with position
        void game_OnUpdatePosition(GameObject gameObject, PointF newPosition)
        {
            gameObject.position = newPosition;
            gameObject.Update();
            Invalidate();
        }
        //On paint. (gets called when invalidate gets called)
        protected override void OnPaint(PaintEventArgs e)
        {
            for(int i = 0; i < game.gameObjects.Count; i++)
            {
                //now rotate the image
                float dx = (float)game.gameObjects[i].position.X + game.gameObjects[i].image.Width * 0.5f;// / 2;
                float dy = (float)game.gameObjects[i].position.Y + game.gameObjects[i].image.Height * 0.5f;// / 2;
                e.Graphics.TranslateTransform(dx, dy);
                e.Graphics.RotateTransform(game.gameObjects[i].angle);
                e.Graphics.TranslateTransform(-dx, -dy);
                //Draw image en update position.
                e.Graphics.DrawImage(game.gameObjects[i].image, game.gameObjects[i].position);
                e.Graphics.ResetTransform();

                /* DEBUG! */
#if __DEBUG_MODE
                //Center
                e.Graphics.DrawEllipse(new Pen(Color.Turquoise),  game.gameObjects[i].position.X + game.gameObjects[i].center.X - 5,game.gameObjects[i].position.Y + game.gameObjects[i].center.Y - 5, 10, 10);
                e.Graphics.ResetTransform();
                //Collision circles Debug
                float centerXWorld = game.gameObjects[i].position.X + game.gameObjects[i].center.X;
                float centerYWorld = game.gameObjects[i].position.Y + game.gameObjects[i].center.Y;

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.topLeft.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.topLeft.Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.topRight.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.topRight.Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.botLeft.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.botLeft.Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.botRight.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.botRight.Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.topCenter.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.topCenter.Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.botCenter.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.botCenter.Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.leftCenter.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.leftCenter.Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].rotatedFourPoints.rightCenter.X, centerYWorld + game.gameObjects[i].rotatedFourPoints.rightCenter.Y, 2, 2);
                e.Graphics.ResetTransform();

#endif
            }
        }
        //Update the ui. (Winforms ui xD)
        void game_OnUpdateUI(Car carPlayer1, Car carPlayer2)
        {
            //Set texts.
            player1speedText.Text = (int)carPlayer1.velocity + " km/h";
            player2speedText.Text = (int)carPlayer2.velocity + " km/h";
            lapsPlayer1.Text = "Laps:" + carPlayer1.laps;
            lapsPlayer2.Text = "Laps:" + carPlayer2.laps;
            pitStopPlayer1.Text = "Pitstop:" + carPlayer1.pitchStop;
            pitStopPlayer2.Text = "Pitstop:" + carPlayer2.pitchStop;
            //set Fuel bar
            fuelbarrplayer1.Maximum = (int)carPlayer1.maxFuel;
            fuelbarrplayer2.Maximum = (int)carPlayer2.maxFuel;
            fuelbarrplayer1.Value = (int)carPlayer1.fuel;
            fuelbarrplayer2.Value = (int)carPlayer2.fuel;
            //if the game is running for less then 5 seconds
            if(QTime.RunTime < 5)
            {
                CountDownText.Text = (3 - QTime.RunTime).ToString();//Countdown text - 3
                switch((int)QTime.RunTime)//Switch on the time.
                {
                    case 0:
                        if(game.useSound)
                            jSound.PlaySound("3");//Play sound 3
                    break;
                        
                    case 1:
                        if (game.useSound)
                            jSound.PlaySound("2");
                    break;

                    case 2:
                        if (game.useSound)
                            jSound.PlaySound("1");
                    break;

                    case 3:
                        if (game.useSound)
                            jSound.PlaySound("go");

                        CountDownText.Text = "Go!";//Go!
                        game.canPlay = true;//We can drive
                    break;

                    case 4:
                        CountDownText.Hide();//Hide it after 4 seconds
                    break;
                }
            }
            if ((QTime.RunTime - 3) < 0)//If time -3 is lower then 0 the time is 0;
            {
                label2.Text = "Time:" + 0;
            }
            else
            {
                label2.Text = "Time:" + (QTime.RunTime - 3).ToString();
            }
            
        }
        void game_OnDrawGameObject(object sender, GameObject arg)
        {
            //gameObjects.Add(arg);
            arg.Update();//Update gameobject
            Invalidate();//Redraw. Calls the onpaint function.(Fires a event)
        }
        //Little hack because arrow keys doens't work anymore :/ Idk why
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //handle your keys here
            if (keyData == Keys.Up || keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Down)
            {
                game.input.SetKey(keyData, true);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //Key down
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.input.SetKey(e.KeyCode, true);//set keycode to true
        }
        //key up
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            game.input.SetKey(e.KeyCode, false);//set keycode to false
        }
        //Volume settings from menu
        public void SetSoundSettings(bool useMusic,bool useSound)
        {
            game.useMusic = useMusic;
            Console.WriteLine(useMusic);
            if (game.useMusic)
            {
                jSound.PlaySoundLooping("Bgm");
            }
            game.useSound = useSound;
        }
        //playernames from menu.Its alladeen anyway.
        public void SetPlayerNames(string player1,string player2)
        {
            game.player1Car.playerName = player1;
            game.player2Car.playerName = player2;

            label3.Text = player1;
            label4.Text = player2;
        }
        //Game has ended!
        void game_OnGameEnd()
        {
            jSound.StopAllSounds();
            EndGameScreen.Visible = true;
        }
        //clicking on exit button goes back to menu.
        private void Exit_Click(object sender, EventArgs e)
        {
            foreach (Form unhide in Application.OpenForms)
            {
                if (unhide is Menu)// if Unhide is of type Menu
                {
                    unhide.Show();
                    break;
                }
            }
            jSound.StopAllSounds();
            if(game.useSound)
                jSound.PlaySound("greet");
            if(game.useMusic)
                jSound.PlaySoundLooping("menuMusic");

            game.Dispose();
            game.Reset();

            game.OnDrawGameObject -= game_OnDrawGameObject;
            //game.input.RegisterKey(Keys.Up);
            game.DrawObjects();
            game.OnUpdatePosition -= game_OnUpdatePosition;
            game.OnUpdateRotation -= game_OnUpdateRotation;
            game.OnUpdateUI -= game_OnUpdateUI;
            this.Dispose();
            Close();
        }
    }
}
