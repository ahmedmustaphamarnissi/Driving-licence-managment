using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class ApplicationsData
    {
        public static int CreateApplicationAndGetID(int ApplicantPersonID,DateTime ApplicationDate,
            int ApplicationTypeID,decimal PaidFees,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into Applications(ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                           ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID) values
(@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,1,@ApplicationDate,@PaidFees,@CreatedByUserID)
select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            int ApplicationID=-1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader[0]);
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
            return ApplicationID;
        }
        public static bool CancelApplication(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            DateTime Date = DateTime.Now;
            string query = @"Update Applications set ApplicationStatus=2 , LastStatusDate=@Date
                            where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Date", Date);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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

        public static bool CompleteApplication(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            DateTime Date = DateTime.Now;
            string query = @"Update Applications set ApplicationStatus=3 , LastStatusDate=@Date
                            where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Date", Date);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        public static bool DeleteApplicationFromDataBase(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete from LocalDrivingLicenseApplications
                            where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            int rows = 0;
            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    rows = 0;
                    query = @"Delete from Applications
                            where ApplicationID = @ApplicationID";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    rows = command.ExecuteNonQuery();
                }
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
        
        public static DataTable GetApplicationDataByID(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from Applications
                            where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            DataTable DT= new DataTable();
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
        
        
    }
}
