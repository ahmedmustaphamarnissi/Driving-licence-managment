using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBuisnessLayer
{
    public class clsTestAppointments
    {
        public static DataTable GetTestAppointmentByApplicationID(int ApplicationID, int TestTypeID)
        {
            return TestAppointmentsData.GetTestAppointmentByApplicationID(ApplicationID, TestTypeID);
        }
        public static bool AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            return TestAppointmentsData.AddNewTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID
                , AppointmentDate, PaidFees, CreatedByUserID , IsLocked);
        }
        public static bool IsFailedByLocalDrivingLicenseID(int LocalDrivingLicenseApplicationID)
        {
            return TestAppointmentsData.IsFailedByLocalDrivingLicenseID(LocalDrivingLicenseApplicationID);
        }
        public static bool UpadateAppointmentDateByLDLAppID(int LocalDrivingLicenseApplicationID, DateTime AppointmentDate)
        {
            return TestAppointmentsData.UpadateAppointmentDateByLDLAppID(LocalDrivingLicenseApplicationID, AppointmentDate);
        }
        public static DateTime GetAppointmentDateByLDLAppID(int LocalDrivingLicenseApplicationID)
        {
            return TestAppointmentsData.GetAppointmentDateByLDLAppID(LocalDrivingLicenseApplicationID);
        }
        public static bool LockedAppointmentTest(int LocalDrivingLicenseApplicationID)
        {
            return TestAppointmentsData.LockedAppointmentTest(LocalDrivingLicenseApplicationID);
        }
    }
}
