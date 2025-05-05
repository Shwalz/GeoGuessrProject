using System;
using System.Windows.Forms;
using GeoGuessrWinForms.Forms;

namespace GeoGuessrWinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", "--enable-features=WebGL");
            Application.Run(new StartForm());
        }
    }
}
