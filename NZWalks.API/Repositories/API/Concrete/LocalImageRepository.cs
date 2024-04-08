using NZWalks.API.Data.API;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.API.Abstract;

namespace NZWalks.API.Repositories.API.Concrete
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _context;

        public LocalImageRepository(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor, ApplicationDbContext context)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_environment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // Upload image
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            
            image.FilePath = urlFilePath;

            // Add Image to the Images table
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
