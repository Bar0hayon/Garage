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

        public void FillAirPressure(string i_LicensePlate)
        {
            Vehicle vehicleToMaxItTiersAirPressure = GetVehicleByLicensePlateNumber(i_LicensePlate);
            Wheel[] wheelsToFillItAirPressure = vehicleToMaxItTiersAirPressure.Wheels;
            try
            {
                fillAirPressureToMax(wheelsToFillItAirPressure);
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine("Current tiers's air preasure is out of range!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error occured while trying to fill air in tiers");
            }
        }

        public void ChargeBattery(string i_LicensePlate, float i_AmountOfChargeBatteryToDo)
        {
            if (m_VehicleTickets[i_LicensePlate].Vehicle.EnergySource is ElectricEnergy)
            {
                ElectricEnergy CarBattery = m_VehicleTickets[i_LicensePlate].Vehicle.EnergySource as ElectricEnergy;
                try
                {
                    CarBattery.AddEnergy(i_AmountOfChargeBatteryToDo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
            else
            {
                throw new FormatException("Vehicle is not using electricity!");
            }
        }

        private void fillAirPressureToMax(Wheel[] i_WheelsToFillItAirPreasure)
        {
            foreach (Wheel wheel in i_WheelsToFillItAirPreasure)
            {
                wheel.AddAirPressure(wheel.MaxAirPressure - wheel.getAirPressure);
            }
        }

        public void AddFuelToVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_AmountOfFuelToAdd)
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
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
            else
            {
                throw new FormatException("Vehicle is not using Fuel!");
            }
        }
    }
}
