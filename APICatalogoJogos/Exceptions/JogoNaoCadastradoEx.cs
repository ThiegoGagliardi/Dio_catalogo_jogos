using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoJogos.Exceptions
{
    public class JogoNaoCadastradoEx : Exception
    {
         public JogoNaoCadastradoEx()
        : base("Este jogo não está cadastrado")
        { }
        
    }
}
