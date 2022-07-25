using RestAPI.Settings;
using System.Data.SqlClient;

namespace RestAPI.Methods
{
    public static class GetData
    {
        public static int GetMovementId(int UserId)
        {
            int response = 0;
            SqlConnection conn = new(Database.ConnString);
            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.GetMovementId, conn);
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        response = reader.GetInt32(0);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally { conn.Close(); }


            return response;
        }

        public static int GetValue(int MovementId)
        {
            int response = 0;

            SqlConnection conn = new(Database.ConnString);



            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.GetValue, conn);
                command.Parameters.AddWithValue("@MovementId", MovementId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        response = reader.GetInt32(0);
                    }
                }
            }
            catch
            {

            }
            finally { conn.Close(); }

            return response;
        }

        public static int GetBalance(int MovementId)
        {
            int response = 0;

            SqlConnection conn = new(Database.ConnString);



            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.GetBalanceById, conn);
                command.Parameters.AddWithValue("@MovementId", MovementId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        response = reader.GetInt32(0);
                    }
                }
            }
            catch
            {

            }
            finally { conn.Close(); }

            return response;
        }

        public static string GetType(int MovementId)
        {
            string response = string.Empty;

            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.GetTypeById, conn);
                command.Parameters.AddWithValue("@MovementId", MovementId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        response = reader.GetString(0);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally { conn.Close(); }

            return response;
        }

        public static int UserId(int MovementId)
        {
            int response = 0;

            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.GetUserId, conn);
                command.Parameters.AddWithValue("@MovementId", MovementId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    response = reader.GetInt32(0);
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally { conn.Close(); }

            return response;
        }
    }
}
