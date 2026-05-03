using System;
using Microsoft.Win32;

namespace DVLDDataAccessLayer
{
    internal class clsDataAccessSettings
    {
        private static string GetValue()
        {
            string KeyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            string ValueName = "DataBaseInfo";

            try
            {
                return Registry.GetValue(KeyPath, ValueName, null) as string;
            }
            catch
            {
                
                return null;
            }
        }

        private static readonly string Value = GetValue();

        public static readonly string ConnectionString =
            "Server=.;Database=DVLD;" + Value;
    }
}

