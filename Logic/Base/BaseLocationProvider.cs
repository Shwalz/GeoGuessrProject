using System;
using System.Collections.Generic;
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

        public GameLocation GetRandomLocation(string continent, string difficulty)
        {
            string key = $"{continent}_{difficulty}";

            if (!locations.ContainsKey(key) || locations[key].Count == 0)
                throw new NoAvailableLocationsException();

            var list = locations[key];
            return list[random.Next(list.Count)];
        }

        public abstract string GetMapHtmlWithMarkerScript();
        public abstract string GetStreetViewHtml(double lat, double lng);
    }

}
