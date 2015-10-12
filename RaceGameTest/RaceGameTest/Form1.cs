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
namespace RaceGameTest
{
    public partial class Form1 : Form
    {
        private Game game = new Game();
        private Graphics graphics;
        //private Object[] gameObjects;
        private List<Objects.GameObject> gameObjects = new List<Objects.GameObject>();
        public Form1()
        {
            InitializeComponent();
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
            Invoke((MethodInvoker)(() =>
            {
                gameObject.angle = angle;
                Invalidate();
            }));
        }

        void game_OnUpdatePosition(Objects.GameObject gameObject, PointF newPosition)
        {
            Invoke((MethodInvoker)(() =>
            {
                gameObject.position = newPosition;
                Invalidate();
                //gameObject.pictureBox.Location = newPosition;
            }));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            for(int i = 0; i < gameObjects.Count; i++)
            {
                //now rotate the image
                float dx = (float)gameObjects[i].position.X + gameObjects[i].image.Width / 2;
                float dy = (float)gameObjects[i].position.Y + gameObjects[i].image.Height / 2;
                e.Graphics.TranslateTransform(dx, dy);
                e.Graphics.RotateTransform(gameObjects[i].angle);
                e.Graphics.TranslateTransform(-dx, -dy);
                //Draw image en update position.
                e.Graphics.DrawImage(gameObjects[i].image, gameObjects[i].position);
                e.Graphics.ResetTransform();
            }
        }
        void game_OnDrawGameObject(object sender, Objects.GameObject arg)
        {
            gameObjects.Add(arg);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
