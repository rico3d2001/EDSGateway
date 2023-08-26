using EDSCore;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hub.Dominio.Commands
{
    public class AtribuiClaimsCommand : IRequest<Resultado<IdentityResult, ValidationFalhas>>
    {
        public AtribuiClaimsCommand(List<ClaimCommand> claims)
        {
            Claims = claims;
        }

        public List<ClaimCommand> Claims { get; set; }

    }
}
