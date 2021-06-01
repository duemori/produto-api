using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Produto.Application.Command.Commands;
using Produto.Application.Query.DAOs;
using Produto.Application.Query.Queries;
using Produto.Application.Query.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produto.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProdutoDao _produtoDao;

        public ProdutoController(IMediator mediator, IProdutoDao produtoDao)
        {
            _mediator = mediator;
            _produtoDao = produtoDao;
        }

        /// <summary>Lista todos os produtos</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProdutoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Listar()
        {
            return Ok(await _produtoDao.ObterTodosAsync());
        }

        /// <summary>Busca um produto</summary>
        /// <param name="id">Identificador do produto</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(int id)
        {
            var query = new ProdutoQuery(id);

            return Ok(await _mediator.Send(query));
        }

        /// <summary>Cria um novo produto</summary>
        /// <param name="command">Informações do novo produto a ser criado</param>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Criar(CriarProdutoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>Atualiza um produto</summary>
        /// <param name="command">Identificador e novas informações do produto</param>
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Atualizar(AtualizarProdutoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>Remove um produto</summary>
        /// <param name="id">Identificador do produto</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Excluir(int id)
        {
            var command = new RemoverProdutoCommand(id);

            _ = await _mediator.Send(command);

            return NoContent();
        }
    }
}
