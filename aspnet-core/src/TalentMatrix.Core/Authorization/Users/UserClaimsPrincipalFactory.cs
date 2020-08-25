using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using TalentMatrix.Authorization.Roles;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace TalentMatrix.Authorization.Users
{
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        private readonly RoleManager _roleManager ;
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
            _roleManager = roleManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var claim = await base.CreateAsync(user);
            claim.Identities.First().AddClaim(new Claim("Application_UserEmail", user.EmailAddress));

            return claim;
        }
    }
}
