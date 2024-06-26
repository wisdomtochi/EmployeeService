﻿// <auto-generated />
using System;
using EmployeeService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeService.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    [Migration("20240625073555_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EmployeeService.Domains.Connection", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("char(36)");

                    b.HasKey("EmployeeId", "FriendId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("EmployeeService.Domains.ConnectionRequest", b =>
                {
                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("char(36)");

                    b.Property<string>("RequestNotification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ReceiverId", "SenderId");

                    b.ToTable("ConnectionRequests");
                });

            modelBuilder.Entity("EmployeeService.Domains.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
