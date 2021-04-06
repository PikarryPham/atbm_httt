using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace atbm
{
    class DBUtils
    {
        public static OracleConnection GetDBConnection()
        {

            string host = "DESKTOP-ED2S73F";
            int port = 1521;
            string sid = "XE";
            string user = "c##administrator";
            string password = "addm";

            return DBOracleUtils.GetDBConnection(host, port, sid, user, password);
        }
        

    }
}
