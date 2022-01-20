using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Entity.Base;
using Northwind.Entity.Dto;
using Northwind.Entity.IBase;
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
    public class CustomerController : ApiBaseController<ICustomerService,Customer,DtoCustomer>
    {
        private readonly ICustomerService service;
        public CustomerController(ICustomerService service):base(service)
        {
            this.service = service;
        }

        [HttpGet("FindByStringId")]
        public  IResponse<DtoCustomer> Find(string id)
        {
            try
            {
                return service.Find(id);
            }
            catch (Exception ex)
            {
                return new Response<DtoCustomer>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error{ex.Message}",
                    Data = null
                };
            }
        }

    }
}
