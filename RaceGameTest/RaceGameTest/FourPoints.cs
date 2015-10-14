using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
namespace RaceGameTest
{
    class FourPoints
    {
        public PointF topLeft;
        public PointF topRight;
        public PointF botLeft;
        public PointF botRight;
        public FourPoints(PointF tl,PointF tr, PointF bl,PointF br)
        {
            topLeft = tl;
            topRight = tr;
            botLeft = bl;
            botRight = br;
        }
    }
}
