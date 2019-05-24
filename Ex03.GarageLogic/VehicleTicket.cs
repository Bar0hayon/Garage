using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleTicket
    {
        public enum eVehicleStatus { Paid, Repaired, InRepair}
        private string m_OwnerName;
        private string m_OwnerPhone;
        private Vehicle m_Vehicle;
        private eVehicleStatus m_Status;

        public VehicleTicket(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_Vehicle = i_Vehicle;
            m_Status = eVehicleStatus.InRepair;
        }

        public void ChangeStatus(eVehicleStatus i_NewStatus)
        {
            m_Status = i_NewStatus;
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public override string ToString()
        {
            return string.Format("Owner Name: {0}\nOwner phone: {1}\nStatus: {2}\n{3}",
                m_OwnerName, m_OwnerPhone, m_Status.ToString(), m_Vehicle.ToString());
        }
    }
}
