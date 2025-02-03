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
                WHERE ADAccount = @adAccount AND (QUITDATE = 0 OR QUITDATE >= @today)
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
                        command.Parameters.AddWithValue("@today", DateTime.Now.ToString("yyyyMMdd"));
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

        public HrInfoModel GetHrInfo(string adAccount)
        {
            string query = @"
                SELECT TOP 1 stf_code, stf_cname, TitleName, QUITDATE, JOBSTATUS, JOBSTATUS_Text
                FROM Employee (nolock)
                WHERE ADAccount = @adAccount AND (QUITDATE = 0 OR QUITDATE >= @today)
            ";
            HrInfoModel? hrInfo = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@adAccount", adAccount);
                        command.Parameters.AddWithValue("@today", DateTime.Now.ToString("yyyyMMdd"));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hrInfo = new HrInfoModel
                                {
                                    stf_code = reader["stf_code"] as string,
                                    stf_cname = reader["stf_cname"] as string,
                                    TitleName = reader["TitleName"] as string,
                                    QUITDATE = GetDateTime(reader["QUITDATE"]),
                                    JOBSTATUS = reader["JOBSTATUS"] as string,
                                    JOBSTATUS_Text = reader["JOBSTATUS_Text"] as string
                                };
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            return hrInfo ?? new HrInfoModel();
        }

        private static DateTime? GetDateTime(object val)
        {
            if (val != DBNull.Value && Convert.ToInt32(val) != 0)
            {
                return DateTime.ParseExact(val.ToString(), "yyyyMMdd", null);
            }
            return null;
        }
    }
}
