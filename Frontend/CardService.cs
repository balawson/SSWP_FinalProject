using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<GameState> StartGame()
        {
            try
            {
                var response = await _httpClient.PostAsync("/start-game", null);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<GameState>();
            }
            catch (HttpRequestException)
            {
                // Handle exception (e.g., display an error message)
                return null;
            }
        }

        public async Task<GameState> Hit()
        {
            try
            {
                var response = await _httpClient.PostAsync("/hit", null);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<GameState>();
            }
            catch (HttpRequestException)
            {
                // Handle exception (e.g., display an error message)
                return null;
            }
        }

        public async Task<GameState> Stand()
        {
            try
            {
                var response = await _httpClient.PostAsync("/stand", null);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<GameState>();
            }
            catch (HttpRequestException)
            {
                // Handle exception (e.g., display an error message)
                return null;
            }
        }
    }

    public class GameState
    {
        public List<string> UserCards { get; set; }
        public List<string> DealerCards { get; set; }
        public string GameMessage { get; set; }
    }
}
