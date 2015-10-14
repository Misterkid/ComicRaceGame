using System;
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
            input.RegisterKey(Keys.NumPad0);
            //Player Two
            input.RegisterKey(Keys.W);
            input.RegisterKey(Keys.S);
            input.RegisterKey(Keys.A);
            input.RegisterKey(Keys.D);
            input.RegisterKey(Keys.LShiftKey);
        }
        public void DrawObjects()
        {

            /*The Map */
            map = new Map("_Images\\Circuit3.bmp", "_Images\\Circuit3.bmp");
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
                    car.velocity = 0;
                }
                else if (color == ColorCol.slow)
                {
                    if (car.isGoingForward)
                        car.velocity = 20;
                    
                    if (car.isGoingBackwards)
                        car.velocity = -10;

                    //if (car.velocity > 10 && input.GetKey(Keys.W))
                    //{
                       // car.velocity -= (5000 * deltaTime);
                    //}
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
                    if (car.checkPoints == 4 && car.laps < 3)
                    {
                        car.checkPoints = 0;
                        car.laps++;
                    }
                    if(car.laps == 3)
                    {
                        car.checkPoints = 0;
                        car.velocity = 0;
                        Console.WriteLine("finished");
                        //car = null;
                    }
                }
                else if (color == ColorCol.checkp1)
                {
                    //Do things
                    if(car.checkPoints == 0)
                        car.checkPoints = 1;
                }
                else if (color == ColorCol.checkp2)
                {
                    //Do things
                    if (car.checkPoints == 1)
                        car.checkPoints = 2;
                }
                else if (color == ColorCol.checkp3)
                {
                    //Do things
                    if (car.checkPoints == 2)
                        car.checkPoints = 3;
                }
                else if (color == ColorCol.checkp4)
                {
                    //Do things
                    if (car.checkPoints == 3)
                        car.checkPoints = 4;
                }
            }
        }
        private void Update()
        {
            MapColCheck(player1Car);
            MapColCheck(player2Car);

            PlayerOneCarMovement();
            PlayerTwoCarMovement();
        }
        private void PlayerOneCarMovement()
        {

            if (input != null && player1Car != null)
            {
                player1Car.isGoingForward = input.GetKey(Keys.Up);
                player1Car.isGoingBackwards =  input.GetKey(Keys.Down);
                PointF movement = player1Car.Move(input.GetKey(Keys.NumPad0));
                PointF movementDelta = new PointF(movement.X * deltaTime, movement.Y * deltaTime);
                PointF newPosition = new PointF(player1Car.position.X + movementDelta.X, player1Car.position.Y + movementDelta.Y);
                OnUpdatePosition(player1Car, newPosition);

                ;

                Console.WriteLine(player1Car.position.X + player1Car.RotatePoint(player1Car.fourPoints.topLeft).X + ":"+  player1Car.position.Y + player1Car.RotatePoint(player1Car.fourPoints.topLeft).Y);


                float rotation = player1Car.Rotate(input.GetKey(Keys.Left), input.GetKey(Keys.Right));
                OnUpdateRotation(player1Car, player1Car.angle - (rotation * deltaTime));
            }
        }
        private void PlayerTwoCarMovement()
        {

            if (input != null && player2Car != null)
            {
                player2Car.isGoingForward = input.GetKey(Keys.W);
                player2Car.isGoingBackwards = input.GetKey(Keys.S);
                PointF movement = player2Car.Move(input.GetKey(Keys.LShiftKey));
                PointF movementDelta = new PointF(movement.X * deltaTime,movement.Y * deltaTime);
                PointF newPosition = new PointF(player2Car.position.X + movementDelta.X, player2Car.position.Y + movementDelta.Y);
                OnUpdatePosition(player2Car, newPosition);

                float rotation = player2Car.Rotate(input.GetKey(Keys.A), input.GetKey(Keys.D));
                OnUpdateRotation(player2Car, player2Car.angle - (rotation * deltaTime));
            }
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
