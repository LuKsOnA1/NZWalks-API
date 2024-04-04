using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs.Walk
{
    public class CreateWalkRequestDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name has to be maximum 30 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be maximum 1000 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0,100)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
