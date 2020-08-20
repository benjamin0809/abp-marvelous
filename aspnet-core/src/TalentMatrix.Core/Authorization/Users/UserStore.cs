using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq;
using Abp.Organizations;
using System.Threading.Tasks;
using TalentMatrix.Authorization.Roles;

namespace TalentMatrix.Authorization.Users
{
    public class UserStore : AbpUserStore<Role, User>
    {
        private readonly IRepository<User, long> _userRepository;
        public UserStore(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<User, long> userRepository,
            IRepository<Role> roleRepository,
            IAsyncQueryableExecuter asyncQueryableExecuter,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserClaim, long> userClaimRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository)
            : base(
                unitOfWorkManager,
                userRepository,
                roleRepository,
                asyncQueryableExecuter,
                userRoleRepository,
                userLoginRepository,
                userClaimRepository,
                userPermissionSettingRepository,
                userOrganizationUnitRepository,
                organizationUnitRoleRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 根据账号获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public virtual async Task<User> FindByAccountAsync(string account)
        {
            account = account.ToLower();
            return await _userRepository.FirstOrDefaultAsync(
                user => user.UserName.ToLower() == account
            );
        }
    }
}
