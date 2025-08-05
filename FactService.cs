using Netwise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Netwise
{
    public class FactService : IService
    {
        private readonly HttpClient httpClient;
        private readonly string ApiUrl = "https://catfact.ninja/fact";

        public FactService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CatFact> GetFact()
        {
            try
            {
                var response = await httpClient.GetAsync(ApiUrl);
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
