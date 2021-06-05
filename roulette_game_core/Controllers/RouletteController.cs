using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roulette_game_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace roulette_game_core.Controllers
{
    public class RouletteController : BaseController
    {
        // endpoint to list all the roulettes and their status
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

        [HttpPut]
        [Route("close/{id}")]
        public object Close(int id)
        {
            try
            {
                using (var context = new masivianContext())
                {
                    Roulette nRoulette = context.Roulettes.Find(id);
                    if (nRoulette.RouletteStatusId == 2)
                    {
                        nRoulette.RouletteStatusId = 3;
                        context.SaveChanges();
                        Random rd = new Random();
                        int rand_num = rd.Next(0, 36);
                        return (from rb in context.RouletteBets
                                where rb.RouletteId == id
                                select new betOutput(rb.PlayerId, rb.Bet, rb.BetValue, rand_num).displayContent())
                                .ToList();
                    }
                    else return new { error = "La ruleta no está abierta" };
                }
            }
            catch
            {
                return new { error = "La ruleta con el id especificado no existe" };
            }
        }

        private class betOutput{
            int PlayerId;
            string Bet;
            string BetStatus;
            double BetValue;
            double BetPrize;
            public betOutput(int PlayerId, string Bet, double BetValue, int rand)
            {
                this.PlayerId = PlayerId;
                this.Bet = Bet;
                this.BetValue = BetValue;
                this.BetStatus = (Bet == "rojo" && rand % 2 == 0) || (Bet == "negro" && rand % 2 == 1) || (Int32.Parse(Bet) == rand) ? "ganador" : "perdedor";
                this.BetPrize = (this.BetStatus == "perdedor" ? 0.0 : (Bet=="rojo" || Bet=="negro" ? BetValue*1.8 : BetValue*5));
            }

            public object displayContent()
            {
                return new { this.PlayerId, this.Bet, this.BetValue, this.BetStatus, this.BetPrize };
            }
        };
    }
}
