using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiMPAC.Models
{
    public class Parque
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double AvaliacaoMedia { get; set; }
        public int CapacidadeMax { get; set; }
        public int LotacaoAtual { get; set; }
        public bool TemPiscina { get; set; }
        public bool TemBungalow { get; set; }
        public bool TemChurrasqueira { get; set; }
        public bool TemEspacoDesportivo { get; set; }
        public int IdLocalizacao { get; set; }
        public Parque()
        {

        }

    }
}
