using Northwind.Entity.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nortwind.Interface  // Bll deki işlemlerin interface leri buarada olacak
{
    public interface IGenericService<T,TDto>where T:IEntityBase where TDto:IDtoBase
    {
        List<TDto> GetAll();

        List<TDto> GetAll(Expression<Func<T, bool>> expression);

        TDto Find(int id);

        IQueryable<T> GetIQuaryable();

        TDto Add(TDto item);
        Task<TDto> AddAsync(TDto item);

        TDto Update(TDto item);
        Task<TDto> UpdateAsync(TDto item);

        bool DeleteById(int id);
        Task<bool> DeleteByIdAsync(int id);

        bool Delete(TDto item);
        Task<bool> DeleteAsync(TDto item);



        

    }
}
