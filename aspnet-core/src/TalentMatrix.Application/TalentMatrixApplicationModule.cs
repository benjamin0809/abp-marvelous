using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TalentMatrix.Authorization;

namespace TalentMatrix
{
    [DependsOn(
        typeof(TalentMatrixCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TalentMatrixApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TalentMatrixAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TalentMatrixApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
