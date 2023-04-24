using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Events;
using com.softpine.muvany.infrastructure.Identity.Entities;
using com.softpine.muvany.infrastructure.Identity.Models;
using com.softpine.muvany.models.Interfaces;
using com.softpine.muvany.models.Entities.EntitiesIdentity;
using com.softpine.muvany.models.Contracts;

namespace com.softpine.muvany.infrastructure.Identity.Context;

/// <summary>
/// 
/// </summary>
public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, ApplicationRoleClaim, IdentityUserToken<string>>
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly ICurrentUser _currentUser;
    private readonly ISerializerService _serializer;
    private readonly DatabaseSettings _dbSettings;
    private readonly IEventPublisher _events;

    /// <summary>
    /// Constructor para la inyeccion de dependencias
    /// </summary>
    /// <param name="options"></param>
    /// <param name="currentUser"></param>
    /// <param name="serializer"></param>
    /// <param name="dbSettings"></param>
    /// <param name="events"></param>
    public IdentityContext(DbContextOptions<IdentityContext> options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(options)
    {
        _currentUser = currentUser;
        _serializer = serializer;
        _dbSettings = dbSettings.Value;
        _events = events;
    }

    // Used by Dapper
    /// <summary>
    /// 
    /// </summary>
    public IDbConnection Connection => Database.GetDbConnection();

    /// <summary>
    /// 
    /// </summary>
    public DbSet<AuditLogsIdentity> AuditTrails => Set<AuditLogsIdentity>();


    #region DbSets generados
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


    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // QueryFilters need to be applied before base.OnModelCreating
        //modelBuilder.AppendGlobalQueryFilter<ISoftDelete>(s => s.DeletedOn == null);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationRole>(b =>
        {
            b.ToTable("Tbl_Seg_Roles");;
        });

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.ToTable("Tbl_Seg_Usuarios");
        });

        modelBuilder.Entity<IdentityUserClaim<string>>(b =>
        {
            b.ToTable("Tbl_Seg_UsuarioClaims");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(b =>
        {
            b.ToTable("Tbl_Seg_UsuarioLogins");
        });

        modelBuilder.Entity<IdentityUserToken<string>>(b =>
        {
            b.ToTable("Tbl_Seg_UsuarioTokens");
        });

        modelBuilder.Entity<ApplicationRoleClaim>(b =>
        {
            b.ToTable("Tbl_Seg_RolesClaims");

        });

        modelBuilder.Entity<IdentityUserRole<string>>(b =>
        {
            b.ToTable("Tbl_Seg_UsuarioRoles");
        });

        #region Configuraciones generadas
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

            entity.HasIndex(e => e.EndpointId, "IX_Tbl_Tas_Seg_EndpointsPermisos")
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

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(450);
            entity.HasOne(e => e.ModuloPadreNav)
            .WithMany()
            .HasForeignKey(e => e.ModuloPadre);
        });

        modelBuilder.Entity<Permisos>(entity =>
        {
            entity.ToTable("Tbl_Seg_Permisos");

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

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(450);

            entity.HasOne(d => d.IdModuloNavigation)
                .WithMany(p => p.Recursos)
                .HasForeignKey(d => d.IdModulo)
                .HasConstraintName("FK_Seg_Recursos_Seg_Modulos_Id");
        });
        #endregion

        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: We want this only for development probably... maybe better make it configurable in logger.json config?
        optionsBuilder.EnableSensitiveDataLogging();

        // If you want to see the sql queries that efcore executes:

        // Uncomment the next line to see them in the output window of visual studio
        // optionsBuilder.LogTo(m => Debug.WriteLine(m), LogLevel.Information);

        // Or uncomment the next line if you want to see them in the console
        // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {

        int result = await base.SaveChangesAsync(cancellationToken);

        await SendDomainEventsAsync();

        return result;
    }



    private async Task SendDomainEventsAsync()
    {
        var entitiesWithEvents = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToArray();

        foreach ( var entity in entitiesWithEvents )
        {
            var domainEvents = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach ( var domainEvent in domainEvents )
            {
                await _events.PublishAsync(domainEvent);
            }
        }
    }
}
