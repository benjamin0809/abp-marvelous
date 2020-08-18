using Abp.Application.Services.Dto;

namespace TalentMatrix.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

