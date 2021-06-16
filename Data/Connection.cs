using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SiMPAC.Data
{
    public class Connection : IConnection
    {
        private SqlConnection conn;

        public Connection()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SiMPaC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public void close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public SqlConnection Fetch()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                return conn;
            }
            else
            {
                return this.Open();
            }
        }

        public SqlConnection Open()
        {
            conn.Open();
            return conn;
        }
    }
}
