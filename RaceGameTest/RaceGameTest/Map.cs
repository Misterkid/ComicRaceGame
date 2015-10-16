using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaceGameTest.Objects;

using System.Drawing;
namespace RaceGameTest
{
    class Map : GameObject
    {
        //private Bitmap colBitmap;
        public Map(string pathToVisibleMap, string pathToColMap):base(pathToVisibleMap)
        {
            //image = Image.FromFile(pathToVisibleMap);
            //center = new PointF(image.Width / 2, image.Height / 2);
            //Image.FromFile(pathToColMap).
            colBitmap = new Bitmap(pathToColMap);//Bitmap.FromFile(pathToColMap);
        }
        
        public Color GetPixelAt(int x,int y)
        {
            try
            {
                //Console.WriteLine(image.Size);
                if (x > 0 && y > 0 && x < colBitmap.Width && y < colBitmap.Height)
                    return colBitmap.GetPixel(x, y);
                else
                    return Color.White;
            }
            catch
            {
                return Color.White;
            }

        }
        protected override void DrawCollisionImage()
        {
            
            //base.DrawCollisionImage();
        }
    }
    //yucky xD
    public class ColorCol
    {
        public static Color road = Color.FromArgb(255, 0, 0, 0);
        public static Color collision = Color.FromArgb(255, 0, 0, 255);
        public static Color slow = Color.FromArgb(255, 230, 230, 0);
        public static Color pitstop = Color.FromArgb(255, 255, 100, 0);
        public static Color start = Color.FromArgb(255, 0, 255, 0);
        public static Color finnish = Color.FromArgb(255, 0, 200, 0);
        public static Color checkp1 = Color.FromArgb(255, 255, 0, 0);
        public static Color checkp2 = Color.FromArgb(255, 240, 0, 0);
        public static Color checkp3 = Color.FromArgb(255, 220, 0, 0);
        public static Color checkp4 = Color.FromArgb(255, 200, 0, 0);
    }
}
