using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizza_Exercise.Models
{
    public interface IToppingCombinationRepository
    {
        Task<ToppingCombinations> GetAll();
    }
}
