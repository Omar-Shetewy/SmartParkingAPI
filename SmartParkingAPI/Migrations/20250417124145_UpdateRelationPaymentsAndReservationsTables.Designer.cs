﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartParking.API.Data;

#nullable disable

namespace SmartParking.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250417124145_UpdateRelationPaymentsAndReservationsTables")]
    partial class UpdateRelationPaymentsAndReservationsTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.HasKey("EmployeeId");

                    b.HasIndex("GarageId");

                    b.HasIndex("JobId");

                    b.ToTable("Employees");
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

                    b.Property<int>("OwnerId")
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

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OwnerId");

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

            modelBuilder.Entity("SmartParking.API.Data.Models.ReservationRecord", b =>
                {
                    b.Property<int>("ReservationRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationRecordId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GarageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationRecordId");

                    b.HasIndex("GarageId");

                    b.HasIndex("UserId")
                        .IsUnique();

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

                    b.Property<int>("Status")
                        .HasColumnType("int");

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

                    b.HasIndex("UserId");

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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Car", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Spot", "Spot")
                        .WithMany()
                        .HasForeignKey("SpotId");

                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithMany("Cars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Employee", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Garage", "Garage")
                        .WithMany("Employees")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartParking.API.Data.Models.Job", "Job")
                        .WithMany("Employees")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garage");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Garage", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Owner", "Owner")
                        .WithMany("Garages")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
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

            modelBuilder.Entity("SmartParking.API.Data.Models.ReservationRecord", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Garage", "Garage")
                        .WithMany("ReservationRecords")
                        .HasForeignKey("GarageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartParkingAPI.Data.Models.User", "User")
                        .WithOne("Reservation")
                        .HasForeignKey("SmartParking.API.Data.Models.ReservationRecord", "UserId")
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
                        .WithMany("VerificationCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartParkingAPI.Data.Models.User", b =>
                {
                    b.HasOne("SmartParking.API.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SmartParking.API.Data.Models.Garage", b =>
                {
                    b.Navigation("Employees");

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

            modelBuilder.Entity("SmartParkingAPI.Data.Models.User", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Reservation")
                        .IsRequired();

                    b.Navigation("VerificationCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
