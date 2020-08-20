using System.Threading.Tasks;

namespace TalentMatrix.Authentication.External
{
    public interface IExternalAuthManager
    {
        Task<bool> IsValidUser(string provider, string providerKey, string staffNumber, string password);

        Task<ExternalAuthUserInfo> GetUserInfo(string provider, string staffNumber, string password);
    }
}
