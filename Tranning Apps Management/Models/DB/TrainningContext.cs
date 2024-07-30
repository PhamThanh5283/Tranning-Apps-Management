using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tranning_Apps_Management.Models.DB;

public partial class TrainningContext : DbContext
{
    public TrainningContext()
    {
    }

    public TrainningContext(DbContextOptions<TrainningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<UserInfor> UserInfors { get; set; }

    public virtual DbSet<UserSignup> UserSignups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK_Department_1");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
