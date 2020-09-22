using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TalentMatrix.Mediae.Dto
{
    [AutoMapTo(typeof(Media))]
    public class UpdateMediaInputDto : EntityDto<long>
    {
        [MaxLength(Media.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}