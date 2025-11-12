using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Models;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Models.User;
using MinhaSaudeFeminina.Models.UserProfile;

namespace MinhaSaudeFeminina.Data
{
    public class AppDbContext 
        : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // User
        //public DbSet<User> Users { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        //Catalogs
        public DbSet<Gender> Genders { get; set; } = null!;
        public DbSet<Objective> Objectives { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Symptom> Symptoms { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        // Relations
        public DbSet<ProfileGender> ProfileGenders { get; set; } = null!;
        public DbSet<ProfileObjective> ProfileObjectives { get; set; } = null!;
        public DbSet<ProfileStatus> ProfileStatuses { get; set; } = null!;
        public DbSet<ProfileSymptom> ProfileSymptoms { get; set; } = null!;
        public DbSet<TagGender> TagGenders { get; set; } = null!;
        public DbSet<TagObjective> TagObjectives { get; set; } = null!;
        public DbSet<TagStatus> TagStatuses { get; set; } = null!;
        public DbSet<TagSymptom> TagSymptoms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Profile Relations with User

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Profile Relations

            // ProfileGender

            modelBuilder.Entity<ProfileGender>()
                .HasKey(pg => new { pg.ProfileId, pg.GenderId });

            modelBuilder.Entity<ProfileGender>()
                .HasOne(pg => pg.Profile)
                .WithMany(p => p.ProfileGenders)
                .HasForeignKey(pg => pg.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfileGender>()
                .HasOne(pg => pg.Gender)
                .WithMany(p => p.ProfileGenders)
                .HasForeignKey(pg => pg.GenderId)
                .OnDelete(DeleteBehavior.Cascade);


            // ProfileObjective

            modelBuilder.Entity<ProfileObjective>()
                .HasKey(po => new { po.ProfileId, po.ObjectiveId });

            modelBuilder.Entity<ProfileObjective>()
                .HasOne(po => po.Profile)
                .WithMany(p => p.ProfileObjectives)
                .HasForeignKey(po => po.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfileObjective>()
                .HasOne(po => po.Objective)
                .WithMany(p => p.ProfileObjectives)
                .HasForeignKey(po => po.ObjectiveId)
                .OnDelete(DeleteBehavior.Cascade);


            // ProfileStatus

            modelBuilder.Entity<ProfileStatus>()
                .HasKey(ps => new { ps.ProfileId, ps.StatusId });

            modelBuilder.Entity<ProfileStatus>()
                .HasOne(ps => ps.Profile)
                .WithMany(p => p.ProfileStatuses)
                .HasForeignKey(ps => ps.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfileStatus>()
                .HasOne(ps => ps.Status)
                .WithMany(p => p.ProfileStatuses)
                .HasForeignKey(ps => ps.StatusId)
                .OnDelete(DeleteBehavior.Cascade);


            // ProfileSymptom

            modelBuilder.Entity<ProfileSymptom>()
                .HasKey(ps => new { ps.ProfileId, ps.SymptomId });

            modelBuilder.Entity<ProfileSymptom>()
                .HasOne(ps => ps.Profile)
                .WithMany(p => p.ProfileSymptoms)
                .HasForeignKey(ps => ps.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfileSymptom>()
                .HasOne(ps => ps.Symptom)
                .WithMany(p => p.ProfileSymptoms)
                .HasForeignKey(ps => ps.SymptomId)
                .OnDelete(DeleteBehavior.Cascade);


            // Tag Relations

            // TagGender

            modelBuilder.Entity<TagGender>()
                .HasKey(tg => tg.TagGenderId);

            modelBuilder.Entity<TagGender>()
                .HasOne(tg => tg.Tag)
                .WithMany(t => t.TagGenders)
                .HasForeignKey(tg => tg.TagId)
                .OnDelete(DeleteBehavior.Restrict);


            // TagObjective

            modelBuilder.Entity<TagObjective>()
                .HasKey(to => to.TagObjectiveId);

            modelBuilder.Entity<TagObjective>()
                .HasOne(to => to.Tag)
                .WithMany(t => t.TagObjectives)
                .HasForeignKey(to => to.TagId)
                .OnDelete(DeleteBehavior.Restrict);


            // TagStatus

            modelBuilder.Entity<TagStatus>()
                .HasKey(ts => ts.TagStatusId);

            modelBuilder.Entity<TagStatus>()
                .HasOne(ts => ts.Tag)
                .WithMany(t => t.TagStatuses)
                .HasForeignKey(ts => ts.TagId)
                .OnDelete(DeleteBehavior.Restrict);


            // TagSymptom

            modelBuilder.Entity<TagSymptom>()
                .HasKey(ts => new { ts.TagId, ts.SymptomId });

            modelBuilder.Entity<TagSymptom>()
                .HasOne(ts => ts.Tag)
                .WithMany(t => t.TagSymptoms)
                .HasForeignKey(ts => ts.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TagSymptom>()
                .HasOne(ts => ts.Symptom)
                .WithMany(t => t.TagSymptoms)
                .HasForeignKey(ts => ts.SymptomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
