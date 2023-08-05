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
        public virtual DbSet<DietModel>? Diets { get; set; }
        public virtual DbSet<DietDayModel>? DietDays { get; set; }
        public virtual DbSet<DietMenuModel>? DietDayMenus { get; set; }
        public virtual DbSet<RecipeModel>? Recipe { get; set; }
        public virtual DbSet<NutritionistModel>? Nutritionist { get; set; }
        public virtual DbSet<ClientModel>? Client { get; set; }
        public virtual DbSet<VersionModel>? Versions { get; set; }
        public virtual DbSet<InviteModel>? Invites { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<DietModel>()
            .HasMany(e => e.dietDayModel)
            .WithOne(e => e.dietModel)
            .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
            .Entity<DietDayModel>()
            .HasMany(e => e.dietMenus)
            .WithOne(e => e.dietDayModel)
            .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<DietMenuModel>()
            .Ignore(a => a.isDelete);
            modelBuilder.Entity<UserInfo>()
            .Ignore(a => a.Token);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
