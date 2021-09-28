using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoJogos
{
    public interface IJogoService :IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);

        Task<JogoViewModel> Obter(Guid IdJogo);

        Task<JogoViewModel> InserirJogo(JogoInputModel jogo);

        Task AtualizarJogo(Guid idJogo, JogoInputModel jogo);

        Task AtualizarJogo(Guid idJogo, double preco);

        Task ApagarJogo(Guid idJogo);       
    }
}
