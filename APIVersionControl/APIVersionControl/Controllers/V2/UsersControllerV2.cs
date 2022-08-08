using APIVersionControl.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace APIVersionControl.Controllers.V2
{
    [ApiVersion("2.0")] //indica la versión que puede ser consumida por nuestra api
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private const string ApiTestURL = "https://dummyapi.io/data/v1/user?limit=30";
        private const string AppID = "62f0c4e25d948335ddfbf73e";
        private readonly HttpClient _httpClient;
        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetUserData")]
        public async Task<IActionResult> GetUsersDataAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear(); //limpiamos las cabeceras
            _httpClient.DefaultRequestHeaders.Add("app-id", AppID); //Introducimos la cabecera que necesitamos

            var response = await _httpClient.GetStreamAsync(ApiTestURL);
            //nos recoge los datos de la url (devuelve la respuesta con los datos del  usuario)

            var usersData = await JsonSerializer.DeserializeAsync<UsersResponseData>(response);
            //es lo que devolvemos al usuario que consume nuestra api
            // lo serializamos para pasarlo al UsersResponseData en el formato correspondiente
            var users = usersData?.data;             
            // vamos a devolver solo la lista de usuario sin límite por página

            return Ok(usersData);
        }
    }
}