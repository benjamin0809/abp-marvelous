using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TalentMatrix.Mediae.Dto
{
    [AutoMapFrom(typeof(Media))]
    public class MediaDto: EntityDto<long>
    {
        public string Filename { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

        public MediaType Type { get; set; }

        public int Size { get; set; }

        public DateTime CreationTime { get; set; }
    }
}