using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace Google_View
{
    public partial class Form1 : Form
    {
        private string selectedAgent = "Googlebot";
        private string url = "https://51degrees.com/developers/user-agent-tester";
        private bool webViewReady = false;
        private readonly Stopwatch loadTimer = new Stopwatch();
        private string proxy = "";
        private ToolStripMenuItem switchOff = new ToolStripMenuItem("Switch Off");
        private ToolStripSeparator separator = new ToolStripSeparator();
        private Dictionary<string, string> proxyList = new Dictionary<string, string>
            {
                { "Germany", "http://1.1.1.1:8080" },
                { "Italy", "http://2.2.2.2:8080" },
                { "USA", "http://3.3.3.3:8080" },
            };

        private Dictionary<string, string> UserAgentList = new Dictionary<string, string>
        {
            { "Googlebot", "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)" },
            { "Googlebot-Mobile", "Mozilla/5.0 (Linux; Android 6.0.1; Nexus 5X) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.96 Mobile Safari/537.36 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)" },
            { "Googlebot-Desktop", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120 Safari/537.36 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)" },
            { "Safari 17 (MacOS)", "Mozilla/5.0 (Macintosh; Intel Mac OS X 14_0) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.0 Safari/605.1.15" },
            { "Firefox 122 (Windows 11)", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:122.0) Gecko/20100101 Firefox/122.0"},
            { "Edge 120 (Windows 11)", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0" },
            { "Chrome 120 (Windows 11)", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" }
        };
        private ToolStripMenuItem selectedProxyMenuItem = null;


        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            foreach (var ual in UserAgentList)
            {
                user_agent.Items.Add(ual.Key);
            }

            user_agent.SelectedIndex = 0;
            url_address.Text = url;

            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

            PopulateProxyMenu();
            InitializeWebViewWithProxy();
        }

        private async Task InitializeWebViewWithProxy(string proxy = "")
        {

            UpdateProxyMenuUI();

            this.Enabled = false;
            if (webView21 != null)
            {
                webView21.NavigationStarting -= WebView_NavigationStarting;
                webView21.NavigationCompleted -= WebView_NavigationCompleted;

                panel3.Controls.Remove(webView21);
                webView21.Dispose();
                webView21 = null;

                await Task.Delay(50);
            }

            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2
            {
                Dock = DockStyle.Fill
            };
            panel3.Controls.Add(webView21);

            CoreWebView2Environment env = null;
            if (!string.IsNullOrWhiteSpace(proxy))
            {
                env = await CoreWebView2Environment.CreateAsync(
                    null, null,
                    new CoreWebView2EnvironmentOptions($"--proxy-server={proxy}")
                );
            }

            if (env != null)
                await webView21.EnsureCoreWebView2Async(env);
            else
                await webView21.EnsureCoreWebView2Async();

            webView21.NavigationStarting += WebView_NavigationStarting;
            webView21.NavigationCompleted += WebView_NavigationCompleted;

            webViewReady = true;
            Navigate();

            this.Enabled = true;
        }


        private void WebView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            loadTimer.Stop();
            toolStripProgressBar1.Visible = false;
            ShowLoadTime();
        }

        private void WebView_NavigationStarting(object? s, CoreWebView2NavigationStartingEventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            loadTimer.Restart();
            toolStripProgressBar1.Visible = true;
        }

        private void PopulateProxyMenu()
        {
            proxyToolStripMenuItem.DropDownItems.Clear();

            foreach (var kvp in proxyList)
            {
                var item = new ToolStripMenuItem(kvp.Key);
                item.Tag = kvp.Value;               
                item.Click += proxyMenuItem_Click;
                proxyToolStripMenuItem.DropDownItems.Add(item);
            }

            switchOff.Click += switchOffToolStripMenuItem_Click;
            proxyToolStripMenuItem.DropDownItems.Add(separator);
            proxyToolStripMenuItem.DropDownItems.Add(switchOff);
        }
        private void UpdateProxyMenuUI()
        {
            switchOff.Visible = !string.IsNullOrEmpty(proxy);
            separator.Visible = !string.IsNullOrEmpty(proxy);
        }

        private void user_agent_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAgent = user_agent.SelectedItem?.ToString();
            Navigate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            url = url_address.Text;
            Navigate();
        }
        private string GetSiteIP(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                IPAddress[] addresses = Dns.GetHostAddresses(uri.Host);
                return string.Join(", ", addresses.Select(a => a.ToString()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DNS lookup failed: {ex.Message}");
                return "Unknown";
            }
        }


        private void Navigate()
        {

            if (!webViewReady || webView21.CoreWebView2 == null)
                return;

            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = $"https://{url}";
            }


            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                return;

            webView21.CoreWebView2.Settings.UserAgent = UserAgentList.FirstOrDefault(kvp => kvp.Key == selectedAgent).Value;

            if (webView21.Source != null && webView21.Source == uri)
            {
                webView21.Reload();
            }
            else
            {
                webView21.Source = uri;
            }

            toolStripStatusLabel2.Text = $"{GetSiteIP(url)} ({url})";
        }

        private void ShowLoadTime()
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(loadTimer.ElapsedMilliseconds);

            string readable;

            if (ts.TotalHours >= 1)
                readable = $"{(int)ts.TotalHours}h {ts.Minutes}m {ts.Seconds}s";
            else if (ts.TotalMinutes >= 1)
                readable = $"{ts.Minutes}m {ts.Seconds}s";
            else if (ts.TotalSeconds >= 1)
                readable = $"{ts.TotalSeconds:F2}s";
            else
                readable = $"{ts.Milliseconds} ms";

            toolStripStatusLabel1.Text = $"Loaded in {readable}";
        }

        private async void proxyMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                proxy = item.Tag?.ToString() ?? "";

                if (selectedProxyMenuItem != null)
                    selectedProxyMenuItem.ForeColor = Color.Black;

                item.ForeColor = Color.Blue;
                selectedProxyMenuItem = item;

                await InitializeWebViewWithProxy();
            }
        }


        private async void switchOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            proxy = "";

            if (selectedProxyMenuItem != null)
                selectedProxyMenuItem.ForeColor = Color.Black;
            selectedProxyMenuItem = null;

            await InitializeWebViewWithProxy();
        }

        private void url_address_Enter(object sender, EventArgs e)
        {
            url = url_address.Text;
            Navigate();
        }
    }
}
