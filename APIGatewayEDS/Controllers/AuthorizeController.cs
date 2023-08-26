using EDSCore;
using Hubs.Dominio.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceCustomer.Handlers;
using ServicoAutorizacao;
using ServicoAutorizacao.Handlers;
using System.ComponentModel.DataAnnotations;
using ValidacaoHelper.Notification;

namespace APIGatewayEDS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class AuthorizeController : EDSController
    {

        private RoleManager<ApplicationRole> _roleManager;
        private readonly IDomainNotificationContext _notificationContext;
        private readonly HttpClient _httpClient;
        public AuthorizeController(RoleManager<ApplicationRole> roleManager, IMediator mediator,
            HttpClient httpClient, IDomainNotificationContext notificationContext)
            : base(mediator)
        {
            _roleManager = roleManager;
            _httpClient = httpClient;
            _notificationContext = notificationContext;
        }


        [AllowAnonymous]
        [HttpPost("RegistraUsuario")]
        public async Task<IActionResult> RegistraUsuario([FromBody] IniciaHubCommand command)
        {
            try
            {
                var resultado = await _mediator.Send(command);

                if (_notificationContext.HasErrorNotifications)
                {
                    var notifications = _notificationContext.GetErrorNotifications();
                    var message = string.Join(", ", notifications.Select(x => x.Value));
                    var erros = new List<ValidationFalha>(); erros.Add(new ValidationFalha("500", message));
                    return BadRequest(new ValidationFalhas(erros));
                }

                return resultado.Match<IActionResult>(
                     m => CreatedAtAction(nameof(RegistraUsuario), new { id = m.Id }, m),
                     failed => BadRequest(failed.Errors));




                //var uri = $"https://embhuuu7im.us-east-1.awsapprunner.com/api/Hub/v1/IniciaHub/";
                //var httpResponse = _httpClient.PostAsJsonAsync(uri, command).Result;

                //if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                //{
                //    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                //    return BadRequest(jsonResponse);
                //}


                //var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                //using var streamReader = new StreamReader(contentStream);
                //using var jsonReader = new JsonTextReader(streamReader);
                //JsonSerializer serializer = new JsonSerializer();
                //try
                //{
                //    return Ok(serializer.Deserialize<HubDOC>(jsonReader));
                //}
                //catch (JsonReaderException)
                //{

                //    return BadRequest("Invalid JSON.");
                //}

            }
            catch (Exception ex)
            {
                return BadRequest("Hub não foi iniciado");
            }


        }

        [AllowAnonymous]
        [HttpPost("AtribuiClaims")]
        public async Task<IActionResult> AtribuiClaims([FromBody] List<ClaimCommand> claims)
        {

            var command = new AtribuiClaimsCommand(claims);
            var resultado = await _mediator.Send(command);

            return resultado.Match<IActionResult>(
                 m => CreatedAtAction(nameof(RegistraUsuario), new { suscesso = m.Succeeded }, m),
                 failed => BadRequest(failed.Errors));
        }


        [AllowAnonymous]
        [HttpPost("AtribuiRolesCustomer")]
        public async Task<IActionResult> AtribuiRolesCustomer([FromBody] List<RoleCustomerCommand> roles)
        {
            var command = new AtribuiRolesCustomerCommand(roles);
            var resultado = await _mediator.Send(command);

            return resultado.Match<IActionResult>(
                 m => CreatedAtAction(nameof(RegistraUsuario), new { suscesso = m.Succeeded }, m),
                 failed => BadRequest(failed.Errors));

        }



        [AllowAnonymous]
        [HttpPost("CreateRole")]
        public async Task<Ok<IdentityResult>> CreateRole([Required] string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });

            return TypedResults.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginRequest userlogin)
        {
            var command = new GeraTokenCommand(userlogin.email, userlogin.password, userlogin.emailtokenativo);
            var result = await _mediator.Send(command);
            var entender = result.Match<IActionResult>(
                m => CreatedAtAction(nameof(GenerateToken), new { Token = m.Token, RefreshToken = m.RefreshToken }, m),
                failed => BadRequest(failed.Errors));
            return entender;

        }


        [HttpPost("GenerateRefreshToken")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] TokenResponse token)
        {
            var command = new GeraRefreshTokenCommand(token.Token, token.RefreshToken);
            var result = await _mediator.Send(command);
            var entender = result.Match<IActionResult>(
                m => CreatedAtAction(nameof(GenerateToken), new { Token = m.Token, RefreshToken = m.RefreshToken }, m),
                failed => BadRequest(failed.Errors));
            return entender;

        }

        public class RegistraUsuarioRequest
        {
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class LoginRequest
        {
            public string email { get; set; }
            public string password { get; set; }
            public string emailtokenativo { get; set; }
        }

    }


}
