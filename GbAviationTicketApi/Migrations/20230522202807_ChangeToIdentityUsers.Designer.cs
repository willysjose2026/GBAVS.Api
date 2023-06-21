﻿// <auto-generated />
using System;
using GbAviationTicketApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    [DbContext(typeof(GbavsContext))]
    [Migration("20230522202807_ChangeToIdentityUsers")]
    partial class ChangeToIdentityUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GbAviationTicketApi.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("CUSTOMER_NAME");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("varchar(75)")
                        .HasColumnName("EMAIL");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("IS_ACTIVE")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("MODIFIED_AT");

                    b.HasKey("Id")
                        .HasName("PK__CUSTOMER__3214EC27192B8D23");

                    b.ToTable("CUSTOMERS");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.GbavsUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TerminalId");

                    b.ToTable("GBAVS_USERS");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Paymentmthd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("IS_ACTIVE")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("MODIFIED_AT");

                    b.Property<string>("PmtdName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("PMTD_NAME");

                    b.HasKey("Id")
                        .HasName("PK__PAYMENTM__3214EC27790F6D2C");

                    b.ToTable("PAYMENTMTHD");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("IS_ACTIVE")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("MODIFIED_AT");

                    b.Property<string>("ProductName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("PRODUCT_NAME");

                    b.HasKey("Id")
                        .HasName("PK__PRODUCTS__3214EC27149EBD54");

                    b.HasIndex(new[] { "ProductName" }, "UQ__PRODUCTS__6FDD6CA311B83EC2")
                        .IsUnique()
                        .HasFilter("[PRODUCT_NAME] IS NOT NULL");

                    b.ToTable("PRODUCTS");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Terminal", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("IS_ACTIVE")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("MODIFIED_AT");

                    b.Property<string>("TerminalLoc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TERMINAL_LOC");

                    b.Property<string>("TerminalName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TERMINAL_NAME");

                    b.HasKey("Id")
                        .HasName("PK__TERMINAL__3214EC2781EFF9AF");

                    b.HasIndex(new[] { "TerminalName" }, "UQ__TERMINAL__71A915CC35D06BB8")
                        .IsUnique();

                    b.ToTable("TERMINALS");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AircraftType")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("AIRCRAFT_TYPE");

                    b.Property<int?>("ApiDensity")
                        .HasColumnType("int")
                        .HasColumnName("API_DENSITY");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CUSTOMER_ID");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time")
                        .HasColumnName("END_TIME");

                    b.Property<string>("FFrom")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("F_FROM");

                    b.Property<string>("FTo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("F_TO");

                    b.Property<string>("FlightNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("FLIGHT_NO");

                    b.Property<TimeSpan>("InitTime")
                        .HasColumnType("time")
                        .HasColumnName("INIT_TIME");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("IS_ACTIVE")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsClearAndBright")
                        .HasColumnType("bit")
                        .HasColumnName("IS_CLEAR_AND_BRIGHT");

                    b.Property<bool>("IsParticleFree")
                        .HasColumnType("bit")
                        .HasColumnName("IS_PARTICLE_FREE");

                    b.Property<bool>("IsWaterFree")
                        .HasColumnType("bit")
                        .HasColumnName("IS_WATER_FREE");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("MODIFIED_AT");

                    b.Property<string>("OperatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("OPERATOR_ID");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ORDER_DATE")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("PaymentMthdId")
                        .HasColumnType("int")
                        .HasColumnName("PAYMENT_MTHD_ID");

                    b.Property<int?>("PitNo")
                        .HasColumnType("int")
                        .HasColumnName("PIT_NO");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("PRODUCT_ID");

                    b.Property<decimal>("ProductQty")
                        .HasColumnType("decimal(38, 4)")
                        .HasColumnName("PRODUCT_QTY");

                    b.Property<string>("TailNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("TAIL_NO");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("TEMPERATURE");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int")
                        .HasColumnName("TERMINAL_ID");

                    b.Property<string>("TicketNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("TICKET_NO");

                    b.Property<string>("UnitNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("UNIT_NO");

                    b.HasKey("Id")
                        .HasName("PK_TICKET_ID");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OperatorId");

                    b.HasIndex("PaymentMthdId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TerminalId");

                    b.HasIndex(new[] { "TicketNo" }, "UQ__TICKETS__7F5E594C4B78BCDC")
                        .IsUnique();

                    b.ToTable("TICKETS");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.GbavsUser", b =>
                {
                    b.HasOne("GbAviationTicketApi.Models.Terminal", "Terminal")
                        .WithMany("Users")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Ticket", b =>
                {
                    b.HasOne("GbAviationTicketApi.Models.Customer", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_TICKET_CUST");

                    b.HasOne("GbAviationTicketApi.Models.GbavsUser", "Operator")
                        .WithMany("Tickets")
                        .HasForeignKey("OperatorId")
                        .IsRequired()
                        .HasConstraintName("FK_TICKET_USER");

                    b.HasOne("GbAviationTicketApi.Models.Paymentmthd", "PaymentMthd")
                        .WithMany("Tickets")
                        .HasForeignKey("PaymentMthdId")
                        .IsRequired()
                        .HasConstraintName("FK_TICKET_PAYMTHD");

                    b.HasOne("GbAviationTicketApi.Models.Product", "Product")
                        .WithMany("Tickets")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_TICKET_PROD");

                    b.HasOne("GbAviationTicketApi.Models.Terminal", "Terminal")
                        .WithMany("Tickets")
                        .HasForeignKey("TerminalId")
                        .IsRequired()
                        .HasConstraintName("FK_TICKET_TERMINAL");

                    b.Navigation("Customer");

                    b.Navigation("Operator");

                    b.Navigation("PaymentMthd");

                    b.Navigation("Product");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Customer", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.GbavsUser", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Paymentmthd", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Product", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("GbAviationTicketApi.Models.Terminal", b =>
                {
                    b.Navigation("Tickets");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
