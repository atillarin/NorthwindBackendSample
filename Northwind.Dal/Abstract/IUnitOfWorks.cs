using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Dal.Abstract
{
    public interface IUnitOfWorks:IDisposable  //bellek yönetimi için sınıf imhası
    {
        // repository pattern gelecek


        bool BeginTransaction();
        bool RollBackTransaction();
        int SaveChanges();

    }

}
