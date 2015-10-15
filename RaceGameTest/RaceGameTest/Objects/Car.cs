using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

using System.Drawing.Drawing2D;

namespace RaceGameTest.Objects
{
    class Car:GameObject
    {
        public int speed = 0;//Add velocity to speed
        public int rotationSpeed = 200;

       // public int maxSpeed;
        public int mass = 1500;//Mass in KG
        public int maxFuel = 100;//Liters * 10
        public int fuel = 100; // Liters * 10
        public float surface = 1.9f;//m2
        public float CoEfficient = 0.32f;//Air resitance onzin
        //public bool isReverse = false;//Is reverse
        //public bool isForward = true;//Is forward
       // public bool isBreaking = false;//Is breaking
        public float velocity = 0;//CurrentVelocity
        public int currentMotorForce = 0;
        public int motorForce = 0;//Motor force! Horse power *need eddy het is in freaking WATT
        public int maxmotorForce = 27000; //Motor force! Horse power
        public bool isGoingForward = false;
        public bool isGoingBackwards = false;
        public bool pitchStop = false;
        public int checkPoints = 0;//checkpoitns taken.
        public int maxCheckPoints = 4;//4 checkpoints
        public int laps = 0;
        public int maxLaps = 3;

        //For collision
        public PointF lastPos;
        public float lastAngle;
        //public bool[] checkPoints = new bool[4]{false,false,false,false};

        public Bitmap colBitMap;
        public Car(string path):base(path)
        {

        }
        /*
        private Bitmap RotateImage()
        {
            Bitmap rotatedImage = new Bitmap(image);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(image.Width / 2, image.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-image.Width / 2, -image.Height / 2); //restore rotation point into the matrix
                g.DrawImage(image, new Point(0, 0)); //draw the image on the new bitmap
            }
            return rotatedImage;
        }*/

        /*
        private Bitmap RotateImage(Bitmap bmp, float angleT)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angleT); //rotate
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2); //restore rotation point into the matrix
                g.DrawImage(bmp, new Point(0, 0)); //draw the image on the new bitmap
            }

            return rotatedImage;
        }*/
        private Bitmap RotateImage(Bitmap b, float Angle)
        {
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.TranslateTransform(b.Width / 2.0f, b.Height / 2.0f);
                g.RotateTransform(Angle);
                g.TranslateTransform(-b.Width / 2.0f, -b.Height / 2.0f);

                g.DrawImage(b, 0, 0);
            }

            return returnBitmap;
        }
        public void SetCollision()
        {
            colBitMap = new Bitmap(image);//RotateImage(colBitMap, angle);
            
            for (int x = 0; x < colBitMap.Width; x++)
            {
                for (int y = 0; y < colBitMap.Height; y++)
                {
                    colBitMap.SetPixel(x, y, ColorCol.collision);
                }
            }
            colBitMap = RotateImage(colBitMap,angle);
            /*
            for (int x = 0; x < colBitMap.Width; x++)
            {
                for (int y = 0; y < colBitMap.Height; y++)
                {
                    colBitMap.SetPixel(x, y, ColorCol.collision);
                }
            }*/
           // colBitMap = RotateImage();
            
           //colBitMap = RotateImage(colBitMap, angle);
             
        }
        //-MoveForward = backwards... Duh
        public PointF MoveForward()
        {
            float rad = (float)(angle * Math.PI / 180);
            float deltaX = (float)Math.Sin(rad) * velocity;//speed;
            float deltaY = (float)-Math.Cos(rad) * velocity;//speed;
            return new PointF(deltaX, deltaY);
        }
       // public PointF MoveForward(bool isOnPitstop = false)
        public float Rotate(bool left,bool right)
        {
            int trueMass = CarPhysics.MassaAutoCalculated(fuel, mass);
            if(left && !right)
            {
                float rotation = CarPhysics.Rotation(velocity, trueMass);
                return rotation;
            }
            else if( !left && right)
            {
                float rotation = CarPhysics.Rotation(velocity, trueMass);
                return -rotation;
            }
            return 0;
        }
        public PointF Move(/*bool forward,bool backwards,*/bool isBreak)
        {
            /*
            int F_MotorCalc1 = F_motorCalculated(Reverse1, F_motor1, FuelCalc1, Forward1);
            FuelCalc1 = FuelCalculated(Pittstop1, F_MotorCalc1, FuelCalc1, MaxFuel1);
            int MassaCalc1 = MassaAutoCalculated(FuelCalc1, Massa1);
            float F_AirCalc1 = F_Air(0.42f, 1.19f, VelocCalc1, 1.9f);
            float F_RolCalc1 = Frol(200, MassaCalc1, 10, Break1, VelocCalc1);
            VelocCalc1 = Velocity(VelocCalc1, MassaCalc1, F_MotorCalc1, F_RolCalc1, F_AirCalc1, Reverse1, Break1, Forward1);
            float Rotation1 = Rotation(VelocCalc1, MassaCalc1);
            */
            //int F_MotorCalc1 = CarPhysics.F_motorCalculated(isGoingBackwards, motorForce, maxmotorForce, fuel, isGoingForward);
            motorForce = CarPhysics.F_motorCalculated(isGoingBackwards, motorForce, maxmotorForce, fuel, isGoingForward);

            fuel = CarPhysics.FuelCalculated(pitchStop, motorForce/*F_MotorCalc1*/, fuel, maxFuel);

            int trueMass = CarPhysics.MassaAutoCalculated(fuel, mass);
            float airForce = CarPhysics.F_Air(CoEfficient, 1.19f, velocity, surface);
            float rolForce = CarPhysics.Frol(200, trueMass, 10, isBreak, velocity);
            velocity = CarPhysics.Velocity(velocity, trueMass, motorForce /*F_MotorCalc1*/, rolForce, airForce, isGoingBackwards, isBreak, isGoingForward);
            //Console.WriteLine("{0}, {1}, {2}", motorForce, rolForce, airForce);
            //Thread.Sleep(1000);

           // Console.WriteLine(fuel  + ":" + motorForce);
            //speed = speed + (int)velocity;
            /*
            if (isRotating)
            {
                float rotation = CarPhysics.Rotation(velocity, trueMass);
                angle = rotation;
            }*/
            float rad = (float)(angle * Math.PI / 180);
            float deltaX = (float)Math.Sin(rad) * velocity;//speed;
            float deltaY = (float)-Math.Cos(rad) * velocity;//speed;
            return new PointF(deltaX, deltaY);
            
        }
        public bool OnBoxCollision(GameObject other)
        {
            if (other != null)
            {
                //RectangleF currentRect = new RectangleF(position.X, position.Y, image.Width, image.Width);
                // RectangleF otherRect = new RectangleF(other.position.X, other.position.Y, other.image.Width, other.image.Width);
                //region.Transform()
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
            /*
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
            */
            base.Update();
        }
    }
}
