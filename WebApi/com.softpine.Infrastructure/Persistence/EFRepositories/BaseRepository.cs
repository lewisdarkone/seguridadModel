using Microsoft.EntityFrameworkCore;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.infrastructure.Persistence.EFContexts;

namespace com.softpine.muvany.infrastructure.Persistence.EFRepositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly LenderesContext _context;
        /// <summary>
        /// 
        /// </summary>
        protected readonly DbSet<T> _entities;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(LenderesContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return _entities.AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public async Task UpdateAsync(T entity)
        {

            Task.Run(() => { _entities.Update(entity); });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
        }
    }
}
