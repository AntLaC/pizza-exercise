using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;

namespace Pizza_Exercise.Models
{
    public class ToppingCombinations
    {
        public ToppingCombinations()
        {
            Combinations = new List<Combination>();
            CombinationOrders = new ConcurrentDictionary<string, int>();
        }
        public int TotalOrders => Combinations.Count;
        public int TotalCombinations => CombinationOrders.Count;
        public ConcurrentDictionary<string, int> CombinationOrders { get; set; }
        public List<Combination> Combinations { get; set; }
    }
    public class Combination: IEquatable<Combination>, IEqualityComparer<Combination>
    {
        public Combination()
        {
            Toppings = new List<string>();
        }
        [JsonPropertyName("toppings")]
        public List<string> Toppings { get; set; }

        public bool Equals([AllowNull] Combination other)
        {
            if (other is null) return false;
            return Enumerable.SequenceEqual(
                Toppings.Select(s => s.ToLower().Trim()).OrderBy(o => o), 
                other.Toppings.Select(s => s.ToLower().Trim()).OrderBy(o => o));
        }

        public bool Equals([AllowNull] Combination x, [AllowNull] Combination y)
        {
            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] Combination obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Join('|', Toppings);
        }
    }
}