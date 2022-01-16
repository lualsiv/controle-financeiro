using System;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otc.DomainBase.Exceptions;
using Xunit;

namespace ControleFinanceiro.Application.Test
{
	public class CategoriaServiceTest
	{
		private readonly ServiceProvider serviceProvider;
        private readonly int idCategoria = 1;

		public CategoriaServiceTest()
		{
			var categoriaReadOnlyRepoMock = new Mock<ICategoriaReadOnlyRepository>();
			var categoriaWriteOnlyRepoMock = new Mock<ICategoriaWriteOnlyRepository>();

            CategoriaReadOnlyRepositoryMock(categoriaReadOnlyRepoMock);
			CategoriaWriteRepositoryMock(categoriaWriteOnlyRepoMock);

			IServiceCollection services = new ServiceCollection();

			services.AddLogging();
			services.AddTransient(t => categoriaReadOnlyRepoMock.Object);
			services.AddTransient(t => categoriaWriteOnlyRepoMock.Object);
			

			services.AddProjectModelCoreApplication();

			serviceProvider = services.BuildServiceProvider();
		}

        #region Mock Fake Repositories
        private void CategoriaReadOnlyRepositoryMock(Mock<ICategoriaReadOnlyRepository> categoriaReadOnlyRepoMock)
        {
            categoriaReadOnlyRepoMock
                .Setup(s => s.CategoriaExisteAsync(1))
                .ReturnsAsync(true);

            categoriaReadOnlyRepoMock
                .Setup(s => s.RetornaCategoriaAsync(1))
                .ReturnsAsync(new Domain.Models.CategoriaEntity
                {
                    id = 1,
                    nome = "Transporte"
                });

            categoriaReadOnlyRepoMock
                .Setup(s => s.RetornaCategoriaAsync(2))
                .ReturnsAsync(new Domain.Models.CategoriaEntity
                {
                    id = 2,
                    nome = "Freelancer"
                });

        }

        private void CategoriaWriteRepositoryMock(Mock<ICategoriaWriteOnlyRepository> categoriaWriteOnlyRepoMock)
        {
            categoriaWriteOnlyRepoMock
                .Setup(s => s.IncluiCategoriaAsync(It.IsAny<CategoriaEntity>()))
                .Returns(Task.FromResult(new CategoriaEntity
                {
                    id = 1,
                    nome = "Transporte"
                }));

            
        }
        #endregion

        [Fact]
        public async Task AddCategoria_With_Success()
        {
            var categoriaService = serviceProvider.GetService<ICategoriaService>();

            var categoriaPost = new CategoriaEntity
            {
                nome = "Transporte"
            };

            await categoriaService.IncluiCategoriaAsync(categoriaPost);
            
            Assert.Equal("Transporte", categoriaPost.nome);
        }

        [Fact]
        public async Task Add_Categoria_With_Invalid_Name()
        {
            var categoriaService = serviceProvider.GetService<ICategoriaService>();

            var categoriaPost = new CategoriaEntity
            {
                nome = ""
            };

            await Assert.ThrowsAnyAsync<ModelValidationException>(() => categoriaService.IncluiCategoriaAsync(categoriaPost));
        }

        [Fact]
        public async Task Get_Client_By_ClientId()
        {
            var categoriaService = serviceProvider.GetService<ICategoriaService>();

            var categoria = await categoriaService.RetornaCategoriaAsync(idCategoria);

            Assert.NotNull(categoria);
            Assert.IsType<CategoriaEntity>(categoria);
            Assert.Equal("Transporte", categoria.nome);
        }

        [Fact]
        public async Task Get_Client_By_Id_Not_Found()
        {
            var categoriaService = serviceProvider.GetService<ICategoriaService>();

            var categoria = await categoriaService.RetornaCategoriaAsync(5);

            Assert.Null(categoria);
        }
    }

}