using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roulette_game_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace roulette_game_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        // endpoint to create a new roulette
        [HttpPost]
        public Object Post()
        {
            using (var context = new masivianContext())
            {
                Roulette nRoulette = new Roulette();
                context.Roulettes.Add(nRoulette);
                context.SaveChanges();
                return nRoulette.Id;
            }
        }
    }
}
