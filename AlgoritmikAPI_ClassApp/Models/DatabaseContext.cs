﻿using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<UserInfo>? UserInfos { get; set; }
        public virtual DbSet<Student>? Students { get; set; }
        public virtual DbSet<MyClass>? MyClasses { get; set; }
        public virtual DbSet<Report>? Reports { get; set; }
        public virtual DbSet<Performance>? Performances { get; set; }
        public virtual DbSet<HomeWork>? HomeWorks { get; set; }
        public virtual DbSet<DietModel>? Diets { get; set; }
        public virtual DbSet<DietDayModel>? DietDays { get; set; }
        public virtual DbSet<DietMenuModel>? DietMenus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.DisplayName).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.UserName).HasMaxLength(30).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CreatedDate).IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");
                entity.Property(e => e.studentId).HasColumnName("studentId");
                entity.Property(e => e.studentTeacherId).IsUnicode(false);
                entity.Property(e => e.studentClassId).IsUnicode(false);
                entity.Property(e => e.studentNumber).IsUnicode(false);
                entity.Property(e => e.studentName).IsUnicode(false);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");
                entity.Property(e => e.reportId).HasColumnName("reportId");
                entity.Property(e => e.reportStudentId).IsUnicode(false);
                entity.Property(e => e.reportName).IsUnicode(false);
                entity.Property(e => e.reportDesc).IsUnicode(false);
            });

            modelBuilder.Entity<MyClass>(entity =>
            {
                entity.ToTable("Class");
                entity.Property(e => e.classId).HasColumnName("classId");
                entity.Property(e => e.classTeacherId).IsUnicode(false);
                entity.Property(e => e.className).IsUnicode(false);
                entity.Property(e => e.classLevel).IsUnicode(false);
            });

            modelBuilder.Entity<HomeWork>(entity =>
            {
                entity.ToTable("HomeWork");
                entity.Property(e => e.homeWorkId).HasColumnName("homeWorkId");
                entity.Property(e => e.homeWorkClassId).IsUnicode(false);
                entity.Property(e => e.homeWorkTeacherId).IsUnicode(false);
                entity.Property(e => e.homeWorkName).IsUnicode(false);
                entity.Property(e => e.homeWorkDesc).IsUnicode(false);
                entity.Property(e => e.homeWorkDate).IsUnicode(false);
            });

            modelBuilder.Entity<Performance>(entity =>
            {
                entity.ToTable("Performance");
                entity.Property(e => e.performanceId).HasColumnName("performanceID");
                entity.Property(e => e.performanceStudentId).IsUnicode(false);
                entity.Property(e => e.performanceDate).IsUnicode(false);
            });
            modelBuilder.Entity<DietModel>(entity =>
            {
                entity.ToTable("Diets");
                entity.Property(e => e.dietId).HasColumnName("dietId");
                entity.Property(e => e.clientId).HasColumnName("clientId");
                entity.Property(e => e.dietTitle).IsUnicode(false);
                entity.Property(e => e.dietStartDate).IsUnicode(false);
                entity.Property(e => e.dietEndDate).IsUnicode(false);
            });


            modelBuilder.Entity<DietDayModel>(entity =>
            {
                entity.ToTable("DietDays");
                entity.Property(e => e.dietDayId).HasColumnName("dietDayId");
                entity.Property(e => e.dietId).HasColumnName("dietId");
                entity.Property(e => e.dietTime).HasColumnName("dietTime");
            });

            modelBuilder.Entity<DietMenuModel>(entity =>
            {
                entity.ToTable("DietDayMenus");
                entity.Property(e => e.dietDayId).HasColumnName("dietDayId");
                entity.Property(e => e.dietMenuId).IsUnicode(false);
                entity.Property(e => e.dietMenuTitle).IsUnicode(false);
                entity.Property(e => e.dietMenuDetail).IsUnicode(false);
                entity.Property(e => e.dietMenuTime).IsUnicode(false);
                entity.Property(e => e.isNotification).IsUnicode(false);
                entity.Property(e => e.isCompleted).IsUnicode(false);
            });
            modelBuilder.Entity<DietModel>().Property(p => p.dietId).ValueGeneratedOnAdd();
            modelBuilder.Entity<DietDayModel>().Property(p => p.dietDayId).ValueGeneratedOnAdd();
            modelBuilder.Entity<DietMenuModel>().Property(p => p.dietMenuId).ValueGeneratedOnAdd();





            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
