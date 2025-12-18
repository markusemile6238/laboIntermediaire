using Agency.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.BLL.Services
{
     public interface IBaseService<TEntity, Tk>
    {

        //READ
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity>? GetByIdAsyn(Tk id);


        // CREATE
        Task<TEntity> CreateAsync(TEntity destination);


        //DELETE
        Task<int> DeleteAsynch(Tk id);
    }
}
