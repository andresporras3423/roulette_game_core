using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using roulette_game_core.Classes;
using roulette_game_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace roulette_game_core.Controllers
{
    public class RouletteController : BaseController
    {
        // endpoint para listar todas las ruletas y su respectivo estado
        // no requiere ningún parámetro
        // retorna todos las ruletas con su respectivo id y estado
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

        // enpoint para crear una ruleta
        // No necesita parámetros
        // retorna el id de la ruleta creada
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

        // endpoint para cambiar estado de un ruleta, de creada a abierta
        // recibe como parámetro en la url el id de la ruleta
        // retorna un html status, 200 si el proceso fue exitoso, 403 si la ruleta no está abierta, 400 si la ruleta no existe
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

        // enpoint para cerrar la ruleta
        // recibe como parámetro en la url el id de la ruleta
        // retorna un objecto que puede ser un string con un mensaje de error o la lista de todas las apuestas realizadas en la ruleta y su resultado
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
                                select new BetOutput(rb.PlayerId, rb.Bet, rb.BetValue, rand_num).displayContent())
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

        [HttpPut]
        [Route("close2/{id}")]
        public async Task<List<RouletteOutput>> Close2(int id)
        {
            using (var context = new masivianContext())
            {
                var pId = new SqlParameter("@id", id);
                List<RouletteOutput> outputs = await context.RouletteOutputs.FromSqlRaw("EXECUTE dbo.cr @id", parameters: new[] { pId }).ToListAsync();
                return outputs;
            }
        }
    }
}
