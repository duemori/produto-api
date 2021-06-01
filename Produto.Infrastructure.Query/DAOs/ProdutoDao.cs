using Dapper;
using Produto.Application.Query.DAOs;
using Produto.Application.Query.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Produto.Infrastructure.Query.DAOs
{
    public class ProdutoDao : IProdutoDao
    {
        private readonly IDbConnection _dbConnection;

        public ProdutoDao(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ProdutoViewModel> ObterPorIdAsync(int id)
        {
            var query = "SELECT Id, Nome, ValorVenda, Imagem FROM Produto WITH(NOLOCK) WHERE Id = @id";

            return await _dbConnection.QuerySingleAsync<ProdutoViewModel>(query, new { id });
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodosAsync()
        {
            var query = "SELECT Id, Nome, ValorVenda, Imagem FROM Produto WITH(NOLOCK)";

            return await _dbConnection.QueryAsync<ProdutoViewModel>(query);
        }

        public async Task<bool> VerificarSeCadastradoAsync(int id)
        {
            var query = "SELECT 1 FROM Produto WITH(NOLOCK) WHERE Id = @id";

            return await _dbConnection.ExecuteScalarAsync<bool>(query, new { id });
        }
    }
}
