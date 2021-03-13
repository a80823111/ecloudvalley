using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Linq;
using System.Net.Sockets;

namespace Repository.Connection
{
    public class SqlServerConnection
    {
        private readonly IDbConnection _sqlConn;
        public SqlServerConnection(IDbConnection sqlConn)
        {
            _sqlConn = sqlConn;
        }

        public IDbConnection Database(string sqlConnectionString)
        {
            //var privateIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            //sqlConnectionString = String.Format(sqlConnectionString, privateIp);

            if (_sqlConn.State != ConnectionState.Open)
            {
                _sqlConn.ConnectionString = sqlConnectionString;
                _sqlConn.Open();
            }
            return _sqlConn;
        }
    }
}
