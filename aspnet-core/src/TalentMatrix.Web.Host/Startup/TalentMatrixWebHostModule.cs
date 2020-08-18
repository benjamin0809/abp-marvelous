using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TalentMatrix.Configuration;

namespace TalentMatrix.Web.Host.Startup
{
    [DependsOn(
       typeof(TalentMatrixWebCoreModule))]
    public class TalentMatrixWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TalentMatrixWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TalentMatrixWebHostModule).GetAssembly());
        }
    }
}
