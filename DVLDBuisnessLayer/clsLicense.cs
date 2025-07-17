using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsLicense
    {
        public int ApplicationID { get; set; }
        public int LicenseID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }
        public string Notes { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }

        private clsLicense(int ApplicationID, int LicenseID, int DriverID,int  LicenseClass,
            int IssueReason, int CreatedByUserID,string Notes,
        DateTime IssueDate, DateTime ExpirationDate,decimal PaidFees,bool IsActive)
        {
            this.ApplicationID = ApplicationID;
            this.LicenseID = LicenseID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.Notes = Notes;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
        }
        public clsLicense()
        {
            this.ApplicationID = -1;
            this.LicenseID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueReason = -1;
            this.CreatedByUserID = -1;
            this.Notes = "Notes";
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.PaidFees = 0;
            this.IsActive = false;
        }
        public static bool IssueLocalLicense(int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive,
            int IssusReason, int CreatedByUserID)
        {
            return LicensesData.IssueLocalLicense(ApplicationID, DriverID, LicenseClass, IssueDate, 
                ExpirationDate, Notes, PaidFees, IsActive, IssusReason, CreatedByUserID);
        }

        public static void GetLicenseInfo(ref int ApplicationID, ref int LicenseID, ref int DriverID, ref string ClassName
            , ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref string IsActive,
            ref string IssueReason, ref string FullName,
            ref string NationalNo, ref string Gendor, ref DateTime DateOfBirth, ref string ImagePath)
        {
            LicensesData.GetLicenseInfo(ref ApplicationID, ref LicenseID, ref DriverID, ref ClassName, ref IssueDate
                , ref ExpirationDate, ref Notes, ref IsActive, ref IssueReason, ref FullName, ref NationalNo
                , ref Gendor, ref DateOfBirth,ref ImagePath);
        }
        public static clsLicense GetLicenseInfo(int LicenseID)
        {
            int ApplicationID=-1,  DriverID=-1, LicenseClass=-1, IssueReason=-1, CreatedByUserID=-1;
            string Notes="";
            DateTime IssueDate=DateTime.Now, ExpirationDate=DateTime.Now;
            decimal PaidFees=0;
            bool IsActive=false;

            LicensesData.GetLicenseInformation(ref ApplicationID, ref LicenseID, ref DriverID,
                ref LicenseClass, ref IssueReason, ref CreatedByUserID, ref Notes, ref IssueDate,
                ref ExpirationDate, ref PaidFees, ref IsActive);
            if (LicenseID != -1)
                return new clsLicense(ApplicationID, LicenseID, DriverID,
                 LicenseClass, IssueReason, CreatedByUserID, Notes, IssueDate,
                 ExpirationDate, PaidFees, IsActive);

            return new clsLicense();
        }
        public static DataTable GetAllLocalLicensesByDriverID(int DriverID)
        {
            return LicensesData.GetAllLocalLicensesByDriverID(DriverID);
        }
        public static int GetApplicationIDWithLicenseID(int LicenseID)
        {
            return LicensesData.GetApplicationIDWithLicenseID(LicenseID);
        }
        public static bool IsLocalLicenseActive(int LocalDrivingLicenseID)
        {
            return LicensesData.IsLocalLicenseActive(LocalDrivingLicenseID);
        }
        public static bool IsExpirationLicense(int ApplicationID)
        {
            return LicensesData.IsExpirationLicense(ApplicationID);
        }

        public static int GetLicenseClassID(int LicenseID)
        {
            return LicensesData.GetLicenseClassID(LicenseID);
        }

        public static int GetLicenseIDWithApplicationID(int ApplicationID)
        {
            return LicensesData.GetLicenseIDWithApplicationID(ApplicationID);
        }

        public static bool UnActivateLicenseWithID(int LicenseID)
        {
            return LicensesData.UnActivateLicenseWithID(LicenseID);
        }
        public static bool ActivateLicenseWithID(int LicenseID)
        {
            return LicensesData.ActivateLicenseWithID(LicenseID);
        }
        
    }
}
