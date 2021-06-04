using System;
using System.Collections.Generic;

#nullable disable

namespace roulette_game_core.Models
{
    public partial class Roulette
    {
        public Roulette()
        {
            RouletteBets = new HashSet<RouletteBet>();
        }

        public int Id { get; set; }
        public int RouletteStatusId { get; set; }

        public virtual RouletteStatus RouletteStatus { get; set; }
        public virtual ICollection<RouletteBet> RouletteBets { get; set; }
    }
}
