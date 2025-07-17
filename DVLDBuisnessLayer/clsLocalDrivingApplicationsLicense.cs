using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsLocalDrivingApplicationsLicense
    {
        public int _DLAppID { get; set; }
        public clsApplication _Application { get; set; }
        public int _ClassID { get; set; }

        private clsLocalDrivingApplicationsLicense()
        {
            _ClassID = -1;
            _DLAppID = -1;
            _Application = null;

        }
        public clsLocalDrivingApplicationsLicense(int ApplicationID,
            int LicenseClassID, int LocalDrivingLicenseApplicationID)
        {
            _ClassID = LicenseClassID;
            _DLAppID = LocalDrivingLicenseApplicationID;
            _Application = clsApplication.GetApplicationInformationByID(ApplicationID);

        }
        public static bool IsExistSameLocalDrivingLicense(int PersonID, int LicenseClassID)
        {
            return LocalDrivingLicenseApplicationsData.IsExistSameLocalDrivingLicense(PersonID, LicenseClassID);
        }
        public static bool AddNewLocalDrivingLicense(int ApplicationID, int LicenseClassID)
        {
            return LocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicense(ApplicationID, LicenseClassID);
        }

        public static DataTable GetManageLocalDrivingLicenseApplications()
        {
            return LocalDrivingLicenseApplicationsData.GetAllDataTestsAndInformation();
        }
        public static DataTable FilterLocalDrivingByString(string filterName, string filterValue)
        {
            return LocalDrivingLicenseApplicationsData.FilterLocalDrivingByString(filterName, filterValue);
        }
        public static DataTable FilterLocalDrivingByInt(string filterName, int filterValue)
        {
            return LocalDrivingLicenseApplicationsData.FilterLocalDrivingByInt(filterName, filterValue);
        }
        public static DataTable FilterLocalDrivingByStatus( string status)
        {
            return LocalDrivingLicenseApplicationsData.FilterLocalDrivingByStatus( status);
        }
        public static clsLocalDrivingApplicationsLicense GetLocalGrivingLicenseApplicationByApplicationID(int ApplicationID)
        {
            int ClassID = -1, DLAppID = -1;
            LocalDrivingLicenseApplicationsData.GetLocalGrivingLicenseApplicationByApplicationID(ApplicationID, ref ClassID,ref DLAppID);
            if(ClassID!=-1 && DLAppID != -1)
            {
                return new clsLocalDrivingApplicationsLicense(ApplicationID, ClassID, DLAppID);
            }
            return new clsLocalDrivingApplicationsLicense();
        }
        public static string GetLicenseClassNameByAppID(int ID)
        {
            return LocalDrivingLicenseApplicationsData.GetLicenseClassNameByAppID(ID);
        }
        public static int GetPassedTestsByApplicationID(int ApplicationID)
        {
            return LocalDrivingLicenseApplicationsData.GetPassedTestsByApplicationID(ApplicationID);
        }

        public static int GetLicenseClassIDByAppID(int ID)
        {
            return LocalDrivingLicenseApplicationsData.GetLicenseClassIDByAppID(ID);
        }
        public static bool UpdateLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            return LocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication(ApplicationID, LicenseClassID);
        }
    }
}
