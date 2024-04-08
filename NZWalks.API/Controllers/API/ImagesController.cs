using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.Image;
using NZWalks.API.Repositories.API.Abstract;

namespace NZWalks.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly IImageRepository _repository;

        public ImagesController(IImageRepository repository)
        {
            _repository = repository;
        }



        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO model)
        {
            ValidateFileUpload(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageDomainModel = new Image
            {
                File = model.File,
                FileExtension = Path.GetExtension(model.File.FileName),
                FileSizeInBytes = model.File.Length,
                FileName = model.FileName,
                FileDescription = model.FileDescription,
            };

            await _repository.Upload(imageDomainModel);

            return Ok(imageDomainModel);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO model)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg" };

            if (!allowedExtensions.Contains(Path.GetExtension(model.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension!");
            }

            if (model.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Only files with size 10MB or less is supported, please upload smaller size file");
            }
        }
    }
}
