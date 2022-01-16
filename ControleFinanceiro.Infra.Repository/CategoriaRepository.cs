using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using System.Linq;
using Dapper;

namespace ControleFinanceiro.Infra.Repository
{
    public class CategoriaRepository : ICategoriaReadOnlyRepository, ICategoriaWriteOnlyRepository
	{
        private readonly IDbConnection dbConnection;

        public CategoriaRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public CategoriaRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }        

        public async Task AtualizaCategoriaAsync(CategoriaEntity categoria)
        {
            var categoriaParams = new DynamicParameters();
            categoriaParams.Add("Id", categoria.id);
            categoriaParams.Add("Nome", categoria.nome);
            var queryCliente = @"UPDATE Categoria SET Nome = @Nome WHERE Id = @Id";

            await dbConnection.ExecuteAsync(queryCliente, categoriaParams);
        }

        public async Task<bool> CategoriaExisteAsync(int idCategoria)
        {
            var categoriaParams = new DynamicParameters();
            categoriaParams.Add("Id", idCategoria);

            var query = @"select count(Id) from Categoria Where Id = @Id";

            var result = await dbConnection.ExecuteScalarAsync(query, categoriaParams);

            return Convert.ToUInt16(result) > 0;
        }

        public async Task ExcluiCategoriaAsync(int idCategoria)
        {
            var categoriaParams = new DynamicParameters();
            categoriaParams.Add("Id", idCategoria);

            var query = @"delete from Categoria Where Id = @Id";

            await dbConnection.ExecuteScalarAsync<int>(query, categoriaParams);
        }

        public async Task IncluiCategoriaAsync(CategoriaEntity categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            var categoriaParams = new DynamicParameters();
            categoriaParams.Add("Nome", categoria.nome);

            var queryCliente = @"INSERT INTO Categoria (Nome) 
                                VALUES (@Nome)";

            await dbConnection.ExecuteAsync(queryCliente, categoriaParams);
        }

        public async Task<IEnumerable<CategoriaEntity>> ListaCategoriasAsync()
        {
            var query = @"select Id, Nome from Categoria";    

            var categorias = await dbConnection.QueryAsync<CategoriaEntity>(query);

            return categorias;
        }

        public async Task<CategoriaEntity> RetornaCategoriaAsync(int idCategoria)
        {
            var categoriaParams = new DynamicParameters();
            categoriaParams.Add("Id", idCategoria);

            var query = @"select Id, Nome from Categoria Where Id = @Id";

            var categoria = await dbConnection.QueryAsync<CategoriaEntity>(query, categoriaParams);

            return categoria.SingleOrDefault();
        }
    }
}

