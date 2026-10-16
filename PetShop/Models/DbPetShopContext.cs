using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Models;

public partial class DbPetShopContext : DbContext
{
    public DbPetShopContext()
    {
    }

    public DbPetShopContext(DbContextOptions<DbPetShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<NomeCrmvEspecialidade> NomeCrmvEspecialidades { get; set; }

    public virtual DbSet<ReservaServico> ReservaServicos { get; set; }

    public virtual DbSet<Veterinario> Veterinarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConexaoSqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Cliente__06370DADA9ECDD2C");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Cpf, "UQ__Cliente__C1FF93092E05CBBA").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NomeCrmvEspecialidade>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Nome,Crmv,Especialidade");

            entity.Property(e => e.Crmv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Especialidade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReservaServico>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__ReservaS__06370DAD30AB311A");

            entity.ToTable("ReservaServico");

            entity.Property(e => e.DataHora).HasColumnType("datetime");
            entity.Property(e => e.StatusServico)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ReservaServicos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservaServico_Cliente");

            entity.HasOne(d => d.Veterinario).WithMany(p => p.ReservaServicos)
                .HasForeignKey(d => d.VeterinarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservaServico_Veterinario");
        });

        modelBuilder.Entity<Veterinario>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Veterina__06370DAD9B5C149D");

            entity.ToTable("Veterinario");

            entity.HasIndex(e => e.Crmv, "UQ__Veterina__AD6F9BA4387D94F2").IsUnique();

            entity.Property(e => e.Crmv)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Especialidade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
