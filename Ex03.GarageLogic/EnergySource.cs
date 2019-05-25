using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        protected float m_EnergyLeft;
        protected float m_MaxEnergy;
        protected float m_MinEnergy;

        protected EnergySource(float i_MaxEnergy, float i_EnergyLeft = 0, float i_MinEnergyToAdd = 0)
        {
            m_MaxEnergy = i_MaxEnergy;
            m_EnergyLeft = i_EnergyLeft;
            m_MinEnergy = i_MinEnergyToAdd;
        }

        public override string ToString()
        {
            return string.Format("Energy left: {0}", m_EnergyLeft);
        }
    }
}
