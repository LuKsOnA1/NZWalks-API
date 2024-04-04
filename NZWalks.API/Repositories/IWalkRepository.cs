using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filetrOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true);
        Task<Walk?> GetAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk model);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
