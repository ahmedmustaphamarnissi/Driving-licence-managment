using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsLicenseClasses
    {
        public static DataTable GetAllLicenseClassesNames()
        {
            return LicenseClassesData.GetAllLicenseClassesNames();
        }
        public static int GetDefaultValidityLength(int ClassTypeID)
        {
            return LicenseClassesData.GetDefaultValidityLength(ClassTypeID);
        }

        public static decimal GetPaidFeesLength(int ClassTypeID)
        {
            return LicenseClassesData.GetPaidFees(ClassTypeID);
        }
    }
}
