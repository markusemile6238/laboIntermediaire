using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Database
{
    public interface IDbCommandFactory
    {
        SqlCommand CreateCommand(string  commandText, SqlConnection connection);
        SqlCommand CreateParameterizedCommand(string  commandText, SqlConnection connection, params SqlParameter[] param);
        SqlCommand CreateStoredProcedure(string procedureName, SqlConnection connection);
        SqlCommand CreateParameterizedStoredProcedured(string procedureName, SqlConnection connection, params SqlParameter[] parameters);

    }
}
