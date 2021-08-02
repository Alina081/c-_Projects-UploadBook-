using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace UploadBook
{
    class DataAccess : IDisposable
    {
        public static SqlConnection connection;
        static SqlCommand command;

        public DataAccess()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["upld"].ConnectionString.ToString());
            connection.Open();
        }

        public int UpdateDB(string sql)
        {
            connection.Close();
            connection.Open();
            command = new SqlCommand(sql, connection);
            return command.ExecuteNonQuery();
        }

        public SqlDataReader GetData(string sql)
        {
            connection.Close();
            connection.Open();
            command = new SqlCommand(sql, connection);
            return command.ExecuteReader();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
