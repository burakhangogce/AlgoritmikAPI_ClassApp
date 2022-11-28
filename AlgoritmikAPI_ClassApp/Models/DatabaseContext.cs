using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserInfo>? UserInfo { get; set; }
        public virtual DbSet<Student>? Student { get; set; }
        public virtual DbSet<MyClass>? MyClass { get; set; }
        public virtual DbSet<Report>? Report { get; set; }
        public virtual DbSet<Performance>? Performance { get; set; }
        public virtual DbSet<HomeWork>? HomeWork { get; set; }
        public virtual DbSet<DietModel>? Diets { get; set; }
        public virtual DbSet<DietDayModel>? DietDays { get; set; }
        public virtual DbSet<DietMenuModel>? DietDayMenus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            





            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
