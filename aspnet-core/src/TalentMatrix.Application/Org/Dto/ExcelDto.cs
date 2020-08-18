using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TalentMatrix.Org.Dto
{
    public class ExcelDto

    {
        [Required]
        public string Title { get; set; }
        public string Name { get; set; }
        public string Staffno { get; set; }
        public float Hours { get; set; }
    }
}
