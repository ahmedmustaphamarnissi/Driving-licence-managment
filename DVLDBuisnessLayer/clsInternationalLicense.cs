using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsInternationalLicense
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;
        }

        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
           DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
        }
        public static DataTable GetAllInternationalLicensesByDriverID(int DriverID)
        {
            return InternationalLicensesData.GetAllInternationalLicensesByDriverID(DriverID);
        }
        public static bool HaveDriverAInternationalLicense(int DriverID)
        {
            return InternationalLicensesData.HaveDriverAInternationalLicense(DriverID);
        }
        public static int CreateInternationalLicenseAndGetID(int ApplicationID, int DriverID,
            int LocalDrivingLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            return InternationalLicensesData.CreateInternationalLicenseAndGetID(ApplicationID, DriverID, LocalDrivingLicenseID,
                IssueDate, ExpirationDate, IsActive, CreatedByUserID);
        }

        public static clsInternationalLicense GetInternationalLicenseInfoByID(ref int InternationalLicenseID)
        {
            int  ApplicationID = -1,IssuedUsingLocalLicenseID = -1, DriverID = -1, CreatedByUserID = -1;
            bool IsActive = false;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            InternationalLicensesData.GetInternationalLicenseInfoByID(ref InternationalLicenseID,
                ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate,
                ref ExpirationDate, ref IsActive, ref CreatedByUserID);
            if (InternationalLicenseID != -1)
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,
                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            return new clsInternationalLicense();
        }

        public static DataTable GetAllInternationalLicensesData()
        {
            return InternationalLicensesData.GetAllInternationalLicensesData();
        }

        public static DataTable GetAllInternationalLicensesInformationWithFiltration(string Filtre, int fl)
        {
            return InternationalLicensesData.GetAllInternationalLicensesInformationWithFiltration(Filtre, fl);
        }
    }
}
