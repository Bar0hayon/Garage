using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class SystemVehiclesSupport
    {
        private static string[] s_SupportedVehicles = {"Car", "Motorcycle", "Truck"};
        private static string[] s_SupportedEnergySources = {"Electric", "Fuel" };
        private static string[] s_VehicleProperties = {"Model Name", "Energy Percentage (number 0-100)",
        "Wheel Manufacturer", "Wheels Air Pressure"};
        private static string[] s_CarProperties = {"Color (Red/ Blue/ Black/ Gray)",
            "Number of Doors (2/ 3/ 4/ 5)"};
        private static string[] s_MotorcycleProperties = { "License Type (A/ A1/ A2/ B)", "EngineVolume" };
        private static string[] s_TruckProperties = { "Is the Carriage Dangerous (yes/ no)",
        "CarriageCapacity"};

        public static List<string> GetVehicleProperties(string i_VehicleType, string i_EnergySource)
        {
            List<string> VehicleProperties = new List<string>();
            VehicleProperties.AddRange(s_VehicleProperties);
            
            switch (i_VehicleType)
            {
                case ("Car"):
                    VehicleProperties.AddRange(s_CarProperties);
                    break;
                case ("Motorcycle"):
                    VehicleProperties.AddRange(s_MotorcycleProperties);
                    break;
                case ("Truck"):
                    VehicleProperties.AddRange(s_TruckProperties);
                    break;
                default:
                    throw new Exception("Energy source is not valid!");
            }

            return VehicleProperties;
        }

        public static string[] SupportedVehicles
        {
            get
            {
                return s_SupportedVehicles;
            }
        }

        public static string[] GetEnergySources(string i_VehicleType)
        {
            if(i_VehicleType == "Truck")
            {
                return new string[] { "Fuel" };
            }
            else
            {
                return s_SupportedEnergySources;
            }
        }

        public static Vehicle CreateVehicle(ref string i_VehicleType, string i_EnergySource, 
            Dictionary<string, string> i_VehicleProperties)
        {
            Vehicle NewVehicle;
            string ModelName = i_VehicleProperties["Model Name"];
            string LicensePlate = i_VehicleProperties["License plate"];
            float EnergyPercentage = float.Parse(i_VehicleProperties["Energy Percentage (number 0-100)"]);
            EnergyPercentage /= 100;
            Wheel[] Wheels = getWheels(i_VehicleType, i_VehicleProperties);
            EnergySource energySource = GetEnergySource(i_EnergySource, i_VehicleType, i_VehicleProperties);
            switch (i_VehicleType)
            {
                case ("Car"):
                    Car.eCarColor CarColor = getCarColor(i_VehicleProperties[s_CarProperties[0]]);
                    int NumOfDoors = int.Parse(i_VehicleProperties[s_CarProperties[1]]);
                    NewVehicle = new Car(ModelName, LicensePlate, EnergyPercentage,
                        Wheels, energySource, CarColor, NumOfDoors);
                    break;
                case ("Motorcycle"):
                    Motorcycle.eLicenseType LicenseType = getMotorcycleLicesneType(
                        i_VehicleProperties[s_MotorcycleProperties[0]]);
                    int EngineVolume = int.Parse(i_VehicleProperties[s_MotorcycleProperties[1]]);
                    NewVehicle = new Motorcycle(ModelName, LicensePlate, EnergyPercentage,
                        Wheels, energySource, LicenseType, EngineVolume);
                    break;
                case ("Truck"):
                    bool IsCarriageDangerous = getIsTruckCarriageDangerous(
                        i_VehicleProperties[s_TruckProperties[0]]);
                    float CarriageCapacity = float.Parse(
                        i_VehicleProperties[s_TruckProperties[1]]);
                    NewVehicle = new Truck(ModelName, LicensePlate, EnergyPercentage, Wheels,
                        energySource, IsCarriageDangerous, CarriageCapacity);
                    break;
                default:
                    throw new Exception("Vehicle type is not valid");
            }

            return NewVehicle;
        }

        private static bool getIsTruckCarriageDangerous(string i_IsCarriageDangerousString)
        {
            bool IsCarriageDangerous;
            switch (i_IsCarriageDangerousString)
            {
                case ("yes"):
                    IsCarriageDangerous = true;
                    break;
                case ("no"):
                    IsCarriageDangerous = false;
                    break;
                default:
                    throw new Exception("input string should be yes/no");
            }

            return IsCarriageDangerous;
        }

        private static Motorcycle.eLicenseType getMotorcycleLicesneType(string i_LicenseTypeString)
        {
            Motorcycle.eLicenseType LicenseType;
            switch (i_LicenseTypeString)
            {
                case ("A"):
                    LicenseType = Motorcycle.eLicenseType.A;
                    break;
                case ("A1"):
                    LicenseType = Motorcycle.eLicenseType.A1;
                    break;
                case ("A2"):
                    LicenseType = Motorcycle.eLicenseType.A2;
                    break;
                case ("B"):
                    LicenseType = Motorcycle.eLicenseType.B;
                    break;
                default:
                    throw new Exception("License Type is not supported");
            }

            return LicenseType;
        }

        private static Car.eCarColor getCarColor(string i_CarColorString)
        {
            Car.eCarColor CarColor;
            switch (i_CarColorString)
            {
                case ("Red"):
                    CarColor = Car.eCarColor.Red;
                    break;
                case ("Blue"):
                    CarColor = Car.eCarColor.Blue;
                    break;
                case ("Black"):
                    CarColor = Car.eCarColor.Black;
                    break;
                case ("Gray"):
                    CarColor = Car.eCarColor.Gray;
                    break;
                default:
                    throw new Exception("Car color is not supported!");
            }

            return CarColor;
        }

        private static EnergySource GetEnergySource(string i_EnergySource, string i_VehicleType,
            Dictionary<string, string> i_VehicleProperties)
        {
            EnergySource energySource;
            float MaxEnergy;
            float EnergyLeft;
            float MinEnergyToAdd = 0;
            float EnergyPercenatge = float.Parse(i_VehicleProperties["Energy Percentage (number 0-100)"]);
            EnergyPercenatge /= 100;
            if (i_EnergySource == "Fuel")
            {
                eFuelType FuelType = getFuelType(i_VehicleType);
                MaxEnergy = getFuelMaxEnergy(i_VehicleType);
                EnergyLeft = MaxEnergy * EnergyPercenatge;
                energySource = new FuelEnergy(FuelType, MaxEnergy, EnergyLeft, MinEnergyToAdd);
            }
            else if (i_EnergySource == "Electric")
            {
                MaxEnergy = getElectricMaxEnergy(i_VehicleType);
                EnergyLeft = MaxEnergy * EnergyPercenatge;
                energySource = new ElectricEnergy(MaxEnergy, EnergyLeft, MinEnergyToAdd);
            }
            else
            {
                throw new Exception("energy source is not valid");
            }

            return energySource;
        }

        private static float getElectricMaxEnergy(string i_VehicleType)
        {
            float MaxEnergy;
            switch (i_VehicleType)
            {
                case ("Motorcycle"):
                    MaxEnergy = (float)1.4;
                    break;
                case ("Car"):
                    MaxEnergy = (float)1.8;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }

            return MaxEnergy;
        }

        private static float getFuelMaxEnergy(string i_VehicleType)
        {
            float MaxEnergy;
            switch (i_VehicleType)
            {
                case ("Truck"):
                    MaxEnergy = 110;
                    break;
                case ("Motorcycle"):
                    MaxEnergy = 8;
                    break;
                case ("Car"):
                    MaxEnergy = 55;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }

            return MaxEnergy;
        }

        private static eFuelType getFuelType(string i_VehicleType)
        {
            eFuelType FuelType;
            switch (i_VehicleType)
            {
                case ("Truck"):
                    FuelType = eFuelType.Soler;
                    break;
                case ("Motorcycle"):
                    FuelType = eFuelType.Octan95;
                    break;
                case ("Car"):
                    FuelType = eFuelType.Octan96;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }
            return FuelType;
        }
        
        private static Wheel[] getWheels(string i_VehicleType, Dictionary<string, string> i_VehicleProperties)
        {
            int numOfWheels = getNumOfWheels(i_VehicleType);
            Wheel[] Wheels = new Wheel[numOfWheels];
            string Manufacturer = i_VehicleProperties["Wheel Manufacturer"];
            float MaxAirPressure = getMaxAirPressure(i_VehicleType);
            float MinAirPressure = 0;
            float CurrentAirPressure = float.Parse(i_VehicleProperties["Wheels Air Pressure"]);
            if(CurrentAirPressure > MaxAirPressure || CurrentAirPressure < MinAirPressure)
            {
                throw new ValueOutOfRangeException(MaxAirPressure, MinAirPressure, "Wheels air pressure");
            }
            for (int i = 0; i < numOfWheels; i++)
            {
                Wheels[i] = new Wheel(Manufacturer, MaxAirPressure, MinAirPressure, CurrentAirPressure);
            }

            return Wheels;
        }

        private static float getMaxAirPressure(string i_VehicleType)
        {
            float MaxAirPressure;
            switch (i_VehicleType)
            {
                case ("Car"):
                    MaxAirPressure = 31;
                    break;
                case ("Motorcycle"):
                    MaxAirPressure = 33;
                    break;
                case ("Truck"):
                    MaxAirPressure = 26;
                    break;
                default:
                    throw new Exception("Vehicle Type is not supported!");
            }

            return MaxAirPressure;
        }

        private static int getNumOfWheels(string i_VehicleType)
        {
            int numOfWheels;
            switch (i_VehicleType)
            {
                case ("Car"):
                    numOfWheels = 4;
                    break;
                case ("Motorcycle"):
                    numOfWheels = 2;
                    break;
                case ("Truck"):
                    numOfWheels = 12;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }

            return numOfWheels;
        }
    }
}
