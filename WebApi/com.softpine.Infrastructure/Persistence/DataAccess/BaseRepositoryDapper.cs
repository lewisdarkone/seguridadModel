using com.softpine.muvany.core.Interfaces;
using Dapper;
using System.Data;

namespace com.softpine.muvany.infrastructure.Persistence.DataAccess
{

    internal class BaseRepositoryDapper : IBaseRepositoryDapper
    {
        protected readonly IDatabaseConnectionFactory _database;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public BaseRepositoryDapper(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Este método ejecuta un query de varios elementos
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Una lista de objetos de tipo T</returns>
        public async Task<IEnumerable<T>> QueryTemplate<T>(string query)
        {
            using var conn = await _database.CreateDBConnectionAsync();
            return conn.Query<T>(query).ToList();
        }

        /// <summary>
        /// Este método ejecuta un store procedure 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns>Un objeto de tipo T</returns>
        public async Task<T> ExecuteSP<T>(string query, object param, string database = "")
        {

            var connDB = await _database.CreateDBConnectionAsync();
            try
            {
                var result = await connDB.QueryFirstAsync<T>(query, param, commandType: CommandType.StoredProcedure);
                connDB.Close();

                return result;

            }
            catch (Exception)
            {
                connDB.Close();
                throw;
            }
        }     

      

        
    }
}
