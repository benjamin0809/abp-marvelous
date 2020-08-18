using System.ComponentModel.DataAnnotations;

namespace TalentMatrix.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}