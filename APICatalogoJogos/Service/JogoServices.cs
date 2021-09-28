using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalogoJogos.Exceptions;


namespace APICatalogoJogos
{
    public class JogoServices : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoServices(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task ApagarJogo(Guid idJogo)
        {
            var entidadejogo = await _jogoRepository.Obter(idJogo);

            if (entidadejogo == null)
            {
                throw new JogoNaoCadastradoEx();
            }            

            await _jogoRepository.ApagarJogo(idJogo);
        }
    
        public async Task AtualizarJogo(Guid idJogo, JogoInputModel jogo)
        {
            var entidadejogo = await _jogoRepository.Obter(idJogo);

            if (entidadejogo == null) {
                throw new JogoNaoCadastradoEx();
            }

            entidadejogo.nome      = jogo.nome;
            entidadejogo.produtora = jogo.produtora;
            entidadejogo.preco     = jogo.preco;

            await _jogoRepository.AtualizarJogo(entidadejogo);
        }

        public async Task AtualizarJogo(Guid idJogo, double preco)
        {
            var entidadejogo = await _jogoRepository.Obter(idJogo);

            if (entidadejogo == null)
            {
                throw new JogoNaoCadastradoEx();
            }
                        
            entidadejogo.preco = preco;

            await _jogoRepository.AtualizarJogo(entidadejogo);
        }

        public async Task<JogoViewModel> InserirJogo(JogoInputModel jogo)
        {
            var jogoExistente = await _jogoRepository.Obter(jogo.nome, jogo.produtora);

            if (jogoExistente.Count > 1)
            {
                 throw new JogoCadastradoEx();
            }

            var novoJogo = new Jogo
            {
                Id = Guid.NewGuid(),
                nome = jogo.nome,
                produtora = jogo.produtora,
                preco = jogo.preco
            };

            await _jogoRepository.InserirJogo(novoJogo);

            return new JogoViewModel
            {
                id = novoJogo.Id,
                nome = novoJogo.nome,
                produtora = novoJogo.produtora,
                preco = novoJogo.preco
            };

        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                id = jogo.Id,
                nome = jogo.nome,
                produtora = jogo.produtora,
                preco = jogo.preco
            }).ToList();            
        }

        public async Task<JogoViewModel> Obter(Guid IdJogo)
        {
            var jogo = await _jogoRepository.Obter(IdJogo);

            if (jogo == null)
            {
                return null;
            }

            return new JogoViewModel
            {
                id = jogo.Id,
                nome = jogo.nome,
                produtora = jogo.produtora,
                preco = jogo.preco
            };
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
