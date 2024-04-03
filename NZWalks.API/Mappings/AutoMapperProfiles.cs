using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.Difficulty;
using NZWalks.API.Models.DTOs.Region;
using NZWalks.API.Models.DTOs.Walk;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<CreateRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<CreateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();

            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        }
    }
}
