using System.Data.SqlClient;

using RestAPI.Settings;
using RestAPI.Methods;

namespace RestAPI.Methods
{
    public static class Movements
    {
        public static bool Insert( int UserId, string Wallet, string Type, int Value)
        {
            bool response = false;
            SqlConnection conn = new(Database.ConnString);

            int balance = Balances.GetBalance(UserId);

            switch (Type)
            {
                case "Income":
                    balance += Value;
                    break;
                case "Expense":
                    balance -= Value;
                    break;   
            }
            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.InsertMovement, conn);
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@Wallet", Wallet);
                command.Parameters.AddWithValue("@Type", Type);
                command.Parameters.AddWithValue("@Value", Value);
                command.Parameters.AddWithValue("@Balance", balance);

                int rows = command.ExecuteNonQuery();
                response = rows > 0;
                
                
            }
            catch(SqlException)
            {
                throw;
            }
            finally 
            {
                conn.Close();
                int MovementId = GetMovementId(UserId);
                Balances.UpdateBalance(MovementId, UserId, balance);

            }


            return response;
        }

        public static int GetMovementId( int UserId)
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
        
    }
}
