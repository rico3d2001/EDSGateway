using EDSCore;

namespace Hub.Dominio.ValueObjects
{
    //public class TipoContrato : Enumeration
    //{
    //    public static TipoContrato GuardaChuva = new TipoContrato(1, nameof(GuardaChuva));
    //    public static TipoContrato TurnKey = new TipoContrato(2, nameof(TurnKey));
    //    public static TipoContrato Demanda = new TipoContrato(3, nameof(Demanda));

    //    public TipoContrato(int id, string name) : base(id, name)
    //    {
    //    }
    //}
    public record TipoContrato : ValueObject
    {
        private TipoContrato()
        {
            
        }
        public TipoContrato(string value)
        {
            Texto = value.Trim();
        }
        public string Texto { get; private set; }

    }
}
