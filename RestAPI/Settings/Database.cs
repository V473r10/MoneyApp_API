namespace RestAPI.Settings
{
    public static class Database
    {

        private static string connString = "Data Source = MSI; Initial Catalog = MoneyApp; User = sa; Password = v473r10";

        public static string ConnString { get => connString; set => connString = value; }

        public static class Queries
        {

            public static class Users
            {

                private static readonly string login     = "SELECT * FROM Users WHERE Username = @Username AND Pass = @Password";

                private static readonly string signup    = "INSERT INTO Users (Username, Email, Pass) VALUES (@Username, @Email, @Pass)";

                private static readonly string update    = @"UPDATE Users SET Email = @Email WHERE Username = @Username
                                                             UPDATE UsersDetails SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Country = @Country, Estate =  @Estate WHERE Username = @Username ";

                private static readonly string delete    = @"DELETE Users WHERE Username = @Username
                                                             DELETE UserDetails WHERE Username = @Username";

                private static readonly string getEmail = @"SELECT Email FROM Users WHERE Username = @Username";

                public static string Login      { get => login; }
                public static string Signup     { get => signup; }
                public static string Update     { get => update; }
                public static string Delete     { get => delete; }
                public static string GetEmail { get => getEmail; }

            }

            public static class Movements
            {
                private static readonly string getMovementId = @"SELECT TOP 1 Id FROM Movements WHERE UserId = @UserId ORDER BY Id DESC";

                private static readonly string insertMovement = @"INSERT INTO Movements SELECT @UserId, @Wallet, @Type, @Value, @Balance, GETDATE()";

                private static readonly string getBalance = @"SELECT TOP 1 Balance FROM Movements WHERE UserId = @UserId ORDER BY Date DESC";
                public static string GetMovementId { get => getMovementId; }
                public static string InsertMovement { get => insertMovement; }
                public static string GetBalance { get => getBalance; }
            }

            public static class Balances
            {
                private static readonly string hasBalance = @"SELECT * FROM Balances WHERE UserId = @UserId";

                private static readonly string insert = @"INSERT INTO Balances SELECT @MovementId, @UserId, @Balance";

                private static readonly string update = @"UPDATE Balances SET Movement = @MovementId, Balance = @Balance WHERE UserId = @UserId";

                public static string HasBalance { get => hasBalance; }
                public static string Update { get => update; }
                public static string Insert { get => insert; }
            }

        }        

    }
}
