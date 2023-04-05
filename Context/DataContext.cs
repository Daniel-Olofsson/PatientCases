using Microsoft.EntityFrameworkCore;
using PatientCases.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Context;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\leon\source\repos\2023\Assigment\0331\PatientCases\Context\patientcasesdb0405_1.mdf;Integrated Security=True;Connect Timeout=30");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<CustomerEntity>(entity => entity.HasIndex(e => e.Email).IsUnique());
        modelBuilder.Entity<PatientEntity>()
        .HasMany(p => p.Cases)
        .WithOne(c => c.Patient)
        .HasForeignKey(c => c.PatientId)
        .OnDelete(DeleteBehavior.Restrict);

        // configure DoctorEntity
        modelBuilder.Entity<DoctorEntity>()
            .HasMany(d => d.Patients)
            .WithOne(p => p.Doctor)
            .HasForeignKey(p => p.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // configure CaseEntity
        modelBuilder.Entity<CaseEntity>()
            .HasMany(c => c.Comments)
            .WithOne(c => c.Case)
            .HasForeignKey(c => c.CaseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CaseEntity>()
            .HasOne(c => c.Patient)
            .WithMany(p => p.Cases)
            .HasForeignKey(c => c.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CaseEntity>()
            .HasOne(c => c.Doctor)
            .WithMany(d => d.Cases)
            .HasForeignKey(c => c.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CaseEntity>()
            .HasOne(c => c.Status)
            .WithMany(s => s.Cases)
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<CaseEntity> Cases { get ; set; }
    public DbSet<CommentEntity> Comments { get ; set; }
    public DbSet<PatientEntity> Patients { get ; set; }
    public DbSet<DoctorEntity> Doctors { get ; set; }
    public DbSet<StatusEntity> Statuses { get ; set; }
}
