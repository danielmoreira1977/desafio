using System.Data;

namespace Questao5.Infrastructure.Database
{
    public interface IDbSession
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }

        void Dispose();
        void Open();
    }
}