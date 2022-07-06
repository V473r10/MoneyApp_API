using RestAPI.Models;
using RestAPI.Security;
using System.Data.SqlClient;

using RestAPI.Settings;

namespace RestAPI.Methods
{
    public class Users
    {
        public static bool Login(string Username, string Password)
        {
            bool result = false;
            
            string hash = Encrypt.GetSHA256(Password); 

            SqlConnection conn = new(Database.ConnString);
            try
            {
                conn.Open();  
                SqlCommand command = new(Database.Queries.Users.Login, conn);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", hash);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = true;
                }

            }
            catch (SqlException)
            {
                throw;
            }
            finally { conn.Close(); }
            return result;
        }

        public static bool Signup(string Username, string Email, string Password)
        {
            bool result = false;
            string hash = Encrypt.GetSHA256(Password);

            SqlConnection conn = new(Database.ConnString);
            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Users.Signup, conn);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Pass", hash);
                command.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException)    
            {

            }
            finally { conn.Close(); }
            return result;
        }
        public static bool Delete(string Username)
        {
            bool result = false;
            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Users.Delete, conn);
                command.Parameters.AddWithValue("@Username", Username);
                command.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally { conn.Close(); }
            return result;

        }

        public static EmailUser GetEmail(string Username)
        {
            EmailUser emailUser = new();

            SqlConnection conn = new(Database.ConnString);
            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Users.GetEmail, conn);
                command.Parameters.AddWithValue("@Username", Username);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    emailUser.Email = reader.GetString(0);
                }
            } 
            catch (SqlException)
            {

            }
            finally { conn.Close(); }
            return emailUser;
        }

        public static bool UpdateUser(string Username, string FirstName, string LastName, string Email, string Phone, string Country, string Estate)
        {
            bool result = false;
            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Users.Update, conn);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Phone", Phone);
                command.Parameters.AddWithValue("@Country", Country);
                command.Parameters.AddWithValue("@Estate", Estate);
                command.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally { conn.Close(); }
            return result;
        }
    }
}
