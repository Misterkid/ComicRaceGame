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
        public static int F_motorCalculated(bool Reverse, int F_motor, int Fuel, bool Forward)
        {
            if (Reverse == true && Fuel > 0)
            {
                int F_MotorReturn = F_motor / 6;
                return F_MotorReturn;
            }
            if (Reverse == true && Fuel == 0 || Fuel < 0)
            {
                int F_MotorReturn = F_motor / (6 * 20);
                return F_MotorReturn;
            }
            if (Fuel == 0 || Fuel < 0 && Forward)
            {
                int F_MotorReturn = Convert.ToInt16(F_motor * 0.05f);
                return F_MotorReturn;
            }
            if (Forward)
            {

                return F_motor;
            }
            else
            {
                F_motor = 0;
                return F_motor;
            }
        }

        public static int FuelCalculated(bool Pitsstop, int F_motorCalculated, int Fuel, int MaxFuel)
        {
            if (Fuel <= 0)
            {
                int ReturnFuel = 0;
                return ReturnFuel;

            }
            else if (Pitsstop == true && Fuel < MaxFuel)
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
                float F_rolCalculated = RolResistance * MassaAutoCalculated * Gravity * 0.001f;
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
                
                float AccelerateCalc = ((F_motorCalculated - F_rol - F_air) / MassaAutoCalculated);
                Speed -= AccelerateCalc;
                return Speed;
                 
            }
            if (Reverse && Speed >= 0 && !Forward)
            {
                float AccelerateCalc = ((-F_motorCalculated - F_rol - F_air) / MassaAutoCalculated);
                Speed += AccelerateCalc;
                return Speed;
            }
            
            if (Speed <= 0 && !Reverse && !Forward)
            {
                float AccelerateCalc = (F_motorCalculated - F_rol - F_air) / MassaAutoCalculated;
                Speed -= AccelerateCalc;
                return Speed;
            }
                
            if (Speed > 0 && !Break && !Reverse && Forward)
            {
                float AccelerateCalc = (F_motorCalculated - F_rol - F_air) / MassaAutoCalculated;
                Speed += AccelerateCalc;
                return Speed;
            }
            else
            {
                float AccelerateCalc = (F_motorCalculated - F_rol - F_air) / MassaAutoCalculated;
                Speed += AccelerateCalc;
                return Speed;
            }

        }

        public static float Rotation(float Velocity, int Massa)
        {
            if (Velocity == 0)
            {
                float RotationCalculated = 0;
                return RotationCalculated;
            }
            /* if (Velocity > 0 && Velocity < 45)
             {
                 float RotationCalculated = 60;
                 return RotationCalculated;
             }*/
            else
            {
                int Steering = 100000;
                float RotationCalculated = Steering / (Massa + (Convert.ToSingle(Math.Pow(Math.Abs(Velocity), 1.5)))); //0.85
                return RotationCalculated;
            }
        }
    }
}
