using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsDriver
    {
        public static int AddNewDriver(int PersonID, int UserID, DateTime IssueDate)
        {
            return DriversData.AddNewDriver(PersonID, UserID, IssueDate);
        }
        public static int GetDriverIDByPersonID(int PersonID)
        {
            return DriversData.GetDriverIDByPersonID(PersonID);
        }
        public static DataTable GetAllDriversInformation()
        {
            return DriversData.GetAllDriversInformation();
        }
        public static DataTable GetAllDriversInformationWithFiltration(string Filtre, string fl)
        {
            return DriversData.GetAllDriversInformationWithFiltration(Filtre, fl);
        }
        public static DataTable GetAllDriversInformationWithFiltration(string Filtre, int fl)
        {
            return DriversData.GetAllDriversInformationWithFiltration(Filtre, fl);
        }
    }
}
