using Microsoft.Data.SqlClient;

namespace Volunteering.Helpers
{
    public static class ConnectionHealth
    {
        public static bool CheckConnectionHealth(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open(); 
                    return true; 
                }
            }
            catch
            {
                return false; 
            }
        }
    }
}
