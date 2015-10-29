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

namespace RaceGameTest
{

    class Game:BaseGame
    {
        public Input input;

        public Car player1Car;
        public Car player2Car;

        private Map map;
        public Gauge speed1Car;// = new Gauge();
        public Gauge speed2Car;// = new Gauge();
        public Gauge fuel1Car;// = new Gauge();
        public Gauge fuel2Car;// = new Gauge();
#if !__NO_OBJ_COL 
        public ObjectCollisionMap objectCollisionMap;
#endif
        public List<GameObject> gameObjects; //= new List<GameObject>();//Game Object to draw.
        private bool gameEnd;// = false;
        public bool canPlay;// = false;
        public Game()
        {
#if !__NO_OBJ_COL 
            objectCollisionMap = new ObjectCollisionMap();
#endif
            
        }
        public void InitializeGame()
        {
            gameObjects = new List<GameObject>();
            /*
            speed1Car = new Gauge();
            
            speed2Car = new Gauge();
            fuel1Car = new Gauge();
            fuel2Car = new Gauge();*/
            gameEnd = false;
            canPlay = false;
            input = new Input();
        }
        //Prepare objects to draw
        public void DrawObjects()
        {

            /*The Map */
            map = new Map("_Images\\Circuit4.bmp", "_Images\\Circuit4Col.bmp");//Map
            DrawObject(map);//Send Event to draw!
            gameObjects.Add(map);
            /* Player one Car */
            player1Car = new Car("_Images\\rrcargame.png");
            player1Car.position = new PointF(500, 700);
            player1Car.angle = 270;
            player1Car.DrawCollisionImage("_Images\\JeffersonGTA2Col.bmp");
            player1Car.initSoundNames("break1", "engine1", "bump1");
            DrawObject(player1Car);
            gameObjects.Add(player1Car);

            /* Player Two Car */
            player2Car = new Car("_Images\\JeffersonGTA2.png");
            player2Car.position = new PointF(500, 675);
            player2Car.angle = 270;
            player2Car.DrawCollisionImage("_Images\\JeffersonGTA2Col.bmp");
            player2Car.initSoundNames("break2", "engine2", "bump2");
            DrawObject(player2Car);
            gameObjects.Add(player2Car);

            InitSounds();
            //player1Car.SetCollision();
            //player2Car.SetCollision();

        }
        private void InitSounds()
        {
            jSound.AddSound(player1Car.breakSoundName, "_Sounds\\rem.wav", 1);
            jSound.AddSound(player1Car.engineSoundName, "_Sounds\\vroem.wav", 0.1f);
            jSound.AddSound(player1Car.bumpSoundName, "_Sounds\\bots.wav", 1);

            jSound.AddSound(player2Car.breakSoundName, "_Sounds\\rem.wav", 1);
            jSound.AddSound(player2Car.engineSoundName, "_Sounds\\vroem.wav", 0.1f);
            jSound.AddSound(player2Car.bumpSoundName, "_Sounds\\bots.wav", 1);

            jSound.AddSound("finish", "_Sounds\\finished.wav", 1);
        }
        //Update on each frame! :D
        protected override void UpdateFrame()
        {
            if (!gameEnd && canPlay)
            {
                PlayerCarMovement(player1Car,Keys.W,Keys.S,Keys.A,Keys.D);
                PlayerCarMovement(player2Car, Keys.Up, Keys.Down, Keys.Left, Keys.Right);
#if !__NO_OBJ_COL
            objectCollisionMap.UpdateObjects(this);
#endif          
                MapColCheck(player1Car);
                MapColCheck(player2Car);
                //speed1Car.updateSpeedGauge(player1Car.velocity);
                CarSound();
                //Send out ui update event!
            }
            /*
            OnDrawObjectHandler handler = OnDrawGameObject;
            if (OnDrawGameObject != null)
                */
            if (OnUpdateUI != null)
                OnUpdateUI(player1Car, player2Car);

            base.UpdateFrame();
        }
        private void CarSound()
        {
            //sound
            if (player1Car.isGoingForward || player1Car.isGoingBackwards)
            {
                jSound.PlaySoundLooping(player1Car.engineSoundName);
            }
            else
            {
                jSound.StopSound(player1Car.engineSoundName);
            }
            if (player2Car.isGoingForward || player2Car.isGoingBackwards)
            {
                jSound.PlaySoundLooping(player2Car.engineSoundName);
            }
            else
            {
                jSound.StopSound(player2Car.engineSoundName);
            }
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
                //"collision" with road.
                if (topLeftColor == ColorCol.road || topRightColor == ColorCol.road || botLeftColor == ColorCol.road || botRightColor == ColorCol.road)
                {

                }
                // "Collision" with "sand"
                if (topLeftColor == ColorCol.slow || topRightColor == ColorCol.slow || botLeftColor == ColorCol.slow || botRightColor == ColorCol.slow)
                {
                    if (car.isGoingForward)
                        car.CoEfficient = 10f;

                    if (car.isGoingBackwards)
                        car.CoEfficient = 10f;
                }
                if (topLeftColor != ColorCol.slow || topRightColor != ColorCol.slow || botLeftColor != ColorCol.slow || botRightColor != ColorCol.slow)
                {
                    if (car.isGoingForward)
                        car.CoEfficient = 0.42f;

                    if (car.isGoingBackwards)
                        car.CoEfficient = 0.42f;
                }
                //"Collision" against a object
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
                    jSound.PlaySound(car.bumpSoundName);

                    OnUpdatePosition(car, car.lastPos);
                    OnUpdateRotation(car, car.lastAngle);
                }
                //else if (color == ColorCol.pitstop)
                // if ((topLeftColor == ColorCol.pitstop || topRightColor == ColorCol.pitstop || botLeftColor == ColorCol.pitstop || botRightColor == ColorCol.pitstop) && car.velocity == 0)

