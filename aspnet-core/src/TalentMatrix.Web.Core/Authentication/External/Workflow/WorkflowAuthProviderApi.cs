using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TalentMatrix.Authentication.External.Workflow
{
    public class WorkflowAuthProviderApi : ExternalAuthProviderApiBase
    {
        public override async Task<ExternalAuthUserInfo> GetUserInfo(string staffNumber, string password)
        {
            string clientId = ProviderInfo.ClientId;
            string clientSecret = ProviderInfo.ClientSecret;
            return new WorkflowAuthUserInfo
            {
                EmailAddress = "894306909@qq.com",
                Name = staffNumber,
                Provider = ProviderInfo.Name,
                ProviderKey = ProviderInfo.ClientId,
                Surname = "Chiu",
                WorkflowToken = "go"
            };
        }
    }
}
