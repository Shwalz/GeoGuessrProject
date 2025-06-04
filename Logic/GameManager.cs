using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoGuessrWinForms.Forms;
using GeoGuessrWinForms.Models;
using Timer = System.Timers.Timer;

namespace GeoGuessrWinForms.Logic
{
    public class GameManager
    {
        private readonly Player player;
        private readonly GameSettings settings;
        private readonly GameForm gameForm;

        private readonly HashSet<GameLocation> usedLocations = new();
        private GameLocation currentLocation;
        private bool guessMade = false;

        private int currentRound = 1;
        private int timeLeft;
        private readonly Timer roundTimer;

        public readonly LocationProvider locationProvider;

        public double GuessedLat { get; private set; }
        public double GuessedLng { get; private set; }
        public double CorrectLat => currentLocation.Latitude;
        public double CorrectLng => currentLocation.Longitude;

        public delegate void RoundCompletedHandler(int roundNumber, int score, double distance, int totalScore);
        public event RoundCompletedHandler RoundCompleted;

        public GameManager(Player player, GameSettings settings, GameForm gameForm)
        {
            this.player = player;
            this.settings = settings;
            this.gameForm = gameForm;
            this.locationProvider = new LocationProvider();

            roundTimer = new Timer(1000);
            roundTimer.Elapsed += OnTimerTick;
        }

        public async Task InitializeAsync()
        {
            await locationProvider.InitializeAsync();
        }

        public void StartNextRound()
        {
            if (currentRound > settings.TotalRounds)
            {
                EndGame();
                return;
            }

            guessMade = false;

            GameLocation location;
            int attempts = 0;

            do
            {
                location = locationProvider.GetRandomLocation(settings.Continent, settings.Difficulty);
                attempts++;
            } while (usedLocations.Contains(location) && attempts < 100);

            usedLocations.Add(location);
            currentLocation = location;

            gameForm.UpdateRoundDisplay(currentRound);
            gameForm.UpdateTimerDisplay(settings.TimePerRoundSeconds);

            timeLeft = settings.TimePerRoundSeconds;
            roundTimer.Start();

            string streetViewHtml = locationProvider.GetStreetViewHtml(currentLocation.Latitude, currentLocation.Longitude);
            gameForm.ShowStreetView(streetViewHtml);

            string mapHtml = locationProvider.GetMapHtmlWithMarkerScript();
            gameForm.ShowMiniMap(mapHtml);
        }


        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            timeLeft--;

            if (gameForm.IsHandleCreated)
            {
                gameForm.BeginInvoke(new Action(() =>
                {
                    gameForm.UpdateTimerDisplay(timeLeft);
                }));
            }

            if (timeLeft <= 0)
            {
                roundTimer.Stop();

                if (!guessMade)
                {
                    if (gameForm.IsHandleCreated)
                    {
                        gameForm.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show("You didn't make a choice. Round was finished.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GuessedLat = 999.0;
                            GuessedLng = 999.0;
                            ProcessGuess();
                        }));
                    }
                    else
                    {
                        GuessedLat = 999.0;
                        GuessedLng = 999.0;
                        ProcessGuess();
                    }
                }
                else
                {
                    if (gameForm.IsHandleCreated)
                    {
                        gameForm.BeginInvoke(new Action(() =>
                        {
                            ProcessGuess();
                        }));
                    }
                    else
                    {
                        ProcessGuess();
                    }
                }
            }
        }


        public void SetPlayerGuess(double lat, double lng)
        {
            GuessedLat = lat;
            GuessedLng = lng;
            guessMade = true;
        }

        public void ProcessGuess()
        {
            roundTimer.Stop();

            double distance = GeoUtils.CalculateDistance(currentLocation.Latitude, currentLocation.Longitude, GuessedLat, GuessedLng);
            int score = ScoreCalculator.CalculateScore(distance);

            player.Score += score;

            RoundCompleted?.Invoke(currentRound, score, distance, player.Score);
        }

        public void NextRoundAfterResult()
        {
            currentRound++;
            StartNextRound();
        }

        private void EndGame()
        {
            var allEntries = LeaderboardStorage.Load();

            allEntries.Add(new LeaderboardEntry
            {
                Nickname = player.Nickname,
                Score = player.Score
            });

            LeaderboardStorage.Save(allEntries);

            var leaderboardForm = new LeaderboardForm(allEntries, player);
            leaderboardForm.Show();
            gameForm.Close();
        }
    }
}
