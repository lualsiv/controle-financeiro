using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Repositories;
using Dapper;

namespace ControleFinanceiro.Infra.Repository
{
	public class LancamentoRepository : ILancamentoReadOnlyRepository, ILancamentoWriteOnlyRepository
	{
        private readonly IDbConnection dbConnection;

        public LancamentoRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public LancamentoRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task AtualizaLancamentoAsync(LancamentoEntity lancamento)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", lancamento.id);
            parameters.Add("valor", lancamento.valor);
            parameters.Add("subcategoria_id", lancamento.idSubCategoria);
            parameters.Add("comentario", lancamento.comentario);
            parameters.Add("data", lancamento.data);
            var queryCliente = @"UPDATE Lancamento SET valor = @valor, subcategoria_id = subcategoria_id, comentario = @comentario, data = @data WHERE Id = @Id";

            await dbConnection.ExecuteAsync(queryCliente, parameters);
        }

        public async Task ExcluiLancamentoAsync(int idLancamento)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", idLancamento);

            var query = @"delete from Lancamento Where Id = @Id";

            await dbConnection.ExecuteScalarAsync<int>(query, parameters);
        }

        public async Task IncluiLancamentoAsync(LancamentoEntity lancamento)
        {
            if (lancamento == null)
                throw new ArgumentNullException(nameof(lancamento));

            var parameters = new DynamicParameters();
            parameters.Add("valor", lancamento.valor);
            parameters.Add("subcategoria_id", lancamento.idSubCategoria, DbType.Decimal);
            parameters.Add("comentario", lancamento.comentario);
            parameters.Add("data", lancamento.data);

            var queryCliente = @"INSERT INTO Lancamento (valor, subcategoria_id, comentario, data) 
                                VALUES (@valor, @subcategoria_id, @comentario, @data)";

            await dbConnection.ExecuteAsync(queryCliente, parameters);
        }

        public async Task<bool> LancamentoExisteAsync(int idLancamento)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", idLancamento);

            var query = @"select count(Id) from Lancamento Where Id = @Id";

            var result = await dbConnection.ExecuteScalarAsync(query, parameters);

            return Convert.ToUInt16(result) > 0;
        }

        public async Task<IEnumerable<LancamentoEntity>> ListaLancamentosAsync()
        {
            var query = @"select id, valor, subcategoria_id as idSubCategoria, comentario, data, tipoLancamento from Lancamento";

            var result = await dbConnection.QueryAsync<LancamentoEntity>(query);

            return result;
        }

        public async Task<IEnumerable<LancamentoEntity>> ListaLancamentosDespesaAsync(BalancoFiltro filtro)
        {
            var parameters = new DynamicParameters();
            parameters.Add("dataInicio", filtro.dataInicio);
            parameters.Add("dataFim", filtro.dataFim);
            parameters.Add("idCategoria", filtro.idCategoria.HasValue ? filtro.idCategoria.Value : 0);
            var query = @"select
                                l.id
                                ,valor
                                ,subcategoria_id as idSubCategoria
                                ,comentario
                                ,data
                                ,tipoLancamento
                            from Lancamento l
                            inner join SubCategoria sc
                            on sc.id = l.subcategoria_id
                        Where (@dataInicio is null or CONVERT(l.data, DATE) > CONVERT(@dataInicio, DATE))
                        and (@dataFim is null or CONVERT(l.data, DATE) <= CONVERT(@dataFim, DATE))
                        and (@idCategoria = 0 or sc.categoria_id = @idCategoria)
                        and tipoLancamento = 2;";

            var result = await dbConnection.QueryAsync<LancamentoEntity>(query, parameters);

            return result;
        }

        public async Task<IEnumerable<LancamentoEntity>> ListaLancamentosReceitaAsync(BalancoFiltro filtro)
        {
            var parameters = new DynamicParameters();
            parameters.Add("dataInicio", filtro.dataInicio);
            parameters.Add("dataFim", filtro.dataFim);            
            var query = @"select
                                l.id
                                ,valor
                                ,subcategoria_id as idSubCategoria
                                ,comentario
                                ,data
                                ,tipoLancamento
                            from Lancamento l
                        Where (@dataInicio is null or CONVERT(l.data, DATE) > CONVERT(@dataInicio, DATE))
                        and (@dataFim is null or CONVERT(l.data, DATE) <= CONVERT(@dataFim, DATE))
                        and tipoLancamento = 1;";

            var result = await dbConnection.QueryAsync<LancamentoEntity>(query, parameters);

            return result;
        }

        public async Task<LancamentoEntity> RetornaLancamentoAsync(int idLancamento)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", idLancamento);

            var query = @"select id, valor, subcategoria_id as idSubCategoria, comentario, data, tipoLancamento from Lancamento Where Id = @Id";

            var categoria = await dbConnection.QueryAsync<LancamentoEntity>(query, parameters);

            return categoria.SingleOrDefault();
        }
    }
}

