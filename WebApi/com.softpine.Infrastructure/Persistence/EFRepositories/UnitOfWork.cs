
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Persistence.EFContexts;
using com.softpine.muvany.infrastructure.Persistence.EFRepositories;
using com.softpine.muvany.models.Entities.Subscripcion;
using com.softpine.muvany.models.EntitiesViews;

namespace com.softpine.muvany.infrastructure.Persistence.Repositories;

/// <summary>
/// Unidad de trabajo con las Entidades de los mantenimientos basicos
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly LenderesContext _context;
    private readonly IRepository<UvwUsuariosDominio> _uvwUsuariosDominioRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="uvwUsuariosDominioRepository"></param>
    public UnitOfWork(LenderesContext context, IRepository<UvwUsuariosDominio> uvwUsuariosDominioRepository)
    {

        _context = context;
        _uvwUsuariosDominioRepository = uvwUsuariosDominioRepository;
    }

   


    /// <summary>
    /// Repositorio usuario dominio
    /// </summary>
    public IRepository<UvwUsuariosDominio> UvwUsuariosDominioRepository => _uvwUsuariosDominioRepository ?? new BaseRepository<UvwUsuariosDominio>(_context);


    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        if ( _context != null )
        {
            _context.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task SaveChangesAsync(bool isSucceful = true)
    {
        await _context.SaveChangesAsync(isSucceful);
    }
}
