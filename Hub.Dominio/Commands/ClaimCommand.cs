using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Dominio.Commands
{
    public class ClaimCommand
    {
        public string Email { get; set; } = string.Empty;
        public string Chave { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }
}
