
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using com.softpine.muvany.models.Entities;

namespace com.softpine.muvany.infrastructure.Persistence.EntityConfig;

/// <summary>
/// Configuracion de Referencias usuarios
public class ReferenciaUsuariosConfig
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="entity"></param>
    public ReferenciaUsuariosConfig(EntityTypeBuilder<ReferenciaUsuarios> entity)
    {

        entity.ToTable("Tbl_Seg_Usuarios");


        entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

        entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            .IsUnique()
            .HasFilter("([NormalizedUserName] IS NOT NULL)");

        entity.Property(e => e.Email).HasMaxLength(256);

        entity.Property(e => e.ImagenUrl).HasColumnName("ImagenURL");

        entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

        entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

        entity.Property(e => e.RefreshTokenExpiryTime).HasColumnType("datetime");

        entity.Property(e => e.TipoIdentificacion).HasMaxLength(2);

        entity.Property(e => e.UserName).HasMaxLength(256);
    }
}
