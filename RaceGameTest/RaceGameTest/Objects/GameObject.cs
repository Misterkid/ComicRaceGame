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
        public FourPoints fourPoints;
        public GameObject(string path)
        {
            image = Image.FromFile(path);
            center = new PointF(image.Width / 2, image.Height / 2);
            //fourPoints
            PointF topLeft = new PointF(center.X - (image.Width / 2), center.Y - (image.Height / 2));
            PointF topRight = new PointF(center.X + (image.Width / 2), center.Y - (image.Height / 2));
            PointF botLeft = new PointF(center.X - (image.Width / 2), center.Y + (image.Height / 2));
            PointF botRight = new PointF(center.X + (image.Width / 2), center.Y + (image.Height / 2));
            fourPoints = new FourPoints(topLeft, topRight, botLeft, botRight);
        }
        //Trying to move one point.
        public PointF MatTest()
        {
            //PointF topLeft = new PointF(center.X - (image.Width / 2), center.Y - (image.Height / 2));
            float rad = (float)(angle * Math.PI / 180);
            float x = fourPoints.topLeft.X * (float)Math.Cos(rad) - fourPoints.topLeft.Y * (float)Math.Sin(rad);
            float y = fourPoints.topLeft.X * (float)Math.Sin(rad) + fourPoints.topLeft.Y * (float)Math.Cos(rad);
            return new PointF(x, y);
        }
        public virtual void Update()
        {

        }
    }
}
