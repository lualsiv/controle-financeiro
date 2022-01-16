using System;
using ControleFinanceiro.Application.Services;
using ControleFinanceiro.Domain.Services;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ApplicationServiceCollectionExtensions
	{
        public static IServiceCollection AddProjectModelCoreApplication(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ISubCategoriaService, SubCategoriaService>();
            services.AddScoped<ILancamentoService, LancamentoService>();
            services.AddScoped<IBalancoService, BalancoService>();

            return services;
        }
    }
}

