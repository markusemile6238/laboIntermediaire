using Agency.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Domaine.Repositories
{
    public interface IDestinationRepo : IRepoBase<Destination,int>
    {
        // READ
        Task<Destination>? FindByKeyword(string keyword);
       
        
    }
}
