using Agency.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.BLL.Services
{
    public interface IDestinationService
    {

        //READ
        Task<IEnumerable<Destination>> GetAsync();
        Task<Destination>? GetByIdAsyn(int id);


        // CREATE
        Task<Destination> CreateAsync(Destination destination);

    }
}
