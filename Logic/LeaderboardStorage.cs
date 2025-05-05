using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GeoGuessrWinForms.Models;

namespace GeoGuessrWinForms.Logic
{
    public static class LeaderboardStorage
    {
        private static readonly string filePath = "leaderboard.json";

        public static List<LeaderboardEntry> Load()
        {
            if (!File.Exists(filePath)) return new List<LeaderboardEntry>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<LeaderboardEntry>>(json) ?? new List<LeaderboardEntry>();
        }

        public static void Save(List<LeaderboardEntry> entries)
        {
            var json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
