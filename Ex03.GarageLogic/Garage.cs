using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private List<VehicleTicket> m_VehicleTickets;

        //TO DO:
        public void AddVehicle()
        {

        }

        public bool IsVehicleExists(string licensePlate)
        {
            return false;
        }

        public void ChangeStatus(string licensePlate, VehicleTicket.eVehicleStatus inRepair)
        {
            throw new NotImplementedException();
        }
    }
}
