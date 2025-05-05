using System.Threading.Tasks;
using GeoGuessrWinForms.Models;

namespace GeoGuessrWinForms.Logic.Abstractions
{
    public interface ILocationProvider
    {
        Task InitializeAsync();
        GameLocation GetRandomLocation(string difficulty);
        string GetMapHtmlWithMarkerScript();
        string GetStreetViewHtml(double lat, double lng);
    }
}
