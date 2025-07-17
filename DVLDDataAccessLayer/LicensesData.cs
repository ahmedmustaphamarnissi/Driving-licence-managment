using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class LicensesData
    {
        public static bool IssueLocalLicense(int ApplicationID,int DriverID,int LicenseClass,
            DateTime IssueDate,DateTime ExpirationDate,string Notes,decimal PaidFees,bool IsActive,
            int IssusReason,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into Licenses (ApplicationID,DriverID,LicenseClass,IssueDate,
ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID) values (@ApplicationID,@DriverID
,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssusReason,@CreatedByUserID)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssusReason", IssusReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
        public static void GetLicenseInfo(ref int ApplicationID,ref int LicenseID,ref int DriverID,ref string ClassName
            ,ref DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes,ref string IsActive,
            ref string IssueReason,ref string FullName,
            ref string NationalNo,ref string Gendor , ref DateTime DateOfBirth,ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT 
    Licenses.LicenseID,
    Licenses.DriverID,
    LicenseClasses.ClassName,
    Licenses.IssueDate,
    Licenses.ExpirationDate,
    
    CASE 
        WHEN Licenses.Notes IS NULL THEN 'No Notes'
        ELSE Licenses.Notes 
    END AS Notes,
    
    CASE
        WHEN Licenses.IsActive = 'True' THEN 'Yes'
        ELSE 'No' 
    END AS IsActive,
    
    CASE
        WHEN Licenses.IssueReason = 1 THEN 'New'
        WHEN Licenses.IssueReason = 2 THEN 'Renew'
        WHEN Licenses.IssueReason = 3 THEN 'Replacement For Damage'
        ELSE 'Replacement For Lost'
    END AS IssueReason,
    
    FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName,
    People.NationalNo,People.ImagePath,case
	when People.Gendor = 0 then 'Male'
	else 'Female'
	end as Gendor,
    People.DateOfBirth

FROM Licenses
INNER JOIN LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
INNER JOIN Drivers ON Drivers.DriverID = Licenses.DriverID
INNER JOIN People ON People.PersonID = Drivers.PersonID
WHERE Licenses.ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DriverID= Convert.ToInt32(reader["DriverID"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ClassName = (string)reader["ClassName"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = (string)reader["Notes"];
                    IsActive = (string)reader["IsActive"];
                    IssueReason = (string)reader["IssueReason"];
                    FullName = (string)reader["FullName"];
                    NationalNo = (string)reader["NationalNo"];
                    Gendor = (string)reader["Gendor"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    ImagePath = reader["ImagePath"] == DBNull.Value? "UnknownImagePath": (string)reader["ImagePath"];


                }
            }
            catch(Exception ex)
            {
                ApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }
            
        }

        public static void GetLicenseInformation(ref int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClass,
         ref int IssueReason, ref int CreatedByUserID, ref string Notes,
         ref DateTime IssueDate, ref DateTime ExpirationDate, ref decimal PaidFees, ref bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT * from Licenses
                           WHERE LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    LicenseClass = (int)reader["LicenseClass"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = (string)reader["Notes"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = Convert.ToInt32(reader["IssueReason"]);
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    CreatedByUserID= Convert.ToInt32(reader["CreatedByUserID"]);


                }
            }
            catch (Exception ex)
            {
                LicenseID = -1;
            }
            finally
            {
                connection.Close();
            }

        }
        public static DataTable GetAllLocalLicensesByDriverID(int DriverID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT 
    Licenses.LicenseID,Licenses.ApplicationID,
    LicenseClasses.ClassName,
    Licenses.IssueDate,
    Licenses.ExpirationDate,Licenses.IsActive FROM Licenses
INNER JOIN LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
WHERE Licenses.DriverID = @DriverID;";

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
        public static int GetApplicationIDWithLicenseID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select ApplicationID from Licenses where LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            int ApplicationID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
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
            return ApplicationID;
        }

        public static int GetLicenseIDWithApplicationID(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select LicenseID from Licenses where ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            int LicenseID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
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
            return LicenseID;
        }
        public static bool UnActivateLicenseWithID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update Licenses set IsActive=0
                             where LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
            return rows>0;
        }
        public static bool ActivateLicenseWithID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update Licenses set IsActive=1
                             where LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
        public static bool IsLocalLicenseActive(int LocalDrivingLicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select IsActive from Licenses 
                            where LicenseID=@LocalDrivingLicenseID and LicenseClass=3";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseID", LocalDrivingLicenseID);

            bool IsActive = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsActive = (bool)reader["IsActive"];
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return IsActive;
        }

        public static bool IsExpirationLicense(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select Found=1  from Licenses
                            where ApplicationID=@ApplicationID and GetDate()>ExpirationDate";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            bool IsExpiration = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsExpiration =true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return IsExpiration;
        }

        public static int GetLicenseClassID(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select LicenseClass  from Licenses
                            where LicenseID=@LicenseID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            int LicenseClassID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseClassID = Convert.ToInt32(reader["LicenseClass"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return LicenseClassID;
        }
    }
}
