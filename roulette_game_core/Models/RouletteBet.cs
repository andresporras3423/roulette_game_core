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
        public double BetValue { get; set; }

        public virtual Roulette Roulette { get; set; }
        public object P { get; }
        public string V1 { get; }
        public string V2 { get; }
        public string V3 { get; }
        public string V4 { get; }
    }
}
