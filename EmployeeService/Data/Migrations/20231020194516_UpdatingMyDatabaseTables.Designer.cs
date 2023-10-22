﻿// <auto-generated />
using EmployeeService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeService.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    [Migration("20231020194516_UpdatingMyDatabaseTables")]
    partial class UpdatingMyDatabaseTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ConnectionEmployee", b =>
                {
                    b.Property<int>("ConnectionsId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.HasKey("ConnectionsId", "EmployeesId");

                    b.HasIndex("EmployeesId");

                    b.ToTable("ConnectionEmployee");
                });

            modelBuilder.Entity("EmployeeService.Domains.Connection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("EmployeeService.Domains.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Miselyn",
                            Gender = "F",
                            LastName = "Kisera",
                            Salary = 847300
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Jurome",
                            Gender = "M",
                            LastName = "Anthony",
                            Salary = 324300
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Doseel",
                            Gender = "F",
                            LastName = "Paul",
                            Salary = 332300
                        });
                });

            modelBuilder.Entity("ConnectionEmployee", b =>
                {
                    b.HasOne("EmployeeService.Domains.Connection", null)
                        .WithMany()
                        .HasForeignKey("ConnectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeService.Domains.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}