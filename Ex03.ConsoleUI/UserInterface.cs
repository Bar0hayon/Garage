using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        public class QualityCheck
        {
            internal const int k_MaximumInputSize = 16;
            internal const int k_MaximumInputPhoneSize = 10;
            internal const int k_MinimumInputSize = 1;

            internal static bool IsStringContainOnlyDigits(string i_InputString)
            {
                bool IsOnlyDigitsExist = true;
                foreach (char ch in i_InputString)
                {
                    if (!char.IsDigit(ch))
                    {
                        IsOnlyDigitsExist = false;
                        throw new FormatException("Invalid input: string contain special characters and/or letters.");
                    }
                }

                return IsOnlyDigitsExist;
            }

            internal static bool IsStringContainOnlyChars(string i_InputString)
            {
                bool IsOnlyCharsExist = true;
                foreach (char ch in i_InputString)
                {
                    if (!char.IsLetter(ch))
                    {
                        IsOnlyCharsExist = false;
                        throw new FormatException("Invalid input: string contain special characters and/or digits number.");
                    }
                }

                return IsOnlyCharsExist;
            }

            internal static bool IsStringContainOnlyCharsOrDigits(string i_InputString)
            {
                bool IsOnlyCharsOrDigitsExist = true;
                foreach (char ch in i_InputString)
                {
                    if (!(char.IsDigit(ch) || char.IsLetter(ch)))
                    {
                        IsOnlyCharsOrDigitsExist = false;
                        throw new FormatException("Invalid input: string contain special characters.");
                    }
                }

                return IsOnlyCharsOrDigitsExist;
            }

            internal static bool IsInputLengthValid(string i_InputString)
            {
                bool IsInputLengthValid = i_InputString.Length <= k_MaximumInputSize && i_InputString.Length >= k_MinimumInputSize;
                if (!IsInputLengthValid)
                {
                    throw new ValueOutOfRangeException(k_MaximumInputSize, k_MinimumInputSize, "Input string length is invalid!");
                }

                return IsInputLengthValid;
            }

            internal static void IsFullLicensePlateValid(string i_LicensePlate, ref bool io_IsLicensePlateValid)
            {
                io_IsLicensePlateValid = IsStringContainOnlyCharsOrDigits(i_LicensePlate) && IsInputLengthValid(i_LicensePlate);
            }

            internal static bool IsInputPhoneLengthValid(string i_InputString)
            {
                bool IsInputLengthValid = i_InputString.Length == k_MaximumInputPhoneSize;
                if (!IsInputLengthValid)
                {
                    throw new FormatException("Phone number length is invalid!");
                }

                return IsInputLengthValid;
            }

            internal static bool IsPhoneNumberValid(string i_PhoneNumber)
            {
                return IsStringContainOnlyDigits(i_PhoneNumber) && IsInputPhoneLengthValid(i_PhoneNumber);
            }
        }

        private Garage m_Garage = new Garage();

        public void MainMenu()
        {
            string userSelection;
            do
            {
                userSelection = getUserSelection(UserOptions.MainMenu);
                switch (userSelection)
                {
                    case "Add a vehicle to the garage":
                        string licensePlate = getLicensePlate();
                        addVehicleToGarage(licensePlate);
                        break;
                    case "Show vehicles License-plates":
                        showLicensePlates();
                        break;
                    case "Change vehicle status":
                        changeVehicleStatus();
                        break;
                    case "Fill the Wheels air-pressure of a vehicle to maximum":
                        fillAirPressureToMax();
                        break;
                    case "Add fuel":
                        addFuel();
                        break;
                    case "Charge battery":
                        chargeBattery();
                        break;
                    case "Show vehicle details":
                        showVehicleDetails();
                        break;
                    case "EXIT":
                        Console.WriteLine("GoodBye!");
                        break;
                    default:
                        Console.WriteLine("Input is not valid!");
                        break;
                }
            }
            while (userSelection != "EXIT");
        }

        private void showVehicleDetails()
        {
            string licensePlate = getLicensePlate();
            if (m_Garage.IsVehicleExists(licensePlate))
            {
                Console.WriteLine(m_Garage.GetVehicleDetailsAsString(licensePlate));
            }
            else
            {
                Console.WriteLine("Vehicle is not listed in the garage!");
            }
        }

        private void chargeBattery()
        {
            string licensePlate = getLicensePlate();
            if (m_Garage.IsVehicleExists(licensePlate))
            {
                Console.WriteLine("Amount of battery to charge:");
                float amountOfChargeBatteryToDo = float.Parse(Console.ReadLine());
                try
                {
                    m_Garage.ChargeBattery(licensePlate, amountOfChargeBatteryToDo);
                    Console.WriteLine("Battery was charged successfully!");
                }
                catch
                {
                    Console.WriteLine("Battery was not charged!");
                }
            }
            else
            {
                Console.WriteLine("License plate was not found. Could not finish charging battery operation.");
            }
        }

        private void addFuel()
        {
            string licensePlate = getLicensePlate();
            if (m_Garage.IsVehicleExists(licensePlate))
            {
                eFuelType fuelType = getFuelType();
                Console.WriteLine("Amount of fuel to add:");
                float amountOfFuelToAdd = float.Parse(Console.ReadLine());
                try
                {
                    m_Garage.AddFuelToVehicle(licensePlate, fuelType, amountOfFuelToAdd);
                    Console.WriteLine("Fuel was added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Fuel was not added!");
                }
            }
            else
            {
                Console.WriteLine("License plate was not found. Could not finish add fuel operation.");
            }
        }

        private eFuelType getFuelType()
        {
            eFuelType fuelType;
            string userSelection = getUserSelection(UserOptions.FuelTypes);
            switch (userSelection)
            {
                case "Soler":
                    fuelType = eFuelType.Soler;
                    break;
                case "Octan98":
                    fuelType = eFuelType.Octan98;
                    break;
                case "Octan96":
                    fuelType = eFuelType.Octan96;
                    break;
                case "Octan95":
                    fuelType = eFuelType.Octan95;
                    break;
                default:
                    throw new FormatException("Fuel type is not supported");
            }

            return fuelType;
        }

        private void fillAirPressureToMax()
        {
            string licensePlate = getLicensePlate();
            if (m_Garage.IsVehicleExists(licensePlate))
            {
                try
                {
                    m_Garage.FillAirPressure(licensePlate);
                    Console.WriteLine("Tiers's air pressure is fixed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Error occured while trying to adjust tiers's air pressure");
                }
            }
            else
            {
                Console.WriteLine("License plate was not found. Could not finish fill air pressure to max operation.");
            }
        }

        private void changeVehicleStatus()
        {
            string licensePlate = getLicensePlate();
            string userSelection = getUserSelection(UserOptions.VehicleStatus);
            switch (userSelection)
            {
                case "Change to 'In Repair'":
                    m_Garage.ChangeStatus(licensePlate, VehicleTicket.eVehicleStatus.InRepair);
                    Console.WriteLine("Status changed to 'In Repair' successfully");
                    break;
                case "Change to 'Repaired'":
                    m_Garage.ChangeStatus(licensePlate, VehicleTicket.eVehicleStatus.Repaired);
                    Console.WriteLine("Status changed to 'Repaired' successfully");
                    break;
                case "Change to 'Paid'":
                    m_Garage.ChangeStatus(licensePlate, VehicleTicket.eVehicleStatus.Paid);
                    Console.WriteLine("Status changed to 'Paid' successfully");
                    break;
                default:
                    throw new Exception("User selection is not supported!");
            }
        }

        private void showLicensePlates()
        {
            string userSelection = getUserSelection(UserOptions.LicensePlatesFilter);
            switch (userSelection)
            {
                case "Show all License plates":
                    printStringArray(m_Garage.GetAllLicensePlates());
                    break;
                case "Show License plates of vehicles in status 'In Repair'":
                    printStringArray(m_Garage.GetLicesnePlates(VehicleTicket.eVehicleStatus.InRepair));
                    break;
                case "Show License plates of vehicles in status 'Repaired'":
                    printStringArray(m_Garage.GetLicesnePlates(VehicleTicket.eVehicleStatus.Repaired));
                    break;
                case "Show License plates of vehicles in status 'Paid'":
                    printStringArray(m_Garage.GetLicesnePlates(VehicleTicket.eVehicleStatus.Paid));
                    break;
                default:
                    break;
            }
        }

        private void printStringArray(List<string> i_StringArrayToPrint)
        {
            foreach (string stringToPrint in i_StringArrayToPrint)
            {
                Console.WriteLine(stringToPrint);
            }
        }

        private void addVehicleToGarage(string io_LicensePlate)
        {
            string ownerName;
            string ownerPhone;
            Vehicle newVehicle;
            if (m_Garage.IsVehicleExists(io_LicensePlate))
            {
                Console.WriteLine("Vehcile is allready listed in the garage!");
                Console.WriteLine("Changing the vehicle status to 'In Repair'...");
                m_Garage.ChangeStatus(io_LicensePlate, GarageLogic.VehicleTicket.eVehicleStatus.InRepair);
            }
            else
            {
                try
                {
                    Console.WriteLine("Owner name:");
                    ownerName = Console.ReadLine();
                    QualityCheck.IsStringContainOnlyChars(ownerName);
                    Console.WriteLine("Owner phone number:");
                    ownerPhone = Console.ReadLine();
                    QualityCheck.IsPhoneNumberValid(ownerPhone);
                    newVehicle = getNewVehicle(io_LicensePlate);
                    m_Garage.AddVehicleTicket(ownerName, ownerPhone, newVehicle);
                    Console.WriteLine("Vehicle was added successfully");
                }
                catch (Exception ex)
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
            foreach (string propertie in SystemVehiclesSupport.GetVehicleProperties(i_VehicleType, i_EnergySource))
            {
                Console.WriteLine(propertie + ":");
                VehicleProperties.Add(propertie, Console.ReadLine());
            }

            return VehicleProperties;
        }

        private string getUserSelection(string[] i_Options)
        {
            int userSelection;
            if (i_Options.Length > 1)
            {
                do
                {
                    Console.WriteLine("Please choose one of the followings:");
                    for (int i = 0; i < i_Options.Length; i++)
                    {
                        Console.WriteLine(string.Format("\t{0}.{1}", (i + 1).ToString(), i_Options[i]));
                    }

                    userSelection = int.Parse(Console.ReadLine()) - 1;
                    if (userSelection < 0 || userSelection >= i_Options.Length)
                    {
                        throw new FormatException("Input string is not valid!");
                    }
                }
                while (userSelection < 0 || userSelection >= i_Options.Length);
            }
            else
            {
                userSelection = 0;
            }

            return i_Options[userSelection];
        }

        private string getLicensePlate()
        {
            bool isLicensePlateValid = false;
            string licensePlate;
            do
            {
                Console.WriteLine("Please insert license plate:");
                licensePlate = Console.ReadLine();
                QualityCheck.IsFullLicensePlateValid(licensePlate, ref isLicensePlateValid);
            }
            while (!isLicensePlateValid);

            return licensePlate;
        }
    }
}