using com.softpine.muvany.infrastructure.Identity.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace com.softpine.muvany.infrastructure.Persistence.DataAccess
{
    /// <summary>
    /// Interfaz para la creación de conexiones a bases de datos con sqlconnection
    /// </summary>
    public interface IDatabaseConnectionFactory
    {
        /// <summary>
        /// Tarea que crea una conexión a la base de datos Template
        /// </summary>
        Task<IDbConnection> CreateDBConnectionAsync();

    }

    /// <summary>
    /// Clase para la creación de conexiones a bases de datos con sqlconnection
    /// </summary>
    public class SqlConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly DatabaseSettings _databaseSettings;

        /// <summary>
        /// Constructor donde se inyecta el objeto con la información del connection strings de las bases de datos
        /// </summary>
        /// <param name="databaseSettings">Parámetro con la información del connection strings de las bases de datos/></param>
        public SqlConnectionFactory(DatabaseSettings databaseSettings) => _databaseSettings = databaseSettings ??
            throw new ArgumentNullException(nameof(DatabaseSettings));

        /// <summary>
        /// Método factory que crea una conexión a la base de datos Template
        /// </summary>
        public async Task<IDbConnection> CreateDBConnectionAsync()
        {
            //var sqlConnection = new SqlConnection(_databaseSettings.ConnectionString);
            var sqlConnection = new MySqlConnection(_databaseSettings.ConnectionString);
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }


        
    }
}
