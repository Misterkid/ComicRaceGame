﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using RaceGameTest.Objects;
using System.Windows.Forms;
using RaceGameTest.Keyboard;
using System.Drawing;

using System.Diagnostics;
namespace RaceGameTest
{

    class Game
    {

        private float deltaTime;
        private float prevFrame;
        private float currentFrame;
        private Stopwatch stopWatch = new Stopwatch();

        public Input input = new Input();

        private Car player1Car;
        private Car player2Car;
        private Map map;
        public List<Objects.GameObject> gameObjects = new List<Objects.GameObject>();
        public Game()
        {
            //60 frames per second = 16.6666667(1000/60)
            System.Timers.Timer gameTimer = new System.Timers.Timer(0.5f);//Locked ? ha not needed now!
            gameTimer.Start();
            gameTimer.Elapsed += gameTimer_Elapsed;

            stopWatch.Start();

            RegisterKeys();
        }
        //Register input here
        private void RegisterKeys()
        {
            //Player One
            input.RegisterKey(Keys.Up);
            input.RegisterKey(Keys.Down);
            input.RegisterKey(Keys.Left);
            input.RegisterKey(Keys.Right);
            //Player Two
            input.RegisterKey(Keys.W);
            input.RegisterKey(Keys.S);
            input.RegisterKey(Keys.A);
            input.RegisterKey(Keys.D);
        }
        public void DrawObjects()
        {

            /*The Map */
            map = new Map("_Images\\Circuit2.bmp", "_Images\\Circuit2.bmp");
            DrawObject(map);
            gameObjects.Add(map);
            /* Player one Car */
            player1Car = new Car("_Images\\JeffersonGTA2.png");
            player1Car.position = new PointF(300, 400);
            DrawObject(player1Car);
            gameObjects.Add(player1Car);
            /* Player Two Car */
            player2Car = new Car("_Images\\JeffersonGTA2.png");
            DrawObject(player2Car);
            gameObjects.Add(player2Car);
            //map.Draw()


        }
        private void MapColCheck(Car car)
        {
            if (car != null)
            {
                Color color = map.GetPixelAt((int)car.position.X + (int)car.center.X, (int)car.position.Y + (int)car.center.Y);
                // Can't put color in a switch for some reason!
                if(color == ColorCol.road)
                {
                    //Do things
                    //car.speed = 200;
                }
                else if( color == ColorCol.collision)
                {
                    //Do things
                    //car.speed = 0;
                }
                else if (color == ColorCol.slow)
                {
                    //Do things
                    //car.speed = 100;
                }
                else if (color == ColorCol.pitstop)
                {
                    //Do things
                }
                else if (color == ColorCol.start)
                {
                    //Do things
                }
                else if (color == ColorCol.finnish)
                {
                    //Do things
                }
                else if (color == ColorCol.checkp1)
                {
                    //Do things
                }
                else if (color == ColorCol.checkp2)
                {
                    //Do things
                }
                else if (color == ColorCol.checkp3)
                {
                    //Do things
                }
                else if (color == ColorCol.checkp4)
                {
                    //Do things
                }
            }
        }
        private void Update()
        {
            MapColCheck(player1Car);
            MapColCheck(player2Car);

            PlayerOneCarMovement();
            PlayerTwoCarMovement();
            /*
            if (player1Car != null)
            {
                if (player1Car.OnBoxCollision(player2Car))
                {
                    //Console.WriteLine("To Do");
                    PointF forward = player1Car.MoveForward();
                    PointF newPosition = new PointF(player1Car.position.X - ((forward.X * 5) * deltaTime) , player1Car.position.Y - ((forward.Y * 5) * deltaTime)) ;
                    OnUpdatePosition(player1Car, newPosition);

                    forward = player2Car.MoveForward();
                    newPosition = new PointF(player2Car.position.X - ((forward.X * 5) * deltaTime), player2Car.position.Y - ((forward.Y * 5) * deltaTime));
                    OnUpdatePosition(player2Car, newPosition);
                }
            }*/
        }
        private void PlayerOneCarMovement()
        {
            
            if (input.GetKey(Keys.Up))
            {
                PointF forward = player1Car.MoveForward();
                PointF forwardDelta = new PointF(forward.X * deltaTime, forward.Y * deltaTime);
                PointF newPosition = new PointF(player1Car.position.X + forwardDelta.X, player1Car.position.Y + forwardDelta.Y);
                OnUpdatePosition(player1Car, newPosition);
            }
            if (input.GetKey(Keys.Down))
            {
                PointF forward = player1Car.MoveForward();
                PointF forwardDelta = new PointF(forward.X * deltaTime, forward.Y * deltaTime);
                PointF newPosition = new PointF(-forwardDelta.X + player1Car.position.X, -forwardDelta.Y + player1Car.position.Y);
                OnUpdatePosition(player1Car, newPosition);
            }
            if (input.GetKey(Keys.Left))
            {
                OnUpdateRotation(player1Car, player1Car.angle - (player1Car.rotationSpeed * deltaTime));
            }
            if (input.GetKey(Keys.Right))
            {
                OnUpdateRotation(player1Car, player1Car.angle + (player1Car.rotationSpeed * deltaTime));
            }
        }
        private void PlayerTwoCarMovement()
        {

            if (input != null && player2Car != null)
            {
                PointF movement = player2Car.Move(input.GetKey(Keys.W), input.GetKey(Keys.S), false);
                PointF movementDelta = new PointF(movement.X * deltaTime,movement.Y * deltaTime);
                PointF newPosition = new PointF(player2Car.position.X + movementDelta.X, player2Car.position.Y + movementDelta.Y);
                OnUpdatePosition(player2Car, newPosition);
            }
            /*
            if (input.GetKey(Keys.W))
            {
                player1Car.isForward = true;
                PointF forward = player2Car.Move();//MoveForward();
                PointF forwardDelta = new PointF(forward.X * deltaTime,forward.Y * deltaTime);
                PointF newPosition = new PointF(player2Car.position.X + forwardDelta.X, player2Car.position.Y + forwardDelta.Y);
                OnUpdatePosition(player2Car, newPosition);
            }
            if (input.GetKey(Keys.S))
            {
                PointF forward = player2Car.Move();//MoveForward();
                player1Car.isForward = false;
                player1Car.isReverse = true;
                PointF forwardDelta = new PointF(forward.X * deltaTime, forward.Y * deltaTime);
                PointF newPosition = new PointF(-forwardDelta.X + player2Car.position.X, -forwardDelta.Y + player2Car.position.Y);
                OnUpdatePosition(player2Car, newPosition);
            }
            if (input.GetKey(Keys.A))
            {
                OnUpdateRotation(player2Car, player2Car.angle - (player2Car.rotationSpeed * deltaTime));
            }
            if (input.GetKey(Keys.D))
            {
                OnUpdateRotation(player2Car, player2Car.angle + (player2Car.rotationSpeed * deltaTime));
            }
             */ 
        }
        public void DrawObject(Objects.GameObject objectToDraw)
        {
            OnDrawObjectHandler handler = OnDrawGameObject;
            if (OnDrawGameObject != null)
                handler(this, objectToDraw);
        }
        private void gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            currentFrame = stopWatch.ElapsedMilliseconds;
            deltaTime = (currentFrame - prevFrame) / 1000;
            prevFrame = currentFrame;

            Update();

        }
        public delegate void OnDrawObjectHandler(object sender, Objects.GameObject arg);
        public event OnDrawObjectHandler OnDrawGameObject;

        public delegate void OnUpdateObjectPositionHandler(Objects.GameObject gameObject, PointF newPosition);
        public event OnUpdateObjectPositionHandler OnUpdatePosition;


        public delegate void OnUpdateObjectRotationHandler(Objects.GameObject gameObject, float angle);
        public event OnUpdateObjectRotationHandler OnUpdateRotation;
    }
}
