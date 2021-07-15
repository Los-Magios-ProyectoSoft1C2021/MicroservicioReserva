using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Template.Domain.DTOs.HttpResponse;

namespace Template.Application.HttpServices
{
    public class MicroservicioUsuarioService
    {
        public HttpClient Client { get; set; }

        public MicroservicioUsuarioService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44310/");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

            Client = client;
        }

        public async Task<ResponseUsuarioDto> GetUsuarioById(int usuarioId, string token)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await Client.GetAsync($"/api/usuario/id/{usuarioId}");
                Client.DefaultRequestHeaders.Remove("Authorization");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string jsonText = await response.Content.ReadAsStringAsync();

                    var deserialized = JsonConvert.DeserializeObject<ResponseUsuarioDto>(jsonText);
                    return deserialized;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseUsuarioDto> GetUsuarioByToken(string token)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await Client.GetAsync($"/api/usuario/id");
                Client.DefaultRequestHeaders.Remove("Authorization");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string jsonText = await response.Content.ReadAsStringAsync();

                    var deserialized = JsonConvert.DeserializeObject<ResponseUsuarioDto>(jsonText);
                    return deserialized;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
