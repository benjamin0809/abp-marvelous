using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TalentMatrix.Configuration;
using TalentMatrix.Web;

namespace TalentMatrix.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TalentMatrixDbContextFactory : IDesignTimeDbContextFactory<TalentMatrixDbContext>
    {
        public TalentMatrixDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TalentMatrixDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TalentMatrixDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TalentMatrixConsts.ConnectionStringName));

            return new TalentMatrixDbContext(builder.Options);
        }
    }
}
