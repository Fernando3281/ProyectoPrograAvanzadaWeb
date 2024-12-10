using FrontEnd.Helpers.Interfaces;
using System.Configuration;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;



namespace FrontEnd.Helpers.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        public HttpClient Client { get; set; }
        IConfiguration Configuration;
        private readonly IHttpContextAccessor HttpContextAccessor;


        public ServiceRepository(HttpClient _client, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Client = _client;
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            string baseUrl = configuration.GetValue<string>("BackEnd:Url")?? "";
            string apikey = configuration.GetValue<string>("BackEnd:ApiKey") ?? "";
            Client.BaseAddress = new Uri(baseUrl);
            Client.DefaultRequestHeaders.Add("ApiKey", apikey);
        }

        private void AddAuthorizationHeader()
        {
            Console.WriteLine("Entrando al método AddAuthorizationHeader...");

            // Recupera el token desde la sesión
            string jwtToken = HttpContextAccessor.HttpContext?.Session?.GetString("token") ?? "";

            if (!string.IsNullOrEmpty(jwtToken))
            {
                Console.WriteLine($"Token encontrado en sesión: {jwtToken}");
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                Console.WriteLine("Token JWT agregado al encabezado Authorization.");
            }
            else
            {
                Console.WriteLine("No se encontró un token JWT válido en la sesión.");
            }
        }



        public HttpResponseMessage GetResponse(string url)
        {
            AddAuthorizationHeader();
            return Client.GetAsync(url).Result;
        }

        public HttpResponseMessage PutResponse(string url, object model)
        {
            AddAuthorizationHeader();
            return Client.PutAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            AddAuthorizationHeader();
            return Client.PostAsJsonAsync(url, model).Result;
        }

        public HttpResponseMessage DeleteResponse(string url)
        {
            AddAuthorizationHeader();
            return Client.DeleteAsync(url).Result;
        }
    }
}
