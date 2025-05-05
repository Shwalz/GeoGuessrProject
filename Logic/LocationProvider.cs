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
                var loadedLocations = JsonSerializer.Deserialize<Dictionary<string, List<GameLocation>>>(json);

                if (loadedLocations == null)
                {
                    MessageBox.Show("Error with reading JSON: null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var pair in loadedLocations)
                {
                    var validList = new List<GameLocation>();

                    foreach (var loc in pair.Value)
                    {
                        bool hasStreetView = await StreetViewValidator.IsStreetViewAvailable(loc.Latitude, loc.Longitude);
                        if (hasStreetView)
                            validList.Add(loc);
                    }

                    if (validList.Count > 0)
                        locations[pair.Key] = validList;
                }

                if (locations.Count == 0)
                {
                    MessageBox.Show("Locations didn't found. Google Street View.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show($"Street View locations loaded: {locations.Sum(l => l.Value.Count)}", "Verification");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in locations loading: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
