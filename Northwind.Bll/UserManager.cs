using Northwind.Bll.Base;
using Northwind.Dal.Abstract;
using Northwind.Entity.Dto;
using Northwind.Entity.Models;
using Nortwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Entity.IBase;
using Northwind.Entity.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Northwind.Bll
{
    public class UserManager : BllBase<User, DtoUser>, IUserService
    {
        public readonly IUserRepository userRepository;
        IConfiguration configuration;

        public UserManager(IServiceProvider service, IConfiguration configuration) : base(service)
        {
            userRepository = service.GetService<IUserRepository>();
            this.configuration = configuration;
        }



        public IResponse<DtoUserToken> Login(DtoLogin login)
        {
            // apiden gelecek dtologin user a maplenerek repodan sonuç alınır. şifreyi hashleyerek gönder
            login.Password = HashManager.MD5Hash(login.Password);
            var user= userRepository.Login(ObjectMapper.Mapper.Map<User>(login));
            

            if(user != null)
            {   // kayıt varsa user dtouser a, ordandan dtousertoken a
                var dtoLoginUser = ObjectMapper.Mapper.Map<DtoLoginUser>(user);
                var token = new TokenManager(configuration).CreateAccessToken(dtoLoginUser);
                var userToken = new DtoUserToken()
                {
                    DtoLoginUser = dtoLoginUser,
                    AccessToken =token
                };

                return new Response<DtoUserToken>
                {
                    Message = "Success",
                    StatusCode = StatusCodes.Status200OK,
                    Data = userToken
                };
            }
            else
            {
                return new Response<DtoUserToken>
                {
                    Message = "UserCode or Password is incorrect",
                    Data = null,
                    StatusCode = StatusCodes.Status406NotAcceptable
                };
            }
            
        }

        public IResponse RegisterUser(DtoRegisterUser dtoRegisterUser)
        {
                var user = ObjectMapper.Mapper.Map<User>(dtoRegisterUser);
                //usercode mevcutmu ?
                var userCode = userRepository.UserCodeCheck(user);

                if (userCode == null)
                {   //kayıtyoksa şifreyi hashle, gönder 
                    user.Password = HashManager.MD5Hash(dtoRegisterUser.Password);
                    userRepository.AddUser(user);
                    return new Response
                    {
                        Message = "The user has been registered.",
                        Data = null,
                        StatusCode = StatusCodes.Status200OK
                    };

                }
                else
                {
                    return new Response
                    {
                        Message = "Usercode already exists",
                        Data = null,
                        StatusCode = StatusCodes.Status406NotAcceptable
                    };
                }
        }
    }
}
