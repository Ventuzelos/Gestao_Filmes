using System;
using System.Collections.Generic;
using System.Text;

namespace Gestao_Filmes.Domain
{
    public class Filme
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public int Ano { get; set; }

        public string Lingua { get; set; }

        public decimal Classificacao { get; set; }
    }
}
