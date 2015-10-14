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
        public int speed = 200;//Add velocity to speed
        public int rotationSpeed = 200;

       // public int maxSpeed;

        public int mass = 1500;//Mass in KG
        public int maxFuel = 100;//Liters
        public int fuel = 100;
        public float surface = 1.9f;//m2
        public float CoEfficient = 0.42f;//Air resitance onzin
        public bool isReverse = false;//Is reverse
        public float velocity = 0;//CurrentVelocity
        public int motorForce = 27000;//Motor force! Horse power
        
        public Car(string path):base(path)
        {
           
        }
        //-MoveForward = backwards... Duh
        public PointF MoveForward()
        {
            float rad = (float)(angle * Math.PI / 180);
            float deltaX = (float)Math.Sin(rad) * speed;
            float deltaY = (float)-Math.Cos(rad) * speed;
            return new PointF(deltaX, deltaY);
            
        }
        public bool OnBoxCollision(GameObject other)
        {
            if (other != null)
            {
                //RectangleF currentRect = new RectangleF(position.X, position.Y, image.Width, image.Width);
                // RectangleF otherRect = new RectangleF(other.position.X, other.position.Y, other.image.Width, other.image.Width);
                if (boxRect.IntersectsWith(other.boxRect))
                {
                    return true;
                }
            }
            return false;
        }
        public override void Update()
        {
            // game.gameObjects[i].position.X + game.gameObjects[i].center.X - 5,game.gameObjects[i].position.Y + game.gameObjects[i].center.Y - 5, 10, 10);
            //ToDo add rotation in drawing the rectangle.... :(

            boxRect = new RectangleF(position.X, position.Y, image.Width, image.Height);
            PointF topLeft = new PointF(center.X - (image.Width/2),center.Y - (image.Height/2));

            PointF topRight = new PointF(boxRect.Right, boxRect.Top);
            PointF botLeft = new PointF(boxRect.Left, boxRect.Bottom);
            PointF botRight = new PointF(boxRect.Right, boxRect.Bottom);

            float rad = (float) (angle * Math.PI / 180);

            //x' = x \cos \theta - y \sin \theta\,,
           // y' = x \sin \theta + y \cos \theta\,.
            //center
            float x = topLeft.X * (float)Math.Cos(rad) - topLeft.Y * (float)Math.Sin(rad);
            float y = topLeft.X * (float)Math.Sin(rad) + topLeft.Y * (float)Math.Cos(rad);
            boxRect = new RectangleF(position.X + x, position.Y + y,image.Width,image.Height);

            base.Update();
        }
    }
}
