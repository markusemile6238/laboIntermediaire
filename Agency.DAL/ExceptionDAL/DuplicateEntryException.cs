using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.ExceptionDAL
{
    public class DuplicateEntryException : Exception
    {
        public string Entity { get; }
        public string Field { get; }
        public string Value { get; }

        public DuplicateEntryException(string entity, string field, string value)
            : base($"{entity} avec {field}:'{value}' existe déjà")
        {
            Entity = entity;
            Field = field;
            Value = value;
        }
        public DuplicateEntryException(string entity, string field, string value, Exception innerException)
            : base($"{entity} avec {field}:'{value}' existe déjà.",innerException)
        {
            Entity = entity;
            Field = field;
            Value = value;
        }
    }
}
