using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoGuessrWinForms.Models
{
    public class Player
    {
        public string Nickname { get; set; }
        public int Score { get; set; } = 0;
        public int CurrentRound { get; set; } = 1;
    }
}
