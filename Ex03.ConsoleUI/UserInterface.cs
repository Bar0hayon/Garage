using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        GarageLogic.Garage m_Garage = new GarageLogic.Garage();

        public void MainMenu()
        {
            string userSelection;
            do
            {
                printMainMenuOptions();
                userSelection = Console.ReadLine();
                switch (userSelection)
                {
                    case ("1"):
                        addVehicle();
                        break;
                    case ("2"):
                        showLicensePlates();
                        break;
                    case ("3"):
                        changeVehicleStatus();
                        break;
                    case ("4"):
                        fillAirPressureToMax();
                        break;
                    case ("5"):
                        addFuel();
                        break;
                    case ("6"):
                        chargeBattery();
                        break;
                    case ("7"):
                        showVehicleDetails();
                        break;
                    case ("0"):
                        Console.WriteLine("GoodBye!");
                        break;
                    default:
                        Console.WriteLine("Input is not valid!");
                        break;
                }
            } while (userSelection != "0");
        }

        private void showVehicleDetails()
        {
            throw new NotImplementedException();
        }

        private void chargeBattery()
        {
            throw new NotImplementedException();
        }

        private void addFuel()
        {
            throw new NotImplementedException();
        }

        private void fillAirPressureToMax()
        {
            throw new NotImplementedException();
        }

        private void changeVehicleStatus()
        {
            throw new NotImplementedException();
        }

        private void showLicensePlates()
        {
            throw new NotImplementedException();
        }

        private void addVehicle()
        {
            string licensePlate;
            string ownerName;
            string ownerPhone;
            Vehicle newVehicle;
            Console.WriteLine("License Plate:");
            licensePlate = Console.ReadLine();
            if(m_Garage.IsVehicleExists(licensePlate))
            {
                Console.WriteLine("Vehcile is allready listed in the garage!");
                Console.WriteLine("changing the vehicle status to 'In Repair'...");
                m_Garage.ChangeStatus(licensePlate, GarageLogic.VehicleTicket.eVehicleStatus.InRepair);
            }
            else
            {
                try
                {
                    Console.WriteLine("Owner name:");
                    ownerName = Console.ReadLine();
                    Console.WriteLine("Owner phone number:");
                    ownerPhone = Console.ReadLine();
                    newVehicle = getNewVehicle(licensePlate);
                    m_Garage.AddVehicleTicket(ownerName, ownerPhone, newVehicle);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Vehicle was not added!");
                }
            }
        }

        private Vehicle getNewVehicle(string i_LicensePlate)
        {
            Vehicle newVehicle;
            string vehicleType;
            string energySource;
            Dictionary<string, string> vehicleProperties;
            vehicleType = getUserSelection(SystemVehiclesSupport.SupportedVehicles);
            energySource = getUserSelection(SystemVehiclesSupport.GetEnergySources(vehicleType));
            vehicleProperties = getVehicleProperties(vehicleType, energySource);
            vehicleProperties.Add("License plate", i_LicensePlate);
            newVehicle = SystemVehiclesSupport.CreateVehicle(vehicleType, energySource, vehicleProperties);

            return newVehicle;
        }

        private Dictionary<string, string> getVehicleProperties(string i_VehicleType, string i_EnergySource)
        {
            Dictionary<string, string> VehicleProperties = new Dictionary<string, string>();
            foreach(string propertie in SystemVehiclesSupport.GetVehicleProperties(i_VehicleType, i_EnergySource))
            {
                Console.WriteLine(propertie + ":");
                VehicleProperties.Add(propertie, Console.ReadLine());
            }

            return VehicleProperties;
        }

        private string getUserSelection(string[] i_Options)
        {
            int UserSelection;
            if (i_Options.Length > 1)
            {
                do
                {
                    Console.WriteLine("Please choose one of the followings:");
                    for (int i = 0; i < i_Options.Length; i++)
                    {
                        Console.WriteLine(string.Format("\t{0}.{1}", (i + 1).ToString(), i_Options[i]));
                    }
                    UserSelection = (int.Parse(Console.ReadLine()) - 1);
                    if (UserSelection < 0 || UserSelection >= i_Options.Length)
                    {
                        Console.WriteLine("Input is not valid!");
                    }
                } while (UserSelection < 0 || UserSelection >= i_Options.Length);
            }
            else
            {
                UserSelection = 0;
            }
            return i_Options[UserSelection];
        }

        private void printMainMenuOptions()
        {
            Console.WriteLine("Please choose one of the followings:");
            Console.WriteLine("\t1.Add a vehicle to the garage.");
            Console.WriteLine("\t2.Show vehicles License-plates.");
            Console.WriteLine("\t3.Change vehicle status");
            Console.WriteLine("\t4.Fill the Wheels air-pressure of a vehicle to maximum.");
            Console.WriteLine("\t5.Add fuel.");
            Console.WriteLine("\t6.Charge battery.");
            Console.WriteLine("\t7.Show vehicle details.");
            Console.WriteLine("0.EXIT");
        }
    }
}
