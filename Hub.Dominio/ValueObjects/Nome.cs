using EDSCore;
using Hub.Dominio.Contratos;
using System.Text.RegularExpressions;

namespace Hub.Dominio.ValueObjects
{
    public record Nome : ValueObject
    {
        private Nome() { }

        public Nome(string texto) 
        {
            Texto = texto;
        }
        public Nome(string texto, int comprimentoMin, int comprimentoMax)
        {
            if (texto.Length < comprimentoMin || texto.Length > comprimentoMax)
            {
                throw new NomeInvalidoException(texto);
            }
            Texto = texto;
        }

        public Nome(string texto, Regex regex)
        {
            if (regex is not null && !regex.IsMatch(texto))
            {
                throw new NomeInvalidoException(texto);
            }

            Texto = texto;
        }

        public string Texto { get; private set; }

        
    }
}
