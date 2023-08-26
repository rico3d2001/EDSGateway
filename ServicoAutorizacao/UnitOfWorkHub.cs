using HubDTOs.Documentos;
using Hubs.Dominio.Interfaces;
using Repositorios;

namespace ServicoAutorizacao
{
    public class UnitOfWorkHub : IUnitOfWorkHub, IDisposable
    {
        private IMongoDBContextEDS _contexto;
        private IHubRepo _hubRepo;

        public UnitOfWorkHub(IMongoDBContextEDS mongoDBContext)
        {
            _contexto = mongoDBContext;
        }
        public IHubRepo HubRepositorio
        {
            get
            {
                if (_hubRepo == null)
                {
                    var collection = _contexto.Database.GetCollection<HubDOC>("Hub");
                    _hubRepo = new HubRepo(collection);
                }
                return _hubRepo;
            }
        }


        #region Dispose

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
