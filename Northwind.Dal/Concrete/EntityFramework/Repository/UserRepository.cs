using Microsoft.EntityFrameworkCore;
using Northwind.Dal.Abstract;
using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Dal.Concrete.EntityFramework.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) :base(context)
        {

        }

        public User Login(User user)
        {
            var _user = dbset.FirstOrDefault(x => x.UserCode == user.UserCode && x.Password == user.Password);

            return _user;
        }

        public User UserCodeCheck(User user)
        {
            var _user = dbset.FirstOrDefault(x => x.UserCode == user.UserCode);
            
            return _user;
        }
        public void AddUser(User user)
        {
            dbset.Add(user);
            context.SaveChanges();
        }
    }
}
