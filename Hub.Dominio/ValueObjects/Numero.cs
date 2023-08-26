using EDSCore;
using Hub.Dominio.Contratos;
using System.Text.RegularExpressions;

namespace Hub.Dominio.ValueObjects
{
    public record Numero : ValueObject
    {
        private Numero()
        {
            
        }
        public Numero(string texto)
        {
            Texto = texto;
        }
        public Numero(string texto, int comprimentoMin, int comprimentoMax)
        {
            if (texto.Length < comprimentoMin || texto.Length > comprimentoMax)
            {
                throw new NomeInvalidoException(texto);
            }
            Texto = texto;
        }

        public Numero(string texto, Regex regex)
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
