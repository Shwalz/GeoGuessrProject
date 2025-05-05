namespace GeoGuessrWinForms.Logic
{
    public static class ScoreCalculator
    {
        public static int CalculateScore(double distanceKm)
        {
            if (distanceKm < 0.2) return 5000;
            if (distanceKm < 1) return 4000;
            if (distanceKm < 5) return 3000;
            if (distanceKm < 20) return 2000;
            if (distanceKm < 100) return 1000;
            if (distanceKm < 500) return 500;
            return 0;
        }
    }
}
