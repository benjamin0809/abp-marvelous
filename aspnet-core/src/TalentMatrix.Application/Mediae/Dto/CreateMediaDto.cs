using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TalentMatrix.Mediae.Dto
{
    public class CreateMediaDto
    {
        [Required]
        public IFormFile File { get; set; }
    }
}