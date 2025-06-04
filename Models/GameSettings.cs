using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoGuessrWinForms.Models
{
    public class GameSettings
    {
        public string Difficulty { get; set; }
        public int TotalRounds { get; set; }
        public int TimePerRoundSeconds { get; set; } = 90;
        public string Continent { get; set; }
    }

}
