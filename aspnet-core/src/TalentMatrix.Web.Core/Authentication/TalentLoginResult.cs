using Abp.Authorization;
using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using TalentMatrix.Authorization.Users;
using TalentMatrix.MultiTenancy;

namespace TalentMatrix.Authentication
{
    public class TalentLoginResult: AbpLoginResult<Tenant, User>
    {
        public TalentLoginResult(AbpLoginResultType result) : base(result)
        {

        }

        public string Role { get; }
    }
}
