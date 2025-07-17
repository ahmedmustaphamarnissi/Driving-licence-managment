using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class DetainedLicensesData
    {
        public static string IsDetained(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select top 1  IsDetained='Yes' from DetainedLicenses
where LicenseID=@LicenseID and IsReleased=0 order by DetainID DESC";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            string IsDetained = "No";
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsDetained = "Yes";
                }
            }
            catch (Exception ex){
                IsDetained = "Error";

            }
            finally
            {
                connection.Close();
            }
            return IsDetained;
        }
        public static int CreateDetainedLicenseAndGetID(int LicenseID,DateTime DetainDate,decimal FineFees
            ,int CreatedByUserID,bool IsReleased)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into DetainedLicenses(LicenseID,DetainDate,FineFees,CreatedByUserID
,IsReleased)values(@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased)
select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);


            int DetainID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DetainID = Convert.ToInt32(reader[0]);
                }
                reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return DetainID;
        }
        public static void GetDetainedLicenseInfo(int LicenseID,ref int DetainID, ref DateTime DetainDate,
            ref decimal FineFees,ref  int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select top 1 DetainDate,FineFees,CreatedByUserID,DetainID 
            from DetainedLicenses where LicenseID=@LicenseID
			order by DetainDate desc";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToDecimal(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                }
                else
                {
                    LicenseID = -1;
                }
                reader.Close();
            }
            catch (Exception ex){

            }
            finally
            {
                connection.Close();
            }
            
        }

        public static bool ReleaseDetainedLicense(int DetainID,bool IsReleased,DateTime ReleaseDate,
            int ReleaseByUserID,int ReleaseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update DetainedLicenses set IsReleased=@IsReleased,ReleaseDate=@ReleaseDate,
ReleasedByUserID=@ReleaseByUserID,ReleaseApplicationID=@ReleaseApplicationID
where DetainID=@DetainID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleaseByUserID", ReleaseByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

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
        
        public static DataTable GetAllDetainedDataInfo()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from  DetainedLicenses_View ";
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
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return DT;
        }

        public static DataTable GetAllDetainedLicensesInformationWithFiltration(string Filtre, string fl)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"select * from DetainedLicenses_View
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


        public static DataTable GetAllDetainedLicensesInformationWithFiltration(string Filtre, int fl)
        {

            
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"select * from DetainedLicenses_View
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
