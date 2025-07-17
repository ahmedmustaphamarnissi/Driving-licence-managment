using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsApplication
    {
        public int _ApplicationID { get; set; }
        public int _ApplicantID { get; set; }
        public DateTime _ApplicationDate { get; set; }
        public int _ApplicationTypeID { get; set; }
        public int _ApplicationStatus { get; set; }
        public DateTime _LastStatusDate { get; set; }
        public decimal _PaidFees { get; set; }
        public int _CreatedBy { get; set; }

        public clsApplication()
        {
            _ApplicationID = -1;
            _ApplicantID = -1;
            _ApplicationDate = DateTime.Now;
            _ApplicationTypeID = -1;
            _ApplicationStatus = -1;
            _LastStatusDate = DateTime.Now;
            _PaidFees = -1;
            _CreatedBy = -1;
        }
        public clsApplication(int ApplicationID,int ApplicantID,DateTime ApplicationDate,int ApplicationTypeID,
            int ApplicationStatus,DateTime LastStatusDate, decimal PaidFees,int UserID)
        {
            _ApplicationID = ApplicationID;
            _ApplicantID = ApplicantID;
            _ApplicationDate = ApplicationDate;
            _ApplicationTypeID = ApplicationTypeID;
            _ApplicationStatus = ApplicationStatus;
            _LastStatusDate = LastStatusDate;
            _PaidFees = PaidFees;
            _CreatedBy = UserID;
        }

        public static int CreateApplicationAndGetID(int ApplicantPersonID, DateTime ApplicationDate,
    int ApplicationTypeID, decimal PaidFees, int CreatedByUserID)
        {
            return ApplicationsData.CreateApplicationAndGetID(ApplicantPersonID, ApplicationDate, ApplicationTypeID, PaidFees, CreatedByUserID);
        }

        public static bool CancelApplicationByID(int ApplicationID)
        {
            return ApplicationsData.CancelApplication(ApplicationID);
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            return ApplicationsData.DeleteApplicationFromDataBase(ApplicationID);
        }
        public static clsApplication GetApplicationInformationByID(int ApplicationID)
        {
            DataTable DT = ApplicationsData.GetApplicationDataByID(ApplicationID);

            if (DT != null && DT.Rows.Count > 0)
            {
                DataRow row = DT.Rows[0];

                return new clsApplication(
                    Convert.ToInt32(row["ApplicationID"]),
                    Convert.ToInt32(row["ApplicantPersonID"]),
                    Convert.ToDateTime(row["ApplicationDate"]),
                    Convert.ToInt32(row["ApplicationTypeID"]),
                    Convert.ToInt32(row["ApplicationStatus"]),
                    Convert.ToDateTime(row["LastStatusDate"]),
                    Convert.ToDecimal(row["PaidFees"]),
                    Convert.ToInt32(row["CreatedByUserID"])
                );
            }

            return new clsApplication();
        }
        public static bool CompleteApplication(int ApplicationID)
        {
            return ApplicationsData.CompleteApplication(ApplicationID);
        }
    }
}
