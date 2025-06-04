using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoGuessrWinForms.Models;
using GeoGuessrWinForms.Logic.Abstractions;
using GeoGuessrWinForms.Logic.Base;

namespace GeoGuessrWinForms.Logic
{
    public class LocationProvider : BaseLocationProvider
    {
        private Dictionary<string, Dictionary<string, List<GameLocation>>> continentDifficultyLocations = new();

        public override async Task InitializeAsync()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "locations.json");

                if (!File.Exists(path))
                {
                    MessageBox.Show("File locations.json not found:\n" + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string json = await File.ReadAllTextAsync(path);
                var rawData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<GameLocation>>>>(json);

                if (rawData == null)
                {
                    MessageBox.Show("Invalid JSON format: null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var errors = new List<string>();

                foreach (var continentEntry in rawData)
                {
                    string continent = continentEntry.Key;

                    foreach (var difficultyEntry in continentEntry.Value)
                    {
                        string difficulty = difficultyEntry.Key;

                        var validList = new List<GameLocation>();

                        foreach (var loc in difficultyEntry.Value)
                        {
                            if (await StreetViewValidator.IsStreetViewAvailable(loc.Latitude, loc.Longitude, errors))
                                validList.Add(loc);
                        }

                        if (validList.Count > 0)
                        {
                            if (!continentDifficultyLocations.ContainsKey(continent))
                                continentDifficultyLocations[continent] = new Dictionary<string, List<GameLocation>>();

                            continentDifficultyLocations[continent][difficulty] = validList;
                        }
                    }
                }

                if (continentDifficultyLocations.Count == 0)
                {
                    MessageBox.Show("No valid locations found with Street View.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Loaded {continentDifficultyLocations.Sum(c => c.Value.Sum(l => l.Value.Count))} valid locations.", "Success");
                }

                if (errors.Any())
                {
                    string summary = $"Street View unavailable for {errors.Count} locations:\n\n" +
                                     string.Join("\n", errors.Take(10)) +
                                     (errors.Count > 10 ? $"\n...and {errors.Count - 10} more." : "");

                    MessageBox.Show(summary, "Validation Summary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading locations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public GameLocation GetRandomLocation(string continent, string difficulty)
        {
            if (continentDifficultyLocations.TryGetValue(continent, out var difficulties) &&
                difficulties.TryGetValue(difficulty, out var list) &&
                list.Any())
            {
                return list[new Random().Next(list.Count)];
            }

            foreach (var cont in continentDifficultyLocations)
            {
                foreach (var diff in cont.Value)
                {
                    if (diff.Value.Any())
                        return diff.Value[new Random().Next(diff.Value.Count)];
                }
            }

            throw new Exception($"No location found for selected continent '{continent}' and difficulty '{difficulty}'.");
        }


        public IEnumerable<string> GetContinents() => continentDifficultyLocations.Keys;

        public IEnumerable<string> GetDifficulties(string continent)
        {
            if (continentDifficultyLocations.TryGetValue(continent, out var dict))
                return dict.Keys;

            return Enumerable.Empty<string>();
        }

        public override string GetStreetViewHtml(double lat, double lng)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Street View</title>
                <style>
                    html, body {{ height: 100%; margin: 0; padding: 0; }}
                    #pano {{ height: 100%; }}
                </style>
                <script src='https://maps.googleapis.com/maps/api/js?key=AIzaSyDLKBFsyOd9V-TvMIWl0cVXnsotmF-xKHY'></script>
                <script>
                    function initialize() {{
                        const location = {{ lat: {lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}, lng: {lng.ToString(System.Globalization.CultureInfo.InvariantCulture)} }};
                        const panorama = new google.maps.StreetViewPanorama(
                            document.getElementById('pano'), {{
                                position: location,
                                pov: {{ heading: 34, pitch: 10 }},
                                disableDefaultUI: true
                            }}
                        );
                    }}
                </script>
            </head>
            <body onload='initialize()'>
                <div id='pano'></div>
            </body>
            </html>";
        }

        public override string GetMapHtmlWithMarkerScript()
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Yandex Map</title>
                <script src='https://api-maps.yandex.ru/2.1/?lang=ru_RU&apikey=ae8eb905-06d7-4e0a-8117-5307a0568794'></script>
                <style>
                    html, body, #map {{
                        width: 100%;
                        height: 100%;
                        margin: 0;
                        padding: 0;
                    }}
                </style>
                <script>
                    let map, placemark;

                    ymaps.ready(function() {{
                        map = new ymaps.Map('map', {{
                            center: [55.751574, 37.573856],
                            zoom: 2,
                            controls: []
                        }}, {{
                            suppressMapOpenBlock: true
                        }});

                        map.events.add('click', function(e) {{
                            const coords = e.get('coords');

                            if (placemark) {{
                                placemark.geometry.setCoordinates(coords);
                            }} else {{
                                placemark = new ymaps.Placemark(coords, {{}}, {{
                                    preset: 'islands#redDotIcon'
                                }});
                                map.geoObjects.add(placemark);
                            }}

                            if (window.chrome && window.chrome.webview) {{
                                window.chrome.webview.postMessage({{ lat: coords[0], lng: coords[1] }});
                            }}
                        }});
                    }});
                </script>
            </head>
            <body>
                <div id='map'></div>
            </body>
            </html>";
        }
    }
}
