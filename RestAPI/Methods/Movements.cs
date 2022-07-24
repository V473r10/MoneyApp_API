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
            bool response = false;
            SqlConnection conn = new(Database.ConnString);

            int balance = Balances.GetBalance(UserId);

            //switch (Type)
            //{
            //    case "Income":
            //        balance += Value;
            //        break;
            //    case "Expense":
            //        balance -= Value;
            //        break;   
            //}

            balance += Value;

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.InsertMovement, conn);
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@Wallet", Wallet);
                //command.Parameters.AddWithValue("@Type", Type);
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

        //public static int UpdateMovement(int MovementId, int Value)
        //{
        //    int response = 0;
        //    int diff;
        //    int newBalance = 0;
        //    int oldBalance = GetBalance(MovementId);
        //    string Type = GetType(MovementId);

        //    SqlConnection conn = new(Database.ConnString);
            
        //    int prevValue = GetValue(MovementId);

        //    diff = Value - prevValue;

        //    switch(Type)
        //    {
        //        case "Income":
        //            newBalance = oldBalance + diff;
        //            break;
        //        case "Expense":
        //            newBalance = oldBalance - diff;
        //            break;
                    
        //    }

        //    try
        //    {
        //        conn.Open();
        //        SqlCommand command = new(Database.Queries.Movements.UpdateMovement, conn);
        //        command.Parameters.AddWithValue("@MovementId", MovementId);
        //        command.Parameters.AddWithValue("@Type", Type);
        //        command.Parameters.AddWithValue("@Value", Value);
        //        command.Parameters.AddWithValue("@Balance", newBalance);
        //        response = command.ExecuteNonQuery();
        //        //if( response > 0)
        //        //{
        //        //    List<int> IdsToUpdate = GetIdsToUpdate(MovementId);
        //        //    string Ids =  IdsToUpdate.Select(list => list.ToString()).Aggregate((x, y) => x + "," + y);

        //        //    UpdateCascade(Ids);
        //        //}
                
        //    }
        //    catch
        //    {

        //    }
        //    finally { conn.Close(); }

        //    return response;
        //}
        
        public static int UpdateMovement(int MovementId, int Value)
        {
            int Response = 0;
            int Diff;
            int OldBalance = GetBalance(MovementId);
            string Type = GetType(MovementId);

            SqlConnection conn = new(Database.ConnString);
            
            int PrevValue = GetValue(MovementId);

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

        public static List<int> GetIdsToUpdate( int MovementId)
        {
            List<int> response = new();

            return response;
        }

        public static int UpdateCascade( string Ids)
        {
            int response = 0;

            SqlConnection conn = new(Database.ConnString);

            try
            {
                conn.Open();
                SqlCommand command = new(Database.Queries.Movements.UpdateCascade, conn);
                command.Parameters.AddWithValue("@MovementId", Ids);
                response = command.ExecuteNonQuery();
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


    }
}
