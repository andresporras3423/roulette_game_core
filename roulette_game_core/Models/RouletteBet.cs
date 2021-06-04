using System;
using System.Collections.Generic;

#nullable disable

namespace roulette_game_core.Models
{
    public partial class RouletteBet
    {
        public int Id { get; set; }
        public int RouletteId { get; set; }
        public int PlayerId { get; set; }
        public string Bet { get; set; }

        public virtual Roulette Roulette { get; set; }
    }
}
