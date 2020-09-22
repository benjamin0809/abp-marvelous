using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TalentMatrix.Authorization.Roles;
using TalentMatrix.Authorization.Users;
using TalentMatrix.MultiTenancy;
using TalentMatrix.Org;
using TalentMatrix.Mediae;

namespace TalentMatrix.EntityFrameworkCore
{
    public class TalentMatrixDbContext : AbpZeroDbContext<Tenant, Role, User, TalentMatrixDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrgUser> OrgUser { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Media> Media { get; set; }
        

        public TalentMatrixDbContext(DbContextOptions<TalentMatrixDbContext> options)
            : base(options)
        {
        }
    }
}
