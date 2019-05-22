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

        public Vehicle(string i_ModelName, string i_LicensePlate, float i_EnergyPercentage,
            Wheel[] i_Wheels, EnergySource i_EnergySource)
        {
            m_EnergyPercentage = i_EnergyPercentage;
            m_EnergySource = i_EnergySource;
            m_LicensePlate = i_LicensePlate;
            m_ModelName = i_ModelName;
            m_Wheels = i_Wheels;
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
    }
}
