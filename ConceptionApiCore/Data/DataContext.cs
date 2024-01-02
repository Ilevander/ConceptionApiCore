using Doctors.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctors.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Fee>()
        .Property(f => f.Amount)
        .HasColumnType("decimal(18, 2)");

            // Explicit foreign key relationship for Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Patient)
                .WithMany()
                .HasForeignKey(b => b.PatientID);

            // Explicit foreign key relationship for Fee
            modelBuilder.Entity<Fee>()
                .HasOne(f => f.Doctor)  
                .WithMany()
                .HasForeignKey(f => f.DoctorID);

            // Explicit foreign key relationship for Permission
            modelBuilder.Entity<Permission>()
            .HasOne(p => p.Role)
            .WithMany(r => r.Permissions)
            .HasForeignKey(p => p.RoleID);

            // Explicit foreign key relationship for User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleID);
            modelBuilder.Entity<Fee>()
           .Property(f => f.DoctorID)
           .HasColumnType("uniqueidentifier");
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
//protected override void OnModelCreating(ModelBuilder modelBuilder)
// {
//    modelBuilder.Entity<Fees>().HasKey(f => f.FeeId);
//    modelBuilder.Entity<Fees>()
//   .Property(f => f.Amount)
//   .HasColumnType("decimal(18, 2)");

//    modelBuilder.Entity<Fees>()
//     .Property(f => f.DoctorFeeAmount)
//      .HasColumnType("decimal(18, 2)");

//    modelBuilder.Entity<Fees>()
//        .Property(f => f.DoctorFeeTotal)
//        .HasColumnType("decimal(18, 2)");

//    modelBuilder.Entity<Fees>()
//        .Property(f => f.Total)
//        .HasColumnType("decimal(18, 2)");
//    base.OnModelCreating(modelBuilder);
//}
//public DbSet<Schedule> Schedules { get; set; }

//public DbSet<Clinic> Clinics { get; set; }
//public DbSet<Role> Roles { get; set; }
//public DbSet<Permission> Permissions { get; set; }
//public DbSet<Patient> Patients { get; set; }
//public DbSet<User> Users { get; set; }