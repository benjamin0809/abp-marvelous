using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TalentMatrix.Mediae
{
    public class Media: CreationAuditedEntity<long>
    {
        public const int MaxFileNameLength = 32;
        public const int MaxDescriptionLength = 255;
        public const int MaxPathLength = 10;

        [Required]
        [MaxLength(MaxFileNameLength)]
        public string Filename { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(MaxPathLength)]
        public string Path { get; set; }

        [Required]
        [Range(0, 2)]
        [DefaultValue(0)]
        public MediaType Type { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Size { get; set; }
 
    }
}
