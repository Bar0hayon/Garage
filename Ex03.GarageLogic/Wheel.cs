using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_AirPressure;
        private float m_MaxAirPressure;
        private float m_MinAirPressure;
        public const float k_MaxAirPreasureMotorcycle = 33;
        public const float k_MaxAirPreasureCar = 33;
        public const float k_MaxAirPreasureTruck = 26;

        public Wheel(string i_Manufacturer, float i_MaxAirPressure, float i_MinAirPressure = 0,
            float i_CurrentAirPressure = 0)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPressure;
            m_MinAirPressure = i_MinAirPressure;
            m_AirPressure = i_CurrentAirPressure;
        }

        public void AddAirPressure(float i_AirPressureToAdd)
        {
            if((i_AirPressureToAdd + m_AirPressure) > m_MaxAirPressure || 
                i_AirPressureToAdd < m_MinAirPressure)
            {
                throw new ValueOutOfRangeException(m_MaxAirPressure, m_MinAirPressure);
            }

            m_AirPressure += i_AirPressureToAdd;
        }

        public override string ToString()
        {
            return string.Format("Wheels Manufacturer: {0}\nWheels Air Pressure: {1}",
                m_Manufacturer, m_AirPressure);
        }

        public void FillAirPressure(Vehicle i_VehicleToMaxItTiersAirPreasure, Wheel[] i_WheelsToFillItAirPreasure)
        {
            try
            {
                if (i_VehicleToMaxItTiersAirPreasure is Car)
                {
                    fillAirPressureToMaxCar(i_VehicleToMaxItTiersAirPreasure, i_WheelsToFillItAirPreasure);
                }
                else if (i_VehicleToMaxItTiersAirPreasure is Motorcycle)
                {
                    fillAirPressureToMaxMotorcycle(i_VehicleToMaxItTiersAirPreasure, i_WheelsToFillItAirPreasure);
                }
                else if (i_VehicleToMaxItTiersAirPreasure is Truck)
                {
                    fillAirPressureToMaxTruck(i_VehicleToMaxItTiersAirPreasure, i_WheelsToFillItAirPreasure);
                }
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine("Current tiers's air preasure is out of range!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error occured while trying to fill air in tiers");
            }
        }

        private void fillAirPressureToMaxCar(Vehicle i_VehicleType, Wheel[] i_WheelsToFillItAirPreasure)
        {
            foreach (Wheel wheel in i_WheelsToFillItAirPreasure)
            {
                wheel.AddAirPressure(k_MaxAirPreasureCar - wheel.m_AirPressure);
            }
        }

        private void fillAirPressureToMaxMotorcycle(Vehicle i_VehicleType, Wheel[] i_WheelsToFillItAirPreasure)
        {
            foreach (Wheel wheel in i_WheelsToFillItAirPreasure)
            {
                wheel.AddAirPressure(k_MaxAirPreasureMotorcycle - wheel.m_AirPressure);
            }
        }

        private void fillAirPressureToMaxTruck(Vehicle i_VehicleType, Wheel[] i_WheelsToFillItAirPreasure)
        {
            foreach (Wheel wheel in i_WheelsToFillItAirPreasure)
            {
                wheel.AddAirPressure(k_MaxAirPreasureTruck - wheel.m_AirPressure);
            }
        }
    }
}
