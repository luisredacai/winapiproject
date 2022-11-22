using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiProject2.Models;

namespace ApiProject2.Context
{
    public partial class da8k2ujd2nc9e6Context : DbContext
    {
        public da8k2ujd2nc9e6Context()
        {
        }

        public da8k2ujd2nc9e6Context(DbContextOptions<da8k2ujd2nc9e6Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Archivo> Archivos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Equifax> Equifaxes { get; set; } = null!;
        public virtual DbSet<serializarJson> serializarJson { get; set; } = null!;
        public virtual DbSet<factu> factu { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasKey(e => e.IdAr)
                    .HasName("ARCHIVOS_PK");

                entity.ToTable("archivos");

                entity.Property(e => e.IdAr).HasColumnName("id_ar");

                entity.Property(e => e.ExtencionAr)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("extencion_ar");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Tamanio).HasColumnName("tamanio");

                entity.Property(e => e.Ubicacion)
                    .HasColumnType("text")
                    .HasColumnName("ubicacion");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("empleado");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Puesto)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("puesto");

                entity.Property(e => e.Sueldo).HasColumnName("sueldo");
            });

            modelBuilder.Entity<Equifax>(entity =>
            {
                entity.ToTable("EquifaxMed");

               entity.Property(e => e.Id).HasColumnName("Id");

               /* entity.Property(e => e.ApellidoMa)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ApeMat");

                entity.Property(e => e.ApellidoPa)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ApePat");
                 */
                /*entity.Property(e => e.Deuda)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();*/

                entity.Property(e => e.FechaCreacionRuc)
                    .HasColumnType("datetime")
                    .HasColumnName("CreaData");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nombre");

                entity.Property(e => e.Ruc).HasColumnName("Ruc")
                     .HasMaxLength(200)
                     .IsUnicode(false);
                entity.Property(e => e.RUC20).HasColumnName("Ruc20")
                     .HasMaxLength(200)
                     .IsUnicode(false);     
                entity.Property(e => e.dni).HasColumnName("Dni")
                     .HasMaxLength(200)
                     .IsUnicode(false);

                entity.Property(e => e.ScoreCrediticio).HasColumnName("Score");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
