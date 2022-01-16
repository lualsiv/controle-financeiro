using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using Dapper;

namespace ControleFinanceiro.Infra.Repository
{
    public class SubCategoriaRepository : ISubCategoriaReadOnlyRepository, ISubCategoriaWriteOnlyRepository
    {
        private readonly IDbConnection dbConnection;

        public SubCategoriaRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public SubCategoriaRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task AtualizaSubCategoriaAsync(SubCategoriaEntity subCategoria)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", subCategoria.id);
            parameters.Add("nome", subCategoria.nome);
            parameters.Add("categoria_id", subCategoria.idCategoria);
            var queryCliente = @"UPDATE SubCategoria SET nome = @nome , categoria_id = @categoria_id WHERE id = @id";

            await dbConnection.ExecuteAsync(queryCliente, parameters);
        }

        public async Task ExcluiSubCategoriaAsync(int idSubCategoria)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", idSubCategoria);

            var query = @"delete from SubCategoria Where Id = @Id";

            await dbConnection.ExecuteScalarAsync<int>(query, parameters);
        }

        public async Task IncluiSubCategoriaAsync(SubCategoriaEntity subCategoria)
        {
            if (subCategoria == null)
                throw new ArgumentNullException(nameof(subCategoria));

            var parameters = new DynamicParameters();
            parameters.Add("Nome", subCategoria.nome);
            parameters.Add("categoria_id", subCategoria.idCategoria);

            var queryCliente = @"INSERT INTO SubCategoria (Nome, categoria_id) 
                                VALUES (@Nome, @categoria_id)";

            await dbConnection.ExecuteAsync(queryCliente, parameters);
        }

        public async Task<IEnumerable<SubCategoriaEntity>> ListaSubCategoriasAsync()
        {
            var query = @"select Id, Nome, categoria_id as IdCategoria from SubCategoria";

            var result = await dbConnection.QueryAsync<SubCategoriaEntity>(query);

            return result;
        }

        public Task<SubCategoriaEntity> RetornaSubCategoriaAsync(int idSubCategoria)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SubCategoriaExisteAsync(int idSubCategoria)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", idSubCategoria);

            var query = @"select count(Id) from SubCategoria Where Id = @Id";

            var result = await dbConnection.ExecuteScalarAsync(query, parameters);

            return Convert.ToUInt16(result) > 0;
        }
    }
}

