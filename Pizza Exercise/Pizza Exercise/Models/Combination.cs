using System.Collections.Generic;

namespace Pizza_Exercise.Models
{
    public class Combination
    {
        public List<string> Toppings { get; set; }
        public int Count { get; set; }
        public int Rank { get; set; }
        public override string ToString()
        {
            return string.Join('|', Toppings);
        }
    }
}