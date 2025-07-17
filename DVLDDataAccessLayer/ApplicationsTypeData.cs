using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class ApplicationsTypeData
    {
        public static DataTable GetAllApplicationsTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from ApplicationTypes";

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
        public static bool GetApplicationTypeByID(int ApplicationTypeID,ref string ApplicationTypeName,ref decimal ApplicationFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ApplicationFees,ApplicationTypeTitle from ApplicationTypes where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            bool IsExist = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                float money = 0;
                if (reader.Read())
                {
                    ApplicationFees =(decimal) reader["ApplicationFees"];
                    ApplicationTypeName = (string)reader["ApplicationTypeTitle"];
                    IsExist = true;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                IsExist = false;
            }
            finally{
                connection.Close();
            }
            return IsExist;
        }

        public static bool GetApplicationTypeByID(int ApplicationTypeID, ref string ApplicationTypeName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ApplicationTypeTitle from ApplicationTypes where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            bool IsExist = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                float money = 0;
                if (reader.Read())
                {
                    ApplicationTypeName = (string)reader["ApplicationTypeTitle"];
                    IsExist = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsExist = false;
            }
            finally
            {
                connection.Close();
            }
            return IsExist;
        }
        public static bool UpdateApplicationTypeByID(int ApplicationTypeID,  string ApplicationTypeName, decimal ApplicationFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update ApplicationTypes  set ApplicationTypeTitle=@ApplicationTypeName,ApplicationFees=@ApplicationFees
                            where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeName", ApplicationTypeName);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

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
        public static decimal GetApplicationTypeFees(int ApplicationTypeID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ApplicationFees from ApplicationTypes where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            
            decimal ApplicationFees = 0;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                
                if (reader.Read())
                {
                    ApplicationFees = (decimal)reader["ApplicationFees"];
     
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
            return ApplicationFees;
        }
    }
}
