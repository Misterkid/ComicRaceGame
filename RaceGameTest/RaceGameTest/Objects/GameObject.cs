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
        public float angle = 0;

        public GameObject()
        {

        }
        public void Draw(string path)
        {
            image = Image.FromFile(path);
        }
        public void Rotate(/*PointF offset,*/float angle)
        {

        }
        public void Update()
        {

        }
    }
}
