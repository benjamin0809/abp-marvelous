using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Newtonsoft.Json.Linq;


namespace TalentMatrix.Mediae.Dto
{
    public class GetAllMediaInputDto : PagedAndSortedResultRequestDto
    {
        private readonly JObject _allowSorts = new JObject()
        {
            { "creationTime", "CreationTime" },
            { "size", "Size" },
            { "description", "Description" }
        };

        public string Query { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        [Required]
        public int[] Type { get; set; }
        public string Sort { get; set; }


    }
}