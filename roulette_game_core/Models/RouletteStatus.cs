using System;
using System.Collections.Generic;

#nullable disable

namespace roulette_game_core.Models
{
    public partial class RouletteStatus
    {
        public RouletteStatus()
        {
            Roulettes = new HashSet<Roulette>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Roulette> Roulettes { get; set; }
    }
}
