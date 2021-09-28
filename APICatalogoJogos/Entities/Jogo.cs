using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoJogos
{
    public class Jogo
    {
        public Guid Id { get; set; }

        public string nome { get; set; }

        public string produtora { get; set; }

        public double preco { get; set; }
    }
}
