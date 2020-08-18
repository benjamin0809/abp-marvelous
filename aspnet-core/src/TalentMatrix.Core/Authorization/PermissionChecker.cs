using Abp.Authorization;
using TalentMatrix.Authorization.Roles;
using TalentMatrix.Authorization.Users;

namespace TalentMatrix.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
