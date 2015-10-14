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

using System.Runtime.InteropServices;
namespace RaceGameTest
{
    public partial class Form1 : Form
    {
        private Game game = new Game();
        private Graphics graphics;
        //private Object[] gameObjects;
       // private List<Objects.GameObject> gameObjects = new List<Objects.GameObject>();
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public Form1()
        {
            InitializeComponent();
            AllocConsole();

            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;
            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;


            graphics = this.CreateGraphics();

            game.OnDrawGameObject += game_OnDrawGameObject;
            //game.input.RegisterKey(Keys.Up);
            game.DrawObjects();
            game.OnUpdatePosition += game_OnUpdatePosition;
            game.OnUpdateRotation += game_OnUpdateRotation;
            //Controls.Add(pictureBox);
        }

        void game_OnUpdateRotation(Objects.GameObject gameObject, float angle)
        {
           // throw new NotImplementedException();
           // Invoke((MethodInvoker)(() =>
            //{
                gameObject.angle = angle;
                gameObject.Update();
                Invalidate();
            //}));
        }

        void game_OnUpdatePosition(Objects.GameObject gameObject, PointF newPosition)
        {
            //Invoke((MethodInvoker)(() =>
           // {
                gameObject.position = newPosition;
                gameObject.Update();
                Invalidate();
                //gameObject.pictureBox.Location = newPosition;
            //}));
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

                //Center
                e.Graphics.DrawEllipse(new Pen(Color.Turquoise),  game.gameObjects[i].position.X + game.gameObjects[i].center.X - 5,game.gameObjects[i].position.Y + game.gameObjects[i].center.Y - 5, 10, 10);
                e.Graphics.ResetTransform();
                //Collision circles Debug
                float centerXWorld = game.gameObjects[i].position.X + game.gameObjects[i].center.X;
                float centerYWorld = game.gameObjects[i].position.Y + game.gameObjects[i].center.Y;

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.topLeft).X, centerYWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.topLeft).Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.topRight).X, centerYWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.topRight).Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.botLeft).X, centerYWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.botLeft).Y, 2, 2);
                e.Graphics.ResetTransform();

                e.Graphics.DrawEllipse(new Pen(Color.Yellow), centerXWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.botRight).X, centerYWorld + game.gameObjects[i].RotatePoint(game.gameObjects[i].fourPoints.botRight).Y, 2, 2);
                e.Graphics.ResetTransform();
                
                /*
                e.Graphics.DrawRectangle(new Pen(Color.DarkRed),gameObjects[i].GetCollisionDots().X, gameObjects[i].GetCollisionDots().Y, 10, 10);
                e.Graphics.ResetTransform();
                 */ 
            }
        }
        void game_OnDrawGameObject(object sender, Objects.GameObject arg)
        {
            //gameObjects.Add(arg);
            arg.Update();
            Invalidate();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.input.SetKey(e.KeyCode, true);

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            game.input.SetKey(e.KeyCode, false);
        }

    }
}
