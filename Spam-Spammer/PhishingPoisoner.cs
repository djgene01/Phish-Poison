using System.Net;
using System.Net.Http;

namespace PhishPoisoner;

public class PhishingPoisoner : IDisposable
{
    private readonly HttpClient _client;
    private readonly Random _random = new();
    private bool _disposed = false; // Track disposal state
    private readonly List<string> _userAgents = new()
    {
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Safari/605.1.15",
        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36"
    };

    public PhishingPoisoner(string? proxy = null)
    {
        var handler = new HttpClientHandler();
        if (!string.IsNullOrEmpty(proxy))
        {
            handler.Proxy = new WebProxy(proxy);
            handler.UseProxy = true;
        }
        _client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(10) };
    }

    public async Task<(bool success, string message)> SendFakeDataAsync(string endpoint, Action<string> logCallback)
    {
        var (email, password) = FakeDataGenerator.GenerateCredentials();
        var payload = new Dictionary<string, string>
        {
            { "email", email },
            { "pswd", password },
            { "Submit", "Download" }
        };

        var content = new FormUrlEncodedContent(payload);
        _client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgents[_random.Next(_userAgents.Count)]);

        try
        {
            HttpResponseMessage response = await _client.PostAsync(endpoint, content);
            string msg = $"Sent: {email} | Status: {(int)response.StatusCode}";
            logCallback(msg);
            return (true, msg);
        }
        catch (HttpRequestException e)
        {
            string error = $"Error for {email}: {e.Message}";
            logCallback(error);
            return (false, error);
        }
        catch (TaskCanceledException)
        {
            string error = $"Timeout for {email}";
            logCallback(error);
            return (false, error);
        }
    }

    // Implement IDisposable
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // Dispose managed resources
            _client?.Dispose();
        }

        // No unmanaged resources to free in this case, but included for completeness
        _disposed = true;
    }

    // Destructor (finalizer) for completeness, though not strictly necessary here
    ~PhishingPoisoner()
    {
        Dispose(false);
    }
}