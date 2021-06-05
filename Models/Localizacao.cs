using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiMPAC.Models
{
    public class Localizacao
    {
        public int Id { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string CodPostal { get; set; }
        public int Porta { get; set; }
        public string Distrito { get; set; }

        public Localizacao()
        {
          
        }
    }
}
