using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data.API;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.API.Abstract;

namespace NZWalks.API.Repositories.API.Concrete
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLRegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region model)
        {
            await _context.Regions.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region model)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            region.Code = model.Code;
            region.Name = model.Name;
            region.RegionImageUrl = model.RegionImageUrl;

            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }
    }
}
