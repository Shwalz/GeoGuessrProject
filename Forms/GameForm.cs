using System;
using System.Globalization;
using System.Windows.Forms;
using GeoGuessrWinForms.Models;
using GeoGuessrWinForms.Logic;

namespace GeoGuessrWinForms.Forms
{
    public partial class GameForm : Form
    {
        private readonly GameManager gameManager;
        private bool roundChecked = false;

        public GameForm(Player player, GameSettings settings)
        {
            InitializeComponent();
            gameManager = new GameManager(player, settings, this);

            gameManager.RoundCompleted += (round, score, distance, totalScore) =>
            {
                ShowRoundResult(round, distance, score, totalScore);
                ShowMiniMapWithMarkers(
                    gameManager.CorrectLat,
                    gameManager.CorrectLng,
                    gameManager.GuessedLat,
                    gameManager.GuessedLng
                );
            };
        }

        private async void GameForm_Load(object sender, EventArgs e)
        {
            await webViewStreetView.EnsureCoreWebView2Async();
            await webViewMiniMap.EnsureCoreWebView2Async();

            webViewMiniMap.CoreWebView2.WebMessageReceived += MiniMapMessageReceived;

            await gameManager.InitializeAsync();
            gameManager.StartNextRound();
        }

        private void MiniMapMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var msg = System.Text.Json.JsonSerializer.Deserialize<LatLngMessage>(e.WebMessageAsJson);
                gameManager.SetPlayerGuess(msg.lat, msg.lng);
            }
            catch
            {}
        }

        public void ShowStreetView(string html)
        {
            if (webViewStreetView.CoreWebView2 != null)
                webViewStreetView.CoreWebView2.NavigateToString(html);
        }

        public void ShowMiniMap(string html)
        {
            if (webViewMiniMap.CoreWebView2 != null)
                webViewMiniMap.CoreWebView2.NavigateToString(html);
        }

        public void ShowMiniMapWithMarkers(double correctLat, double correctLng, double guessedLat, double guessedLng)
        {
            string centerLat = ((correctLat + guessedLat) / 2).ToString(CultureInfo.InvariantCulture);
            string centerLng = ((correctLng + guessedLng) / 2).ToString(CultureInfo.InvariantCulture);
            string correctLatStr = correctLat.ToString(CultureInfo.InvariantCulture);
            string correctLngStr = correctLng.ToString(CultureInfo.InvariantCulture);
            string guessedLatStr = guessedLat.ToString(CultureInfo.InvariantCulture);
            string guessedLngStr = guessedLng.ToString(CultureInfo.InvariantCulture);

            string html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Mini Map Result</title>
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
        ymaps.ready(function () {{
            var map = new ymaps.Map('map', {{
                center: [{centerLat}, {centerLng}],
                zoom: 3,
                controls: []
            }}, {{
                suppressMapOpenBlock: true
            }});

            var correctPlacemark = new ymaps.Placemark([{correctLatStr}, {correctLngStr}], {{
                balloonContent: 'Correct Location'
            }}, {{
                preset: 'islands#redIcon'
            }});

            var guessPlacemark = new ymaps.Placemark([{guessedLatStr}, {guessedLngStr}], {{
                balloonContent: 'Your Guess'
            }}, {{
                preset: 'islands#blueIcon'
            }});

            map.geoObjects.add(correctPlacemark);
            map.geoObjects.add(guessPlacemark);
        }});
    </script>
</head>
<body>
    <div id='map'></div>
</body>
</html>";

            if (webViewMiniMap.CoreWebView2 != null)
                webViewMiniMap.CoreWebView2.NavigateToString(html);
        }

        public void UpdateTimerDisplay(int seconds)
        {
            if (InvokeRequired)
                Invoke(() => labelTimer.Text = $"Time: {seconds}");
            else
                labelTimer.Text = $"Time: {seconds}";
        }

        public void UpdateRoundDisplay(int round)
        {
            if (InvokeRequired)
                Invoke(() => labelRound.Text = $"Round: {round}");
            else
                labelRound.Text = $"Round: {round}";
        }

        public void ShowRoundResult(int round, double distanceKm, int score, int totalScore)
        {
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    panelResult.Visible = true;
                    labelDistance.Text = $"Distance: {distanceKm:F2} km";
                    labelRoundScore.Text = $"Score this round: {score}";
                    labelTotalScore.Text = $"Total Score: {totalScore}";
                });
            }
            else
            {
                panelResult.Visible = true;
                labelDistance.Text = $"Distance: {distanceKm:F2} km";
                labelRoundScore.Text = $"Score this round: {score}";
                labelTotalScore.Text = $"Total Score: {totalScore}";
            }
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            if (!roundChecked)
            {
                gameManager.ProcessGuess();
                roundChecked = true;
                buttonCheck.Text = "Next Round";
            }
            else
            {
                panelResult.Visible = false;
                roundChecked = false;
                buttonCheck.Text = "Check";
                gameManager.NextRoundAfterResult();
            }
        }

        private class LatLngMessage
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
}
