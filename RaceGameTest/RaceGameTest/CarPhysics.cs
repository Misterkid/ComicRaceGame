using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaceGameTest.Q_Engine;
namespace RaceGameTest
{
    class CarPhysics
    {
        public CarPhysics()
        {

        }
        /// <summary>
        /// Calculates and return MotorForce 
        /// </summary>
        /// <param name="Reverse"></param>
        /// <param name="F_motor"></param>
        /// <param name="MaxF_motor"></param>
        /// <param name="Fuel"></param>
        /// <param name="Forward"></param>
        /// <returns>F_motor</returns>
        public static int F_motorCalculated(bool Reverse, int F_motor, int MaxF_motor, float Fuel, bool Forward)
        {
            if (Reverse && Fuel > 0 && MaxF_motor / 6 > F_motor) // Reverse with Fuel
            {
                F_motor += 50;
                return F_motor;
            }
            if (Reverse && Fuel <= 0 && MaxF_motor / 24 > F_motor) // Reverse without Fuel
            {
                F_motor += 25;
                return F_motor;
            }
            //else if (Reverse && Fuel <= 0) 
            //{
            //    F_motor -= 25;
            //}
            if (Fuel <= 0 && Forward && MaxF_motor / 12 > F_motor) // Forward without Fuel
            {     
                F_motor += 100;
                return F_motor;
            }
            /*else if (Forward && Fuel <= 0)
            {
                F_motor -= 100;
            }*/

            if (Forward && MaxF_motor > F_motor && Fuel != 0) //Forward
            {
                F_motor += 125;
                if (F_motor > MaxF_motor)
                    F_motor = MaxF_motor;

                return F_motor;
            }
            if (!Forward && !Reverse && F_motor >= 0) //No MotorForce
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
        /// <summary>
        /// Calculates Current Fuel and Adds Fuel @ Pitstop
        /// </summary>
        /// <param name="Pitsstop"></param>
        /// <param name="F_motorCalculated"></param>
        /// <param name="Fuel"></param>
        /// <param name="MaxFuel"></param>
        /// <returns>FuelCalculated</returns>
        public static float FuelCalculated(bool Pitsstop, int F_motorCalculated, float Fuel, int MaxFuel)
        {
            if (Fuel <= 0 && !Pitsstop) // Keep fuel @ 0 zero when there is no fuel and no Pitsstop
            {
                float ReturnFuel = 0;
                return ReturnFuel;

            }
            else if (Pitsstop && Math.Abs(Fuel) < MaxFuel) // Refill Fuel @ Pitstop
            {

                float FuelUsage = 1;
                float ReturnFuel = Fuel + ((FuelUsage * 10) * QTime.DeltaTime);
                return ReturnFuel;
            }
            else // Uses Fuel Based on MotorForce
            {
                float FuelUsage = Convert.ToInt32(Math.Abs(F_motorCalculated * 0.00013));
                float ReturnFuel = Fuel - (FuelUsage * QTime.DeltaTime);
                return ReturnFuel;
            }

        }
        /// <summary>
        /// Calculates Massa Based on Vehicle + CurrentFuel
        /// </summary>
        /// <param name="FuelCalculated"></param>
        /// <param name="Massa"></param>
        /// <returns>MassaAutoCalculated</returns>
        public static int MassaAutoCalculated(float FuelCalculated, int Massa)
        {
            int ReturnMassa = Convert.ToInt32(Massa + FuelCalculated);
            return ReturnMassa;
        }
        /// <summary>
        /// Calculates Frol
        /// Disabled for now
        /// </summary>
        /// <param name="RolResistance"></param>
        /// <param name="MassaAutoCalculated"></param>
        /// <param name="Gravity"></param>
        /// <param name="Break"></param>
        /// <param name="Velocity"></param>
        /// <returns>Frol</returns>
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

        /// <summary>
        /// Calculateds Air Resistance
        /// </summary>
        /// <param name="CoEfficient"></param>
        /// <param name="Density"></param>
        /// <param name="Accelerate"></param>
        /// <param name="Surface"></param>
        /// <returns>F_Air</returns>
        public static float F_Air(float CoEfficient, float Density, float Accelerate, float Surface)
        {
            float F_AirCalculated = CoEfficient * 0.5f * Density * Convert.ToSingle(Math.Pow(Math.Abs(Accelerate), 2)) * Surface;
            return F_AirCalculated;
        }


        /// <summary>
        /// Calculateds Velocity Based on Physics
        /// </summary>
        /// <param name="Speed"></param>
        /// <param name="MassaAutoCalculated"></param>
        /// <param name="F_motorCalculated"></param>
        /// <param name="F_rol"></param>
        /// <param name="F_air"></param>
        /// <param name="Reverse"></param>
        /// <param name="Break"></param>
        /// <param name="Forward"></param>
        /// <returns>Velocity</returns>
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
                Speed += (AccelerateCalc * 0.35f);
                return Speed;

            }
            if (!Reverse && Forward && Speed <= 0)
            {

                float AccelerateCalc = ((F_motorCalculated + F_rol + F_air) / MassaAutoCalculated);
                Speed += (AccelerateCalc * 0.35f);
                return Speed;

            }
            if (Reverse && !Forward && Speed >= 0)
            {
                float AccelerateCalc = ((-F_motorCalculated - F_rol - F_air) / MassaAutoCalculated);
                Speed += (AccelerateCalc * 0.35f);
                return Speed;
            }

            if (!Reverse && !Forward && Speed <= 0)
            {
                float AccelerateCalc = (F_motorCalculated + F_rol + F_air) / MassaAutoCalculated;
                Speed += (AccelerateCalc * 0.6f);
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
                Speed += (AccelerateCalc * 0.35f);
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
