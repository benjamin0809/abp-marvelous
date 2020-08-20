using System.Threading.Tasks;
using Abp.Dependency;

namespace TalentMatrix.Authentication.External
{
    public abstract class ExternalAuthProviderApiBase : IExternalAuthProviderApi, ITransientDependency
    {
        public ExternalLoginProviderInfo ProviderInfo { get; set; }

        public void Initialize(ExternalLoginProviderInfo providerInfo)
        {
            ProviderInfo = providerInfo;
        }

        public async Task<bool> IsValidUser(string userId, string staffNumber, string password)
        {
            var userInfo = await GetUserInfo(staffNumber, password);
            return userInfo.ProviderKey == userId;
        }


        public abstract Task<ExternalAuthUserInfo> GetUserInfo(string staffNumber, string password);
    }
}
