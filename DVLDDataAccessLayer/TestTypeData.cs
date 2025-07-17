using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class TestTypeData
    {
        public static DataTable GetAllTestTypes()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from TestTypes";

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
        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle,ref string Description, ref decimal TestFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from TestTypes where TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            bool IsExist = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                
                if (reader.Read())
                {
                    TestFees = (decimal)reader["TestTypeFees"];
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    Description = (string)reader["TestTypeDescription"];
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
        public static decimal GetTestFeesByTestID(int TestID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select TestTypeFees from TestTypes where TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestID);

            decimal Fees = 0;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    Fees = Convert.ToDecimal(reader["TestTypeFees"]);
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
            return Fees;
        }
        public static bool UpdateTestTypeByID(int TestTypeID, string TestTypeTitle, string Description, decimal TestFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestTypes  set TestTypeTitle=@TestTypeTitle,TestTypeFees=@TestTypeFees
                          ,TestTypeDescription=@TestTypeDescription
                            where TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@TestTypeFees", TestFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
