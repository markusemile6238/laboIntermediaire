using Agency.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Domaine.Repositories
{
    public interface IRepoBase<TEntity , Tk> where TEntity : class
    {
        // READ
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity>? GetByIdAsync(Tk id);

        //WRITE
        Task<TEntity> CreateAsync(TEntity destination);
        Task<TEntity> UpdateAsync(TEntity destination);

        //DELETE
        Task<int> DeleteAsync(Tk id);
    }
}
