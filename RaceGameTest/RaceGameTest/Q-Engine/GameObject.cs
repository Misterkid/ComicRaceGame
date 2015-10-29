using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RaceGameTest.Q_Engine
{
    class GameObject
    {
        public Image image;
        public PointF position = new PointF(0, 0);
        public PointF center;
        public float angle = 0;
        public RectangleF boxRect = new RectangleF();

        public FourPoints fourPoints;
        public FourPoints rotatedFourPoints;
        //For collision
        public PointF lastPos;
        public float lastAngle;
        public int width;
        public int height;
        public Bitmap colBitmap;
        //Constructor.! it needs a path for a image!
        public GameObject(string path)
        {
            image = Image.FromFile(path);
            width = image.Width;
            height = image.Height;
            //bitmap = new Bitmap(image);

            center = new PointF(image.Width / 2, image.Height / 2);
            
            //fourPoints
            PointF topLeft = new PointF(center.X - (float)(image.Width / 2), center.Y - (float)(image.Height / 2));
            PointF topRight = new PointF(center.X + (float)(image.Width / 2), center.Y - (float)(image.Height / 2));
            PointF botLeft = new PointF(center.X - (float)(image.Width / 2), center.Y + (float)(image.Height / 2));
            PointF botRight = new PointF(center.X + (float)(image.Width / 2), center.Y + (float)(image.Height / 2));
            fourPoints = new FourPoints(topLeft, topRight, botLeft, botRight,center);
            rotatedFourPoints = new FourPoints(RotatePoint(fourPoints.topLeft), RotatePoint(fourPoints.topRight), RotatePoint(fourPoints.botLeft), RotatePoint(fourPoints.botRight),center);
            
            
           // DrawCollisionImage();
        }
        //draw image collision if used...
        
        public virtual void DrawCollisionImage(string pathToCol)
        {
            colBitmap = new Bitmap(pathToCol);
        }
        //Rotate points from the center of the object.
        public PointF RotatePoint(PointF point) 
        {
            float rad = -(float)(angle * Math.PI / 180);
            float zeroCenterX = (point.X - (center.X ));
            float zeroCenterY = (point.Y - (center.Y ));

            float x = zeroCenterX * (float)Math.Cos(rad) + zeroCenterY * (float)Math.Sin(rad);
            float y = zeroCenterX * (float)-Math.Sin(rad) + zeroCenterY * (float)Math.Cos(rad);

            return new PointF(x , y );
        }
        //For now it just updates the "collision" points.
        //I will have to improve those when I have more time!
        public virtual void Update()
        {
            rotatedFourPoints.topLeft = RotatePoint(fourPoints.topLeft);
            rotatedFourPoints.topRight = RotatePoint(fourPoints.topRight);
            rotatedFourPoints.botLeft = RotatePoint(fourPoints.botLeft);
            rotatedFourPoints.botRight = RotatePoint(fourPoints.botRight);

            rotatedFourPoints.topCenter = RotatePoint(fourPoints.topCenter);
            rotatedFourPoints.botCenter = RotatePoint(fourPoints.botCenter);

            rotatedFourPoints.rightCenter = RotatePoint(fourPoints.rightCenter);
            rotatedFourPoints.leftCenter = RotatePoint(fourPoints.leftCenter);

            //Console.WriteLine(rotatedFourPoints.leftCenter + ":" + rotatedFourPoints.rightCenter);

            //test = RotatePoint(new PointF(center.X,fourPoints.topLeft.Y));
                // = new FourPoints(RotatePoint(topCenter), RotatePoint(botCenter), RotatePoint(leftCenter), RotatePoint(rightCenter));
        }
    }
}