                //"collision" with pitstop.
                // if (topLeftColor == ColorCol.pitstop || topRightColor == ColorCol.pitstop || botLeftColor == ColorCol.pitstop || botRightColor == ColorCol.pitstop)
                if ((topLeftColor == ColorCol.pitstop || topRightColor == ColorCol.pitstop || botLeftColor == ColorCol.pitstop || botRightColor == ColorCol.pitstop) && car.velocity == 0)
                {
                    //Do things
                    car.pitchStop = true;
                }
                if (topLeftColor != ColorCol.pitstop || topRightColor != ColorCol.pitstop || botLeftColor != ColorCol.pitstop || botRightColor != ColorCol.pitstop || car.velocity != 0)
                {
                    if (car.pitchStop)
                    {
                        car.pitchStop = false;
                    }
                }

                if (topLeftColor == ColorCol.start || topRightColor == ColorCol.start || botLeftColor == ColorCol.start || botRightColor == ColorCol.start)
                {
                    //Do things
                }
                if (topLeftColor == ColorCol.finnish || topRightColor == ColorCol.finnish || botLeftColor == ColorCol.finnish || botRightColor == ColorCol.finnish)
                {
                    if (car.checkPoints == 4 && car.laps < 3)
                    {
                        car.checkPoints = 0;
                        car.laps++;
                    }
                    if(car.laps == 3)
                    {
                        //car.checkPoints = 0;
                        //car.velocity = 0;
                        if (car.playerName == "Aladeen" || car.playerName == "علاء الدين")
                        {
                            gameEnd = true;
                            jSound.PlaySound("finish");
                            Console.WriteLine("finished");
                        }
                        else
                        {

                            Console.WriteLine("You are not Aladeen!");
                        }

                        //car = null;
                    }
                }
                //"Collision" with checkpoints
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
        private void PlayerCarMovement(Car car,Keys forward,Keys backwards, Keys left,Keys right)
        {
            if (input != null && car != null)
            {
                car.isGoingForward = input.GetKey(forward);
                car.isGoingBackwards = input.GetKey(backwards);
                //PointF movement = car.Move(input.GetKey(Keys.NumPad0));
                PointF movement = car.Move(false);//Break isn't working :P
                PointF movementDelta = new PointF(movement.X * QTime.DeltaTime, movement.Y * QTime.DeltaTime);
                PointF newPosition = new PointF(car.position.X + movementDelta.X, car.position.Y + movementDelta.Y);
                OnUpdatePosition(car, newPosition);

                float rotation = car.Rotate(input.GetKey(left), input.GetKey(right));
                OnUpdateRotation(car, car.angle - (rotation * QTime.DeltaTime));
            }
        }
        public void DrawObject(GameObject objectToDraw)
        {
            OnDrawObjectHandler handler = OnDrawGameObject;
            if (OnDrawGameObject != null)
                handler(this, objectToDraw);
        }
        public override void Reset()
        {
            canPlay = false;
            base.Reset();
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public delegate void OnDrawObjectHandler(object sender,GameObject arg);
        public event OnDrawObjectHandler OnDrawGameObject;

        public delegate void OnUpdateObjectPositionHandler(GameObject gameObject, PointF newPosition);
        public event OnUpdateObjectPositionHandler OnUpdatePosition;

        public delegate void OnUpdateObjectRotationHandler(GameObject gameObject, float angle);
        public event OnUpdateObjectRotationHandler OnUpdateRotation;

        public delegate void OnUpdateUIHandler(Car carPlayer1, Car carPlayer2);
        public event OnUpdateUIHandler OnUpdateUI;
    }
}
