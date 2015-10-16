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

        public PointF topCenter;
        public PointF botCenter;
        public PointF rightCenter;
        public PointF leftCenter;
        public FourPoints(PointF tl,PointF tr, PointF bl,PointF br,PointF center)
        {
            topLeft = tl;
            topRight = tr;
            botLeft = bl;
            botRight = br;
            topCenter = new PointF(center.X, tl.Y);
            botCenter = new PointF(center.X, bl.Y);

            leftCenter = new PointF(tl.X, center.Y);
            rightCenter = new PointF(tr.X, center.Y);
        }
    }
}
