using System.Collections.Generic;
using System.Text.Json.Nodes;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using WorkitemTst.Entitys;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
//using TodoApi.Models;

namespace WorkitemTst.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        //public DbSet<Sample> SampleSet { get; set; } = null!;
        public DbSet<Workitem> Workitem { get; set; } = null!;

        public DbSet<WorkitemType> WorkitemType { get; set; } = null!;

        public DbSet<WorkitemField> Fields { get; set; } = null!;

        public DbSet<WorkitemTypeRelation> WorkitemTypeRelation { get; set; } = null!;

        public DbSet<WorkitemRelation> WorkitemRelation { get; set; } = null!;

        public DbSet<Iteration> Iteration { get; set; } = null!;

        public DbSet<Worklog> Worklog { get; set; } = null!;


        public DbSet<Workflow> Workflow { get; set; } = null!;

        public DbSet<Status> Status { get; set; } = null!;

        public DbSet<WorkitemTst.Entitys.Transition> Transition { get; set; } = null!;

        public DbSet<WorkProject> WorkProject { get; set; } = null!;


        public DbSet<WorkProjectArea> WorkProjectArea { get; set; } = null!;

        public DbSet<WorkProjectIteration> WorkProjectIteration { get; set; } = null!;

        public DbSet<WorkProjectWorkitemType> WorkProjectWorkitemType { get; set; } = null!;

        public DbSet<Effort> Effort { get; set; } = null!;

        public DbSet<WorkitemValue> WorkitemValue { get; set; } = null!;

        public DbSet<WorkitemField> WorkitemField { get; set; } = null!;

        public DbSet<SimpleWit> SimpleWit { get; set; } = null!;

        public DbSet<SimpleWi> SimpleWi { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }



            //            modelBuilder.HasDbFunction(
            //    typeof(Database).GetMethod(nameof(JsonValue))!
            //).HasName("JSON_VALUE").IsBuiltIn();

            //            modelBuilder.HasDbFunction(
            //                typeof(Database).GetMethod(nameof(JsonQuery))!
            //            ).HasName("JSON_QUERY").IsBuiltIn();



            base.OnModelCreating(modelBuilder);






            //modelBuilder.Entity<WorkitemTypeRelation>()
            //    .HasOne(r => r.TargetWIType)
            //   .WithOne()
            //   .IsRequired(false)
            //   .HasForeignKey<WorkitemTypeRelation>(r => r.TargetWorkitemTypeId)
            //   .OnDelete(DeleteBehavior.Restrict)
            //   ;

            //modelBuilder.Entity<WorkitemRelation>()
            //    .HasOne(r => r.TargetWorkitem)
            //   .WithOne()
            //   .IsRequired(false)
            //   .HasForeignKey<WorkitemRelation>(r => r.TargetWorkitemId)
            //   .OnDelete(DeleteBehavior.Restrict)
            //;

        }

        //    public static string JsonValue(string column, [NotParameterized] string path)
        //=> throw new NotSupportedException();

        //    public static string JsonQuery(string column, [NotParameterized] string path) =>
        //        throw new NotSupportedException();
        //}

        [DbFunction("JSON_VALUE", IsBuiltIn = true, IsNullable = false)]
        public static string JsonValue(string expression, string path) => throw new NotImplementedException();


        [DbFunction("ISJSON", IsBuiltIn = true, IsNullable = false)]
        public static int IsJson(string expression) => throw new NotImplementedException();
    }

}

