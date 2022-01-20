using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        //[AllowAnonymous] kimlik denetimi yapma
        public IResponse<DtoUserToken> Login(DtoLogin login)
        {
            try
            {
                return userService.Login(login);
            }
            catch (Exception ex)
            {

                return new Response<DtoUserToken>
                {
                    Message= $"Error{ex.Message}",
                    Data = null,
                    StatusCode= StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpPost("RegisterUser")]
        public IResponse RegisterUser(DtoRegisterUser dtoRegisterUser)
        {
            try
            {
                return userService.RegisterUser(dtoRegisterUser);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = $"Error:{ex.Message}",
                    Data = null,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            
        }

    }
}
