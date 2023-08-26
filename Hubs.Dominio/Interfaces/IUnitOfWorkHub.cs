namespace Hubs.Dominio.Interfaces
{
    public interface IUnitOfWorkHub
    {
        IHubRepo HubRepositorio { get; }

    }
}
