using EDSCore;

namespace Hub.Dominio.ValueObjects
{
    public record Foto : ValueObject
    {
        private Foto() { }
        public Foto(string endereco)
        {
            Endereco = endereco;
        }

        public string Endereco { get; private set; }


        

    }
}
