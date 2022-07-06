using System.Data.SqlClient;

using RestAPI.Settings;

namespace RestAPI.Methods
{
    public static class Balances
    {
        public static int GetBalance(int UserId)
        {
            int response = 0;

            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.GetBalance, conn);
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

        public static bool HasBalance(int UserId)
        {
            bool response = false;

            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Balances.HasBalance, conn);
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    response = true;
                }

            }
            catch (SqlException)
            {
                throw;
            }
            finally { conn.Close(); }
            return response;
        }

        public static bool UpdateBalance( int MovementId, int UserId, int Balance)
        {
            bool response = false;

            SqlConnection conn = new(Database.ConnString);

            bool hasBalance = HasBalance(UserId);

            try
            {
                conn.Open();
                SqlCommand command = hasBalance ? new(Database.Queries.Balances.Update, conn) : new(Database.Queries.Balances.Insert, conn) ;
                command.Parameters.AddWithValue("@MovementId", MovementId);
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@Balance", Balance);
                int rows = command.ExecuteNonQuery();
                response = rows > 0;
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
