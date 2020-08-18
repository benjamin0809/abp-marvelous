using Abp.Application.Services;
using TalentMatrix.MultiTenancy.Dto;

namespace TalentMatrix.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

