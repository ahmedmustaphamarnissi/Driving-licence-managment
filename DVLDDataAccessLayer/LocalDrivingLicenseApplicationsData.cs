using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class LocalDrivingLicenseApplicationsData
    {
        public static bool IsExistSameLocalDrivingLicense(int PersonID, int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from LocalDrivingLicenseApplications inner join Applications
                         on LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
                         where Applications.ApplicationStatus=1 and Applications.ApplicantPersonID=@PersonID
                         and LocalDrivingLicenseApplications.LicenseClassID=@LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            int found = 0;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    found = (int)reader["found"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return found == 1;
        }
        public static bool AddNewLocalDrivingLicense(int ApplicationID, int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into LocalDrivingLicenseApplications(ApplicationID,LicenseClassID)
                            values (@ApplicationID,@LicenseClassID)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            int Rows = 0;
            try
            {
                connection.Open();
                Rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Rows > 0;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update LocalDrivingLicenseApplications set LicenseClassID=@LicenseClassID
                            where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            int Rows = 0;
            try
            {
                connection.Open();
                Rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Rows > 0;
        }
        public static DataTable GetAllDataTestsAndInformation()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT 
    Applications.ApplicationID,
    LicenseClasses.ClassName,
    People.NationalNo,
    FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName,
    
    PassedTest = (
        SELECT COUNT(*) 
        FROM TestAppointments 
        INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
        INNER JOIN LocalDrivingLicenseApplications L2 
            ON L2.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
        WHERE 
            Tests.TestResult = 'True' AND 
            L2.ApplicationID = Applications.ApplicationID
    ),

    CASE 
        WHEN Applications.ApplicationStatus = 1 THEN 'New'
        WHEN Applications.ApplicationStatus = 2 THEN 'Canceled'
        WHEN Applications.ApplicationStatus = 3 THEN 'Completed'
        ELSE 'Unknown'
    END AS ApplicationStatus

FROM 
    Applications
INNER JOIN 
    People ON Applications.ApplicantPersonID = People.PersonID
INNER JOIN 
    LocalDrivingLicenseApplications L1 ON L1.ApplicationID = Applications.ApplicationID
INNER JOIN 
    LicenseClasses ON LicenseClasses.LicenseClassID = L1.LicenseClassID;";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable DT = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DT.Load(reader);
                }
                reader.Close();
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

        public static DataTable FilterLocalDrivingByString(string filterName, string filterValue)
        {
            string query = $@"SELECT * FROM LocalDrivingLicenseView WHERE {filterName} LIKE @Filter";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Filter", filterValue + "%");

                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                }

                return dt;
            }
        }

        public static DataTable FilterLocalDrivingByInt(string filterName, int filterValue)
        {
            string query = $@"SELECT * FROM LocalDrivingLicenseView WHERE {filterName} = @Filter";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Filter", filterValue);

                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                }

                return dt;
            }
        }

        public static DataTable FilterLocalDrivingByStatus(string status)
        {
            string query = @"SELECT * FROM LocalDrivingLicenseView WHERE ApplicationStatus = @Status";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Status", status);

                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                }

                return dt;
            }
        }
        public static void GetLocalGrivingLicenseApplicationByApplicationID(int ApplicationID,
            ref int LicenseClassID, ref int LocalDrivingLicenseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from LocalDrivingLicenseApplications 
                            where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

        }
        public static string GetLicenseClassNameByAppID(int ID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select LicenseClasses.ClassName from LicenseClasses inner join LocalDrivingLicenseApplications
                   on LocalDrivingLicenseApplications.LicenseClassID=LicenseClasses.LicenseClassID
                     where LocalDrivingLicenseApplications.ApplicationID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            string ClassName = "";
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            ClassName = (string)reader["ClassName"];
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connection.Close();
                    }
            return ClassName;
        }

        public static int GetLicenseClassIDByAppID(int ID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select LicenseClassID from LocalDrivingLicenseApplications
                     where ApplicationID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            int ClassID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ClassID = Convert.ToInt32(reader["LicenseClassID"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ClassID;
        }

        public static int GetPassedTestsByApplicationID(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT PassedTest= COUNT(*) 
FROM TestAppointments 
INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
INNER JOIN LocalDrivingLicenseApplications L2 
    ON L2.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
WHERE 
    Tests.TestResult = 'True' AND 
    L2.ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            int PassedTest = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PassedTest = Convert.ToInt32(reader["PassedTest"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return PassedTest;
        }

        
    }
}
    

