using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.ExceptionDAL
{
    public class NotFoundException : Exception
    {
        public string EntityName { get; }
        public String EntityId { get; }

        public NotFoundException(string entityName, string entityId)
            : base($"Pas de {entityName} trouvé(e) ! Details : Id:{entityId}.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException)
       : base(message, innerException) { }

    }
}
