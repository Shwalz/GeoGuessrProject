using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using GeoGuessrWinForms.Models;
using Microsoft.Web.WebView2.Core;

namespace GeoGuessrWinForms.Forms
{
    public partial class AdminPanelForm : Form
    {
        private double? selectedLat = null;
        private double? selectedLng = null;
        private Dictionary<string, Dictionary<string, List<GameLocation>>> allLocations;

        public AdminPanelForm()
        {
            InitializeComponent();
        }

        private async void AdminPanelForm_Load(object sender, EventArgs e)
        {
            await webViewMap.EnsureCoreWebView2Async();
            webViewMap.CoreWebView2.WebMessageReceived += WebViewMap_WebMessageReceived;
            webViewMap.CoreWebView2.NavigateToString(GetMapHtml());

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "locations.json");


            await webViewPreview.EnsureCoreWebView2Async(); // GSV
        }

        private void WebViewMap_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var msg = JsonSerializer.Deserialize<LatLngMessage>(e.WebMessageAsJson);
                selectedLat = msg.lat;
                selectedLng = msg.lng;

                if (selectedLat != null && selectedLng != null)
                {
                    webViewPreview.CoreWebView2.NavigateToString(GetStreetViewHtml(selectedLat.Value, selectedLng.Value));
                }
            }
            catch
            {
                MessageBox.Show("Error parsing map coordinates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (selectedLat == null || selectedLng == null)
            {
                MessageBox.Show("Please click on the map to choose a location.", "Missing Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxContinent.SelectedItem == null || comboBoxDifficulty.SelectedItem == null)
            {
                MessageBox.Show("Please select both continent and difficulty.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string continent = comboBoxContinent.SelectedItem.ToString();
            string difficulty = comboBoxDifficulty.SelectedItem.ToString();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "locations.json");

            Dictionary<string, Dictionary<string, List<GameLocation>>> data;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<GameLocation>>>>(json) ?? new();
            }
            else
            {
                data = new Dictionary<string, Dictionary<string, List<GameLocation>>>();
            }

            if (!data.ContainsKey(continent))
                data[continent] = new Dictionary<string, List<GameLocation>>();

            if (!data[continent].ContainsKey(difficulty))
                data[continent][difficulty] = new List<GameLocation>();

            data[continent][difficulty].Add(new GameLocation
            {
                Latitude = selectedLat.Value,
                Longitude = selectedLng.Value
            });

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(path, JsonSerializer.Serialize(data, options));

            MessageBox.Show("Location saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }


        private string GetMapHtml()
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <title>Choose Location</title>
                <script src='https://api-maps.yandex.ru/2.1/?lang=ru_RU&apikey=ae8eb905-06d7-4e0a-8117-5307a0568794'></script>
                <style>html, body, #map {{width:100%; height:100%; margin:0; padding:0;}}</style>
            </head>
            <body>
            <div id='map'></div>
            <script>
            ymaps.ready(function () {{
                var map = new ymaps.Map('map', {{
                    center: [55.751574, 37.573856],
                    zoom: 2,
                    controls: []
                }}, {{
                    suppressMapOpenBlock: true
                }});

                var placemark;

                map.events.add('click', function (e) {{
                    var coords = e.get('coords');
                    if (placemark) {{
                        placemark.geometry.setCoordinates(coords);
                    }} else {{
                        placemark = new ymaps.Placemark(coords, {{}}, {{
                            preset: 'islands#blueIcon'
                        }});
                        map.geoObjects.add(placemark);
                    }}

                    if (window.chrome && window.chrome.webview) {{
                        window.chrome.webview.postMessage({{ lat: coords[0], lng: coords[1] }});
                    }}
                }});
            }});
            </script>
            </body>
            </html>";
        }

        private string GetStreetViewHtml(double lat, double lng)
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
                        const location = {{ lat: {lat.ToString(CultureInfo.InvariantCulture)}, lng: {lng.ToString(CultureInfo.InvariantCulture)} }};
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

        private class LatLngMessage
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
}
