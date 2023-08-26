using HubDTOs.Documentos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace APIGatewayEDS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class ContaController : EDSController
    {
        private readonly HttpClient _httpClient;
        public ContaController(IMediator mediator, HttpClient httpClient) : base(mediator)
        {
            _httpClient = httpClient;
        }

        [HttpGet("ChecarStatus/{idCustomer}")]
        public async Task<IActionResult> ChecarStatus(string idCustomer)
        {
            try
            {
                var uri = $"https://3pkw2iyvhm.us-east-1.awsapprunner.com/api/Conta/v1/ChecarStatus/{idCustomer}";
                //var uri = $"https://localhost:32768/api/Conta/v1/ChecarStatus/{idCustomer}";

                var httpResponse = await _httpClient.GetAsync(uri);

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return BadRequest();
                }

                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                    using var streamReader = new StreamReader(contentStream);
                    using var jsonReader = new JsonTextReader(streamReader);
                    JsonSerializer serializer = new JsonSerializer();
                    try
                    {
                        var objeto = serializer.Deserialize<ContaDOC>(jsonReader);
                        return Ok(objeto);
                    }
                    catch (JsonReaderException)
                    {
                        return BadRequest("Invalid JSON.");

                    }
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("IniciarConta")]
        public async Task<IActionResult> InciarConta(IniciaContaRequest command)
        {
            try
            {
                var uri = $"https://3pkw2iyvhm.us-east-1.awsapprunner.com/api/Conta/v1/IniciarConta/";
                //var uri = $"https://localhost:32768/api/Conta/v1/IniciarConta/";
                var httpResponse = _httpClient.PutAsJsonAsync(uri, command).Result;

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return BadRequest();
                }

                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                    using var streamReader = new StreamReader(contentStream);
                    using var jsonReader = new JsonTextReader(streamReader);
                    JsonSerializer serializer = new JsonSerializer();
                    try
                    {
                        var objeto = serializer.Deserialize<ContaDOC>(jsonReader);
                        return Ok(objeto);
                    }
                    catch (JsonReaderException)
                    {
                        return BadRequest("Invalid JSON.");

                    }
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    public class IniciaContaRequest
    {
        public string IdCustomer { get; set; }
        public string Tipo { get; set; }
    }
}
