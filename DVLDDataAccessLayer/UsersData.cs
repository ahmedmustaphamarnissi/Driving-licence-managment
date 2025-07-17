using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class UsersData
    {
        public static bool IsExist(ref int ID,string UserName, string Password)
        {

            
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select IsActive , UserID From Users
                          Where UserName=@UserName and Password = @Password ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            bool IsActive = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    IsActive = (bool)reader["IsActive"];
                    ID = (int)reader["UserID"];

                }
                else
                {
                    IsActive = false;

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsActive = false;

            }
            finally
            {
                connection.Close();

            }


            return IsActive == true;
        }

        public static int GetPersonIDByUserID(int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select PersonID from Users
                             where UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID",UserID);

            int PersonID = -1;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                }
            }
            catch(Exception ex)
            {
                PersonID = -1;
            }
            finally
            {
                connection.Close();
            }
            return PersonID;

            
        }
        public static bool GetUserInformation(int UserID,ref int PersonID,ref string UserName,ref string Password,ref bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select * From Users
                          Where UserID=@UserID ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            bool IsFound = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    IsActive = (bool)reader["IsActive"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsFound = true;
                }
                else
                {
                    IsFound = false;

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;

            }
            finally
            {
                connection.Close();

            }


            return IsFound == true;
        }
        public static bool GetUserInformationByPersonID(ref int UserID,  int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select * From Users
                          Where PersonID=@PersonID ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            bool IsFound = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    IsActive = (bool)reader["IsActive"];
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsFound = true;
                }
                else
                {
                    IsFound = false;

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;

            }
            finally
            {
                connection.Close();

            }


            return IsFound == true;
        }

        public static DataTable GetAllUsersInformation()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Users.UserID,Users.PersonID,FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName
                           ,Users.UserName,IsActive from Users inner join People on People.PersonID=Users.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable DT=new DataTable();
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

        public static DataTable GetAllUsersInformationWithFiltration(string Filtre, string fl)
        {
            
            List<string> allowedFilters = new List<string> { "UserID", "UserName", "IsActive", "FirstName", "SecondName", "ThirdName", "LastName" };

            

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            
            string query = $@"
        SELECT Users.UserID, Users.PersonID,
               FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName,
               Users.UserName, IsActive
        FROM Users
        INNER JOIN People ON People.PersonID = Users.PersonID
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


        public static DataTable GetAllUsersInformationWithFiltration(string Filtre, int fl)
        {
            
            List<string> allowedFilters = new List<string> { "UserID", "PersonID", "IsActive" };

            

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"
        SELECT Users.UserID, Users.PersonID,
               FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName,
               Users.UserName, IsActive
        FROM Users
        INNER JOIN People ON People.PersonID = Users.PersonID
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

        public static bool AddNewUserInDataBase(int PersonID,string UserName,string Password,bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into Users(PersonID,UserName,Password,IsActive)values
                              (@PersonID,@UserName,@Password,@IsActive)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            int rows = 0;
            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();

            }
            catch (Exception ex){
                rows = 0;
            }
            finally
            {
                connection.Close();
            }
            return rows > 0;
        }

        public static bool DeleteUserFromDataBase(int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            int rows = 0;
            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                rows = 0;
            }
            finally
            {
                connection.Close();
            }
            return rows > 0;
        }

        public static string GetCurrentPassword(int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select Password from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            string Password = "";
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Password = (string)reader["Password"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Password = "";
            }
            finally
            {
                connection.Close();
            }
            return Password;
        }
        public static bool UpdateUserPassword(int UserID,string NewPassword)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Users 
                            set Password=@Password
                            where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", NewPassword);

            int rows = 0;
            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                rows = 0;
            }
            finally
            {
                connection.Close();
            }
            return rows > 0;
        }

        public static bool UpdateUserInformationFromDataBase(int UserID,int PersonID,string UserName, string Password,bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Users 
                            set Password=@Password,
                            PersonID=@PersonID,
                            UserName=@UserName,
                            IsActive=@IsActive
                            where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            int rows = 0;
            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                rows = 0;
            }
            finally
            {
                connection.Close();
            }
            return rows > 0;
        }
    }
}
