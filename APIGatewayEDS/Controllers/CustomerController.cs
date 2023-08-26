using Amazon.S3;
using Amazon.S3.Transfer;
using APIGatewayEDS.Commands;
using HubDTOs.Documentos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceFotoUsuario;

namespace APIGatewayEDS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/v1")]
    public class CustomerController : EDSController
    {
        private readonly string _aWSAcessKeyId;
        private readonly string _aWSSecretKeyId;
        private readonly string _domainName;
        private readonly string _bucketName;
        private readonly HttpClient _httpClient;

        public CustomerController(IMediator mediator,
            IOptions<S3ImageCronosConfig> s3ImageCronosConfig, HttpClient httpClient) : base(mediator)
        {
            _aWSAcessKeyId = s3ImageCronosConfig.Value.AWSAcessKeyId;
            _aWSSecretKeyId = s3ImageCronosConfig.Value.AWSSecretKeyId;
            _domainName = s3ImageCronosConfig.Value.DomainName;
            _bucketName = s3ImageCronosConfig.Value.BucketName;
            _httpClient = httpClient;
        }

        [HttpPut("UploadFotoUsuario/{email}")]
        [DisableRequestSizeLimit]
        public async Task<Ok<string>> UploadFotoUsuario(List<IFormFile> files, string email)
        {
            try
            {
                using (var client = new AmazonS3Client(_aWSAcessKeyId, _aWSSecretKeyId, Amazon.RegionEndpoint.USEast1))
                {
                    using var newMemoryStream = new MemoryStream();
                    files[0].CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = email + ".png",
                        BucketName = _bucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

                var resp = _domainName + "/" + email + ".png";
                return TypedResults.Ok(resp);

            }
            catch (Exception)
            {
                return null;
            }
        }

        ///[AllowAnonymous]
        [HttpGet("GetCustomersByHub/{email}")]
        public async Task<IActionResult> GetCustomersByHub(string email)
        {
            try
            {
                var uri = $"https://embhuuu7im.us-east-1.awsapprunner.com/api/Hub/v1/GetCustomersByHub/{email}";

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
                        var objeto = serializer.Deserialize<List<CustomerDOC>>(jsonReader);
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

        //[AllowAnonymous]
        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var uri = $"https://embhuuu7im.us-east-1.awsapprunner.com/api/Hub/v1/GetByEmail/{email}";

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
                        var objeto = serializer.Deserialize<CustomerDOC>(jsonReader);
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

        //[AllowAnonymous]
        [HttpPut("ConfirmaHub")]
        public async Task<IActionResult> ConfirmaHub(ConfirmaCustomerCommand command)
        {
            try
            {
                //var httpResponse = await _service.ConfirmaHub(command);
                var uri = $"https://embhuuu7im.us-east-1.awsapprunner.com/api/Hub/v1/ConfirmaHub/";
                //var uri = $"https://localhost:7259/api/Hub/v1/ConfirmaHub/";
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
                        var objeto = serializer.Deserialize<HubDOC>(jsonReader);
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
}
