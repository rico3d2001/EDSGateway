using HubDTOs.Documentos;

namespace Hubs.Dominio.Interfaces
{
    public interface IHubRepo
    {
        Task Iniciar(HubDOC hub);

    }
}
