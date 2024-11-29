﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HilaryProject.Migrations
{
    [DbContext(typeof(HilaryDbContext))]
    [Migration("20241129145618_DropStylistServicesTable")]
    partial class DropStylistServicesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AppointmentId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("boolean");

                    b.Property<int>("StylistId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeOf")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("AppointmentId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StylistId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            AppointmentId = 1,
                            CustomerId = 1,
                            IsCancelled = false,
                            StylistId = 1,
                            TimeOf = new DateTime(2024, 12, 1, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 2,
                            CustomerId = 2,
                            IsCancelled = false,
                            StylistId = 2,
                            TimeOf = new DateTime(2024, 12, 1, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 3,
                            CustomerId = 3,
                            IsCancelled = true,
                            StylistId = 4,
                            TimeOf = new DateTime(2024, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 4,
                            CustomerId = 4,
                            IsCancelled = true,
                            StylistId = 5,
                            TimeOf = new DateTime(2024, 12, 2, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 5,
                            CustomerId = 5,
                            IsCancelled = true,
                            StylistId = 6,
                            TimeOf = new DateTime(2024, 12, 2, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 6,
                            CustomerId = 6,
                            IsCancelled = false,
                            StylistId = 7,
                            TimeOf = new DateTime(2024, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 7,
                            CustomerId = 7,
                            IsCancelled = false,
                            StylistId = 8,
                            TimeOf = new DateTime(2024, 12, 3, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 8,
                            CustomerId = 8,
                            IsCancelled = false,
                            StylistId = 1,
                            TimeOf = new DateTime(2024, 12, 3, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 9,
                            CustomerId = 9,
                            IsCancelled = false,
                            StylistId = 2,
                            TimeOf = new DateTime(2024, 12, 3, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 10,
                            CustomerId = 1,
                            IsCancelled = false,
                            StylistId = 3,
                            TimeOf = new DateTime(2024, 12, 4, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 11,
                            CustomerId = 2,
                            IsCancelled = false,
                            StylistId = 4,
                            TimeOf = new DateTime(2024, 12, 4, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 12,
                            CustomerId = 3,
                            IsCancelled = false,
                            StylistId = 5,
                            TimeOf = new DateTime(2024, 12, 4, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 13,
                            CustomerId = 4,
                            IsCancelled = false,
                            StylistId = 7,
                            TimeOf = new DateTime(2024, 12, 5, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 14,
                            CustomerId = 5,
                            IsCancelled = false,
                            StylistId = 8,
                            TimeOf = new DateTime(2024, 12, 5, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            AppointmentId = 15,
                            CustomerId = 6,
                            IsCancelled = false,
                            StylistId = 1,
                            TimeOf = new DateTime(2024, 12, 5, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Models.AppointmentServiceJoinTable", b =>
                {
                    b.Property<int>("AppointmentServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AppointmentServiceId"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.HasKey("AppointmentServiceId");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("ServiceId");

                    b.ToTable("AppointmentServices");
                });

            modelBuilder.Entity("Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Name = "Alice Johnson"
                        },
                        new
                        {
                            CustomerId = 2,
                            Name = "Bob Smith"
                        },
                        new
                        {
                            CustomerId = 3,
                            Name = "Charlie Brown"
                        },
                        new
                        {
                            CustomerId = 4,
                            Name = "Diane Evans"
                        },
                        new
                        {
                            CustomerId = 5,
                            Name = "Ethan Parker"
                        },
                        new
                        {
                            CustomerId = 6,
                            Name = "Fiona Grey"
                        },
                        new
                        {
                            CustomerId = 7,
                            Name = "Grace White"
                        },
                        new
                        {
                            CustomerId = 8,
                            Name = "Henry Green"
                        },
                        new
                        {
                            CustomerId = 9,
                            Name = "Isla Blue"
                        });
                });

            modelBuilder.Entity("Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ServiceId"));

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            ServiceId = 1,
                            DurationMinutes = 60,
                            Name = "Haircut",
                            Price = 20.00m
                        },
                        new
                        {
                            ServiceId = 2,
                            DurationMinutes = 120,
                            Name = "Hair Coloring",
                            Price = 50.00m
                        },
                        new
                        {
                            ServiceId = 3,
                            DurationMinutes = 30,
                            Name = "Beard Trim",
                            Price = 15.00m
                        },
                        new
                        {
                            ServiceId = 4,
                            DurationMinutes = 150,
                            Name = "Perm",
                            Price = 70.00m
                        },
                        new
                        {
                            ServiceId = 5,
                            DurationMinutes = 90,
                            Name = "Hair Treatment",
                            Price = 30.00m
                        });
                });

            modelBuilder.Entity("Models.Stylist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Stylists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "Sophia Carter"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "Oliver Davis"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = false,
                            Name = "Liam Taylor"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            Name = "Emma Wilson"
                        },
                        new
                        {
                            Id = 5,
                            IsActive = true,
                            Name = "Ava Moore"
                        },
                        new
                        {
                            Id = 6,
                            IsActive = false,
                            Name = "Mason Harris"
                        },
                        new
                        {
                            Id = 7,
                            IsActive = true,
                            Name = "Lucas Walker"
                        },
                        new
                        {
                            Id = 8,
                            IsActive = true,
                            Name = "Mia Hall"
                        });
                });

            modelBuilder.Entity("Models.StylistServiceJoinTable", b =>
                {
                    b.Property<int>("StylistServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StylistServiceId"));

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<int>("StylistId")
                        .HasColumnType("integer");

                    b.HasKey("StylistServiceId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StylistId");

                    b.ToTable("StylistServices", (string)null);
                });

            modelBuilder.Entity("Models.Appointment", b =>
                {
                    b.HasOne("Models.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Stylist", "Stylist")
                        .WithMany("Appointments")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Stylist");
                });

            modelBuilder.Entity("Models.AppointmentServiceJoinTable", b =>
                {
                    b.HasOne("Models.Appointment", "Appointment")
                        .WithMany("AppointmentServiceJoinList")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Service", "Service")
                        .WithMany("AppointmentServiceJoinList")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Models.StylistServiceJoinTable", b =>
                {
                    b.HasOne("Models.Service", "Service")
                        .WithMany("StylistServiceJoinList")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Stylist", "Stylist")
                        .WithMany("StylistServiceJoinList")
                        .HasForeignKey("StylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("Stylist");
                });

            modelBuilder.Entity("Models.Appointment", b =>
                {
                    b.Navigation("AppointmentServiceJoinList");
                });

            modelBuilder.Entity("Models.Customer", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Models.Service", b =>
                {
                    b.Navigation("AppointmentServiceJoinList");

                    b.Navigation("StylistServiceJoinList");
                });

            modelBuilder.Entity("Models.Stylist", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("StylistServiceJoinList");
                });
#pragma warning restore 612, 618
        }
    }
}
