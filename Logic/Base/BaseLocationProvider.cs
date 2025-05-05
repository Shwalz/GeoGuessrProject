using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoGuessrWinForms.Models;
using GeoGuessrWinForms.Logic.Abstractions;
using GeoGuessrWinForms.Logic.Exceptions;

namespace GeoGuessrWinForms.Logic.Base
{
    public abstract class BaseLocationProvider : ILocationProvider
    {
        protected readonly Dictionary<string, List<GameLocation>> locations = new();
        protected readonly Random random = new();

        public abstract Task InitializeAsync();

        public GameLocation GetRandomLocation(string difficulty)
        {
            if (locations.Count == 0)
                throw new NoAvailableLocationsException();

            if (!locations.ContainsKey(difficulty) || locations[difficulty].Count == 0)
            {
                foreach (var kvp in locations)
                {
                    if (kvp.Value.Count > 0)
                    {
                        difficulty = kvp.Key;
                        break;
                    }
                }
            }

            var list = locations[difficulty];
            return list[random.Next(list.Count)];
        }

        public abstract string GetMapHtmlWithMarkerScript();
        public abstract string GetStreetViewHtml(double lat, double lng);
    }
}
