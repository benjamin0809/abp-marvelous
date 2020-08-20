using System.Threading.Tasks;

namespace TalentMatrix.Authentication.External
{
    public interface IExternalAuthProviderApi
    {
        ExternalLoginProviderInfo ProviderInfo { get; }

        Task<bool> IsValidUser(string userId, string staffNumber, string password);

        Task<ExternalAuthUserInfo> GetUserInfo(string staffNumber, string password);

        void Initialize(ExternalLoginProviderInfo providerInfo);
    }
}
