using System.Collections.Generic;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
//using TodoApi.Models;

namespace WorkitemTst.Models
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
: base(options)
        {
        }

        public DbSet<Workitem> Workitems { get; set; } = null!;

        public DbSet<WIType> WorkitemTypes { get; set; } = null!;

        public DbSet<WIField> Fields { get; set; } = null!;

        public DbSet<Sample> SampleSet { get; set; } = null!;

        public DbSet<WITypeRelation> TypeRelations { get; set; } = null!;

        public DbSet<WIRelation> WIRelations { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WITypeRelation>()
                .HasOne(r => r.TargetWIType)
               .WithOne()
               .IsRequired(false)
               .HasForeignKey<WITypeRelation>(r => r.TargetWITypeId);

            modelBuilder.Entity<WIRelation>()
                .HasOne(r => r.TargetWorkitem)
               .WithOne()
               .IsRequired(false)
               .HasForeignKey<WIRelation>(r => r.TargetWorkitemId);

        }
    }

    
}

