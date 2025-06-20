﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartParking.API.Data;

#nullable disable

namespace SmartParking.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartParking.API.Data.Models.Camera", b =>
                {
                    b.Property<int>("CameraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CameraId"));

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CameraId");

                    b.HasIndex("GarageId");

                    b.ToTable("Cameras");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpotId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.HasIndex("SpotId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.EntryCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<bool>("InApp")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpotId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GarageId");

                    b.HasIndex("SpotId");

                    b.ToTable("EntryCars");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Garage", b =>
                {
                    b.Property<int>("GarageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GarageId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AvailableSpots")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("ReservedSpots")
                        .HasColumnType("int");

                    b.Property<int>("TotalSpots")
                        .HasColumnType("int");

                    b.HasKey("GarageId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Garages");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Owner", b =>
                {
                    b.Property<int>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OwnerId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OwnerId");

                    b.HasIndex("UserId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("bit");

                    b.Property<int>("ReservationRecordId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("ReservationRecordId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpireOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RevokedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.ReservationRecord", b =>
                {
                    b.Property<int>("ReservationRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReservationRecordId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationRecordId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationRecordId");

                    b.HasIndex("GarageId");

                    b.HasIndex("UserId");

                    b.ToTable("ReservationRecords");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "User"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Admin"
                        });
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Spot", b =>
                {
                    b.Property<int>("SpotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpotId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("SpotId");

                    b.HasIndex("GarageId");

                    b.ToTable("Spots");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.UserVerificationCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserVerificationCodes");
                });

            modelBuilder.Entity("SmartParkingAPI.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FcmToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Camera", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Garage", "Garage")
                        .WithMany("Cameras")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Car", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Spot", "Spot")
                        .WithMany()
                        .HasForeignKey("SpotId");

                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithOne("Car")
                        .HasForeignKey("SmartParking.API.Data.Models.Car", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Employee", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Job", "Job")
                        .WithMany("Employees")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("SmartParking.API.Data.Models.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.EntryCar", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Garage", "Garage")
                        .WithMany()
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartParking.API.Data.Models.Spot", "Spot")
                        .WithMany()
                        .HasForeignKey("SpotId");

                    b.Navigation("Garage");

                    b.Navigation("Spot");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Garage", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Owner", "Owner")
                        .WithMany("Garages")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Owner", b =>
                {
                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Payment", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Payment")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartParking.API.Data.Models.ReservationRecord", "ReservationRecord")
                        .WithOne("Payment")
                        .HasForeignKey("SmartParking.API.Data.Models.Payment", "ReservationRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("ReservationRecord");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.RefreshToken", b =>
                {
                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("SmartParking.API.Data.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.ReservationRecord", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Garage", "Garage")
                        .WithMany("ReservationRecords")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithMany("ReservationRecord")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Spot", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Garage", "Garage")
                        .WithMany("Spots")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.UserVerificationCode", b =>
                {
                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithOne("VerificationCode")
                        .HasForeignKey("SmartParking.API.Data.Models.UserVerificationCode", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParkingAPI.Data.Models.User", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Garage", b =>
                {
                    b.Navigation("Cameras");

                    b.Navigation("ReservationRecords");

                    b.Navigation("Spots");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Job", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Owner", b =>
                {
                    b.Navigation("Garages");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.PaymentMethod", b =>
                {
                    b.Navigation("Payment");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.ReservationRecord", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SmartParkingAPI.Data.Models.User", b =>
                {
                    b.Navigation("Car")
                        .IsRequired();

                    b.Navigation("Employee")
                        .IsRequired();

                    b.Navigation("RefreshToken");

                    b.Navigation("ReservationRecord");

                    b.Navigation("VerificationCode")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
