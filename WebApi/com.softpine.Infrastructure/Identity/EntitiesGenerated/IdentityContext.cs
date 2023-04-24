using Microsoft.EntityFrameworkCore;
using com.softpine.muvany.models.Entities.EntitiesIdentity;

namespace com.softpine.muvany.infrastructure.Identity.EntitiesGenerated
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IdentityContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IdentityContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Acciones> Acciones { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Endpoints> Endpoints { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<EndpointsPermisos> EndpointsPermisos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Modulos> Modulos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Permisos> Permisos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Recursos> Recursos { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acciones>(entity =>
            {
                entity.ToTable("Tbl_Seg_Acciones");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Endpoints>(entity =>
            {
                entity.ToTable("Tbl_Seg_Endpoints");

                entity.Property(e => e.Controlador)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.HttpVerbo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Metodo)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EndpointsPermisos>(entity =>
            {
                entity.ToTable("Tbl_Seg_EndpointsPermisos");

                entity.HasIndex(e => e.EndpointId, "PK__Tbl_Seg___3214EC0720D62817")
                    .IsUnique();

                entity.HasOne(d => d.Endpoint)
                    .WithOne(p => p.EndpointsPermisos)
                    .HasForeignKey<EndpointsPermisos>(d => d.EndpointId);

                entity.HasOne(d => d.Permiso)
                    .WithMany(p => p.EndpointsPermisos)
                    .HasForeignKey(d => d.PermisoId);
            });

            modelBuilder.Entity<Modulos>(entity =>
            {
                entity.ToTable("Tbl_Seg_Modulos");

                entity.Property(e => e.Cssicon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CSSIcon");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Permisos>(entity =>
            {
                entity.ToTable("Tbl_Seg_Permisos");

                entity.HasIndex(e => new { e.IdAccion, e.IdRecurso })
                    .IsUnique();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdAccionNavigation)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.IdAccion)
                    .HasConstraintName("FK_Seg_Permisos_Seg_Acciones_Id");

                entity.HasOne(d => d.IdRecursoNavigation)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.IdRecurso)
                    .HasConstraintName("FK_Seg_Permisos_Seg_Recursos_Id");
            });

            modelBuilder.Entity<Recursos>(entity =>
            {
                entity.ToTable("Tbl_Seg_Recursos");

                entity.Property(e => e.DescripcionMenuConfiguracion).HasMaxLength(200);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.Recursos)
                    .HasForeignKey(d => d.IdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Seg_Recursos_Seg_Modulos_Id");
            });

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
