using Domain.Entities;
using Infraestructure.Persistence.Configuratios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //dbsets    
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Domain.Entities.DoctorSpecialty> DoctorSpecialties { get; set; }

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
            });
            base.OnModelCreating(modelBuilder);

            // ================= SPECIALTY =================
            modelBuilder.Entity<Specialty>(s =>
            {
                s.ToTable("Specialty");
                s.HasKey(s => s.SpecialtyId);
                s.Property(s => s.Name).HasMaxLength(100).IsRequired();
                s.Property(s => s.Description).HasMaxLength(250);
            });

            // ================= DOCTORSPECIALTY =================
            modelBuilder.Entity<Domain.Entities.DoctorSpecialty>(ds =>
            {
                ds.ToTable("DoctorSpecialty");
                ds.HasKey(ds => new { ds.DoctorId, ds.SpecialtyId });
                ds.HasOne(ds => ds.Doctor)
                  .WithMany(d => d.DoctorSpecialties)
                  .HasForeignKey(ds => ds.DoctorId);
                ds.HasOne(ds => ds.Specialty)
                  .WithMany(s => s.DoctorSpecialties)
                  .HasForeignKey(ds => ds.SpecialtyId);
            });

            //Precarga de datos
            modelBuilder.ApplyConfiguration(new DoctorConfig());
            modelBuilder.ApplyConfiguration(new PatientConfig());
            modelBuilder.ApplyConfiguration(new SpecialtyConfig());
            modelBuilder.ApplyConfiguration(new DoctorSpecialtyConfig());
        }
    } 
}
