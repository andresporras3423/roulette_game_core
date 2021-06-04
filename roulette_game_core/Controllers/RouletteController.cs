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
        // endpoint to list all the roulettes and their status
        // if winnerNumber=-1 then the roulette is still open, otherwise it's already closed
        [HttpGet]
        public IEnumerable<Object> Get()
        {
            using (var context = new masivianContext())
            {
                return context.Roulettes.ToList<Roulette>().Select(nRoulette => new
                {
                    nRoulette.Id,
                    Status= nRoulette.WinnerNumber == -1 ? "Open" : "Close"
                });
            }
        }

        // endpoint to create a new roulette
        [HttpPost]
        public int Post()
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
