using EDSCore;

namespace Hub.Dominio.ValueObjects
{
    public record CNPJ : ValueObject
    {
        private CNPJ() { }
        public CNPJ(string caracteres)
        {
            Texto = caracteres;
        }

        public string Texto { get; private set; }
    }
}
