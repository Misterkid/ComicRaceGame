using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

namespace RaceGameTest.Objects
{
    class GameObject
    {
        public Image image;
        public PointF position = new PointF(0, 0);
        public PointF center;
        public float angle = 0;



        public RectangleF boxRect = new RectangleF();

        public GameObject()
        {

        }
        public void Draw(string path)
        {
            image = Image.FromFile(path);
            center = new PointF(image.Width / 2, image.Height / 2);
            //rect = new RectangleF(position.X, position.Y, image.Width, image.Height);
            //rect.Location = position;

        }
        /*
        public PointF GetCollisionDots()
        {

            float rad = (float)(angle * Math.PI / 180);
            GraphicsUnit unit = GraphicsUnit.Pixel;
            //return corner1;
            //TOP LEFT
            PointF top = new PointF(image.GetBounds(ref unit).Left, image.GetBounds(ref unit).Top);
            return new PointF((top.X * (float)Math.Sin(rad)) + position.X, (top.Y * (float)-Math.Cos(rad)) + position.Y);


            /* From the internet.
            corner1 = new Vector2((rect.Center.X + rect.Width / 2) * (float)Math.Cos(rot) - (rect.Center.Y + rect.Width / 2) * (float)Math.Sin(rot), (rect.Center.Y + rect.Width / 2) * (float)Math.Cos(rot) + (rect.Center.X + rect.Width / 2) * (float)Math.Sin(rot));
            corner2 = new Vector2((rect.Center.X - rect.Width / 2) * (float)Math.Cos(rot) - (rect.Center.Y + rect.Width / 2) * (float)Math.Sin(rot), (rect.Center.Y - rect.Width / 2) * (float)Math.Cos(rot) + (rect.Center.X + rect.Width / 2) * (float)Math.Sin(rot));
            corner3 = new Vector2((rect.Center.X + rect.Width / 2) * (float)Math.Cos(rot) - (rect.Center.Y - rect.Width / 2) * (float)Math.Sin(rot), (rect.Center.Y + rect.Width / 2) * (float)Math.Cos(rot) + (rect.Center.X - rect.Width / 2) * (float)Math.Sin(rot));
            corner4 = new Vector2((rect.Center.X - rect.Width / 2) * (float)Math.Cos(rot) - (rect.Center.Y - rect.Width / 2) * (float)Math.Sin(rot), (rect.Center.Y - rect.Width / 2) * (float)Math.Cos(rot) + (rect.Center.X - rect.Width / 2) * (float)Math.Sin(rot));

        }*/

        /*
        public float Top()
        {
            float raidance = (float)(angle * Math.PI / 180);
            float deltaX = (float)Math.Sin(raidance);
           // float deltaY = (float)-Math.Cos(raidance);
            float x = deltaX + position.X + (image.Width/2);


            return x;
        }*/
        /*
        public void Rotate(float angle)
        {

        }*/
        /*
        public void Update()
        {
            //ToDo add rotation in drawing the rectangle.... :(
            boxRect = new RectangleF(position.X , position.Y  , image.Width, image.Width);
        }
         */
        public virtual void Update()
        {

        }
    }
}
