using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectFront
{
    public class CardService
    {
        private readonly HttpClient _httpClient;

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> StartGame()
        {
            try
            {
                var response = await _httpClient.PostAsync("/start-game", null);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<string>>();
            }
            catch (HttpRequestException)
            {
                // Handle exception (e.g., display an error message)
                return null;
            }
        }

        public async Task<List<string>> Hit()
        {
            try
            {
                var response = await _httpClient.PostAsync("/hit", null);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<string>>();
            }
            catch (HttpRequestException)
            {
                // Handle exception (e.g., display an error message)
                return null;
            }
        }

        public async Task<List<string>> Stand()
        {
            try
            {
                var response = await _httpClient.PostAsync("/stand", null);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<string>>();
            }
            catch (HttpRequestException)
            {
                // Handle exception (e.g., display an error message)
                return null;
            }
        }
    }
}
