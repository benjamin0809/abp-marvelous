using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalentMatrix.Authorization.Users
{
    public class MyAppSession : ClaimsAbpSession, ITransientDependency
    {
        public MyAppSession(
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider) :
            base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {

        }

        public string UserEmail
        {
            get
            {
                var userEmailClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Application_UserEmail");
                if (string.IsNullOrEmpty(userEmailClaim?.Value))
                {
                    return null;
                }

                return userEmailClaim.Value;
            }
        }

        public string UserRole
        {
            get
            {
                var userEmailClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Role");
                if (string.IsNullOrEmpty(userEmailClaim?.Value))
                {
                    return null;
                }

                return userEmailClaim.Value;
            }
        }
    }
}
