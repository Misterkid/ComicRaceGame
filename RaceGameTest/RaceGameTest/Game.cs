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
using RaceGameTest.Q_Engine;
using RaceGameTest.Objects;
namespace RaceGameTest
{

    class Game:BaseGame
    {
        public Input input = new Input();

        private Car player1Car;
        private Car player2Car;
        private Map map;
#if !__NO_OBJ_COL 
        public ObjectCollisionMap objectCollisionMap;
#endif
        public List<GameObject> gameObjects = new List<GameObject>();
        public Game()
        {
#if !__NO_OBJ_COL 
            objectCollisionMap = new ObjectCollisionMap();
#endif
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
            player1Car.position = new PointF(500, 700);
            player1Car.angle = 270;
            player1Car.DrawCollisionImage("_Images\\JeffersonGTA2Col.bmp");
            DrawObject(player1Car);
            gameObjects.Add(player1Car);

            /* Player Two Car */
            player2Car = new Car("_Images\\JeffersonGTA2.png");
            player2Car.position = new PointF(500, 675);
            player2Car.angle = 270;
            player2Car.DrawCollisionImage("_Images\\JeffersonGTA2Col.bmp");
            DrawObject(player2Car);
            gameObjects.Add(player2Car);
            //player1Car.SetCollision();
            //player2Car.SetCollision();

        }
        //Update on each frame! :D
        protected override void UpdateFrame()
        {
            PlayerOneCarMovement();
            PlayerTwoCarMovement();
#if !__NO_OBJ_COL
            objectCollisionMap.UpdateObjects(this);
#endif
            MapColCheck(player1Car);
            MapColCheck(player2Car);

            base.UpdateFrame();
        }

        protected override void DrawFrame()
        {

            base.DrawFrame();
        }
        //Only use this on MOVING objects
        private void MapColCheck(Car car)
        {
            if (car != null)
            {
                //car.SetCollision();
                //Color color = map.GetPixelAt((int)car.position.X + (int)car.center.X, (int)car.position.Y + (int)car.center.Y);
                float centerXWorld = (int)car.position.X + (int)car.center.X;
                float centerYWorld = (int)car.position.Y + (int)car.center.Y;

                Color topLeftColor = map.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.topLeft.X, (int)centerYWorld + (int)car.rotatedFourPoints.topLeft.Y);
                Color topRightColor = map.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.topRight.X, (int)centerYWorld + (int)car.rotatedFourPoints.topRight.Y);
                Color botLeftColor = map.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.botLeft.X, (int)centerYWorld + (int)car.rotatedFourPoints.botLeft.Y);
                Color botRightColor = map.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.botRight.X, (int)centerYWorld + (int)car.rotatedFourPoints.botRight.Y);

                //Object collision points.
#if !__NO_OBJ_COL 
                Color topLeftColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.topLeft.X, (int)centerYWorld + (int)car.rotatedFourPoints.topLeft.Y);
                Color topRightColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.topRight.X, (int)centerYWorld + (int)car.rotatedFourPoints.topRight.Y);
                Color botLeftColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.botLeft.X, (int)centerYWorld + (int)car.rotatedFourPoints.botLeft.Y);
                Color botRightColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.botRight.X, (int)centerYWorld + (int)car.rotatedFourPoints.botRight.Y);

                Color topCenterColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.topCenter.X, (int)centerYWorld + (int)car.rotatedFourPoints.topCenter.Y);
                Color botCenterColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.botCenter.X, (int)centerYWorld + (int)car.rotatedFourPoints.botCenter.Y);
                Color LeftCenterColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.leftCenter.X, (int)centerYWorld + (int)car.rotatedFourPoints.leftCenter.Y);
                Color RightCenterColor2 = objectCollisionMap.GetPixelAt((int)centerXWorld + (int)car.rotatedFourPoints.rightCenter.X, (int)centerYWorld + (int)car.rotatedFourPoints.rightCenter.Y);
#endif                
                // Can't put color in a switch for some reason!
#if __NO_OBJ_COL
                if (topLeftColor != ColorCol.collision && topRightColor != ColorCol.collision && botLeftColor != ColorCol.collision && botRightColor != ColorCol.collision)
#else
                if (topLeftColor != ColorCol.collision && topRightColor != ColorCol.collision && botLeftColor != ColorCol.collision && botRightColor != ColorCol.collision &&
                    topLeftColor2 != ColorCol.collision && topRightColor2 != ColorCol.collision && botLeftColor2 != ColorCol.collision && botRightColor2 != ColorCol.collision &&
                    topCenterColor2 != ColorCol.collision && botCenterColor2 != ColorCol.collision && LeftCenterColor2 != ColorCol.collision && RightCenterColor2 != ColorCol.collision)
