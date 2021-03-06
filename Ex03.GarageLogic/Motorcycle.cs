﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            A2,
            B
        }
        
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public Motorcycle(
            string i_ModelName, 
            string i_LicensePlate, 
            float i_EnergyPercentage,
            Wheel[] i_Wheels, 
            EnergySource i_EnergySource, 
            eLicenseType i_LicenseType, 
            int i_EngineVolume) : 
            base(i_ModelName, i_LicensePlate, i_EnergyPercentage, i_Wheels, i_EnergySource)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        public override string ToString()
        {
            return string.Format(
                "License Type: {0}\nEngineVolume: {1}\n{2}",
                m_LicenseType.ToString(),
                m_EngineVolume.ToString(),
                base.ToString());
        }
    }
}
