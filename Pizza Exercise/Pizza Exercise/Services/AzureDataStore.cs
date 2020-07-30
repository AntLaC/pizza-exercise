using Newtonsoft.Json;
using Pizza_Exercise.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Pizza_Exercise.Services
{
    public class AzureDataStore : IDataStore<Combination>
    {
        HttpClient client;
        List<Combination> items;

        public AzureDataStore()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };

            items = new List<Combination>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Combination>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                if(App.UseWebApi)
                {
                    items = await GetWebApiAsync();
                }
                else
                {
                    items = await GetDirectAsync();
                }
            }
            return items;
        }

        private async Task<List<Combination>> GetWebApiAsync()
        {
            try
            {
                var json = await client.GetStringAsync($"api/toppingcombination");
                var data = JsonConvert.DeserializeObject<dynamic>(json);
                items = new List<Combination>();
                var listObject = data["combinationOrders"]?.ToString();
                Dictionary<string, int> list = JsonConvert.DeserializeObject<Dictionary<string, int>>(listObject);
                items = list
                    .OrderByDescending(o => o.Value)
                    .Take(20)
                    .Select((s, index) =>
                        new Combination
                        {
                            Count = s.Value,
                            Toppings = s.Key.Split('|').OrderBy(o => o).ToList(),
                            Rank = index + 1
                        }).ToList();
            }
            catch
            {
                // log error
            }
            return items;
        }

        private async Task<List<Combination>> GetDirectAsync()
        {
            try
            {
                using HttpClient webClient = new HttpClient();
                string result = await webClient.GetStringAsync(new Uri("https://www.olo.com/pizzas.json"));
                var toppings = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(result);
                List<Combination> combinations = new List<Combination>();
                foreach (var item in toppings)
                {
                    Combination ingredients = JsonConvert.DeserializeObject<Combination>(item.ToString());
                    combinations.Add(new Combination { Toppings = ingredients.Toppings.Select(s => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower())).ToList() });
                }
                ConcurrentDictionary<string, int> combinationOrders = new ConcurrentDictionary<string, int>();
                Parallel.ForEach(combinations, combination =>
                {
                    combinationOrders.AddOrUpdate(combination.ToString(), 1, (key, oldValue) => oldValue + 1);
                });
                items = combinationOrders
                            .OrderByDescending(o => o.Value)
                            .Take(20)
                            .Select((s, index) =>
                                new Combination
                                {
                                    Count = s.Value,
                                    Toppings = s.Key.Split('|').OrderBy(o => o).ToList(),
                                    Rank = index + 1
                                }).ToList();
            }
            catch
            {
                // log error
            }
            return items;
        }
    }
}
