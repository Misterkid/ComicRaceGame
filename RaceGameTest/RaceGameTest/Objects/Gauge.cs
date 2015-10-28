using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RaceGameTest.Objects;



namespace RaceGameTest.Objects
{
    class Gauge
    {
        float angle = 0;
        public Bitmap gaugeLine;// = new Bitmap("_Images//Red.png");
        private Bitmap source = new Bitmap("_Images//Red.png");
        public Gauge()
        {
            //  gaugeLine = RotateImage(gaugeLine, angle);
            gaugeLine = RotateImage(source, angle);
        }

        public void updateSpeedGauge(float Velocity)
        {
            angle = Math.Abs(Velocity / 2);
            Console.WriteLine(angle);
            gaugeLine = RotateImage(source, angle);
        }

        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2); //restore rotation point into the matrix
                g.DrawImage(bmp, new Point(0, 0)); //draw the image on the new bitmap
                g.ResetTransform();
            }

            return rotatedImage;
        }
    }
}
