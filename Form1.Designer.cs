namespace Google_View
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            proxyToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            panel3 = new Panel();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel2 = new Panel();
            user_agent = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            url_address = new TextBox();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ActiveCaption;
            statusStrip1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1, toolStripStatusLabel3, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 716);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(2, 0, 16, 0);
            statusStrip1.Size = new Size(1581, 35);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(120, 27);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = SystemColors.ControlText;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 28);
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.ForeColor = Color.Black;
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(19, 28);
            toolStripStatusLabel3.Text = "|";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.ForeColor = SystemColors.ControlText;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 28);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { proxyToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1581, 36);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // proxyToolStripMenuItem
            // 
            proxyToolStripMenuItem.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            proxyToolStripMenuItem.Name = "proxyToolStripMenuItem";
            proxyToolStripMenuItem.Size = new Size(82, 32);
            proxyToolStripMenuItem.Text = "Proxy";
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(1581, 680);
            panel1.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.Controls.Add(webView21);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 56);
            panel3.Name = "panel3";
            panel3.Size = new Size(1581, 624);
            panel3.TabIndex = 1;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(0, 0);
            webView21.Name = "webView21";
            webView21.Size = new Size(1581, 624);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // panel2
            // 
            panel2.Controls.Add(user_agent);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(url_address);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1581, 56);
            panel2.TabIndex = 0;
            // 
            // user_agent
            // 
            user_agent.FormattingEnabled = true;
            user_agent.Location = new Point(1306, 6);
            user_agent.Name = "user_agent";
            user_agent.Size = new Size(248, 32);
            user_agent.TabIndex = 3;
            user_agent.SelectedIndexChanged += user_agent_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(1188, 4);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 2;
            button1.Text = "Go";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(95, 24);
            label1.TabIndex = 1;
            label1.Text = "Address:";
            // 
            // url_address
            // 
            url_address.Location = new Point(119, 6);
            url_address.Name = "url_address";
            url_address.Size = new Size(1063, 30);
            url_address.TabIndex = 0;
            url_address.Enter += url_address_Enter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1581, 751);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ControlLightLight;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BotView";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem proxyToolStripMenuItem;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private TextBox url_address;
        private Button button1;
        private Label label1;
        private ComboBox user_agent;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}
