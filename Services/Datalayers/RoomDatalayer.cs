using System.Net;
using AppMeteo.Models;
using AppMeteo.Services.Interfaces;
using Microsoft.Extensions.Options;
using Config = AppMeteo.Models.Internals.Config;

namespace AppMeteo.Services.Datalayers
{
    public class RoomDatalayer : IRoomDatalayer
    {
        #region Properties
        private readonly IOptions<Config> _config;
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public RoomDatalayer(IOptions<Config> config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }
        #endregion

        #region Methods
        public async Task<Room> AddRoom(Room room)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.PostAsJsonAsync($"{_config.Value.UrlAPI}Room", room);

            if (response.IsSuccessStatusCode)
            {
                return room;
            }
            else
            {
                throw new Exception($"Erreur lors de l'ajout de la pièce : {response.StatusCode}");
            }
        }

        public async Task<bool> DeleteRoom(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.DeleteAsync($"{_config.Value.UrlAPI}Room/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression de la pièce : {response.StatusCode}");
            }
        }

        public async Task<List<Room>> GetAllRooms()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(_config.Value.UrlAPI + "Room");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Room>>();
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                throw new Exception("Il n'y a pas de pièces enregistrées.");
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération des pièces : {response.StatusCode}");
            }
        }

        public async Task<Room> GetRoomById(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(_config.Value.UrlAPI + "Room/" + id);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Room>();
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                throw new Exception("Il n'y a pas de pièces enregistrées.");
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération des pièces : {response.StatusCode}");
            }
        }

        public async Task<Room> UpdateRoom(Room room)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PutAsJsonAsync(_config.Value.UrlAPI + "Room/", room);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Room>();
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                throw new Exception("Il n'y a pas de pièces enregistrées.");
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération des pièces : {response.StatusCode}");
            }
        }
        #endregion
    }
}