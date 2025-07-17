using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class PeopleData
    {
        public static DataTable GetPeopleData()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,
                             case 
                             when People.Gendor=0 then 'Male'
                             else 'Female'
                             end as Gendor
                             ,DateOfBirth,Nationality = Countries.CountryName,Phone,
                              case 
                              when People.Email is null then 'unknon'
                              else People.Email
                              end
                              as Email from People
                              inner join Countries on Countries.CountryID=People.NationalityCountryID";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable GetPeopleDataByFiltre(string Filtre)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $@"select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,
                             case 
                             when People.Gendor=0 then 'Male'
                             else 'Female'
                             end as Gendor
                             ,DateOfBirth,Nationality = Countries.CountryName,Phone,
                              case 
                              when People.Email is null then 'unknon'
                              else People.Email
                              end
                              as Email from People
                              inner join Countries on Countries.CountryID=People.NationalityCountryID
                              order by {Filtre}";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Filtre", Filtre);
            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable GetPeopleDataByStartWith(string Filtre, string Text)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,
                             case 
                             when People.Gendor=0 then 'Male'
                             else 'Female'
                             end as Gendor
                             ,DateOfBirth,Nationality = Countries.CountryName,Phone,
                              case 
                              when People.Email is null then 'unknon'
                              else People.Email
                              end
                              as Email from People
                              inner join Countries on Countries.CountryID=People.NationalityCountryID
                              where {Filtre} LIKE @Text + '%'
                              ORDER BY {Filtre}";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Text", Text);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool IsPersonExistWithNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select Found=1 from People where NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            int Found = 0;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Found = (int)reader["Found"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Found = 0;
            }
            finally
            {
                connection.Close();
            }
            return Found == 1;
        }

        public static DataTable GetAllCountrieNames()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select CountryName from Countries";

            SqlCommand command = new SqlCommand(query, connection);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static bool AddPersonToDatabase(string NationalNo, string FirstName, string SecondName,
    string ThirdName, string LastName, string Phone, DateTime DateOfBirth, int Gendor,
    string Address, string Email, int NationalityCountryID, string ImagePath)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            INSERT INTO People (
                NationalNo, FirstName, SecondName, ThirdName, LastName,
                Phone, DateOfBirth, Gendor, Address, Email,
                NationalityCountryID, ImagePath
            ) VALUES (
                @NationalNo, @FirstName, @SecondName, @ThirdName, @LastName,
                @Phone, @DateOfBirth, @Gendor, @Address, @Email,
                @NationalityCountryID, @ImagePath
            )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);

                    try
                    {
                        connection.Open();
                        int rows = command.ExecuteNonQuery();
                        return rows > 0;
                    }
                    catch (Exception ex)
                    {
                        // Optional: Log the error or show a message
                        // MessageBox.Show("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static void GetPersonInformationByPersonID(ref int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
   ref string ThirdName, ref string LastName, ref string Phone, ref DateTime DateOfBirth, ref int Gendor,
   ref string Address, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from People where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ID = (int)reader["PersonID"];
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Phone = (string)reader["Phone"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "Without Email";
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "UnknownImagePath"; // or a default value
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ID = -1;
            }
            finally
            {
                connection.Close();
            }

        }

        public static void GetPersonInformationByNationalNo(ref int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
   ref string ThirdName, ref string LastName, ref string Phone, ref DateTime DateOfBirth, ref int Gendor,
   ref string Address, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from People where NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ID = (int)reader["PersonID"];
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Phone = (string)reader["Phone"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "Without Email";
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "UnknownImagePath"; // or a default value
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ID = -1;
            }
            finally
            {
                connection.Close();
            }

        }

        public static bool UpdatePersonInfo(int ID, string NationalNo, string FirstName, string SecondName,
     string ThirdName, string LastName, string Phone, DateTime DateOfBirth, int Gendor,
     string Address, string Email, int NationalityCountryID, string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                UPDATE People
                SET 
                NationalNo = @NationalNo,FirstName = @FirstName,SecondName = @SecondName,
                ThirdName = @ThirdName,LastName = @LastName,Phone = @Phone,DateOfBirth = @DateOfBirth,
                Gendor = @Gendor,Address = @Address,Email = @Email,
                NationalityCountryID = @NationalityCountryID,ImagePath = @ImagePath
                WHERE PersonID = @PersonID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                // Optional: Log the error or show a message
                // MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            

        }

        public static bool DeletePersonFromDataBase(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                DELETE FROM People
                WHERE PersonID = @PersonID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            

            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
        public static string GetCountryNameUsingID(int CountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                select CountryName from Countries
                WHERE CountryID = @CountryID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            string Name="";
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Name = (string)reader["CountryName"];
                }
            }
            catch (Exception ex)
            {

                Name = "";
            }
            finally
            {
                connection.Close();
            }
            return Name;
        }

        public static int GetLastPersonID()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select top 1 PersonID from People 
                             order by PersonID DESC";


            SqlCommand command = new SqlCommand(query, connection);

            int ID = -1;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ID = (int)reader["PersonID"];
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
            return ID;
        } 

        public static string GetFullNameByID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select FullName=FirstName+' '+SecondName+' '+ThirdName,+' ' + LastName from People
                            where PersonID=@PersonID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            string FullName = "";

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    FullName = (string)reader["FullName"];
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
            return FullName;
        }
    }
    
}
