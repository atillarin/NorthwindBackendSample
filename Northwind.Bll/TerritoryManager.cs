using Northwind.Bll.Base;
using Northwind.Entity.Dto;
using Northwind.Entity.Models;
using Nortwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Bll
{
    public class TerritoryManager : BllBase<Territory, DtoTerritory>, ITerritoryService
    {
        public TerritoryManager(IServiceProvider service):base(service)
        {

        }
    }
}
