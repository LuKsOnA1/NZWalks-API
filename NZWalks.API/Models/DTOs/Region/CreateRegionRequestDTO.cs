﻿namespace NZWalks.API.Models.DTOs.Region
{
    public class CreateRegionRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}