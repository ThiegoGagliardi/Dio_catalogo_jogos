using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace APICatalogoJogos
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength =3, ErrorMessage ="O nome do jogo deve conter mais de 3 caracteres e menos que 100.")]
        public string nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter mais de 3 caracteres e menos que 100.")]
        public string produtora { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage =" O valor do jogo deve ser superior a 0 e infrior a 1000 Reais.")]
        public double preco { get; set; }
    }
}

