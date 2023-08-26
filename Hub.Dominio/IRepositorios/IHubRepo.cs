using Hub.Dominio.Agregado;
using Hub.Dominio.Entities;
using HubDTOs.Documentos;

namespace Hub.Dominio.IRepositorios
{
    public interface IHubRepo 
    {
        Task Iniciar(HubAgregate hub);
        Task Confirmar(string idHub, Customer customer);
        Task<HubDOC> GetByEmail(string email);
        Task<HubDOC> GetById(string id);
        Task AddContaBase(HubAgregate hub);
    }
}
