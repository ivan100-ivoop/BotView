using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

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
        private string AgentUrl = "https://raw.githubusercontent.com/ivan100-ivoop/BotView/refs/heads/main/agent.json";
        private string ProxyUrl = "https://raw.githubusercontent.com/ivan100-ivoop/BotView/refs/heads/main/proxy.json";
        private Dictionary<string, string> proxyList = new Dictionary<string, string> { };
        private Dictionary<string, string> UserAgentList = new Dictionary<string, string> { };
        private ToolStripMenuItem? selectedProxyMenuItem = null;


        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string AgentJson = await client.GetStringAsync(AgentUrl);
                    UserAgentList = JsonSerializer.Deserialize<Dictionary<string, string>>(AgentJson)!;
                    foreach (var ual in UserAgentList)
                    {
                        user_agent.Items.Add(ual.Key);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load User-Agents: {ex.Message}", "ERROR", MessageBoxButtons.OK);
                }

                try
                {
                    string ProxyJson = await client.GetStringAsync(ProxyUrl);
                    proxyList = JsonSerializer.Deserialize<Dictionary<string, string>>(ProxyJson)!;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load Proxy: {ex.Message}", "ERROR", MessageBoxButtons.OK);
                }
            }

            await loadUIAsync();
        }

        private async Task loadUIAsync()
        {
            if (UserAgentList.Count >= 1)
            {
                user_agent.SelectedIndex = 0;
                proxyToolStripMenuItem.Visible = false;
            }

            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

            PopulateProxyMenu();
            await InitializeWebViewWithProxy();
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

            CoreWebView2Environment? env = null;
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
            selectedAgent = user_agent.SelectedItem?.ToString() ?? string.Empty;
            Navigate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            url = url_address.Text;
            Navigate();
        }
        private string GetSiteIP()
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

            if (string.IsNullOrEmpty(url_address.Text))
            {
                url_address.Text = url;
            }

            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = $"https://{url}";
                url_address.Text = url;
            }


            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                return;

            if (UserAgentList.Count >= 1)
                webView21.CoreWebView2.Settings.UserAgent = UserAgentList.FirstOrDefault(kvp => kvp.Key == selectedAgent).Value;

            if (webView21.Source != null && webView21.Source == uri)
            {
                webView21.Reload();
            }
            else
            {
                webView21.Source = uri;
            }

            toolStripStatusLabel2.Text = $"{GetSiteIP()} ({url})";
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

        private async void proxyMenuItem_Click(object? sender, EventArgs e)
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


        // Change the signature of switchOffToolStripMenuItem_Click to allow nullable sender
        private async void switchOffToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            proxy = "";

            if (selectedProxyMenuItem != null)
                selectedProxyMenuItem.ForeColor = Color.Black;
            selectedProxyMenuItem = null;

            await InitializeWebViewWithProxy();
        }

        private void url_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                url = url_address.Text;
                Navigate();
            }
        }
    }
}
