using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContratoDTOs
{
    public class ContratoDOC
    {
        [BsonElement("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string IdOrganizacao { get; set; } = string.Empty;
        public string NumeroEDS { get; set; } = string.Empty;
        public string NumeroCliente { get; set; } = string.Empty;
        public string DescricaoServico { get; set; } = string.Empty;
        public List<ProjetosDOC> Projetos { get; set; } = new List<ProjetosDOC>();
        public ClienteDOC Cliente { get; set; } = new ClienteDOC();
        public List<ClaimDOC> AtributosEspecificos { get; set; } = new List<ClaimDOC>();
        public string TipoContratacao { get; set; } = string.Empty;

    }
}