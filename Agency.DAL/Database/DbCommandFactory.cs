using Microsoft.Data.SqlClient;
using System.Data;

namespace Agency.DAL.Database
{
    public class DbCommandFactory : IDbCommandFactory
    {
        public SqlCommand CreateCommand(string commandText, SqlConnection connection)
        {
            ValidateConnection(connection);

            return new SqlCommand(commandText, connection)
            {
                CommandType = CommandType.Text,
                CommandTimeout = 30
            };
        }

        public SqlCommand CreateParameterizedCommand(string commandText,SqlConnection connection, params SqlParameter[] parameters)
        {
            var command = CreateCommand(commandText, connection);
            command.Parameters.AddRange(parameters);
            return command;
        }


        public SqlCommand CreateStoredProcedure(string procedureName, SqlConnection connection)
        {
            ValidateConnection(connection);

            return new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 30
            };
        }
        public SqlCommand CreateParameterizedStoredProcedured(string procedureName,SqlConnection connection, params SqlParameter[] parameters)
        {
            var command = CreateStoredProcedure(procedureName, connection);
            command.Parameters.AddRange(parameters);
            return command;
        }



        private void ValidateConnection(SqlConnection connection)
        {
            if(connection == null) throw new ArgumentNullException(nameof(connection));
            if (connection.State != ConnectionState.Open) throw new InvalidOperationException("Connection fermé ! Veuillez d'abord ouvrir la connection");
        }
    }
}
