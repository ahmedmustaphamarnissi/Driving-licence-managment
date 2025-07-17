using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class DriversData
    {
        public static int AddNewDriver(int PersonID,int UserID,DateTime IssueDate)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into Drivers(PersonID,CreatedByUserID,CreatedDate)
values(@PersonID,@CreatedByUserID,@IssueDate)
select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", UserID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            
            int DriverID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DriverID = Convert.ToInt32(reader[0]);
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
            return DriverID;
        }

        public static int GetDriverIDByPersonID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select DriverID from Drivers where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            
            int DriverID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DriverID = Convert.ToInt32(reader[0]);
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
            return DriverID;
        }

        public static DataTable GetAllDriversInformation()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "select * from Drivers_View";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllDriversInformationWithFiltration(string Filtre, string fl)
        {

            List<string> allowedFilters = new List<string> { "NationalNo", "FullName" };



            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = $@"select * from Drivers_View
        WHERE {Filtre} LIKE @fl + '%'";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@fl", fl);

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static DataTable GetAllDriversInformationWithFiltration(string Filtre, int fl)
        {

            List<string> allowedFilters = new List<string> { "DriverID", "PersonID" };



            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"select * from Drivers_View
        WHERE {Filtre} = @fl";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@fl", fl);

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}
