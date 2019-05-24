using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eCarColor { Red, Blue, Black, Gray}

        private eCarColor m_Color;
        private int m_NumOfDoors;
        private readonly int r_MinNumOfDoors = 2;
        private readonly int r_MaxNumOfDoors = 5;

        public Car(string i_ModelName, string i_LicensePlate, float i_EnergyPercentage,
            Wheel[] i_Wheels, EnergySource i_EnergySource, eCarColor i_Color, int i_NumOfDoors)
            : base(i_ModelName, i_LicensePlate, i_EnergyPercentage, i_Wheels, i_EnergySource)
        {
            if(i_NumOfDoors > r_MaxNumOfDoors || i_NumOfDoors < r_MinNumOfDoors)
            {
                throw new ValueOutOfRangeException(r_MaxNumOfDoors, r_MinNumOfDoors);
            }
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }

        public override string ToString()
        {
            return string.Format("Color: {0}\nNumber of doors: {1}\n{2}",
                m_Color.ToString(), m_NumOfDoors.ToString(), base.ToString());
        }
    }
}
