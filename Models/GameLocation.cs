using System;

namespace GeoGuessrWinForms.Models
{
    public class GameLocation
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }

        public string EmbedUrl { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not GameLocation other)
                return false;


            return Math.Abs(Latitude - other.Latitude) < 0.000001 &&
                   Math.Abs(Longitude - other.Longitude) < 0.000001;
        }

        public override int GetHashCode()
        {
            int latHash = (Latitude * 1_000_000).GetHashCode();
            int lngHash = (Longitude * 1_000_000).GetHashCode();
            return HashCode.Combine(latHash, lngHash);
        }
    }
}
