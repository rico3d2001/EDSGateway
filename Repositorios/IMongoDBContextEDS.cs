using MongoDB.Driver;

namespace Repositorios
{
    public interface IMongoDBContextEDS : IDisposable
    {
        IMongoDatabase Database { get; set; }
        IMongoClient Client { get; set; }
    }
}
