﻿// <auto-generated />
using System;
using Doctors.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConceptionApiCore.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240102054637_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Doctors.Models.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DoctorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatientID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppointmentID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("PatientID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Doctors.Models.Booking", b =>
                {
                    b.Property<Guid>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ClinicID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PatientID1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookingID");

                    b.HasIndex("ClinicID");

                    b.HasIndex("PatientID");

                    b.HasIndex("PatientID1");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Doctors.Models.Clinic", b =>
                {
                    b.Property<Guid>("ClinicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClinicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClinicID");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("Doctors.Models.Doctor", b =>
                {
                    b.Property<Guid>("DoctorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClinicID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DoctorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorID");

                    b.HasIndex("ClinicID");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Doctors.Models.Fee", b =>
                {
                    b.Property<Guid>("FeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("DoctorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DoctorID1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FeeID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("DoctorID1");

                    b.ToTable("Fees");
                });

            modelBuilder.Entity("Doctors.Models.Patient", b =>
                {
                    b.Property<Guid>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Doctors.Models.Permission", b =>
                {
                    b.Property<Guid>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Module")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PermissionID");

                    b.HasIndex("RoleID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Doctors.Models.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Doctors.Models.Schedule", b =>
                {
                    b.Property<Guid>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DoctorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ScheduleID");

                    b.HasIndex("DoctorID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Doctors.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Doctors.Models.Appointment", b =>
                {
                    b.HasOne("Doctors.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doctors.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Doctors.Models.Booking", b =>
                {
                    b.HasOne("Doctors.Models.Clinic", "Clinic")
                        .WithMany("Bookings")
                        .HasForeignKey("ClinicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doctors.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doctors.Models.Patient", null)
                        .WithMany("Bookings")
                        .HasForeignKey("PatientID1");

                    b.Navigation("Clinic");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Doctors.Models.Doctor", b =>
                {
                    b.HasOne("Doctors.Models.Clinic", "Clinic")
                        .WithMany("Doctors")
                        .HasForeignKey("ClinicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("Doctors.Models.Fee", b =>
                {
                    b.HasOne("Doctors.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doctors.Models.Doctor", null)
                        .WithMany("Fees")
                        .HasForeignKey("DoctorID1");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Doctors.Models.Permission", b =>
                {
                    b.HasOne("Doctors.Models.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Doctors.Models.Schedule", b =>
                {
                    b.HasOne("Doctors.Models.Doctor", "Doctor")
                        .WithMany("Schedules")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Doctors.Models.User", b =>
                {
                    b.HasOne("Doctors.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Doctors.Models.Clinic", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("Doctors.Models.Doctor", b =>
                {
                    b.Navigation("Fees");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Doctors.Models.Patient", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Doctors.Models.Role", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
