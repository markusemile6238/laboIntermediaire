using Agency.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Domaine.Repositories
{
    public interface IDestinationRepo
    {
        // READ
        Task<IEnumerable<Destination>> GetAsync();
        Task<Destination>? GetByIdAsync(int id);
        Task<Destination>? FindByKeyword(string keyword);
        

        //WRITE
        Task<Destination> CreateAsync(Destination destination);
        Task<Destination> UpdateAsync(Destination destination);

        //DELETE
        Task<int> DeleteAsync(int id);
        
    }
}
