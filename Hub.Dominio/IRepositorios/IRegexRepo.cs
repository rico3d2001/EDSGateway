using HubDTOs.Documentos;

namespace Hub.Dominio.IRepositorios
{
    public interface IRegexRepo
    {
        Task Salvar(RegexDOC regex);
        Task<RegexDOC> ObterUm(string projeto,string className);
    }
    
}
