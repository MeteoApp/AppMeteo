using AppMeteo.Models;
using AppMeteo.Services.Interfaces;
using Microsoft.Extensions.Options;
using Config = AppMeteo.Models.Internals.Config;

namespace AppMeteo.Services.Datalayers
{
    public class AlertDatalayer : IAlertDatalayer
    {
        #region Properties
        private readonly IOptions<Config> _config;
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public AlertDatalayer(IOptions<Config> config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<Alert> CreateAlert(Alert alert)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.PostAsJsonAsync($"{_config.Value.UrlAPI}Alert", alert);

            if (response.IsSuccessStatusCode)
            {
                return alert;
            }
            else
            {
                throw new Exception($"Erreur lors de l'ajout de l'alerte : {response.StatusCode}");
            }
        }
        #endregion

        #region Read
        public async Task<Alert> GetAlertById(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.GetAsync($"{_config.Value.UrlAPI}Alert/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Alert>();
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération de l'alerte : {response.StatusCode}");
            }
        }

        public async Task<List<Alert>> GetAllAlerts()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.GetAsync($"{_config.Value.UrlAPI}Alert");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Alert>>();
            }
            else
            {
                throw new Exception($"Erreur lors de la récupération des alertes : {response.StatusCode}");
            }
        }
        #endregion

        #region Update
        public async Task<Alert> UpdateAlert(Alert alert)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.PutAsJsonAsync($"{_config.Value.UrlAPI}Alert/{alert.AlertId}", alert);

            if (response.IsSuccessStatusCode)
            {
                return alert;
            }
            else
            {
                throw new Exception($"Erreur lors de la mise à jour de la mesure : {response.StatusCode}");
            }
        }
        #endregion

                #region Delete
        public async Task<bool> DeleteAlert(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

            HttpResponseMessage response = await client.DeleteAsync($"{_config.Value.UrlAPI}Alert/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Erreur lors de la suppression de l'alerte : {response.StatusCode}");
            }
        }
        #endregion

        #endregion
    }
}