using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public static class UserOptions
    {
        private static readonly string[] r_MainMenuOptions = 
            {
            "Add a vehicle to the garage",
            "Show vehicles License-plates",
            "Change vehicle status",
            "Fill the Wheels air-pressure of a vehicle to maximum",
            "Add fuel",
            "Charge battery",
            "Show vehicle details",
            "EXIT"
            };

        private static readonly string[] r_LicensePlatesFilterOptions =
            {
            "Show all License plates",
            "Show License plates of vehicles in status 'In Repair'",
            "Show License plates of vehicles in status 'Repaired'",
            "Show License plates of vehicles in status 'Paid'"
            };

        private static readonly string[] r_VehicleStatusOptions = 
            {
            "Change to 'In Repair'",
            "Change to 'Repaired'", "Change to 'Paid'"
            };

        private static readonly string[] r_FuelTypes = 
            {
            "Soler",
            "Octan98",
            "Octan96",
            "Octan95"
            };

        public static string[] MainMenu
        {
            get
            {
                return r_MainMenuOptions;
            }
        }

        public static string[] LicensePlatesFilter
        {
            get
            {
                return r_LicensePlatesFilterOptions;
            }
        }

        public static string[] VehicleStatus
        {
            get
            {
                return r_VehicleStatusOptions;
            }
        }

        public static string[] FuelTypes
        {
            get
            {
                return r_FuelTypes;
            }
        }
    }
}