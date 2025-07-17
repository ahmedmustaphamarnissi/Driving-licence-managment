using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsApplicationType
    {
        public static DataTable GetAllApplicationTypes()
        {
            return ApplicationsTypeData.GetAllApplicationsTypes();
        }
        public static bool GetApplicationTypeByID(int ApplicationTypeID, ref string ApplicationTypeName, ref decimal ApplicationFees)
        {
            return ApplicationsTypeData.GetApplicationTypeByID(ApplicationTypeID, ref ApplicationTypeName, ref ApplicationFees);
        }
        public static bool UpdateApplicationTypeByID(int ApplicationTypeID, string ApplicationTypeName, decimal ApplicationFees)
        {
            return ApplicationsTypeData.UpdateApplicationTypeByID(ApplicationTypeID, ApplicationTypeName, ApplicationFees);
        }
        public static decimal GetApplicationTypeFeesByID(int ApplicationTypeID)
        {
            return ApplicationsTypeData.GetApplicationTypeFees(ApplicationTypeID);
        }
        public static bool GetApplicationTypeByID(int ApplicationTypeID, ref string ApplicationTypeName)
        {
            return ApplicationsTypeData.GetApplicationTypeByID(ApplicationTypeID, ref ApplicationTypeName);
        }


    }
}
