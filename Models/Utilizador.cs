using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiMPAC.Models
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int PrefLotacao { get; set; }
        public bool PrefPiscina { get; set; }
        public bool PrefBungalow { get; set; }
        public bool PrefChurrasqueira { get; set; }
        public bool PrefEspDesportivo { get; set; }
        public int IdLocalizacao { get; set; }

        public Utilizador()
        {

        }
    }
}
