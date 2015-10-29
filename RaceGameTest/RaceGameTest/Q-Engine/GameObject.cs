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
        public virtual void DrawCollisionImage(string pathToCol)
        {
            colBitmap = new Bitmap(pathToCol);

            /*
            colBitmap = new Bitmap(image);//RotateImage(colBitMap, angle);

            
            for (int x = 0; x < colBitmap.Width; x++)
            {
                for (int y = 0; y < colBitmap.Height; y++)
                {
                    if (x > 2 && x < (colBitmap.Width - 2) &&
                        y > 2 && y < (colBitmap.Height - 2))
                    {
                        colBitmap.SetPixel(x, y, ColorCol.collision);
                    }
                }
            }*/
            /*
            DrawPixelsFromPoint(4, rotatedFourPoints.topLeft, Color.Transparent);
            DrawPixelsFromPoint(4, rotatedFourPoints.topRight, Color.Transparent);
            DrawPixelsFromPoint(4, rotatedFourPoints.botLeft, Color.Transparent);
            DrawPixelsFromPoint(4, rotatedFourPoints.botRight, Color.Transparent);

            DrawPixelsFromPoint(4, rotatedFourPoints.topCenter, Color.Transparent);
            DrawPixelsFromPoint(4, rotatedFourPoints.botCenter, Color.Transparent);
            DrawPixelsFromPoint(4, rotatedFourPoints.leftCenter, Color.Transparent);
            DrawPixelsFromPoint(4, rotatedFourPoints.rightCenter, Color.Transparent);
           */
            //Lol Lets not have collision with ourself :)
            //colBitmap.SetPixel((int)fourPoints.topLeft.X, (int)fourPoints.topLeft.Y, Color.Transparent);
            //colBitmap.SetPixel((int)fourPoints.topRight.X, (int)fourPoints.topRight.Y, Color.Transparent);
            //colBitmap.SetPixel((int)fourPoints.botLeft.X, (int)fourPoints.botLeft.Y - 1, Color.Transparent);
            //colBitmap.SetPixel((int)fourPoints.botRight.X, (int)fourPoints.botRight.Y - 1, Color.Transparent);
        }

        public void DrawPixelsFromPoint(int width,PointF point,Color color)
        {
            int start = -(width / 2);
            int end = width / 2;
            for(int i = start; i < end; i ++)
            {
                for(int j = start; j < end; j ++)
                {
                    colBitmap.SetPixel((int)point.X + i, (int)point.Y + j, color);
                }
            }
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
