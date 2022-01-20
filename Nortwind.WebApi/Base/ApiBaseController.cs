using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Entity.Base;
using Northwind.Entity.IBase;
using Nortwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nortwind.WebApi.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class ApiBaseController<TInterface, T, TDto> : ControllerBase where TInterface : IGenericService<T, TDto> where T : EntityBase where TDto : DtoBase
    {
        private readonly TInterface service;

        public ApiBaseController(TInterface service)
        {
            this.service = service;
        }


        [HttpGet("Find")]
        public IResponse<TDto> Find(int id)
        {
            try
            {
                return service.Find(id);
            }
            catch (Exception ex)
            {
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error{ex.Message}",
                    Data = null
                };
            }
        }


        [HttpGet("GetAll")]
        public IResponse<List<TDto>> GetAll()
        {
            try
            {
                return service.GetAll();
            }
            catch (Exception ex)
            {
                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }


    }
}
