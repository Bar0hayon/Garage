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
            internal const int k_MinimumInputSize = 1;
            internal static bool isStringContainOnlyDigits(string i_InputString)
            {
                bool IsOnlyDigitsExist = true;
                foreach (char ch in i_InputString)
                {
                    if (!Char.IsDigit(ch))
                    {
                        IsOnlyDigitsExist = false;
                        throw new FormatException("Invalid input: string contain special characters and/or letters.");
                    }
                }
                return IsOnlyDigitsExist;
            }

            internal static bool isStringContainOnlyChars(string i_InputString)
            {
                bool IsOnlyCharsExist = true;
                foreach (char ch in i_InputString)
                {
                    if (!Char.IsLetter(ch))
                    {
                        IsOnlyCharsExist = false;
                        throw new FormatException("Invalid input: string contain special characters and/or digits number.");
                    }
                }
                return IsOnlyCharsExist;
            }

            internal static bool isStringContainOnlyCharsOrDigits(string i_InputString)
            {
                bool IsOnlyCharsOrDigitsExist = true;
                foreach (char ch in i_InputString)
                {
                    if (!(Char.IsDigit(ch) || Char.IsLetter(ch)))
                    {
                        IsOnlyCharsOrDigitsExist = false;
                        throw new FormatException("Invalid input: string contain special characters.");
                    }
                }
                return IsOnlyCharsOrDigitsExist;
            }

            internal static bool isInputLengthValid (string i_InputString)
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
                io_IsLicensePlateValid = isStringContainOnlyCharsOrDigits(i_LicensePlate) && isInputLengthValid(i_LicensePlate);
            }
        }
        Garage m_Garage = new Garage();

        public void MainMenu()
        {
            string userSelection;
            do
            {
                userSelection = getUserSelection(UserOptions.MainMenu);
                switch (userSelection)
                {
                    case ("Add a vehicle to the garage"):
                        string LicensePlate = getLicensePlate();
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
            string LicensePlate = getLicensePlate();
            if (m_Garage.IsVehicleExists(LicensePlate))
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
            string LicensePlate = getLicensePlate();
            if (m_Garage.IsVehicleExists(LicensePlate))
            {
                Console.WriteLine("Amount of battery to charge:");
                float AmountOfChargeBatteryToDo = float.Parse(Console.ReadLine());
                try
                {
                    m_Garage.ChargeBattery(LicensePlate, AmountOfChargeBatteryToDo);
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
            string LicensePlate = getLicensePlate();
            if (m_Garage.IsVehicleExists(LicensePlate))
            {
                eFuelType FuelType = getFuelType();
                Console.WriteLine("Amount of fuel to add:");
                float AmountOfFuelToAdd = float.Parse(Console.ReadLine());
                try
                {
                    m_Garage.AddFuelToVehicle(LicensePlate, FuelType, AmountOfFuelToAdd);
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
            eFuelType FuelType;
            string UserSelection = getUserSelection(UserOptions.FuelTypes);
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
            string LicensePlate = getLicensePlate();
            string UserSelection = getUserSelection(UserOptions.VehicleStatus);
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
                    throw new Exception("User selection is not supported!");
            }
        }

        private void showLicensePlates()
        {
            string UserSelection = getUserSelection(UserOptions.LicensePlatesFilter);
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
                    QualityCheck.isStringContainOnlyChars(ownerName);
                    Console.WriteLine("Owner phone number:");
                    ownerPhone = Console.ReadLine();
                    QualityCheck.isStringContainOnlyDigits(ownerName);
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
                        throw new FormatException("Input string is not valid!");
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
            bool IsLicensePlateValid = false;
            string LicensePlate;
            do
            {
                Console.WriteLine("Please insert license plate:");
                LicensePlate = Console.ReadLine();
                QualityCheck.IsFullLicensePlateValid(LicensePlate, ref IsLicensePlateValid);
            } while (!IsLicensePlateValid);

            return LicensePlate;
        }
    }
}