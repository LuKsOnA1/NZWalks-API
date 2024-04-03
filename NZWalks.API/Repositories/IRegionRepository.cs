using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetAsync(Guid id);
        Task<Region> CreateAsync(Region model);
        Task<Region?> UpdateAsync(Guid id, Region model);
        Task<Region?> DeleteAsync(Guid id);
    }
}
