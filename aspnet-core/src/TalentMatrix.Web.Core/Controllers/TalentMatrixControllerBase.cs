using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TalentMatrix.Controllers
{
    public abstract class TalentMatrixControllerBase: AbpController
    {
        protected TalentMatrixControllerBase()
        {
            LocalizationSourceName = TalentMatrixConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
