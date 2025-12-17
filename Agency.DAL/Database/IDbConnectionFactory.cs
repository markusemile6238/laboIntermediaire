using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Database
{
    public interface IDbConnectionFactory
    {
        SqlConnection CreateConnection();
        Task<SqlConnection> CreateConnectionAsync();
    }
}
