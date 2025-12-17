using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.ExceptionDAL
{
    public class RepositoryException: Exception
    {
        public string SqlQuery { get; }
        public int SqlErrorNumber { get; }
        public string Operation { get; }

        public RepositoryException()
        {
        }
        public RepositoryException(string message)
           : base(message) { }

        public RepositoryException(string message, Exception innerException)
         : base(message, innerException) { }

        public RepositoryException(string message, string sqlQuery, int sqlErrorNumber, Exception innerException)
            : base(message, innerException)
        {
            SqlQuery = sqlQuery;
            SqlErrorNumber = sqlErrorNumber;
        }

        public RepositoryException(string message, string operation, Exception innerException)
           : base(message, innerException)
        {
            Operation = operation;
        }




    }
}
