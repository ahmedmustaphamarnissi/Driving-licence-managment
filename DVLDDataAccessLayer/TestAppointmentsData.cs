using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class TestAppointmentsData
    {
        public static DataTable GetTestAppointmentByApplicationID(int ApplicationID,int TestTypeID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select TestAppointments.TestAppointmentID , TestAppointments.AppointmentDate,TestAppointments.PaidFees,TestAppointments.IsLocked
from TestAppointments inner join LocalDrivingLicenseApplications on 
LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=TestAppointments.LocalDrivingLicenseApplicationID
inner join Applications on Applications.ApplicationID= LocalDrivingLicenseApplications.ApplicationID
where TestAppointments.TestTypeID=@TestTypeID and Applications.ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            DataTable DT = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DT.Load(reader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return DT;
        }

        public static bool AddNewTestAppointment(int TestTypeID,int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate , decimal PaidFees,int CreatedByUserID,bool IsLocked)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,
AppointmentDate,PaidFees,CreatedByUserID,IsLocked) values (@TestTypeID,@LocalDrivingLicenseApplicationID,
@AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            int rows = 0;
            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return rows > 0;
        }
        public static bool IsFailedByLocalDrivingLicenseID(int localDrivingLicenseApplicationID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT 1 
            FROM TestAppointments 
            INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
            WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
              AND Tests.TestResult = 0"; // 0 = failed

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return reader.Read(); // If a row is found, return true
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the error here
                        throw new Exception("Error checking failed test.", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }



        public static bool UpadateAppointmentDateByLDLAppID(int LocalDrivingLicenseApplicationID,DateTime AppointmentDate)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update TestAppointments set AppointmentDate=@AppointmentDate
where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

            int rows = 0;

            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return rows >0;
        }
        public static DateTime GetAppointmentDateByLDLAppID(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select AppointmentDate from TestAppointments 
                       where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            DateTime Date = DateTime.Now;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Date = (DateTime)reader["AppointmentDate"];
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Date;
        }
        public static bool LockedAppointmentTest(int LocalDrivingLicenseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update TestAppointments set IsLocked=@IsLocked
where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            bool IsLocked = true;
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            int rows = 0;

            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return rows > 0;
        }
    }
}
