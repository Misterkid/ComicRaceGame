﻿using System;
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

        public Car()
        {

        }
        //-MoveForward = backwards... Duh
        public PointF MoveForward()
        {
            float raidance = (float) (angle * Math.PI / 180);
            float deltaX = (float)Math.Sin(raidance) * speed;
            float deltaY = (float)-Math.Cos(raidance) * speed;
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
            //ToDo add rotation in drawing the rectangle.... :(
            boxRect = new RectangleF(position.X, position.Y, image.Width, image.Width);
            base.Update();
        }
    }
}
