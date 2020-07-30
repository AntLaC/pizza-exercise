using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pizza_Exercise.Models
{
    public class ToppingCombinationRepository : IToppingCombinationRepository
    {
        public async Task<ToppingCombinations> GetAll()
        {
            using WebClient webClient = new WebClient();
            string result = await webClient.DownloadStringTaskAsync(new Uri("https://www.olo.com/pizzas.json"));
            var toppings = JsonSerializer.Deserialize<IEnumerable<dynamic>>(result);
            ToppingCombinations combinations = new ToppingCombinations();
            foreach (var item in toppings)
            {
                Combination ingredients = JsonSerializer.Deserialize<Combination>(item.ToString());
                combinations.Combinations.Add(new Combination { Toppings = ingredients.Toppings.Select(s => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower())).ToList() });
            }
            Parallel.ForEach(combinations.Combinations, combination =>
            {
                combinations.CombinationOrders.AddOrUpdate(combination.ToString(), 1, (key, oldValue) => oldValue + 1);
            });
            return combinations;
        }
    }
}