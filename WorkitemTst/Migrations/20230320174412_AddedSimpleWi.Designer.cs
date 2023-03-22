﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkitemTst.Models;

#nullable disable

namespace WorkitemTst.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230320174412_AddedSimpleWi")]
    partial class AddedSimpleWi
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WorkitemTst.Entitys.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentAreaIdId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentAreaIdId");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Effort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkitemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkitemId")
                        .IsUnique();

                    b.ToTable("Effort");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Iteration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Iteration");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.SimpleWi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SimpleWi");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.SimpleWit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SimpleWit");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Transition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("InitialStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NextStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InitialStatusId");

                    b.HasIndex("NextStatusId");

                    b.ToTable("Transition");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkProject");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkProjectArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("WorkProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("WorkProjectId");

                    b.ToTable("WorkProjectArea");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkProjectIteration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IterationId")
                        .HasColumnType("int");

                    b.Property<int>("WorkProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IterationId");

                    b.HasIndex("WorkProjectId");

                    b.ToTable("WorkProjectIteration");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkProjectWorkitemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("WorkProjectId")
                        .HasColumnType("int");

                    b.Property<int>("WorkitemTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkProjectId");

                    b.HasIndex("WorkitemTypeId");

                    b.ToTable("WorkProjectWorkitemType");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Workflow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workflow");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Workitem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedMoment")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InteractionId")
                        .HasColumnType("int");

                    b.Property<int?>("IterationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("WorkitemTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("InteractionId");

                    b.HasIndex("StatusId");

                    b.HasIndex("WorkitemTypeId");

                    b.ToTable("Workitem");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldMode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("WorkitemTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkitemTypeId");

                    b.ToTable("WorkitemField");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SourceWorkitemId")
                        .HasColumnType("int");

                    b.Property<int>("TargetWorkitemId")
                        .HasColumnType("int");

                    b.Property<int>("WorkitemTypeRelationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SourceWorkitemId");

                    b.HasIndex("TargetWorkitemId");

                    b.HasIndex("WorkitemTypeRelationId");

                    b.ToTable("WorkitemRelation");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowId");

                    b.ToTable("WorkitemType");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemTypeRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RelationMode")
                        .HasColumnType("int");

                    b.Property<int>("SourceWorkitemTypeId")
                        .HasColumnType("int");

                    b.Property<int>("TargetWorkitemTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SourceWorkitemTypeId");

                    b.HasIndex("TargetWorkitemTypeId");

                    b.ToTable("WorkitemTypeRelation");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkitemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("WorkitemId");

                    b.ToTable("WorkitemValue");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Worklog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkitemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkitemId");

                    b.ToTable("Worklog");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Area", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Area", "ParentAreaId")
                        .WithMany()
                        .HasForeignKey("ParentAreaIdId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentAreaId");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Effort", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Workitem", null)
                        .WithOne("Effort")
                        .HasForeignKey("WorkitemTst.Entitys.Effort", "WorkitemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Iteration", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Iteration", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Status", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Transition", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Status", "InitialStatus")
                        .WithMany()
                        .HasForeignKey("InitialStatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WorkitemTst.Entitys.Status", "NextStatus")
                        .WithMany()
                        .HasForeignKey("NextStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("InitialStatus");

                    b.Navigation("NextStatus");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkProjectArea", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Entitys.WorkProject", "WorkProject")
                        .WithMany()
                        .HasForeignKey("WorkProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("WorkProject");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkProjectIteration", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Iteration", "Iteration")
                        .WithMany()
                        .HasForeignKey("IterationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Entitys.WorkProject", "WorkProject")
                        .WithMany()
                        .HasForeignKey("WorkProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Iteration");

                    b.Navigation("WorkProject");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkProjectWorkitemType", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.WorkProject", "WorkProject")
                        .WithMany()
                        .HasForeignKey("WorkProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Entitys.WorkitemType", "WorkitemType")
                        .WithMany()
                        .HasForeignKey("WorkitemTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("WorkProject");

                    b.Navigation("WorkitemType");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Workitem", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WorkitemTst.Entitys.Iteration", "Interaction")
                        .WithMany()
                        .HasForeignKey("InteractionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WorkitemTst.Entitys.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WorkitemTst.Entitys.WorkitemType", "WorkitemType")
                        .WithMany()
                        .HasForeignKey("WorkitemTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Interaction");

                    b.Navigation("Status");

                    b.Navigation("WorkitemType");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemField", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.WorkitemType", null)
                        .WithMany("Fields")
                        .HasForeignKey("WorkitemTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemRelation", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Workitem", "SourceWorkitem")
                        .WithMany()
                        .HasForeignKey("SourceWorkitemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Entitys.Workitem", "TargetWorkitem")
                        .WithMany()
                        .HasForeignKey("TargetWorkitemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Entitys.WorkitemTypeRelation", "Relation")
                        .WithMany()
                        .HasForeignKey("WorkitemTypeRelationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Relation");

                    b.Navigation("SourceWorkitem");

                    b.Navigation("TargetWorkitem");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemType", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemTypeRelation", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.WorkitemType", "SourcetWorkitemType")
                        .WithMany()
                        .HasForeignKey("SourceWorkitemTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Entitys.WorkitemType", "TargetWorkitemType")
                        .WithMany()
                        .HasForeignKey("TargetWorkitemTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SourcetWorkitemType");

                    b.Navigation("TargetWorkitemType");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemValue", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.WorkitemField", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Entitys.Workitem", null)
                        .WithMany("Values")
                        .HasForeignKey("WorkitemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Field");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Worklog", b =>
                {
                    b.HasOne("WorkitemTst.Entitys.Workitem", "Workitem")
                        .WithMany()
                        .HasForeignKey("WorkitemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Workitem");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.Workitem", b =>
                {
                    b.Navigation("Effort")
                        .IsRequired();

                    b.Navigation("Values");
                });

            modelBuilder.Entity("WorkitemTst.Entitys.WorkitemType", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}