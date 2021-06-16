using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SiMPAC.Data
{
    public interface IConnection
    {
        SqlConnection Open();

        SqlConnection Fetch();

        void Close();
    }
}
