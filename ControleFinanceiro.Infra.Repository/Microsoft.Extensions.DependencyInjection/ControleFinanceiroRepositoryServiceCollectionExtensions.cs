using System;
using System.Data;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ControleFinanceiroRepositoryServiceCollectionExtensions
	{
        public static IServiceCollection AddControleFinanceiroRepository(this IServiceCollection services, RepositoryConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddScoped<ICategoriaReadOnlyRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaWriteOnlyRepository, CategoriaRepository>();
            services.AddScoped<ISubCategoriaReadOnlyRepository, SubCategoriaRepository>();
            services.AddScoped<ISubCategoriaWriteOnlyRepository, SubCategoriaRepository>();
            services.AddScoped<ILancamentoReadOnlyRepository, LancamentoRepository>();
            services.AddScoped<ILancamentoWriteOnlyRepository, LancamentoRepository>();

            services.AddScoped<IDbConnection>(d =>
            {
                return new MySqlConnection(configuration.SqlConnectionString);
            });

            return services;
        }
    }
}

