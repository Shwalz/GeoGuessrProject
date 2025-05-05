using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoGuessrWinForms.Logic
{
    public static class StreetViewValidator
    {
        private const string apiKey = "AIzaSyDLKBFsyOd9V-TvMIWl0cVXnsotmF-xKHY";

        public static async Task<bool> IsStreetViewAvailable(double lat, double lng)
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
                    MessageBox.Show($" Places not found: {lat}, {lng}\nResponse: {status}", "StreetView", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return status == "OK";
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error HTTP: " + ex.Message, "HTTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (JsonException ex)
            {
                MessageBox.Show("Error with JSON: " + ex.Message, "JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
