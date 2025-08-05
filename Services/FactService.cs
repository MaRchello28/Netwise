using Netwise.Interfaces;
using Netwise.Model;
using Netwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Netwise.Services
{
    public class FactService : IService
    {
        private readonly HttpClient httpClient;
        private readonly string apiUrl;

        public FactService(HttpClient httpClient, CatFactConfig config)
        {
            this.httpClient = httpClient;
            apiUrl = config.ApiUrl;
        }

        public async Task<CatFact> GetFact()
        {
            try
            {
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();

                if (body.Contains("fact") && body.Contains("length"))
                {
                    CatFact catFact = JsonSerializer.Deserialize<CatFact>(body, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? throw new Exception("Deserialization failed");

                    return catFact;
                }
                else
                {
                    throw new Exception("Invalid response format: missing 'fact' or 'length' keys");
                }

            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Request Exception: {e.Message}");
            }
        }
    }

}
