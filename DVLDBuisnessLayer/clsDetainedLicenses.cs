using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsDetainedLicenses
    {
        public static string IsDetained(int LicenseID)
        {
            return DetainedLicensesData.IsDetained(LicenseID);
        }
        public static int CreateDetainedLicenseAndGetID(int LicenseID, DateTime DetainDate, decimal FineFees
            , int CreatedByUserID, bool IsRelesed)
        {
            return DetainedLicensesData.CreateDetainedLicenseAndGetID(LicenseID, DetainDate, FineFees, CreatedByUserID, IsRelesed);
        }
        public static void GetDetainedLicenseInfo(int LicenseID, ref int DetainID, ref DateTime DetainDate,
        ref decimal FineFees, ref int CreatedByUserID)
        {
            DetainedLicensesData.GetDetainedLicenseInfo(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID);
        }

        public static bool ReleaseDetainedLicense(int DetainID, bool IsReleased, DateTime ReleaseDate,
            int ReleaseByUserID, int ReleaseApplicationID)
        {
            return DetainedLicensesData.ReleaseDetainedLicense(DetainID, IsReleased, ReleaseDate,
                ReleaseByUserID, ReleaseApplicationID);
        }

        public static DataTable GetAllDetainedDataInfo()
        {
            return DetainedLicensesData.GetAllDetainedDataInfo();
        }

        public static DataTable GetAllDriversInformationWithFiltration(string Filtre, string fl)
        {
            return DetainedLicensesData.GetAllDetainedLicensesInformationWithFiltration(Filtre, fl);
        }

        public static DataTable GetAllDriversInformationWithFiltration(string Filtre, int fl)
        {
            return DetainedLicensesData.GetAllDetainedLicensesInformationWithFiltration(Filtre, fl);
        }
    }
}
