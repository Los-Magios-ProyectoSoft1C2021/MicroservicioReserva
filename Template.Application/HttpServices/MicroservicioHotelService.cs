using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Template.Application.HttpServices
{
    public class MicroservicioHotelService
    {
        public HttpClient Client { get; }

        public MicroservicioHotelService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44309/");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

            Client = client;
        }

        public async Task<bool> CheckIfHotelExists(int hotelId)
        {
            try
            {
                var response = await Client.GetAsync($"api/hotel/{hotelId}");
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckIfHabitacionExists(int hotelId, int habitacionId)
        {
            try
            {
                var response = await Client.GetAsync($"api/hotel/{hotelId}/habitacion/{habitacionId}");
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
