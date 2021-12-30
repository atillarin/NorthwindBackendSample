using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Dal.Abstract
{
    public interface IUnitOfWork:IDisposable  //bellek yönetimi için sınıf imhası
    {
        IGenericRepository<T> GetRepository<T>() where T : EntityBase;

        bool BeginTransaction();
        bool RollBackTransaction();
        int SaveChanges();

    }

}
