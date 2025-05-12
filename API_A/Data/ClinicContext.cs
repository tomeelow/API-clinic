namespace API_A.Data;
using Microsoft.EntityFrameworkCore;
using API_A.Models;

public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentService> AppointmentServices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // appointment
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");
                entity.HasKey(e => e.AppointmentId)
                      .HasName("Appointment_pk");
                entity.Property(e => e.AppointmentId)
                      .HasColumnName("appoitment_id");
                entity.Property(e => e.PatientId)
                      .HasColumnName("patient_id");
                entity.Property(e => e.DoctorId)
                      .HasColumnName("doctor_id");
                entity.Property(e => e.Date)
                      .HasColumnName("date");

                entity.HasOne(e => e.Patient)
                      .WithMany(p => p.Appointments)
                      .HasForeignKey(e => e.PatientId)
                      .HasConstraintName("Appointment_Patient");

                entity.HasOne(e => e.Doctor)
                      .WithMany(d => d.Appointments)
                      .HasForeignKey(e => e.DoctorId)
                      .HasConstraintName("Appointment_Doctor");
            });

            // appointmentService
            modelBuilder.Entity<AppointmentService>(entity =>
            {
                entity.ToTable("Appointment_Service");
                entity.HasKey(e => new { e.ServiceId, e.AppointmentId })
                      .HasName("Appointment_Service_pk");
                entity.Property(e => e.AppointmentId)
                      .HasColumnName("appoitment_id");
                entity.Property(e => e.ServiceId)
                      .HasColumnName("service_id");
                entity.Property(e => e.ServiceFee)
                      .HasColumnName("service_fee");

                entity.HasOne(e => e.Appointment)
                      .WithMany(a => a.AppointmentServices)
                      .HasForeignKey(e => e.AppointmentId)
                      .HasConstraintName("Appointment_Service_Appointment");

                entity.HasOne(e => e.Service)
                      .WithMany(s => s.AppointmentServices)
                      .HasForeignKey(e => e.ServiceId)
                      .HasConstraintName("Appointment_Service_Service");
            });

            // doctor
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");
                entity.HasKey(e => e.DoctorId)
                      .HasName("Doctor_pk");
                entity.Property(e => e.DoctorId)
                      .HasColumnName("doctor_id");
                entity.Property(e => e.FirstName)
                      .HasColumnName("first_name")
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.LastName)
                      .HasColumnName("last_name")
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.PWZ)
                      .HasColumnName("PWZ")
                      .IsRequired()
                      .HasMaxLength(7);
            });

            // patient
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");
                entity.HasKey(e => e.PatientId)
                      .HasName("Patient_pk");
                entity.Property(e => e.PatientId)
                      .HasColumnName("patient_id");
                entity.Property(e => e.FirstName)
                      .HasColumnName("first_name")
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.LastName)
                      .HasColumnName("last_name")
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.DateOfBirth)
                      .HasColumnName("date_of_birth");
            });

            // service
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");
                entity.HasKey(e => e.ServiceId)
                      .HasName("Service_pk");
                entity.Property(e => e.ServiceId)
                      .HasColumnName("service_id");
                entity.Property(e => e.Name)
                      .HasColumnName("name")
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.BaseFee)
                      .HasColumnName("base_fee");
            });
        }
    }
    