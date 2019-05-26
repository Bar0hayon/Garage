using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class SystemVehiclesSupport
    {
        private static string[] s_SupportedVehicles = { "Car", "Motorcycle", "Truck" };
        private static string[] s_SupportedEnergySources = { "Electric", "Fuel" };
        private static string[] s_MotorcycleProperties = { "License Type (A/ A1/ A2/ B)", "EngineVolume" };
        private static string[] s_VehicleProperties = 
        {
            "Model Name", "Energy Percentage (number 0-100)", "Wheel Manufacturer", "Wheels Air Pressure"
        };

        private static string[] s_CarProperties = 
        {
            "Color (Red/ Blue/ Black/ Gray)",
            "Number of Doors (2/ 3/ 4/ 5)"
        };

        private static string[] s_TruckProperties = 
        {
            "Is the Carriage Dangerous (yes/ no)",
            "CarriageCapacity"
        };

        public static List<string> GetVehicleProperties(string i_VehicleType, string i_EnergySource)
        {
            List<string> VehicleProperties = new List<string>();
            VehicleProperties.AddRange(s_VehicleProperties);
            
            switch (i_VehicleType)
            {
                case "Car":
                    VehicleProperties.AddRange(s_CarProperties);
                    break;
                case "Motorcycle":
                    VehicleProperties.AddRange(s_MotorcycleProperties);
                    break;
                case "Truck":
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

        public static Vehicle CreateVehicle(
                                            ref string i_VehicleType, 
                                            string i_EnergySource,
                                            Dictionary<string, string> i_VehicleProperties)
        {
            Vehicle newVehicle;
            string modelName = i_VehicleProperties["Model Name"];
            string licensePlate = i_VehicleProperties["License plate"];
            float energyPercentage = float.Parse(i_VehicleProperties["Energy Percentage (number 0-100)"]);
            energyPercentage /= 100;
            Wheel[] wheelsOfVehicle = getWheels(i_VehicleType, i_VehicleProperties);
            EnergySource energySource = getEnergySource(i_EnergySource, i_VehicleType, i_VehicleProperties);
            switch (i_VehicleType)
            {
                case "Car":
                    Car.eCarColor CarColor = getCarColor(i_VehicleProperties[s_CarProperties[0]]);
                    int numOfDoors = int.Parse(i_VehicleProperties[s_CarProperties[1]]);
                    newVehicle = new Car(
                                        modelName, 
                                        licensePlate, 
                                        energyPercentage,
                                        wheelsOfVehicle, 
                                        energySource, 
                                        CarColor, 
                                        numOfDoors);
                    break;
                case "Motorcycle":
                    Motorcycle.eLicenseType LicenseType = getMotorcycleLicesneType(
                        i_VehicleProperties[s_MotorcycleProperties[0]]);
                    int engineVolume = int.Parse(i_VehicleProperties[s_MotorcycleProperties[1]]);
                    newVehicle = new Motorcycle(
                                                modelName, 
                                                licensePlate, 
                                                energyPercentage, 
                                                wheelsOfVehicle, 
                                                energySource, 
                                                LicenseType, 
                                                engineVolume);
                    break;
                case "Truck":
                    bool isCarriageDangerous = getIsTruckCarriageDangerous(
                        i_VehicleProperties[s_TruckProperties[0]]);
                    float carriageCapacity = float.Parse(
                        i_VehicleProperties[s_TruckProperties[1]]);
                    newVehicle = new Truck(
                                            modelName, 
                                            licensePlate, 
                                            energyPercentage, 
                                            wheelsOfVehicle,
                                            energySource, 
                                            isCarriageDangerous, 
                                            carriageCapacity);
                    break;
                default:
                    throw new Exception("Vehicle type is not valid");
            }

            return newVehicle;
        }

        private static bool getIsTruckCarriageDangerous(string i_IsCarriageDangerousString)
        {
            bool isCarriageDangerous;
            i_IsCarriageDangerousString = i_IsCarriageDangerousString.ToLower();
            switch (i_IsCarriageDangerousString)
            {
                case "yes":
                    isCarriageDangerous = true;
                    break;
                case "no":
                    isCarriageDangerous = false;
                    break;
                default:
                    throw new Exception("input string should be yes/no");
            }

            return isCarriageDangerous;
        }

        private static Motorcycle.eLicenseType getMotorcycleLicesneType(string i_LicenseTypeString)
        {
            Motorcycle.eLicenseType licenseType;
            i_LicenseTypeString = i_LicenseTypeString.ToLower();
            switch (i_LicenseTypeString)
            {
                case "a":
                    licenseType = Motorcycle.eLicenseType.A;
                    break;
                case "a1":
                    licenseType = Motorcycle.eLicenseType.A1;
                    break;
                case "a2":
                    licenseType = Motorcycle.eLicenseType.A2;
                    break;
                case "b":
                    licenseType = Motorcycle.eLicenseType.B;
                    break;
                default:
                    throw new Exception("License Type is not supported");
            }

            return licenseType;
        }

        private static Car.eCarColor getCarColor(string i_CarColorString)
        {
            Car.eCarColor carColor;
            i_CarColorString = i_CarColorString.ToLower();
            switch (i_CarColorString)
            {
                case "red":
                    carColor = Car.eCarColor.Red;
                    break;
                case "blue":
                    carColor = Car.eCarColor.Blue;
                    break;
                case "black":
                    carColor = Car.eCarColor.Black;
                    break;
                case "gray":
                    carColor = Car.eCarColor.Gray;
                    break;
                default:
                    throw new Exception("Car color is not supported!");
            }

            return carColor;
        }

        private static EnergySource getEnergySource(
                                                    string i_EnergySource, 
                                                    string i_VehicleType,
                                                    Dictionary<string, string> i_VehicleProperties)
        {
            EnergySource energySource;
            float maxEnergy;
            float energyLeft;
            float minEnergyToAdd = 0;
            float energyPercenatge = float.Parse(i_VehicleProperties["Energy Percentage (number 0-100)"]);
            energyPercenatge /= 100;
            if (i_EnergySource == "Fuel")
            {
                eFuelType fuelType = getFuelType(i_VehicleType);
                maxEnergy = getFuelMaxEnergy(i_VehicleType);
                energyLeft = maxEnergy * energyPercenatge;
                energySource = new FuelEnergy(fuelType, maxEnergy, energyLeft, minEnergyToAdd);
            }
            else if (i_EnergySource == "Electric")
            {
                maxEnergy = getElectricMaxEnergy(i_VehicleType);
                energyLeft = maxEnergy * energyPercenatge;
                energySource = new ElectricEnergy(maxEnergy, energyLeft, minEnergyToAdd);
            }
            else
            {
                throw new Exception("energy source is not valid");
            }

            return energySource;
        }

        private static float getElectricMaxEnergy(string i_VehicleType)
        {
            float maxEnergy;
            switch (i_VehicleType)
            {
                case "Motorcycle":
                    maxEnergy = (float)1.4;
                    break;
                case "Car":
                    maxEnergy = (float)1.8;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }

            return maxEnergy;
        }

        private static float getFuelMaxEnergy(string i_VehicleType)
        {
            float maxEnergy;
            switch (i_VehicleType)
            {
                case "Truck":
                    maxEnergy = 110;
                    break;
                case "Motorcycle":
                    maxEnergy = 8;
                    break;
                case "Car":
                    maxEnergy = 55;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }

            return maxEnergy;
        }

        private static eFuelType getFuelType(string i_VehicleType)
        {
            eFuelType fuelType;
            switch (i_VehicleType)
            {
                case "Truck":
                    fuelType = eFuelType.Soler;
                    break;
                case "Motorcycle":
                    fuelType = eFuelType.Octan95;
                    break;
                case "Car":
                    fuelType = eFuelType.Octan96;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }

            return fuelType;
        }
        
        private static Wheel[] getWheels(string i_VehicleType, Dictionary<string, string> i_VehicleProperties)
        {
            int numOfWheels = getNumOfWheels(i_VehicleType);
            Wheel[] wheelsOfVehicle = new Wheel[numOfWheels];
            string manufacturer = i_VehicleProperties["Wheel Manufacturer"];
            float maxAirPressure = getMaxAirPressure(i_VehicleType);
            float minAirPressure = 0;
            float currentAirPressure = float.Parse(i_VehicleProperties["Wheels Air Pressure"]);
            if(currentAirPressure > maxAirPressure || currentAirPressure < minAirPressure)
            {
                throw new ValueOutOfRangeException(maxAirPressure, minAirPressure, "Wheels air pressure");
            }

            for (int i = 0; i < numOfWheels; i++)
            {
                wheelsOfVehicle[i] = new Wheel(manufacturer, maxAirPressure, minAirPressure, currentAirPressure);
            }

            return wheelsOfVehicle;
        }

        private static float getMaxAirPressure(string i_VehicleType)
        {
            float maxAirPressure;
            switch (i_VehicleType)
            {
                case "Car":
                    maxAirPressure = 31;
                    break;
                case "Motorcycle":
                    maxAirPressure = 33;
                    break;
                case "Truck":
                    maxAirPressure = 26;
                    break;
                default:
                    throw new Exception("Vehicle Type is not supported!");
            }

            return maxAirPressure;
        }

        private static int getNumOfWheels(string i_VehicleType)
        {
            int numOfWheels;
            switch (i_VehicleType)
            {
                case "Car":
                    numOfWheels = 4;
                    break;
                case "Motorcycle":
                    numOfWheels = 2;
                    break;
                case "Truck":
                    numOfWheels = 12;
                    break;
                default:
                    throw new Exception("Vehicle Type is not valid");
            }

            return numOfWheels;
        }
    }
}