using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public sealed class DbSession : IDisposable, IDbSession
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }


        public DbSession(DatabaseConfig databaseConfig)
        {

            Connection = new SqliteConnection(databaseConfig.Name);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();

        public void Open()
        {
            Connection.Open(); 
        }
    }
}
