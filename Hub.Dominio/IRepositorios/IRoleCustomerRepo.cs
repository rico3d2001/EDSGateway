using Hub.Dominio.Entities;
using HubDTOs.Documentos;

namespace Hub.Dominio.IRepositorios
{
    public interface IRoleCustomerRepo
    {
        Task<RoleCustomerDOC> GetByNome(string role);
        Task CreateAsync(RoleCustomer data);
    }
}
