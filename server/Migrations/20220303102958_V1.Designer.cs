﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace server.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220303102958_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Igrac", b =>
                {
                    b.Property<int>("IgracID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Asistencije")
                        .HasColumnType("int");

                    b.Property<int>("GodinaRodjenja")
                        .HasColumnType("int");

                    b.Property<int>("Golovi")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("KlubID")
                        .HasColumnType("int");

                    b.Property<string>("Nacionalnost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IgracID");

                    b.HasIndex("KlubID");

                    b.ToTable("Igrac");
                });

            modelBuilder.Entity("Models.Klub", b =>
                {
                    b.Property<int>("KlubID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("SezonaID")
                        .HasColumnType("int");

                    b.HasKey("KlubID");

                    b.HasIndex("SezonaID");

                    b.ToTable("Klub");
                });

            modelBuilder.Entity("Models.Sezona", b =>
                {
                    b.Property<int>("SezonaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Godina")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("SezonaID");

                    b.ToTable("Sezona");
                });

            modelBuilder.Entity("Models.Sudija", b =>
                {
                    b.Property<int>("SudijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SudijaID");

                    b.ToTable("Sudija");
                });

            modelBuilder.Entity("Models.Utakmica", b =>
                {
                    b.Property<int>("MecID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("DomacinKlubID")
                        .HasColumnType("int");

                    b.Property<int?>("GostKlubID")
                        .HasColumnType("int");

                    b.Property<int>("Kolo")
                        .HasColumnType("int");

                    b.Property<int>("SezonaID")
                        .HasColumnType("int");

                    b.Property<int?>("SudijaID")
                        .HasColumnType("int");

                    b.Property<int>("golovi_domacin")
                        .HasColumnType("int");

                    b.Property<int>("golovi_gost")
                        .HasColumnType("int");

                    b.HasKey("MecID");

                    b.HasIndex("DomacinKlubID");

                    b.HasIndex("GostKlubID");

                    b.HasIndex("SezonaID");

                    b.HasIndex("SudijaID");

                    b.ToTable("Utakmica");
                });

            modelBuilder.Entity("Models.Igrac", b =>
                {
                    b.HasOne("Models.Klub", "Klub")
                        .WithMany("Igraci")
                        .HasForeignKey("KlubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klub");
                });

            modelBuilder.Entity("Models.Klub", b =>
                {
                    b.HasOne("Models.Sezona", null)
                        .WithMany("Klubovi")
                        .HasForeignKey("SezonaID");
                });

            modelBuilder.Entity("Models.Utakmica", b =>
                {
                    b.HasOne("Models.Klub", "Domacin")
                        .WithMany()
                        .HasForeignKey("DomacinKlubID");

                    b.HasOne("Models.Klub", "Gost")
                        .WithMany()
                        .HasForeignKey("GostKlubID");

                    b.HasOne("Models.Sezona", "Sezona")
                        .WithMany("Utakmice")
                        .HasForeignKey("SezonaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Sudija", "sudija")
                        .WithMany()
                        .HasForeignKey("SudijaID");

                    b.Navigation("Domacin");

                    b.Navigation("Gost");

                    b.Navigation("Sezona");

                    b.Navigation("sudija");
                });

            modelBuilder.Entity("Models.Klub", b =>
                {
                    b.Navigation("Igraci");
                });

            modelBuilder.Entity("Models.Sezona", b =>
                {
                    b.Navigation("Klubovi");

                    b.Navigation("Utakmice");
                });
#pragma warning restore 612, 618
        }
    }
}