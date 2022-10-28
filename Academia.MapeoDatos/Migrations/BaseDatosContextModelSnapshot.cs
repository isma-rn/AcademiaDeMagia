﻿// <auto-generated />
using System;
using Academia.MapeoDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Academia.MapeoDatos.Migrations
{
    [DbContext(typeof(BaseDatosContext))]
    partial class BaseDatosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Academia.MapeoDatos.Entidades.Afinidad", b =>
                {
                    b.Property<int>("AfinidadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AfinidadId"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AfinidadId");

                    b.ToTable("Afinidad", (string)null);
                });

            modelBuilder.Entity("Academia.MapeoDatos.Entidades.Estatus", b =>
                {
                    b.Property<int>("EstatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstatusId"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstatusId");

                    b.ToTable("Estatus", (string)null);
                });

            modelBuilder.Entity("Academia.MapeoDatos.Entidades.Estudiante", b =>
                {
                    b.Property<int>("EstudianteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstudianteId"), 1L, 1);

                    b.Property<int>("AfinidadId")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte>("Edad")
                        .HasColumnType("tinyint");

                    b.Property<int?>("GrimorioId")
                        .HasColumnType("int");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("EstudianteId");

                    b.HasIndex("AfinidadId");

                    b.HasIndex("GrimorioId");

                    b.ToTable("Estudiante", (string)null);
                });

            modelBuilder.Entity("Academia.MapeoDatos.Entidades.Grimorio", b =>
                {
                    b.Property<int>("GrimorioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GrimorioId"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("NumeroHojas")
                        .HasColumnType("int");

                    b.HasKey("GrimorioId");

                    b.ToTable("Grimorio", (string)null);
                });

            modelBuilder.Entity("Academia.MapeoDatos.Entidades.Solicitud", b =>
                {
                    b.Property<int>("SolicitudId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SolicitudId"), 1L, 1);

                    b.Property<DateTime>("Creacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("EstatusId")
                        .HasColumnType("int");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UltimaModificacion")
                        .HasColumnType("datetime2");

                    b.HasKey("SolicitudId");

                    b.HasIndex("EstatusId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Solicitud", (string)null);
                });

            modelBuilder.Entity("Academia.MapeoDatos.Entidades.Estudiante", b =>
                {
                    b.HasOne("Academia.MapeoDatos.Entidades.Afinidad", "Afinidad")
                        .WithMany()
                        .HasForeignKey("AfinidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academia.MapeoDatos.Entidades.Grimorio", "Grimorio")
                        .WithMany()
                        .HasForeignKey("GrimorioId");

                    b.Navigation("Afinidad");

                    b.Navigation("Grimorio");
                });

            modelBuilder.Entity("Academia.MapeoDatos.Entidades.Solicitud", b =>
                {
                    b.HasOne("Academia.MapeoDatos.Entidades.Estatus", "Estatus")
                        .WithMany()
                        .HasForeignKey("EstatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academia.MapeoDatos.Entidades.Estudiante", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estatus");

                    b.Navigation("Estudiante");
                });
#pragma warning restore 612, 618
        }
    }
}
