using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleTicket> m_VehicleTickets = new Dictionary<string, VehicleTicket>();

        public bool IsVehicleExists(string i_LicensePlate)
        { 
            return m_VehicleTickets.ContainsKey(i_LicensePlate);
        }

        public void ChangeStatus(string i_LicensePlate, VehicleTicket.eVehicleStatus i_NewStatus)
        {
            m_VehicleTickets[i_LicensePlate].ChangeStatus(i_NewStatus);
        }

        public void AddVehicleTicket(string i_OwnerName, string i_OwnerPhone, Vehicle i_NewVehicle)
        {
            m_VehicleTickets.Add(i_NewVehicle.LicensePlate, new VehicleTicket(i_OwnerName, i_OwnerPhone, i_NewVehicle));
        }

        public string GetVehicleDetailsAsString(string i_LicensePlate)
        {
            return m_VehicleTickets[i_LicensePlate].ToString();
        }

        public Vehicle GetVehicleByLicensePlateNumber(string i_LicensePlate)
        {
            return m_VehicleTickets[i_LicensePlate].Vehicle;
        }

        public List<string> GetLicesnePlates(VehicleTicket.eVehicleStatus i_Filter)
        {
            List<string> LicensePlates = new List<string>();
            foreach(KeyValuePair<string, VehicleTicket> vehicleTicketPair in m_VehicleTickets)
            {
                if(vehicleTicketPair.Value.Status == i_Filter)
                {
                    LicensePlates.Add(vehicleTicketPair.Key);
                }
            }

            return LicensePlates;
        }

        public List<string> GetAllLicensePlates()
        {
            List<string> LicensePlates = new List<string>();
            foreach(KeyValuePair<string, VehicleTicket> vehicleTicketPair in m_VehicleTickets)
            {
                LicensePlates.Add(vehicleTicketPair.Key);
            }

            return LicensePlates;
        }

        public void AddFuelToVehicle(string i_LicensePlate, eFuelType i_FuelType, 
            float i_AmountOfFuelToAdd)
        {
            if(m_VehicleTickets[i_LicensePlate].Vehicle.EnergySource is FuelEnergy)
            {
                FuelEnergy CarFuelTank = m_VehicleTickets[i_LicensePlate].Vehicle.EnergySource as FuelEnergy;
                try
                {
                    CarFuelTank.AddFuel(i_AmountOfFuelToAdd, i_FuelType);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new FormatException("this vehicle is not using Fuel!");
            }
        }
    }
}
