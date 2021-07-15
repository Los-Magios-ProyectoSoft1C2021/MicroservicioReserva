using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Template.Domain.DTOs.HttpResponse;

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

        public async Task<List<int>> GetHabitacionesByHotelAndTipo(int hotelId, int tipo)
        {
            try
            {
                var response = await Client.GetAsync($"/api/hotel/{hotelId}/habitacion/?categoria={tipo}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string jsonText = await response.Content.ReadAsStringAsync();

                    var deserialized = JsonConvert.DeserializeObject<List<ResponseHabitacionDto>>(jsonText);
                    return deserialized.Select(x => x.HabitacionId).Distinct().ToList();
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

        public async Task<ResponseHotelDto> GetHotelById(int hotelId)
        {
            try
            {
                var response = await Client.GetAsync($"/api/hotel/{hotelId}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string jsonText = await response.Content.ReadAsStringAsync();

                    var deserialized = JsonConvert.DeserializeObject<ResponseHotelDto>(jsonText);
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

        public async Task<ResponseHabitacionDto> GetHabitacionById(int hotelId, int habitacionId)
        {
            try
            {
                var response = await Client.GetAsync($"/api/hotel/{hotelId}/habitacion/{habitacionId}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string jsonText = await response.Content.ReadAsStringAsync();

                    var deserialized = JsonConvert.DeserializeObject<ResponseHabitacionDto>(jsonText);
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
