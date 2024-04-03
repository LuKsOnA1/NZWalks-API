using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLWalkRepository(ApplicationDbContext context)
        {
            _context = context;
        }




        public async Task<List<Walk>> GetAllAsync()
        {
            return await _context.Walks
                .Include("Difficulty")
                .Include("Region")
                .ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            return await _context.Walks
                    .Include("Difficulty")
                    .Include("Region")
                    .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk model)
        {
            var walk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }

            walk.Name = model.Name;
            walk.Description = model.Description;
            walk.LengthInKm = model.LengthInKm;
            walk.WalkImageUrl = model.WalkImageUrl;
            walk.DifficultyId = model.DifficultyId;
            walk.RegionId = model.RegionId;

            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }

            _context.Walks.Remove(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

    }
}
