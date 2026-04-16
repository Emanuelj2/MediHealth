using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<ProviderInsurance> ProviderInsurances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // =========================
            // USER ↔ PROVIDER (1-1)
            // =========================
            builder.Entity<User>()
                .HasOne(u => u.ProviderProfile)
                .WithOne(p => p.User)
                .HasForeignKey<Provider>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // USER ↔ PATIENT (1-1)
            // =========================
            builder.Entity<User>()
                .HasOne(u => u.PatientProfile)
                .WithOne(p => p.User)
                .HasForeignKey<Patient>(p => p.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // PROVIDER ↔ APPOINTMENTS (1-M)
            // =========================
            builder.Entity<Provider>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Provider)
                .HasForeignKey(a => a.ProviderId)
                .OnDelete(DeleteBehavior.Restrict); // prevent cascade loops

            // =========================
            // PATIENT ↔ APPOINTMENTS (1-M)
            // =========================
            builder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // PATIENT ↔ INSURANCE PLAN (M-1)
            // =========================
            builder.Entity<Patient>()
                .HasOne(p => p.InsurancePlan)
                .WithMany(i => i.Patients)
                .HasForeignKey(p => p.InsurancePlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // PROVIDER ↔ INSURANCE (M-M via ProviderInsurance)
            // =========================
            builder.Entity<ProviderInsurance>()
                .HasOne(pi => pi.Provider)
                .WithMany(p => p.ProviderInsurances)
                .HasForeignKey(pi => pi.ProviderId);

            builder.Entity<ProviderInsurance>()
                .HasOne(pi => pi.InsurancePlan)
                .WithMany(i => i.ProviderInsurances)
                .HasForeignKey(pi => pi.InsurancePlanId);

            // =========================
            // UNIQUE CONSTRAINTS
            // =========================

            // One provider per user
            builder.Entity<Provider>()
                .HasIndex(p => p.UserId)
                .IsUnique();

            // One patient per user
            builder.Entity<Patient>()
                .HasIndex(p => p.UserId)
                .IsUnique();

            // Prevent duplicate provider-insurance pairs
            builder.Entity<ProviderInsurance>()
                .HasIndex(pi => new { pi.ProviderId, pi.InsurancePlanId })
                .IsUnique();
        }

    }
}
