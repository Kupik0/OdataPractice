using Microsoft.EntityFrameworkCore;
using ODATApractice.WebApi.Infrastructure.DbEntities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;

namespace ODATApractice.WebApi.Infrastructure.Context;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>()
            .HasMany<Student>(g => g.Students)
            .WithOne(s => s.Grade)
            .HasForeignKey(s => s.CurrentGradeId);
    }
}