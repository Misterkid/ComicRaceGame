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
        private Bitmap colBitmap;
        public Map()
        {

        }
        public void Draw(string pathToVisibleMap,string pathToColMap)
        {
            image = Image.FromFile(pathToVisibleMap);
            center = new PointF(image.Width / 2, image.Height / 2);
            //Image.FromFile(pathToColMap).
            colBitmap = new Bitmap(pathToColMap);//Bitmap.FromFile(pathToColMap);
            //rect = new RectangleF(position.X, position.Y, image.Width, image.Height);
            //rect.Location = position;

        }
        /*
        public bool CheckPixel(int x,int y, Color color)
        {
            if(colBitmap.GetPixel(x,y) == color)
            {
                return true;
            }
            return false;
        }*/
        
        public Color GetPixelAt(int x,int y)
        {
            return colBitmap.GetPixel(x, y);
        }
    }
    //yucky xD
    public class ColorCol
    {
        public static Color road = Color.FromArgb(255, 0, 0, 0);
        public static Color collision = Color.FromArgb(255, 255, 0, 0);
    }
}
