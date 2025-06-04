using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;

namespace GeoGuessrWinForms.Logic
{
    public static class StreetViewValidator
    {
        private const string apiKey = "AIzaSyDLKBFsyOd9V-TvMIWl0cVXnsotmF-xKHY";

        public static async Task<bool> IsStreetViewAvailable(double lat, double lng, List<string> errors)
        {
            try
            {
                string url = $"https://maps.googleapis.com/maps/api/streetview/metadata?location={lat},{lng}&key={apiKey}";

                using var client = new HttpClient();
                var response = await client.GetStringAsync(url);

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                string status = root.GetProperty("status").GetString();

                if (status != "OK")
                {
                    errors.Add($"Not available: {lat}, {lng} → {status}");
                    return false;
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                errors.Add($"HTTP error at {lat},{lng}: {ex.Message}");
                return false;
            }
            catch (JsonException ex)
            {
                errors.Add($"JSON error at {lat},{lng}: {ex.Message}");
                return false;
            }
            catch (System.Exception ex)
            {
                errors.Add($"General error at {lat},{lng}: {ex.Message}");
                return false;
            }
        }

        public static void ShowSummaryIfErrors(List<string> errors)
        {
            if (errors.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Street View is NOT available for some locations:\n");

                foreach (var err in errors)
                    sb.AppendLine(" - " + err);

                MessageBox.Show(sb.ToString(), "StreetView Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("All locations passed validation!", "StreetView Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
