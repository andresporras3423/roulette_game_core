using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using roulette_game_core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace roulette_game_core.Controllers
{
    public class RouletteBetController : BaseController
    {
        // endpoint para crear una apuesta
        // recibe en los headers:
        // user_id -> id del usuario
        // recibe como parámetros en el body:
        // roulette_id -> id de la ruleta
        // bet -> se envía como string, puede tener valor "rojo", "negro" o un entero entre "0" y "36"
        // bet_value -> tipo float, es un número mayor a 0 y menor a 10000
        [HttpPost]
        public string Post()
        {
            Dictionary<string, string> body = read_body();
            Dictionary<string, string> headers = read_headers();
            using (var context = new masivianContext())
            {
                Roulette nRoulette = context.Roulettes.Find(Int32.Parse(body["roulette_id"]));
                string message = validate_bet(body, headers, nRoulette);
                if (message != "") return message;
                RouletteBet nRouletteBet = new RouletteBet();
                nRouletteBet.PlayerId = Int32.Parse(headers["user_id"]);
                nRouletteBet.RouletteId = Int32.Parse(body["roulette_id"]);
                nRouletteBet.Bet = body["bet"];
                nRouletteBet.BetValue = float.Parse(body["bet_value"]);
                context.RouletteBets.Add(nRouletteBet);
                context.SaveChanges();
                return "Apuesta creada";
            }
        }


        // valida que todo los parámetros del post
        private string validate_bet(Dictionary<string, string> body, Dictionary<string, string> headers, Roulette nRoulette)
        {
            if (nRoulette == null) 
                return "No existe ruleta con este id";
            else if (nRoulette.RouletteStatusId != 2) 
                return "La ruleta indicada no está abierta";
            else if (headers["user_id"] == null) 
                return "Debe incluir el id del usuario";
            else if (float.Parse(body["bet_value"]) <= 0 || float.Parse(body["bet_value"]) > 10000) 
                return "El valor de la apuesta no es válido";
            else if (body["bet"] != "rojo" && body["bet"] != "negro" && (!Regex.Match(body["bet"], @"[1-9]+").Success ||  Int32.Parse(body["bet"]) < 0 || Int32.Parse(body["bet"]) > 36)) 
                return "Apuesta debe ser rojo, negro o un entero entre 0 y 36";
            else return "";
        }
    }
}
