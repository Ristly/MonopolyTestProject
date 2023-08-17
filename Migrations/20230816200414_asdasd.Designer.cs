﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonopolyTest;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MonopolyTest.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230816200414_asdasd")]
    partial class asdasd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MonopolyTest.Models.DBModels.Box", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Depth")
                        .HasColumnType("double precision");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<int?>("PalletID")
                        .HasColumnType("integer");

                    b.Property<double>("Volume")
                        .HasColumnType("double precision");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.HasIndex("PalletID");

                    b.ToTable("Boxes");
                });

            modelBuilder.Entity("MonopolyTest.Models.DBModels.Pallet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<double>("Depth")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<double?>("Volume")
                        .HasColumnType("double precision");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.ToTable("Pallets");
                });

            modelBuilder.Entity("MonopolyTest.Models.DBModels.Box", b =>
                {
                    b.HasOne("MonopolyTest.Models.DBModels.Pallet", "Pallet")
                        .WithMany("Boxes")
                        .HasForeignKey("PalletID");

                    b.Navigation("Pallet");
                });

            modelBuilder.Entity("MonopolyTest.Models.DBModels.Pallet", b =>
                {
                    b.Navigation("Boxes");
                });
#pragma warning restore 612, 618
        }
    }
}
