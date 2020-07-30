using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza_Exercise.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizza_Exercise.Controllers
{
    [Route("api/toppingcombination")]
    [ApiController]
    public class ToppingCombinationController : ControllerBase
    {
        private readonly IToppingCombinationRepository ToppingCombinationRepository;

        public ToppingCombinationController(IToppingCombinationRepository toppingCombinationRepository)
        {
            ToppingCombinationRepository = toppingCombinationRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> List()
        {
            var data = await ToppingCombinationRepository.GetAll();
            return Ok(new { data.TotalOrders, data.TotalCombinations, data.CombinationOrders });
        }
    }
}
