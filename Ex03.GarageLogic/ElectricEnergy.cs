using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(float i_MaxEnergy, float i_EnergyLeft = 0) : base(i_MaxEnergy, i_EnergyLeft)
        {
        }

        public override void AddEnergy(float i_EnergyToAdd)
        {
            if(i_EnergyToAdd > m_MaxEnergy || i_EnergyToAdd < m_MinEnergy)
            {
                throw new ValueOutOfRangeException(m_MaxEnergy, m_MinEnergy);
            }
        }
    }
}
