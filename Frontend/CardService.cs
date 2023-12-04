using FinalProjectFront.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class CardService
{
    private readonly HttpClient _httpClient;

    public CardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new System.Uri("http://localhost:80");
    }

    public async Task<string> ShuffleDeck()
    {
        try
        {
            var response = await _httpClient.PostAsync("/shuffle-deck/", null);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (HttpRequestException)
        {
            return null; // Handle exception as needed
        }
    }

    public async Task<List<Card>> DealCards(int numberOfCards)
    {
        try
        {
            var cards = new List<Card>();

            for (int i = 0; i < numberOfCards; i++)
            {
                var response = await _httpClient.GetFromJsonAsync<Card>("/deal-card/");
                if (response != null)
                {
                    cards.Add(response);
                }
            }

            return cards;
        }
        catch (HttpRequestException)
        {
            return null; // Handle exception as needed
        }
    }
    private async Task<Card> DealCardFromApi()
    {
        var response = await _httpClient.GetFromJsonAsync<Card>("/deal-card/");
        return new Card { Suit = response.Suit, Value = response.Value };
    }


    public async Task<string> ClearDeck()
    {
        try
        {
            var response = await _httpClient.PostAsync("/clear-deck/", null);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (HttpRequestException)
        {
            return null; // Handle exception as needed
        }
    }
}


