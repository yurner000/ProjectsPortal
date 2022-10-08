using ProjectsPortalBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectsPortalBackend.Data
{
    public class ProjectContext: DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<BudgetSource> BudgetSources { get; set; }
        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<LogOperations> LogOperations { get; set; }
    }
}
