using System;
using System.Collections.Generic;
using GbAviationTicketApi.Data.IData;
using GbAviationTicketApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GbAviationTicketApi.Data;

public partial class GbavsContext : IdentityDbContext, IGbavsContext
{
    public GbavsContext()
    {
    }

    public GbavsContext(DbContextOptions<GbavsContext> options) : base(options)
    {
    }
    public virtual DbSet<GenerateTicketReport_Result> GenerateTicketReport_Results { get; set; }
    public virtual DbSet<ReportSummary> TicketsReportSummaries { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Paymentmthd> Paymentmthds { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Terminal> Terminals { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public DbSet<GbavsUser> GbavsUsers { get; set; }
    public async Task SaveAsync()
    {
        await Database.BeginTransactionAsync();
        try
        {
            await SaveChangesAsync();
            await Database.CommitTransactionAsync();
        }
        catch (DbUpdateException)
        {
            //TODO: handle here
            await Database.RollbackTransactionAsync();
            throw;
        }
    } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:GBAVS");

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenerateTicketReport_Result>(entity =>
            entity.HasNoKey());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC27192B8D23");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Paymentmthd>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PAYMENTM__3214EC27790F6D2C");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCTS__3214EC27149EBD54");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Terminal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TERMINAL__3214EC2781EFF9AF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TICKET_ID");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_CUST");

            entity.HasOne(d => d.Operator).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_USER");

            entity.HasOne(d => d.PaymentMthd).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_PAYMTHD");

            entity.HasOne(d => d.Product).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_PROD");

            entity.HasOne(d => d.Terminal).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_TERMINAL");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
