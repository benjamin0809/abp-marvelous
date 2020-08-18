using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TalentMatrix.Org.Dto
{
    public class OrganizationDto
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string InnerCode { get; set; }

        public string Description { get; set; }
    }
}
