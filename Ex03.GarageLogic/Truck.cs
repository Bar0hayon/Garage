using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool v_IsCarriageDangerous;
        private float m_CarriageCapacity;

        public Truck(
                    string i_ModelName, 
                    string i_LicensePlate, 
                    float i_EnergyPercentage,
                    Wheel[] i_Wheels, 
                    EnergySource i_EnergySource, 
                    bool i_IsCarrageDangerous,
                    float i_CarriageCapacity) :
                    base(i_ModelName, i_LicensePlate, i_EnergyPercentage, i_Wheels, i_EnergySource)
        {
            v_IsCarriageDangerous = i_IsCarrageDangerous;
            m_CarriageCapacity = i_CarriageCapacity;
        }

        public override string ToString()
        {
            return string.Format(
                                "Dangerous Carriage: {0}\nCarriage capacity: {1}\n{2}",
                                v_IsCarriageDangerous.ToString(), 
                                m_CarriageCapacity.ToString(), 
                                base.ToString());
        }
    }
}
