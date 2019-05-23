using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(float i_MaxEnergy, float i_EnergyLeft = 0, float i_MinEnergyToAdd = 0) : 
            base(i_MaxEnergy, i_EnergyLeft, i_MinEnergyToAdd)
        {
        }

        public void AddEnergy(float i_EnergyToAdd)
        {
            if((i_EnergyToAdd + m_EnergyLeft) > m_MaxEnergy || i_EnergyToAdd < m_MinEnergyToAdd)
            {
                throw new ValueOutOfRangeException(m_MaxEnergy, m_MinEnergyToAdd);
            }
            m_EnergyLeft += i_EnergyToAdd;
        }
    }
}
