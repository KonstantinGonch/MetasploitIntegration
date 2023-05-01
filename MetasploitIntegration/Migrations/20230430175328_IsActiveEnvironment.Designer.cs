﻿// <auto-generated />
using MetasploitIntegration.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MetasploitIntegration.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20230430175328_IsActiveEnvironment")]
    partial class IsActiveEnvironment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MetasploitIntegration.Models.Environment", b =>
                {
                    b.Property<long>("EnvironmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("EnvironmentName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("EnvironmentId");

                    b.ToTable("Environments");
                });

            modelBuilder.Entity("MetasploitIntegration.Models.Resource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("EnvironmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ResourceReference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("MetasploitIntegration.Models.Resource", b =>
                {
                    b.HasOne("MetasploitIntegration.Models.Environment", "Environment")
                        .WithMany()
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");
                });
#pragma warning restore 612, 618
        }
    }
}
