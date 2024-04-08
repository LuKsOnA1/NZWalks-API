using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.API.Abstract
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
