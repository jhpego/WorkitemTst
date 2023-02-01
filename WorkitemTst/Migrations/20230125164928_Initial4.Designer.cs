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
    [Migration("20230125164928_Initial4")]
    partial class Initial4
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

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("WIFormId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WIFormId");

                    b.ToTable("WIField");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("WIForm");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("WITypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WITypeId");

                    b.ToTable("WIRelation");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<string>("InternalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("WIType");
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

                    b.Property<int>("WITypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WITypeId");

                    b.ToTable("Workitems");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIField", b =>
                {
                    b.HasOne("WorkitemTst.Models.WIForm", null)
                        .WithMany("Fields")
                        .HasForeignKey("WIFormId");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIRelation", b =>
                {
                    b.HasOne("WorkitemTst.Models.WIType", null)
                        .WithMany("Relations")
                        .HasForeignKey("WITypeId");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIType", b =>
                {
                    b.HasOne("WorkitemTst.Models.WIForm", "Form")
                        .WithMany()
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
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

            modelBuilder.Entity("WorkitemTst.Models.WIForm", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("WorkitemTst.Models.WIType", b =>
                {
                    b.Navigation("Relations");
                });

            modelBuilder.Entity("WorkitemTst.Models.Workitem", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
