using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RaceGameTest.Objects
{
    class Car:GameObject
    {
        public int speed = 200;
        public int rotationSpeed = 200;
        public Car()
        {

        }
        public PointF MoveForward()
        {
            float raidance = (float) (angle * Math.PI / 180);
            float deltaX = (float)Math.Sin(raidance) * speed;
            float deltaY = (float)-Math.Cos(raidance) * speed;
            return new PointF(deltaX, deltaY);
            
        }
    }
}
