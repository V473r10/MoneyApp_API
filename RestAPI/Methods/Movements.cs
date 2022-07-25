using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

using RestAPI.Settings;
using RestAPI.Methods;

namespace RestAPI.Methods
{
    public static class Movements
    {
        
        public static bool Insert( int UserId, string Wallet, string Type, int Value)
        {
            bool Response = false;
            SqlConnection conn = new(Database.ConnString);

            int Balance = Balances.GetBalance(UserId);

            switch (Type)
            {
                case "Income":
                    Balance += Value;
                    break;
                case "Expense":
                    Balance -= Value;
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
                command.Parameters.AddWithValue("@Balance", Balance);

                int Rows = command.ExecuteNonQuery();
                Response = Rows > 0;
                if (Response)
                {
                    int MovementId = GetData.GetMovementId(UserId);
                    Balances.UpdateBalance(MovementId, UserId, Wallet, Balance);
                }
                
            }
            catch(SqlException)
            {
                throw;
            }
            finally 
            {
                conn.Close();                
            }


            return Response;
        }
                
        public static int UpdateMovement(int MovementId, int Value)
        {
            int Response = 0;
            int Diff;
            int OldBalance = GetData.GetBalance(MovementId);
            string Type = GetData.GetType(MovementId);

            SqlConnection conn = new(Database.ConnString);
            
            int PrevValue = GetData.GetValue(MovementId);

            Diff = Value - PrevValue;

            Diff = Type == "Expense" ? Diff * (-1) : Diff;

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.UpdateMovement, conn);
                command.Parameters.AddWithValue("@Id", MovementId);
                command.Parameters.AddWithValue("@Value", Value);
                command.Parameters.AddWithValue("@Diff", Diff);
                Response = command.ExecuteNonQuery();
                
            }
            catch
            {

            }
            finally { conn.Close(); }

            return Response;
        }
        
    }
}
