using Core.Entities;
using Core.Infrastructure.Contract.User;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context) { }


        //public async Task<User?> GetUserByIdWithRoleAsync(int userId, string role)
        //{
        //     return await _context.Users
        //    .Where(u => u.Id == userId && u.Roles.Any(r => r.Name == role))
        //    .FirstOrDefaultAsync();
        //}

        public async Task<User?> GetUserByIdWithRoleAsync(int id, string roleName)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id && u.Roles.Any(r => r.Name == roleName));
            return user;
        }
    }
}
