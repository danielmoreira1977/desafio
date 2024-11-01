using Questao5.Application.Abstractions.Messaging;
using Questao5.Application.Idempotencia.Commands;
using Questao5.Domain.Repositories;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Extensions
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependenciesroup(
            this IServiceCollection services)
        {
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();
            services.AddScoped<IIdempotenciaService, IdempotenciaService>();
            services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbSession, DbSession>();


            return services;
        }
    }
}