#endif
                {
                   // Console.WriteLine("?");
                    car.lastPos = car.position;
                    car.lastAngle = car.angle;
                }

                if (topLeftColor == ColorCol.road || topRightColor == ColorCol.road || botLeftColor == ColorCol.road || botRightColor == ColorCol.road)
                {
                    //Do things
                    //car.speed = 200;
                    /*
                    if (topLeftColor == ColorCol.road && topRightColor == ColorCol.road && botLeftColor == ColorCol.road && botRightColor == ColorCol.road)
                        car.lastRoadPos = car.position;
                     */ 
                }
                //else if( color == ColorCol.collision)
                //else if (color == ColorCol.slow)
                if (topLeftColor == ColorCol.slow || topRightColor == ColorCol.slow || botLeftColor == ColorCol.slow || botRightColor == ColorCol.slow)
                {
                    if (car.isGoingForward)
                        car.velocity = 20;
                    
                    if (car.isGoingBackwards)
                        car.velocity = -10;
                }
#if __NO_OBJ_COL 

                if (topLeftColor == ColorCol.collision || topRightColor == ColorCol.collision || botLeftColor == ColorCol.collision || botRightColor == ColorCol.collision)
#else
                if (topLeftColor == ColorCol.collision || topRightColor == ColorCol.collision || botLeftColor == ColorCol.collision || botRightColor == ColorCol.collision ||                   
                    topLeftColor2 == ColorCol.collision || topRightColor2 == ColorCol.collision || botLeftColor2 == ColorCol.collision || botRightColor2 == ColorCol.collision ||
                    topCenterColor2 == ColorCol.collision || botCenterColor2 == ColorCol.collision || LeftCenterColor2 == ColorCol.collision || RightCenterColor2 == ColorCol.collision)
#endif
                {
                    //Do things
                   // Console.WriteLine("?");
                    OnUpdatePosition(car, car.lastPos);
                    OnUpdateRotation(car, car.lastAngle);
                }
                //else if (color == ColorCol.pitstop)
                if (topLeftColor == ColorCol.pitstop || topRightColor == ColorCol.pitstop || botLeftColor == ColorCol.pitstop || botRightColor == ColorCol.pitstop)
                {
                    //Do things
                }
               // else if (color == ColorCol.start)
                if (topLeftColor == ColorCol.start || topRightColor == ColorCol.start || botLeftColor == ColorCol.start || botRightColor == ColorCol.start)
                {
                    //Do things
                }
                //else if (color == ColorCol.finnish)
                if (topLeftColor == ColorCol.finnish || topRightColor == ColorCol.finnish || botLeftColor == ColorCol.finnish || botRightColor == ColorCol.finnish)
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
                //else if (color == ColorCol.checkp1)
                if (topLeftColor == ColorCol.checkp1 || topRightColor == ColorCol.checkp1 || botLeftColor == ColorCol.checkp1 || botRightColor == ColorCol.checkp1)
                {
                    //Do things
                    if(car.checkPoints == 0)
                        car.checkPoints = 1;
                }
                //else if (color == ColorCol.checkp2)
                if (topLeftColor == ColorCol.checkp2 || topRightColor == ColorCol.checkp2 || botLeftColor == ColorCol.checkp2 || botRightColor == ColorCol.checkp2)
                {
                    //Do things
                    if (car.checkPoints == 1)
                        car.checkPoints = 2;
                }
                //else if (color == ColorCol.checkp3)
                if (topLeftColor == ColorCol.checkp3 || topRightColor == ColorCol.checkp3 || botLeftColor == ColorCol.checkp3 || botRightColor == ColorCol.checkp3)
                {
                    //Do things
                    if (car.checkPoints == 2)
                        car.checkPoints = 3;
                }
               // else if (color == ColorCol.checkp4)
                if (topLeftColor == ColorCol.checkp4 || topRightColor == ColorCol.checkp4 || botLeftColor == ColorCol.checkp4 || botRightColor == ColorCol.checkp4)
                {
                    //Do things
                    if (car.checkPoints == 3)
                        car.checkPoints = 4;
                }
            }
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
                //Console.WriteLine(player1Car.position.X + player1Car.RotatePoint(player1Car.fourPoints.topLeft).X + ":"+  player1Car.position.Y + player1Car.RotatePoint(player1Car.fourPoints.topLeft).Y);

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
        public void DrawObject(GameObject objectToDraw)
        {
            OnDrawObjectHandler handler = OnDrawGameObject;
            if (OnDrawGameObject != null)
                handler(this, objectToDraw);
        }
        public delegate void OnDrawObjectHandler(object sender,GameObject arg);
        public event OnDrawObjectHandler OnDrawGameObject;

        public delegate void OnUpdateObjectPositionHandler(GameObject gameObject, PointF newPosition);
        public event OnUpdateObjectPositionHandler OnUpdatePosition;

        public delegate void OnUpdateObjectRotationHandler(GameObject gameObject, float angle);
        public event OnUpdateObjectRotationHandler OnUpdateRotation;
    }
}
