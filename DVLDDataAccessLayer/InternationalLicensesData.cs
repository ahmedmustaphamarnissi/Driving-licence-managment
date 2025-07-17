using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class InternationalLicensesData
    {
        public static DataTable GetAllInternationalLicensesByDriverID(int DriverID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT 
    InternationalLicenseID,ApplicationID,IssueDate,ExpirationDate,IsActive FROM InternationalLicenses
           WHERE DriverID = @DriverID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            DataTable DT = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DT.Load(reader);
                }
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

        public static bool HaveDriverAInternationalLicense(int DriverID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            DateTime Date = DateTime.Now;
            string query = @"SELECT Found =1 from InternationalLicenses
           WHERE DriverID = @DriverID and ExpirationDate>@Date;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@Date", Date);

            bool HaveInternational = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    HaveInternational = true;
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
            return HaveInternational ;
        }

        public static int CreateInternationalLicenseAndGetID(int ApplicationID,int DriverID,
            int LocalDrivingLicenseID,DateTime IssueDate,DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into InternationalLicenses(ApplicationID,DriverID,IssuedUsingLocalLicenseID,
IssueDate,ExpirationDate,IsActive,CreatedByUserID) values (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,
@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID) select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", LocalDrivingLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            int InternationalID = -1;
            bool HaveInternational = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    InternationalID = Convert.ToInt32(reader[0]);
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
            return InternationalID;

        }
        public static void GetInternationalLicenseInfoByID(ref int InternationalLicenseID,
            ref int ApplicationID,ref int DriverID,ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate
            ,ref DateTime ExpirationDate,ref bool IsActive,ref int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from InternationalLicenses 
                            where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                }
                else
                {
                    InternationalLicenseID = -1;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                InternationalLicenseID = -1;
            }
            finally { connection.Close(); }
        }
        public static DataTable GetAllInternationalLicensesData()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT InternationalLicenseID,ApplicationID,DriverID,
IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive FROM InternationalLicenses";

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

        public static DataTable GetAllInternationalLicensesInformationWithFiltration(string Filtre, int fl)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"select InternationalLicenseID,ApplicationID,DriverID,
IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive from InternationalLicenses
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
