using Terminal.Gui;
using System.Text.RegularExpressions;

namespace PhishPoisoner;

class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        var top = Application.Top;

        // Create the main window
        var win = new Window("PhishPoisoner - Combat Phishing")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        top.Add(win);

        // UI Elements
        var endpointLabel = new Label("Endpoint URL:") { X = 2, Y = 2 };
        var endpointText = new TextField("https://example.com/phish.php")
        {
            X = Pos.Right(endpointLabel) + 1,
            Y = 2,
            Width = 50
        };

        var proxyLabel = new Label("Proxy (optional, e.g., http://proxy:port):") { X = 2, Y = 4 };
        var proxyText = new TextField("")
        {
            X = Pos.Right(proxyLabel) + 1,
            Y = 4,
            Width = 50
        };

        var countLabel = new Label("Number of Entries:") { X = 2, Y = 6 };
        var countText = new TextField("100")
        {
            X = Pos.Right(countLabel) + 1,
            Y = 6,
            Width = 10
        };

        var logView = new TextView()
        {
            X = 2,
            Y = 8,
            Width = Dim.Fill() - 4,
            Height = Dim.Fill() - 6,
            ReadOnly = true
        };

        var startButton = new Button("Start Poisoning")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(logView) + 1
        };

        // Add elements to window
        win.Add(endpointLabel, endpointText, proxyLabel, proxyText, countLabel, countText, logView, startButton);

        // Event handler for Start button
        startButton.Clicked += async () =>
        {
            startButton.Enabled = false;
            logView.Text = "Starting...\n";

            string endpoint = endpointText.Text.ToString() ?? "";
            string proxy = proxyText.Text.ToString();
            if (!int.TryParse(countText.Text.ToString(), out int count) || count <= 0)
            {
                MessageBox.ErrorQuery("Error", "Please enter a valid number of entries.", "OK");
                startButton.Enabled = true;
                return;
            }
            if (string.IsNullOrEmpty(endpoint) || !Uri.IsWellFormedUriString(endpoint, UriKind.Absolute))
            {
                MessageBox.ErrorQuery("Error", "Please enter a valid URL.", "OK");
                startButton.Enabled = true;
                return;
            }
            if (!string.IsNullOrEmpty(proxy) && !Regex.IsMatch(proxy, @"^https?://.+:\d+$"))
            {
                MessageBox.ErrorQuery("Error", "Invalid proxy format. Use http://host:port", "OK");
                startButton.Enabled = true;
                return;
            }

            using var poisoner = new PhishingPoisoner(proxy);
            for (int i = 0; i < count; i++)
            {
                var (success, message) = await poisoner.SendFakeDataAsync(endpoint, msg =>
                    Application.MainLoop.Invoke(() => logView.Text += $"{msg}\n"));
                await Task.Delay(random.Next(1000, 5001)); // Random delay 1-5s
            }

            Application.MainLoop.Invoke(() => logView.Text += "Done!\n");
            startButton.Enabled = true;
        };

        Application.Run();
        Application.Shutdown();
    }

    private static readonly Random random = new();
}