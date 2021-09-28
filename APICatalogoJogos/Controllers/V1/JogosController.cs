using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using APICatalogoJogos.Exceptions;


namespace APICatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {

        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            this._jogoService = jogoService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var result = await _jogoService.Obter(page,quantidade);
            return Ok(result);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>   
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid IdJogo)
        {
            var result = await _jogoService.Obter(IdJogo);

            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }


        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="jogoInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Caso o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response> 
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo( [FromBody]JogoInputModel jogo)
        {
            try
            {
                var jogo_n = await _jogoService.InserirJogo(jogo);

                return Ok(jogo_n);
            }
            catch (JogoCadastradoEx ex) {
                return UnprocessableEntity("Jogo já cadastrado para esta produtora.");
            }
            
        }


        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser atualizado</param>
        /// <param name="jogoInputModel">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Caso o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPut("{idJogo:guid}")]        
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo, [FromBody]JogoInputModel jogo)
        {
            try
            {
               await _jogoService.AtualizarJogo (idJogo, jogo);

                return Ok();
            }
            catch (JogoNaoCadastradoEx ex)
            {
                return NotFound("Jogo não localizado para atualização.");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid idJogo, [FromRoute]double preco)
        {
            try
            {
                await _jogoService.AtualizarJogo(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastradoEx ex)
            {
                return NotFound("Jogo não localizado para atualização de preço.");
            }
        }

        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser excluído</param>
        /// <response code="200">Caso ocorra a exclusão</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.ApagarJogo(idJogo);

                return Ok();
            }
            catch (JogoNaoCadastradoEx ex)
            {
                return NotFound("Jogo não localizado para remoção.");
            }
        }
    }
}

