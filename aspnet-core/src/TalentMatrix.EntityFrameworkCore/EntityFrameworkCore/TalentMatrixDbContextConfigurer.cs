using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TalentMatrix.EntityFrameworkCore
{
    public static class TalentMatrixDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TalentMatrixDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TalentMatrixDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
