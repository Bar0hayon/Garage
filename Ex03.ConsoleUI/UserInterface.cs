﻿using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        Garage m_Garage = new Garage();

        private readonly string[] r_MainMenuOptions = { "Add a vehicle to the garage",
            "Show vehicles License-plates",
            "Change vehicle status",
            "Fill the Wheels air-pressure of a vehicle to maximum",
            "Add fuel",
            "Charge battery",
            "Show vehicle details",
            "EXIT"};
        private readonly string[] r_LicensePlatesFilterOptions = { "Show all License plates",
            "Show License plates of vehicles in status 'In Repair'",
            "Show License plates of vehicles in status 'Repaired'",
            "Show License plates of vehicles in status 'Paid'"};
        private readonly string[] r_VehicleStatusOptions = { "Change to 'In Repair'" ,
            "Change to 'Repaired'", "Change to 'Paid'"};
        private readonly string[] r_FuelTypes = { "Soler", "Octan98", "Octan96", "Octan95" };

        public void MainMenu()
        {
            string userSelection;
            do
            {
                userSelection = getUserSelection(r_MainMenuOptions);
                switch (userSelection)
                {
                    case ("Add a vehicle to the garage"):
                        Console.WriteLine("Please insert license plate number:");
                        string LicensePlate = Console.ReadLine();
                        addVehicleToGarage(LicensePlate);
                        break;
                    case ("Show vehicles License-plates"):
                        showLicensePlates();
                        break;
                    case ("Change vehicle status"):
                        changeVehicleStatus();
                        break;
                    case ("Fill the Wheels air-pressure of a vehicle to maximum"):
                        fillAirPressureToMax();
                        break;
                    case ("Add fuel"):
                        addFuel();
                        break;
                    case ("Charge battery"):
                        chargeBattery();
                        break;
                    case ("Show vehicle details"):
                        showVehicleDetails();
                        break;
                    case ("EXIT"):
                        Console.WriteLine("GoodBye!");
                        break;
                    default:
                        Console.WriteLine("Input is not valid!");
                        break;
                }
            } while (userSelection != "EXIT");
        }

        private void showVehicleDetails()
        {
            Console.WriteLine("License plate:");
            string LicensePlate = Console.ReadLine();
            if(m_Garage.IsVehicleExists(LicensePlate))
            {
                Console.WriteLine(m_Garage.GetVehicleDetailsAsString(LicensePlate));
            }
            else
            {
                Console.WriteLine("Vehicle is not listed in the garage!");
            }
        }

        private void chargeBattery()
        {
            throw new NotImplementedException();
        }

        private void addFuel()
        {
            string LicensePlate = getLicensePlate();
            eFuelType FuelType = getFuelType();
            Console.WriteLine("Amount of fuel to add:");
            float AmountOfFuelToAdd = float.Parse(Console.ReadLine());
            try
            {
                m_Garage.AddFuelToVehicle(LicensePlate, FuelType, AmountOfFuelToAdd);
                Console.WriteLine("Fuel was added successfully!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Fuel was not added!");
            }
        }
        
        private eFuelType getFuelType()
        {
            eFuelType FuelType;
            string UserSelection = getUserSelection(r_FuelTypes);
            switch (UserSelection)
            {
                case ("Soler"):
                    FuelType = eFuelType.Soler;
                    break;
                case ("Octan98"):
                    FuelType = eFuelType.Octan98;
                    break;
                case ("Octan96"):
                    FuelType = eFuelType.Octan96;
                    break;
                case ("Octan95"):
                    FuelType = eFuelType.Octan95;
                    break;
                default:
                    throw new FormatException("Fuel type is not supported");
            }

            return FuelType;
        }

        private void fillAirPressureToMax()
        {
            Console.WriteLine("Please insert license plate number:");
            string licensePlate = Console.ReadLine();
            addVehicleToGarage(licensePlate);
            Vehicle newVehicle = m_Garage.GetVehicleByLicensePlateNumber(licensePlate);
            Wheel singleTier = new Wheel("null", 0); // DISCUSS WITH BAR - CHANGE/CRERATE NEW 'EMPTY CONSTRUCTOR'?
            Wheel[] tiersToFillItAirPreasure = newVehicle.m_Wheels;
            singleTier.FillAirPressure(newVehicle, tiersToFillItAirPreasure);
        }
        
        private void changeVehicleStatus()
        {
            string LicensePlate = getLicensePlate();
            string UserSelection = getUserSelection(r_VehicleStatusOptions);
            switch (UserSelection)
            {
                case ("Change to 'In Repair'"):
                    m_Garage.ChangeStatus(LicensePlate, VehicleTicket.eVehicleStatus.InRepair);
                    Console.WriteLine("Status changed to 'In Repair' successfully");
                    break;
                case ("Change to 'Repaired'"):
                    m_Garage.ChangeStatus(LicensePlate, VehicleTicket.eVehicleStatus.Repaired);
                    Console.WriteLine("Status changed to 'Repaired' successfully");
                    break;
                case ("Change to 'Paid'"):
                    m_Garage.ChangeStatus(LicensePlate, VehicleTicket.eVehicleStatus.Paid);
                    Console.WriteLine("Status changed to 'Paid' successfully");
                    break;
                default:
                    throw new Exception("user selection is not supported!");
            }
        }
        
        private void showLicensePlates()
        {
            string UserSelection = getUserSelection(r_LicensePlatesFilterOptions);
            switch (UserSelection)
            {
                case ("Show all License plates"):
                    printStringArray(m_Garage.GetAllLicensePlates());
                    break;
                case ("Show License plates of vehicles in status 'In Repair'"):
                    printStringArray(m_Garage.GetLicesnePlates(VehicleTicket.eVehicleStatus.InRepair));
                    break;
                case ("Show License plates of vehicles in status 'Repaired'"):
                    printStringArray(m_Garage.GetLicesnePlates(VehicleTicket.eVehicleStatus.Repaired));
                    break;
                case ("Show License plates of vehicles in status 'Paid'"):
                    printStringArray(m_Garage.GetLicesnePlates(VehicleTicket.eVehicleStatus.Paid));
                    break;
                default:
                    break;
            }
        }

        private void printStringArray(List<string> i_StringArrayToPrint)
        {
            foreach(string stringToPrint in i_StringArrayToPrint)
            {
                Console.WriteLine(stringToPrint);
            }
        }

        private void addVehicleToGarage(string io_LicensePlate)
        {
            string ownerName;
            string ownerPhone;
            Vehicle newVehicle;
            if(m_Garage.IsVehicleExists(io_LicensePlate))
            {
                Console.WriteLine("Vehcile is allready listed in the garage!");
                Console.WriteLine("changing the vehicle status to 'In Repair'...");
                m_Garage.ChangeStatus(io_LicensePlate, GarageLogic.VehicleTicket.eVehicleStatus.InRepair);
            }
            else
            {
                try
                {
                    Console.WriteLine("Owner name:");
                    ownerName = Console.ReadLine();
                    Console.WriteLine("Owner phone number:");
                    ownerPhone = Console.ReadLine();
                    newVehicle = getNewVehicle(io_LicensePlate);
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
            string energySource;
            Dictionary<string, string> vehicleProperties;
            string vehicleType = getUserSelection(SystemVehiclesSupport.SupportedVehicles);
            energySource = getUserSelection(SystemVehiclesSupport.GetEnergySources(vehicleType));
            vehicleProperties = getVehicleProperties(vehicleType, energySource);
            vehicleProperties.Add("License plate", i_LicensePlate);
            newVehicle = SystemVehiclesSupport.CreateVehicle(ref vehicleType, energySource, vehicleProperties);

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

        private string getLicensePlate()
        {
            Console.WriteLine("License plate:");
            return Console.ReadLine();
        }
    }
}
