using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGameTest
{
    class CarPhysics
    {
        public CarPhysics()
        {

        }
        public static int F_motorCalculated(bool Reverse, int F_motor, int MaxF_motor, int Fuel, bool Forward)
        {
            if (Reverse && Fuel > 0 && MaxF_motor / 6 > F_motor)
            {
                //int F_MotorReturn = F_motor / 6;
                F_motor += 50;
                return F_motor;
            }
            if (Reverse && Fuel <= 0 && MaxF_motor / 18 > F_motor)
            {
                //int F_MotorReturn = F_motor / (6 * 20);
                //return F_MotorReturn;
                F_motor += 25;
                return F_motor;
            }
            if (Fuel <= 0 && Forward && MaxF_motor / 3 > F_motor)
            {
                //int F_MotorReturn = Convert.ToInt32(F_motor * 0.05f);
                //return F_MotorReturn;
           
                F_motor += 100;
                return F_motor;
            }
            
            if (Forward && MaxF_motor > F_motor)
            {
                F_motor += 125;
                if (F_motor > MaxF_motor)
                    F_motor = MaxF_motor;

                return F_motor;
            }
            if (!Forward && !Reverse && F_motor >= 0)
            {
                F_motor = 0;
                return F_motor;
            }
            /*
            else
            {
               // F_motor = 0;
                return F_motor;
            }*/
            return F_motor;
        }

        public static int FuelCalculated(bool Pitsstop, int F_motorCalculated, int Fuel, int MaxFuel)
        {
            if (Fuel <= 0)
            {
                int ReturnFuel = 0;
                return ReturnFuel;

            }
            else if (Pitsstop && Fuel < MaxFuel)
            {

                int FuelUsage = 10;
                int ReturnFuel = Fuel + FuelUsage;
                return ReturnFuel;
            }
            else
            {
                int FuelUsage = Convert.ToInt32(F_motorCalculated * 0.005);
                int ReturnFuel = Fuel - FuelUsage;
                return ReturnFuel;
            }

        }

        public static int MassaAutoCalculated(int FuelCalculated, int Massa)
        {
            int ReturnMassa = Convert.ToInt32(Massa + (FuelCalculated * 0.01));
            return ReturnMassa;
        }

        public static float Frol(int RolResistance, int MassaAutoCalculated, int Gravity, bool Break, float Velocity)
        {

            if (Break == true && Math.Abs(Velocity) > 25)
            {
                float F_rolCalculated = (RolResistance * MassaAutoCalculated * Gravity) / (Math.Abs(Velocity * 1f));/// Math.Pow(Velocity, 1.1)/*0.001f*/);
                return F_rolCalculated;
            }
            if (Math.Abs(Velocity) <= 1)
            {
                float F_rolCalculated = 0;
                return F_rolCalculated;
            }
            else
            {
                float F_rolCalculated = RolResistance * MassaAutoCalculated * Gravity * 0.0001f;
                return F_rolCalculated;
            }


        }

        public static float F_Air(float CoEfficient, float Density, float Accelerate, float Surface)
        {
            float F_AirCalculated = CoEfficient * 0.5f * Density * Convert.ToSingle(Math.Pow(Math.Abs(Accelerate), 2)) * Surface;
            return F_AirCalculated;
        }



        public static float Velocity(float Speed, int MassaAutoCalculated, int F_motorCalculated, float F_rol, float F_air, bool Reverse, bool Break, bool Forward/*, bool AutoReverse*/)
        {
            if ((Break && Math.Abs(Speed) < 10) || (F_motorCalculated == 0 && Math.Abs(Speed) < 3))
            {
                Speed = 0;
                return Speed;
            }
            if (Reverse && !Forward && Speed <= 0)
            {
                
                float AccelerateCalc = ((-F_motorCalculated + F_rol + F_air) / MassaAutoCalculated);
                Speed += AccelerateCalc;
                return Speed;
                 
            }
            if (!Reverse && Forward && Speed <= 0)
            {

                float AccelerateCalc = ((F_motorCalculated + F_rol + F_air) / MassaAutoCalculated);
                Speed += AccelerateCalc;
                return Speed;

            }
            if (Reverse && !Forward && Speed >= 0)
            {
                float AccelerateCalc = ((-F_motorCalculated - F_rol - F_air) / MassaAutoCalculated);
                Speed += AccelerateCalc;
                return Speed;
            }
            
            if (!Reverse && !Forward && Speed <= 0)
            {
                float AccelerateCalc = (F_motorCalculated + F_rol + F_air) / MassaAutoCalculated;
                Speed += AccelerateCalc;
                return Speed;
            }
                
          /*  if (Speed >= 0 && !Break && !Reverse && Forward)
            {
                float AccelerateCalc = (F_motorCalculated - F_rol - F_air) / MassaAutoCalculated;
                Speed += AccelerateCalc;
                return Speed;
            } */
            else
            {
                float AccelerateCalc = (F_motorCalculated - F_rol - F_air) / MassaAutoCalculated;
                Speed += AccelerateCalc;
                return Speed;
            }

        }

        public static float Rotation(float Velocity, int Massa)
        {
            if (Math.Abs(Velocity) <= 10)
            {
                float RotationCalculated = 0;
                return RotationCalculated;
            }
            /*if (Velocity > 0 && Velocity < 45)
             {
                 float RotationCalculated = 60;
                 return RotationCalculated;
             } */
            else
            {
                int Steering = 200000;
                float RotationCalculated = Steering / (Massa + (Convert.ToSingle(Math.Pow(Math.Abs(Velocity), 0.85)))); //0.85
                return RotationCalculated;
            }
        }
    }
}
