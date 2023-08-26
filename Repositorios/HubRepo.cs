using HubDTOs.Documentos;
using Hubs.Dominio.Interfaces;
using MongoDB.Driver;

namespace Repositorios
{
    public class HubRepo : IHubRepo
    {

        private readonly IMongoCollection<HubDOC> _collection;

        public HubRepo(IMongoCollection<HubDOC> collection)
        {
            _collection = collection;
        }

        public async Task Iniciar(HubDOC hub)
        {
            try
            {
                await _collection.InsertOneAsync(hub);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
