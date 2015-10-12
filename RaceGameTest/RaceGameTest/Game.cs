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
            /* Player one Car */
            player1Car = new Car();
            player1Car.Draw("_Images\\car.jpg");
            DrawObject(player1Car);

            /* Player Two Car */
            player2Car = new Car();
            player2Car.Draw("_Images\\car.jpg");
            DrawObject(player2Car);


        }
        public void DrawObject(Objects.GameObject objectToDraw)
        {
            OnDrawObjectHandler handler = OnDrawGameObject;
            if (OnDrawGameObject != null)
                handler(this, objectToDraw);
        }
        private void Update()
        {
            PlayerOneCarMovement();
            PlayerTwoCarMovement();
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
            if (input.GetKey(Keys.W))
            {
                PointF forward = player2Car.MoveForward();
                PointF forwardDelta = new PointF(forward.X * deltaTime,forward.Y * deltaTime);
                PointF newPosition = new PointF(player2Car.position.X + forwardDelta.X, player2Car.position.Y + forwardDelta.Y);
                OnUpdatePosition(player2Car, newPosition);
            }
            if (input.GetKey(Keys.S))
            {
                PointF forward = player2Car.MoveForward();
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
