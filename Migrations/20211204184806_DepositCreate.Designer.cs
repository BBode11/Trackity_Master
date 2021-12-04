﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trackity.Models;

namespace Trackity.Migrations
{
    [DbContext(typeof(ExpenseContext))]
    [Migration("20211204184806_DepositCreate")]
    partial class DepositCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Trackity.Models.Deposit", b =>
                {
                    b.Property<int>("DepositId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Amount")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("DepositId");

                    b.ToTable("Deposits");

                    b.HasData(
                        new
                        {
                            DepositId = 1,
                            Amount = 1500.0,
                            Date = new DateTime(2021, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pay Check"
                        },
                        new
                        {
                            DepositId = 2,
                            Amount = 1500.0,
                            Date = new DateTime(2021, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pay Check"
                        },
                        new
                        {
                            DepositId = 3,
                            Amount = 500.0,
                            Date = new DateTime(2021, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Scratch Off Ticket"
                        },
                        new
                        {
                            DepositId = 4,
                            Amount = 47.5,
                            Date = new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Stock Returns"
                        });
                });

            modelBuilder.Entity("Trackity.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Cost")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("ExpenseId");

                    b.HasIndex("ExpenseTypeId");

                    b.ToTable("Expenses");

                    b.HasData(
                        new
                        {
                            ExpenseId = 1,
                            Cost = 600.0,
                            Date = new DateTime(2021, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpenseTypeId = "F",
                            Name = "Rent Payment"
                        },
                        new
                        {
                            ExpenseId = 2,
                            Cost = 8.9900000000000002,
                            Date = new DateTime(2021, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpenseTypeId = "F",
                            Name = "Netflix"
                        },
                        new
                        {
                            ExpenseId = 3,
                            Cost = 42.009999999999998,
                            Date = new DateTime(2021, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpenseTypeId = "R",
                            Name = "Gas"
                        },
                        new
                        {
                            ExpenseId = 4,
                            Cost = 21.850000000000001,
                            Date = new DateTime(2021, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpenseTypeId = "R",
                            Name = "Bubba's"
                        });
                });

            modelBuilder.Entity("Trackity.Models.ExpenseType", b =>
                {
                    b.Property<string>("ExpenseTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseTypeId");

                    b.ToTable("ExpenseTypes");

                    b.HasData(
                        new
                        {
                            ExpenseTypeId = "F",
                            Name = "Fixed"
                        },
                        new
                        {
                            ExpenseTypeId = "R",
                            Name = "Recurring"
                        },
                        new
                        {
                            ExpenseTypeId = "N",
                            Name = "Non-Recurring"
                        },
                        new
                        {
                            ExpenseTypeId = "W",
                            Name = "Whammy"
                        });
                });

            modelBuilder.Entity("Trackity.Models.Expense", b =>
                {
                    b.HasOne("Trackity.Models.ExpenseType", "ExpenseType")
                        .WithMany()
                        .HasForeignKey("ExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
