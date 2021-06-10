using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace roulette_game_core.Models
{
    [Keyless]
    public class RouletteOutput
    {
        public int player_id { get; set; }

        public string bet { get; set; }

        public double bet_value { get; set; }

        public int winner_number { get; set; }

        public string bet_status { get; set; }

        public double bet_prize { get; set; }
    }
}
