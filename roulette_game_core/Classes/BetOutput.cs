using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace roulette_game_core.Classes
{
    // esta clase fue creada para retornar la información de las apuestas cuando una ruleta se cierra
    public class BetOutput
    {
        int PlayerId;
        string Bet;
        string BetStatus;
        double BetValue;
        double BetPrize;
        public BetOutput(int PlayerId, string Bet, double BetValue, int rand)
        {
            this.PlayerId = PlayerId;
            this.Bet = Bet;
            this.BetValue = BetValue;
            this.BetStatus = (Bet == "rojo" && rand % 2 == 0) || (Bet == "negro" && rand % 2 == 1) || (Regex.Match(Bet, @"[1-9]+").Success && Int32.Parse(Bet) == rand) ? "ganador" : "perdedor";
            this.BetPrize = (this.BetStatus == "perdedor" ? 0.0 : (Bet == "rojo" || Bet == "negro" ? BetValue * 1.8 : BetValue * 5));
        }

        public object displayContent()
        {
            return new { this.PlayerId, this.Bet, this.BetValue, this.BetStatus, this.BetPrize };
        }
    }
}

