﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Entity.Dto;
using Northwind.Entity.Models;
using Nortwind.Interface;
using Nortwind.WebApi.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nortwind.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDemographicController : ApiBaseController<ICustomerDemographicService, CustomerDemographic, DtoCustomerDemographic>
    {
        public CustomerDemographicController(ICustomerDemographicService service):base (service)
        {

        }
    }
}
