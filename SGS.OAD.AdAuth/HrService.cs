#if NET472 || NET48
using System.Data.SqlClient;
#else
using Microsoft.Data.SqlClient;
#endif

namespace SGS.OAD.AdAuth
{
    internal class HrService
    {
        private readonly string _connectionString;

        public HrService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 同步方法
        public string GetStaffCode(string adAccount)
        {
            string query = @"
                SELECT TOP 1 stf_code 
                FROM Employee (nolock) 
                WHERE ADAccount = @adAccount AND (QUITDATE = 0 OR QUITDATE >= @yyyymmdd)
            ";
            string? stfCode = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@adAccount", adAccount);
                        command.Parameters.AddWithValue("@yyyymmdd", DateTime.Now.ToString("yyyymmdd"));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stfCode = reader["stf_code"] as string;
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            return stfCode ?? "";
        }
    }
}
