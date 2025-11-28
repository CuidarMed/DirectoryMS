using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        //dbsets    
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // ================= PATIENT =================
            modelBuilder.Entity<Patient>(p =>
            {
                p.ToTable("Patient");
                p.HasKey(p => p.PatientId);
                p.Property(p => p.Name).HasMaxLength(100).IsRequired();
                p.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                p.Property(p => p.Dni).IsRequired();
                p.Property(p => p.Adress).HasMaxLength(200);
                p.Property(p => p.DateOfBirth).IsRequired();
                p.Property(p => p.HealthPlan).HasMaxLength(100).IsRequired();
                p.Property(p => p.MembershipNumber).IsRequired();

            });

            // ================= DOCTOR =================
            modelBuilder.Entity<Doctor>(d =>
            {
                d.ToTable("Doctor");
                d.HasKey(d => d.DoctorId);
                d.Property(d => d.FirstName).HasMaxLength(50).IsRequired();
                d.Property(d => d.LastName).HasMaxLength(50).IsRequired();
                d.Property(d => d.LicenseNumber).HasMaxLength(50).IsRequired();
                d.Property(d => d.Biography).HasMaxLength(500);
                d.Property(d => d.Specialty).HasMaxLength(100); // especialidad
            });

            // ================= SPECIALTY =================
            modelBuilder.Entity<Specialty>(s =>
            {
                s.ToTable("Specialty");
                s.HasKey(s => s.SpecialtyId);
                s.Property(s => s.Name).HasMaxLength(100).IsRequired();
                s.Property(s => s.Color).HasMaxLength(20).IsRequired();
                s.Property(s => s.IsActive).IsRequired();
                s.Property(s => s.CreatedAt).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones de entidades
            modelBuilder.ApplyConfiguration(new DoctorConfig());
            modelBuilder.ApplyConfiguration(new PatientConfig());
        }
    }
}
