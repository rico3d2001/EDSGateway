using EDSCore;

namespace Hub.Dominio.ValueObjects
{
    //public class StatusCustomerType : Enumeration
    //{
    //    public static StatusCustomerType Iniciado = new StatusCustomerType(1, nameof(Iniciado));
    //    public static StatusCustomerType Registrado = new StatusCustomerType(2, nameof(Registrado));
    //    public static StatusCustomerType Completo = new StatusCustomerType(3, nameof(Completo));

    //    public StatusCustomerType(int id, string name) : base(id, name)
    //    {
    //    }
    //}

    public record StatusCustomerType : ValueObject
    {
        private StatusCustomerType()
        {
            
        }

        public StatusCustomerType(string value)
        {
            Texto = value.Trim();
        }
        public string Texto { get; private set; }

    }
}
