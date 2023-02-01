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
    [Migration("20230127111849_addedWIdStatus")]
    partial class addedWIdStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WorkitemTst.Models.Sample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("identifier")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SampleSet");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("WITypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WITypeId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Relation")
                        .HasColumnType("int");

                    b.Property<int>("TargetWorkitemId")
                        .HasColumnType("int");

                    b.Property<int>("WorkitemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TargetWorkitemId")
                        .IsUnique();

                    b.HasIndex("WorkitemId");

                    b.ToTable("WIRelation");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("InternalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkitemTypes");
                });

            modelBuilder.Entity("WorkitemTst.Models.WITypeRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Relation")
                        .HasColumnType("int");

                    b.Property<int>("TargetWITypeId")
                        .HasColumnType("int");

                    b.Property<int>("WITypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TargetWITypeId")
                        .IsUnique();

                    b.HasIndex("WITypeId");

                    b.ToTable("TypeRelations");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIValue", b =>
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

                    b.ToTable("WIValue");
                });

            modelBuilder.Entity("WorkitemTst.Models.Workitem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("WITypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WITypeId");

                    b.ToTable("Workitems");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIField", b =>
                {
                    b.HasOne("WorkitemTst.Models.WIType", null)
                        .WithMany("Fields")
                        .HasForeignKey("WITypeId");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIRelation", b =>
                {
                    b.HasOne("WorkitemTst.Models.Workitem", "TargetWorkitem")
                        .WithOne()
                        .HasForeignKey("WorkitemTst.Models.WIRelation", "TargetWorkitemId");

                    b.HasOne("WorkitemTst.Models.Workitem", "SourceWorkitem")
                        .WithMany("Relations")
                        .HasForeignKey("WorkitemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceWorkitem");

                    b.Navigation("TargetWorkitem");
                });

            modelBuilder.Entity("WorkitemTst.Models.WITypeRelation", b =>
                {
                    b.HasOne("WorkitemTst.Models.WIType", "TargetWIType")
                        .WithOne()
                        .HasForeignKey("WorkitemTst.Models.WITypeRelation", "TargetWITypeId");

                    b.HasOne("WorkitemTst.Models.WIType", "SourcetWIType")
                        .WithMany("Relations")
                        .HasForeignKey("WITypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourcetWIType");

                    b.Navigation("TargetWIType");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIValue", b =>
                {
                    b.HasOne("WorkitemTst.Models.WIField", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkitemTst.Models.Workitem", null)
                        .WithMany("Values")
                        .HasForeignKey("WorkitemId");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("WorkitemTst.Models.Workitem", b =>
                {
                    b.HasOne("WorkitemTst.Models.WIType", "WIType")
                        .WithMany()
                        .HasForeignKey("WITypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WIType");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIType", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("Relations");
                });

            modelBuilder.Entity("WorkitemTst.Models.Workitem", b =>
                {
                    b.Navigation("Relations");

                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
