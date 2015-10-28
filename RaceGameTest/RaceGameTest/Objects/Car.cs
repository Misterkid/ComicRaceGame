using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

using System.Drawing.Drawing2D;
using RaceGameTest.Q_Engine;
namespace RaceGameTest.Objects
{
    class Car:GameObject
    {
        public int speed = 0;//Add velocity to speed
        public int rotationSpeed = 200;

       // public int maxSpeed;
        public int mass = 1500;//Mass in KG
        public int maxFuel = 100;//Liters * 10
        public float fuel = 100; // Liters * 10
        public float surface = 1.9f;//m2
        public float CoEfficient = 0.32f;//Air resitance onzin
        public float velocity = 0;//CurrentVelocity
        public int currentMotorForce = 0;
        public int motorForce = 0;//Motor force! Horse power *need eddy het is in freaking WATT
        public int maxmotorForce = 27000; //Motor force! Horse power
        public bool isGoingForward = false;
        public bool isGoingBackwards = false;
        public bool pitchStop = false;//Are we on a pitchStop?
        public int checkPoints = 0;//checkpoitns taken.
        public int maxCheckPoints = 4;//4 checkpoints
        public int laps = 0;//How many laps did we do?
        public int maxLaps = 3;//How many laps does this car have to do ?

        public string breakSoundName = "break1";
        public string engineSoundName = "engine1";
        public string bumpSoundName = "bump1";

        public Car(string path):base(path)
        {

        }
        public void initSoundNames(string breakName,string engineName,string bumpName)
        {
            breakSoundName = breakName;
            engineSoundName = engineName;
            bumpSoundName = bumpName;
        }
        //Function to rotate the car left or right.
        public float Rotate(bool left,bool right)
        {
            int trueMass = CarPhysics.MassaAutoCalculated(fuel, mass);//Calculate mass rotation depends on it
            if(left && !right)
            {
                float rotation = CarPhysics.Rotation(velocity, trueMass);//Get rotation value from CarPhysics.
                if (isGoingBackwards)//Backwards?
                    return -rotation;//Return new rotation
                else
                    return rotation;
            }//Repeat
            else if( !left && right)
            {
                float rotation = CarPhysics.Rotation(velocity, trueMass);

                if (isGoingBackwards)
                    return rotation;
                else
                    return -rotation;
            }
            return 0;
        }
        //Calculate how the car moves(Forward,backward and how fast) using car physics from Michael
        public PointF Move(/*bool forward,bool backwards,*/bool isBreak)
        {
            motorForce = CarPhysics.F_motorCalculated(isGoingBackwards, motorForce, maxmotorForce,fuel, isGoingForward);
            fuel = CarPhysics.FuelCalculated(pitchStop, motorForce, fuel, maxFuel);

            int trueMass = CarPhysics.MassaAutoCalculated(fuel, mass);
            float airForce = CarPhysics.F_Air(CoEfficient, 1.19f, velocity, surface);
            float rolForce = CarPhysics.Frol(200, trueMass, 10, isBreak, velocity);
            velocity = CarPhysics.Velocity(velocity, trueMass, motorForce, rolForce, airForce, isGoingBackwards, isBreak, isGoingForward);

            float rad = (float)(angle * Math.PI / 180);
            float deltaX = (float)Math.Sin(rad) * velocity;//speed;
            float deltaY = (float)-Math.Cos(rad) * velocity;//speed;
            return new PointF(deltaX, deltaY);
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
