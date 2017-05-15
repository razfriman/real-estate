using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class PropertyService
    {
        private readonly AppDbContext _dbContext;

        public PropertyService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Property>> Get()
        {
            return await this._dbContext.Properties
            .Include(x => x.Tenant)
            .ToListAsync();
        }

        public async Task<Property> GetByID(int id)
        {
            return await this._dbContext.Properties
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.PropertyID == id);
        }

        // public async Task<Property> Create(Tenant model)
        // {
        //     return null;
        // }

        // public async Task<Property> Update(int id, Property model)
        // {
        //     return null;
        // }

        // public async Task<Property> Delete(int id)
        // {
        //     return null;
        // }
    }
}
