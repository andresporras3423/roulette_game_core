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
                return (from r in context.Roulettes
                        join rs in context.RouletteStatuses
                        on r.RouletteStatusId equals rs.Id
                        select new
                        {
                            r.Id,
                            Status = rs.StatusName
                        }).ToList();
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

        [HttpPut]
        [Route("open/{id}")]
        public IActionResult Open(int id)
        {
            try
            {
                using (var context = new masivianContext())
                {
                    Roulette nRoulette = context.Roulettes.Find(id);
                    if (nRoulette.RouletteStatusId == 1)
                    {
                        nRoulette.RouletteStatusId = 2;
                        context.SaveChanges();
                        return StatusCode(200);
                    }
                    else return StatusCode(403);
                }
            }
            catch
            {
                return StatusCode(400);
            }
        }
    }
}
