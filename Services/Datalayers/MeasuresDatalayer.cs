using AppMeteo.Models;
using AppMeteo.Services.Interfaces;
using Microsoft.Extensions.Options;
using Config = AppMeteo.Models.Internals.Config;

namespace AppMeteo.Services.Datalayers
{
    public class MeasuresDatalayer : IMeasuresDatalayer
    {
        #region Properties
        private readonly IOptions<Config> _config;
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public MeasuresDatalayer(IOptions<Config> config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }
        #endregion

        #region Methods
        public async Task<Measure> CreateMeasure(Measure measure)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.PostAsJsonAsync($"{_config.Value.UrlAPI}Measure", measure);

            if (response.IsSuccessStatusCode)
            {
                return measure;
            }
            else
            {
                throw new Exception($"Erreur lors de l'ajout de la mesure : {response.StatusCode}");
            }
        }

        public async Task<bool> DeleteMeasure(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.DeleteAsync($"{_config.Value.UrlAPI}Measure/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression de la mesure : {response.StatusCode}");
            }
        }

        public async Task<List<Measure>> GetAllMeasures()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.GetAsync($"{_config.Value.UrlAPI}Measure");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Measure>>();
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération des mesures : {response.StatusCode}");
            }
        }

        public async Task<List<Measure>> GetMeasuresByRoom(int roomId)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.GetAsync($"{_config.Value.UrlAPI}Measure/Room/{roomId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Measure>>();
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération des mesures : {response.StatusCode}");
            }
        }

        public async Task<Measure> GetMeasureById(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.GetAsync($"{_config.Value.UrlAPI}Measure/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Measure>();
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération de la mesure : {response.StatusCode}");
            }
        }

        public async Task<Measure> UpdateMeasure(Measure measure)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.PutAsJsonAsync($"{_config.Value.UrlAPI}Measure/{measure.MeasureId}", measure);

            if (response.IsSuccessStatusCode)
            {
                return measure;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour de la mesure : {response.StatusCode}");
            }
        }

        public async Task<List<Measure>> GetLastMeasureInRoom()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.GetAsync($"{_config.Value.UrlAPI}Measure/Last");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Measure>>();
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération de la mesure : {response.StatusCode}");
            }
        }
        #endregion

    }
}