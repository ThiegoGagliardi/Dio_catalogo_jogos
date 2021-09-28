using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoJogos
{
    public interface IJogoRepository :IDisposable
    {
        Task<List<Jogo>> Obter(int pagina, int quantidade);

        Task<Jogo> Obter(Guid IdJogo);

        Task<List<Jogo>> Obter(string nome, string produtora);

        Task InserirJogo(Jogo jogo);

        Task AtualizarJogo(Jogo jogo);               

        Task ApagarJogo(Guid idJogo);
    }
}
