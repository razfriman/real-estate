using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Data;
using System.Collections.Generic;

namespace app.Services
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await this._dbContext.Users
            .Include(x => x.Properties)
            .ThenInclude(x => x.Tenant)
            .ToListAsync();
        }

        public async Task<User> GetByID(int id)
        {
            return await this._dbContext.Users
            .Include(x => x.Properties)
            .ThenInclude(x => x.Tenant)
            .SingleOrDefaultAsync(x => x.UserID == id);
        }

        // public async Task<User> Create(User model)
        // {
        //     return null;
        // }

        // public async Task<User> Update(int id, User model)
        // {
        //     return null;
        // }

        // public async Task<User> Delete(int id)
        // {
        //     return null;
        // }
    }
}
