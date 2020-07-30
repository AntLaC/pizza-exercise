using Pizza_Exercise.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizza_Exercise.Services
{
    public class MockDataStore : IDataStore<Combination>
    {
        readonly List<Combination> items;

        public MockDataStore()
        {
            items = new List<Combination>()
            {
                new Combination { Toppings = new List<string> { "Pepperoni" } },
                new Combination { Toppings = new List<string> { "Pepperoni" } },
                new Combination { Toppings = new List<string> { "Pepperoni", "Italian Sausage" } },
                new Combination { Toppings = new List<string> { "Green Peppers" } },
                new Combination { Toppings = new List<string> { "Pepperoni", "Italian Sausage" } },
                new Combination { Toppings = new List<string> { "Green Peppers", "Italian Sausage" } }
            };
        }

        public async Task<IEnumerable<Combination>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}