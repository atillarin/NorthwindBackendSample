﻿using Northwind.Bll.Base;
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
    public class ShipperManager : BllBase<Shipper, DtoShipper>, IShipperService
    {
        public ShipperManager(IServiceProvider service):base(service)
        {

        }
    }
}
