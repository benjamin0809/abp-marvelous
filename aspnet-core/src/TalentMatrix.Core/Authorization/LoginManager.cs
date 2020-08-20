using Microsoft.AspNetCore.Identity;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using TalentMatrix.Authorization.Roles;
using TalentMatrix.Authorization.Users;
using TalentMatrix.MultiTenancy;
using System.Threading.Tasks;
using Abp.Extensions;
using System;

namespace TalentMatrix.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        private readonly UserStore _userStore;
        private readonly AbpUserManager<Role, User> _userManager;
        public LogInManager(
            UserManager userManager, 
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager, 
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, 
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            IPasswordHasher<User> passwordHasher, 
            RoleManager roleManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory,
            UserStore userStore) 
            : base(
                  userManager, 
                  multiTenancyConfig,
                  tenantRepository, 
                  unitOfWorkManager, 
                  settingManager, 
                  userLoginAttemptRepository, 
                  userManagementConfig, 
                  iocResolver, 
                  passwordHasher, 
                  roleManager, 
                  claimsPrincipalFactory)
        {
            _userStore = userStore;
            _userManager = userManager;
        }

        /// <summary>
        /// 自定义登录
        /// </summary>
        /// <param name="account">账号、手机号、身份证号</param>
        /// <param name="password">明文密码</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<AbpLoginResult<Tenant, User>> LoginCustomAsync(string account, string password)
        {
            var result = await LoginCustomAsyncInternal(account, password);

            //保存用户尝试登录的记录
            await SaveLoginAttemptAsync(result, null, account);
            return result;
        }

        protected virtual async Task<AbpLoginResult<Tenant, User>> LoginCustomAsyncInternal(string account, string password)
        {
            if (account.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                throw new Exception("account or password");
            }

            //不启用租户，获取默认租户
            Tenant tenant = await GetDefaultTenantAsync();

            int? tenantId = tenant?.Id;
            using (UnitOfWorkManager.Current.SetTenantId(tenantId))
            {
                //根据用户名获取用户信息
                var user = await _userStore.FindByAccountAsync(account);
                if (user == null)
                {
                    return new AbpLoginResult<Tenant, User>(AbpLoginResultType.UnknownExternalLogin, tenant);
                }

                //验证用户的密码是否正确
                var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.Password, password);
                if (verificationResult != PasswordVerificationResult.Success)
                {
                    if (await TryLockOutAsync(tenantId, user.Id))
                    {
                        return new AbpLoginResult<Tenant, User>(AbpLoginResultType.LockedOut, tenant, user);
                    }

                    return new AbpLoginResult<Tenant, User>(AbpLoginResultType.InvalidPassword, tenant, user);
                }

                //重置用户登录失败次数
                await _userManager.ResetAccessFailedCountAsync(user);

                //生成登录结果
                return await CreateLoginResultAsync(user, tenant);
            }
        }
    }
}
