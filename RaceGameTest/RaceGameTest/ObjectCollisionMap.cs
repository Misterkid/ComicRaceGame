using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using RaceGameTest.Objects;
namespace RaceGameTest
{
    
    class ObjectCollisionMap
    {
        public Bitmap bitmap;
        private int width = 1024;
        private int height = 768;
        public ObjectCollisionMap()
        {
            bitmap = new Bitmap(width, height);
        }
        public void UpdateObjects(Game game)
        {
            //try
           // {
                //Bitmap updatedBitmap = new Bitmap(width, height);
            //remake :|
            try
            {
                bitmap.Dispose();
                bitmap = null;
                bitmap = new Bitmap(width, height);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    for (int i = 0; i < game.gameObjects.Count; i++)
                    {
                        if (!(game.gameObjects[i] is Map))//Not map!
                        {
                            //Used elsewhere?
                            float dx = (float)game.gameObjects[i].position.X + game.gameObjects[i].colBitmap.Width * 0.5f;// / 2;
                            float dy = (float)game.gameObjects[i].position.Y + game.gameObjects[i].colBitmap.Height * 0.5f;// / 2;
                            g.TranslateTransform(dx, dy);
                            g.RotateTransform(game.gameObjects[i].angle);
                            g.TranslateTransform(-dx, -dy);
                            //Draw image en update position.
                            g.DrawImage(game.gameObjects[i].colBitmap, game.gameObjects[i].position);
                            g.ResetTransform();
                        }
                    }

                }
            }
            catch
            {

            }
                /*
                bitmap = updatedBitmap;
                updatedBitmap.Dispose();
                updatedBitmap = null;*/
            //}
            //catch(Exception e)
            //{
                //Console.WriteLine(e.Message + ":" + e.StackTrace + ":" + e.Source);
            //}
        }
        public Color GetPixelAt(int x, int y)
        {

            //Console.WriteLine(image.Size);
            try
            {
                if (x > 0 && y > 0 && x < bitmap.Width && y < bitmap.Height)
                    return bitmap.GetPixel(x, y);
                else
                    return Color.White;
            }
            catch
            {
                return Color.White;
            }

        }
    }
}
