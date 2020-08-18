using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TalentMatrix.EntityFrameworkCore;
using TalentMatrix.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace TalentMatrix.Web.Tests
{
    [DependsOn(
        typeof(TalentMatrixWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TalentMatrixWebTestModule : AbpModule
    {
        public TalentMatrixWebTestModule(TalentMatrixEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TalentMatrixWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TalentMatrixWebMvcModule).Assembly);
        }
    }
}