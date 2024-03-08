﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

namespace Server.Migrations
{
    [DbContext(typeof(BioskopContext))]
    partial class BioskopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Server.Models.Bioskop", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("brojMestaUSalama")
                        .HasColumnType("int")
                        .HasColumnName("BrojMestaUSalama");

                    b.Property<int>("brojSala")
                        .HasColumnType("int")
                        .HasColumnName("BrojSala");

                    b.Property<string>("naziv")
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)")
                        .HasColumnName("Naziv");

                    b.HasKey("id");

                    b.ToTable("Bioskop");
                });

            modelBuilder.Entity("Server.Models.FormaBioskopa", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.HasKey("ID");

                    b.ToTable("FormaBioskopa");
                });

            modelBuilder.Entity("Server.Models.Sala", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("bioskopid")
                        .HasColumnType("int");

                    b.Property<int>("broj")
                        .HasColumnType("int")
                        .HasColumnName("Broj");

                    b.Property<int?>("formaBioskopaID")
                        .HasColumnType("int");

                    b.Property<string>("naziv")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Naziv");

                    b.HasKey("id");

                    b.HasIndex("bioskopid");

                    b.HasIndex("formaBioskopaID");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("Server.Models.SalaFilm", b =>
                {
                    b.Property<int>("salaID")
                        .HasColumnType("int")
                        .HasColumnName("SalaID");

                    b.Property<int>("filmID")
                        .HasColumnType("int")
                        .HasColumnName("FilmID");

                    b.HasKey("salaID", "filmID");

                    b.HasIndex("filmID");

                    b.ToTable("SalaFilm");
                });

            modelBuilder.Entity("Server.Models.SalaSediste", b =>
                {
                    b.Property<int>("salaID")
                        .HasColumnType("int")
                        .HasColumnName("SalaID");

                    b.Property<int>("sedisteID")
                        .HasColumnType("int")
                        .HasColumnName("SedisteID");

                    b.Property<int>("BrojSedista")
                        .HasColumnType("int")
                        .HasColumnName("BrojSedista");

                    b.HasKey("salaID", "sedisteID");

                    b.HasIndex("sedisteID");

                    b.ToTable("SalaSediste");
                });

            modelBuilder.Entity("server.Models.Film", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FormaBioskopaID")
                        .HasColumnType("int")
                        .HasColumnName("FormaBioskopaID");

                    b.Property<int>("cenaKarte")
                        .HasColumnType("int")
                        .HasColumnName("CenaKarte");

                    b.Property<string>("naziv")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Naziv");

                    b.HasKey("id");

                    b.HasIndex("FormaBioskopaID");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("server.Models.Sediste", b =>
                {
                    b.Property<int>("broj")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Broj")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FormaBioskopaID")
                        .HasColumnType("int")
                        .HasColumnName("FormaBioskopaID");

                    b.Property<bool>("zauzetost")
                        .HasColumnType("bit")
                        .HasColumnName("Zauzetost");

                    b.HasKey("broj");

                    b.ToTable("Sediste");
                });

            modelBuilder.Entity("Server.Models.FormaBioskopa", b =>
                {
                    b.HasOne("Server.Models.Bioskop", "bioskop")
                        .WithOne("formaBioskopa")
                        .HasForeignKey("Server.Models.FormaBioskopa", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bioskop");
                });

            modelBuilder.Entity("Server.Models.Sala", b =>
                {
                    b.HasOne("Server.Models.Bioskop", "bioskop")
                        .WithMany("sale")
                        .HasForeignKey("bioskopid");

                    b.HasOne("Server.Models.FormaBioskopa", "formaBioskopa")
                        .WithMany()
                        .HasForeignKey("formaBioskopaID");

                    b.Navigation("bioskop");

                    b.Navigation("formaBioskopa");
                });

            modelBuilder.Entity("Server.Models.SalaFilm", b =>
                {
                    b.HasOne("server.Models.Film", "film")
                        .WithMany("Veza")
                        .HasForeignKey("filmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Sala", "sala")
                        .WithMany("VEZA_Sala_Film")
                        .HasForeignKey("salaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("film");

                    b.Navigation("sala");
                });

            modelBuilder.Entity("Server.Models.SalaSediste", b =>
                {
                    b.HasOne("Server.Models.Sala", "sala")
                        .WithMany("VEZA_Sala_Sediste")
                        .HasForeignKey("salaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Models.Sediste", "sediste")
                        .WithMany("Veza")
                        .HasForeignKey("sedisteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("sala");

                    b.Navigation("sediste");
                });

            modelBuilder.Entity("server.Models.Film", b =>
                {
                    b.HasOne("Server.Models.FormaBioskopa", null)
                        .WithMany("Stavke")
                        .HasForeignKey("FormaBioskopaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.Bioskop", b =>
                {
                    b.Navigation("formaBioskopa");

                    b.Navigation("sale");
                });

            modelBuilder.Entity("Server.Models.FormaBioskopa", b =>
                {
                    b.Navigation("Stavke");
                });

            modelBuilder.Entity("Server.Models.Sala", b =>
                {
                    b.Navigation("VEZA_Sala_Film");

                    b.Navigation("VEZA_Sala_Sediste");
                });

            modelBuilder.Entity("server.Models.Film", b =>
                {
                    b.Navigation("Veza");
                });

            modelBuilder.Entity("server.Models.Sediste", b =>
                {
                    b.Navigation("Veza");
                });
#pragma warning restore 612, 618
        }
    }
}
