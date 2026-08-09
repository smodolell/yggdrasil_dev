using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_PersonaConfiguration : IEntityTypeConfiguration<FI_Persona>
{
    public void Configure(EntityTypeBuilder<FI_Persona> builder)
    {
        builder.ToTable("FI_Persona");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Properties configuration
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .UseIdentityColumn();

        builder.Property(p => p.Identificador)
            .HasColumnName("Identificador")
            .HasMaxLength(16)
            .IsRequired(false);

        builder.Property(p => p.PerfilId)
            .HasColumnName("PerfilId")
            .IsRequired();

        builder.Property(p => p.TipoPersonaId)
            .HasColumnName("TipoPersonaId")
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(p => p.GeneroId)
            .HasColumnName("GeneroId")
            .IsRequired();

        builder.Property(p => p.EdoCivilId)
            .HasColumnName("EdoCivilId")
            .IsRequired();

        builder.Property(p => p.LugarNacimientoId)
            .HasColumnName("LugarNacimientoId")
            .HasMaxLength(2)
            .IsRequired(false);

        builder.Property(p => p.FechaRegistro)
            .HasColumnName("FechaRegistro")
            .IsRequired();

        builder.Property(p => p.PrimerNombre)
            .HasColumnName("PrimerNombre")
            .HasMaxLength(100);

        builder.Property(p => p.SegundoNombre)
            .HasColumnName("SegundoNombre")
            .HasMaxLength(100);

        builder.Property(p => p.ApellidoPaterno)
            .HasColumnName("ApellidoPaterno")
            .HasMaxLength(180);

        builder.Property(p => p.ApellidoMaterno)
            .HasColumnName("ApellidoMaterno")
            .HasMaxLength(180);

        builder.Property(p => p.RFC)
            .HasColumnName("RFC")
            .HasMaxLength(30); // Tu tabla original tiene VARCHAR(13), ajusta si es necesario

        builder.Property(p => p.CURP)
            .HasColumnName("CURP")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.NSS)
            .HasColumnName("NSS")
            .HasMaxLength(50); // Tu tabla original tiene BIGINT, ajusta tipo

        builder.Property(p => p.FechaNacimiento)
            .HasColumnName("FechaNacimiento");

        builder.Property(p => p.RazonSocial)
            .HasColumnName("RazonSocial")
            .HasMaxLength(150);

        builder.Property(p => p.FechaConstitucion)
            .HasColumnName("FechaConstitucion");

        builder.Property(p => p.FechaAltaCliente)
            .HasColumnName("FechaAltaCliente")
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasMaxLength(255);

        //// Relationships (Foreign Keys)
        builder.HasOne(p => p.FI_Perfil)
            .WithMany() // Si FI_Perfil tiene una colección de FI_Persona, ajusta aquí
            .HasForeignKey(p => p.PerfilId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CAT_TipoPersona)
            .WithMany()
            .HasForeignKey(p => p.TipoPersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CAT_Genero)
            .WithMany()
            .HasForeignKey(p => p.GeneroId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CAT_EdoCivil)
            .WithMany()
            .HasForeignKey(p => p.EdoCivilId)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(p => p.CAT_LugarNacimiento)
        //    .WithMany()
        //    .HasForeignKey(p => p.LugarNacimientoId)
        //    .OnDelete(DeleteBehavior.Restrict);

        // Relationships with collections
        builder.HasMany(p => p.FI_Credito)
            .WithOne(p => p.FI_Persona)
            .HasForeignKey(p => p.PersonaId) // Asume que FI_Credito tiene PersonaId
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.FI_Domicilio)
            .WithOne()
            .HasForeignKey(p => p.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.FI_Telefono)
            .WithOne()
            .HasForeignKey(p => p.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.FI_PersonaPerfil)
            .WithOne()
            .HasForeignKey(p => p.PersonaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índices (opcional, basado en tus necesidades)
        builder.HasIndex(p => p.RFC).HasDatabaseName("IX_Persona_RFC");
        builder.HasIndex(p => p.CURP).HasDatabaseName("IX_Persona_CURP");
        builder.HasIndex(p => p.PerfilId).HasDatabaseName("IX_Persona_PerfilId");
        builder.HasIndex(p => p.TipoPersonaId).HasDatabaseName("IX_Persona_TipoPersonaId");
        builder.HasIndex(p => p.GeneroId).HasDatabaseName("IX_Persona_GeneroId");
        builder.HasIndex(p => p.Email).HasDatabaseName("IX_Persona_Email");
    }
}