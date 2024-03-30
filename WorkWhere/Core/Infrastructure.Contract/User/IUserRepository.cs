using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Contract.User
{
    public interface IUserRepository <User>
    {
        Task<User> GetUserByIdWithRoleAsync(int userId, string role);
    }
}
