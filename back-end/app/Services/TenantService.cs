using System.Collections.Generic;
using System.Threading.Tasks;
using app.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class TenantService
    {
        private readonly AppDbContext _dbContext;

        public TenantService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tenant>> Get()
        {
            return await this._dbContext.Tenants
            .ToListAsync();
        }

        public async Task<Tenant> GetByID(int id)
        {
            return await this._dbContext.Tenants.FindAsync(id);
        }

        public async Task<IEnumerable<Tenant>> GetByUserID(int id)
        {
            return await this._dbContext.Users
            .Include(x => x.Properties)
            .ThenInclude(x => x.Tenant)
            .Where(x => x.UserID == id)
            .SelectMany(x => x.Properties)
            .Select(x => x.Tenant)
            .ToListAsync();
        }

        // public async Task<Tenant> Create(Tenant model)
        // {
        //     return null;
        // }

        // public async Task<Tenant> Update(int id, Tenant model)
        // {
        //     return null;
        // }

        // public async Task<Tenant> Delete(int id)
        // {
        //     return null;
        // }
    }
}
