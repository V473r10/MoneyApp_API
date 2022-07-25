using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

using RestAPI.Settings;
using RestAPI.Methods;

namespace RestAPI.Methods
{
    public static class Wallets
    {
        public static bool CreateWallet(int UserId, string Wallet, int Balance)
        {
            bool Response = false;

            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Wallets.CreateWallet, conn);
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@Wallet", Wallet);
                command.Parameters.AddWithValue("@Value", Balance);
                command.Parameters.AddWithValue("@Balance", Balance);
                int rows = command.ExecuteNonQuery();
                Response = rows > 0;
                if (Response)
                {
                    int MovementId = GetData.GetMovementId(UserId);
                    Balances.UpdateBalance(MovementId, UserId, Wallet, Balance);
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return Response;
        }
    }
}
