using EDSCore;
using Hub.Dominio.ValueObjects;
using System.Text.RegularExpressions;

namespace Hub.Dominio.Contratos
{
    public static class Validacoes
    {
        public static void ValidaTexto(Nome nome, int? comprimentoMin, int? comprimentoMax, Regex? regex)
        {

            if ((comprimentoMin is not null && comprimentoMax is not null)
                && (nome.Texto.Length < comprimentoMin || nome.Texto.Length > comprimentoMax))
            {
                throw new NomeInvalidoException(nome.Texto);
            }

            if (regex is not null && !regex.IsMatch(nome.Texto))
            {
                throw new NomeInvalidoException(nome.Texto);
            }

        }

    }
}
