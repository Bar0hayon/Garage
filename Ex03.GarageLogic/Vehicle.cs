using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicensePlate;
        protected float m_EnergyPercentage;
        protected Wheel[] m_Wheels;
        protected EnergySource m_EnergySource;

        public Vehicle(
                        string i_ModelName, 
                        string i_LicensePlate, 
                        float i_EnergyPercentage,
                        Wheel[] i_Wheels, 
                        EnergySource i_EnergySource)
        {
            m_EnergyPercentage = i_EnergyPercentage;
            m_EnergySource = i_EnergySource;
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
            m_Wheels = i_Wheels;
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }
        }

        public override bool Equals(object obj)
        {
            return m_LicensePlate == ((Vehicle)obj).LicensePlate;
        }

        public override int GetHashCode()
        {
            return m_LicensePlate.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
                                "Model Name: {0}\n" +
                                "License Plate: {1}\n" +
                                "Energy Percentage: {2}\n{3}\n{4}",
                                m_ModelName, 
                                m_LicensePlate, 
                                m_EnergyPercentage.ToString(),
                                m_EnergySource.ToString(), 
                                m_Wheels[0].ToString());
        }
    }
}
