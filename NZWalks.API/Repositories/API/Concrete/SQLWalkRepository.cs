using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data.API;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.API.Abstract;

namespace NZWalks.API.Repositories.API.Concrete
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLWalkRepository(ApplicationDbContext context)
        {
            _context = context;
        }




        public async Task<List<Walk>> GetAllAsync(string? filetrOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var walks = _context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filetrOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filetrOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

                if (filetrOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }

                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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
