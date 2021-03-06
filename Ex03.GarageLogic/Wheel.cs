﻿using System;
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

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public Wheel(
                    string i_Manufacturer, 
                    float i_MaxAirPressure, 
                    float i_MinAirPressure = 0,
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
            return string.Format(
                                "Wheels Manufacturer: {0}\nWheels Air Pressure: {1}",
                                m_Manufacturer, 
                                m_AirPressure);
        }

        public float getAirPressure
        {
            get
            {
                return m_AirPressure;
            }

            set
            {
                m_AirPressure = value;
            }
        }
    }
}
