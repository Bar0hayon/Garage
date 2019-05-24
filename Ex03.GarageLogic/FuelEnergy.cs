using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : EnergySource
    {
        private eFuelType m_FuelType;

        public FuelEnergy(eFuelType i_FuelType, float i_MaxEnergy, 
            float i_EnergyLeft = 0, float i_MinEnergyToAdd = 0) : 
            base(i_MaxEnergy, i_EnergyLeft, i_MinEnergyToAdd)
        {
            m_FuelType = i_FuelType;
        }

        public void AddFuel(float i_EnergyToAdd, eFuelType i_FuelType)
        {
            if((i_EnergyToAdd + m_EnergyLeft) > m_MaxEnergy || i_EnergyToAdd < m_MinEnergyToAdd)
            {
                throw new ValueOutOfRangeException(m_MaxEnergy, m_MinEnergyToAdd);
            }
            if(i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Wrong fuel type!");
            }
            m_EnergyLeft += i_EnergyToAdd;
        }

        public override string ToString()
        {
            return string.Format("Fuel Type: {0} \n{1}",
                m_FuelType.ToString(), base.ToString());
        }
    }
}
