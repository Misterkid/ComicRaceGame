//#define __DEBUG_MODE

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
            this.KeyPreview = true;
            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;
            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

            graphics = this.CreateGraphics();
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            game = _game;
            //game.Initialize();//Start update shit.
            game.OnDrawGameObject += game_OnDrawGameObject;
            //game.input.RegisterKey(Keys.Up);
            game.DrawObjects();
            game.OnUpdatePosition += game_OnUpdatePosition;
            game.OnUpdateRotation += game_OnUpdateRotation;
            game.OnUpdateUI += game_OnUpdateUI;
            //Controls.Add(pictureBox);
        }

        void game_OnUpdateRotation(GameObject gameObject, float angle)
        {
            gameObject.angle = angle;
            gameObject.Update();
            Invalidate();//Redraw. Calls the onpaint function
        }

        void game_OnUpdatePosition(GameObject gameObject, PointF newPosition)
        {
            gameObject.position = newPosition;
            gameObject.Update();
            Invalidate();
        }
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

        void game_OnUpdateUI(Car carPlayer1, Car carPlayer2)
        {
            player1speedText.Text = (int)carPlayer1.velocity + " km/h";
            player2speedText.Text = (int)carPlayer2.velocity + " km/h";

            //Fuel bar
            fuelbarrplayer1.Maximum = (int)carPlayer1.maxFuel;
            fuelbarrplayer2.Maximum = (int)carPlayer2.maxFuel;
            fuelbarrplayer1.Value = (int)carPlayer1.fuel;
            fuelbarrplayer2.Value = (int)carPlayer2.fuel;
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
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.input.SetKey(e.KeyCode, true);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            game.input.SetKey(e.KeyCode, false);
        }

        //Martin 
        /*
        public void PassValue1(string strvalue)
        {
            label3.Text = strvalue;
        }
        public void PassValue2(string strvalue)
        {
            label4.Text = strvalue;
        }*/
        public void SetPlayerNames(string player1,string player2)
        {
            label3.Text = player1;
            label4.Text = player2;
        }
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
            this.Dispose();
            Close();
        }

    }
}
