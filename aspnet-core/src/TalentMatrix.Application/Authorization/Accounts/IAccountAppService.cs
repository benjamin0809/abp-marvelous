using System.Threading.Tasks;
using Abp.Application.Services;
using TalentMatrix.Authorization.Accounts.Dto;

namespace TalentMatrix.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
