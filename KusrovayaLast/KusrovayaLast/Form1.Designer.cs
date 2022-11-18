namespace KusrovayaLast
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
            this.NetworkInfo = new System.Windows.Forms.TabPage();
            this.iList = new System.Windows.Forms.FlowLayoutPanel();
            this.label_route_ip = new System.Windows.Forms.Label();
            this.label_ip = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PingTest = new System.Windows.Forms.TabPage();
            this.textBox_Log = new System.Windows.Forms.TextBox();
            this.labelRouterIp = new System.Windows.Forms.Label();
            this.labelPingDelay = new System.Windows.Forms.Label();
            this.labelPingCount = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.checkbox_log = new System.Windows.Forms.CheckBox();
            this.textbox_delay = new System.Windows.Forms.TextBox();
            this.textbox_pingCount = new System.Windows.Forms.TextBox();
            this.textBox_hosts = new System.Windows.Forms.TextBox();
            this.button_ping = new System.Windows.Forms.Button();
            this.ProcessMonitor = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_monitor_start = new System.Windows.Forms.Button();
            this.processList = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NetworkInfo.SuspendLayout();
            this.PingTest.SuspendLayout();
            this.ProcessMonitor.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NetworkInfo
            // 
            this.NetworkInfo.Controls.Add(this.iList);
            this.NetworkInfo.Controls.Add(this.label_route_ip);
            this.NetworkInfo.Controls.Add(this.label_ip);
            this.NetworkInfo.Controls.Add(this.label12);
            this.NetworkInfo.Controls.Add(this.label11);
            this.NetworkInfo.Controls.Add(this.label7);
            this.NetworkInfo.Controls.Add(this.label6);
            this.NetworkInfo.Controls.Add(this.label5);
            this.NetworkInfo.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NetworkInfo.Location = new System.Drawing.Point(4, 29);
            this.NetworkInfo.Name = "NetworkInfo";
            this.NetworkInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NetworkInfo.Size = new System.Drawing.Size(1423, 587);
            this.NetworkInfo.TabIndex = 2;
            this.NetworkInfo.Text = "NetworkInfo";
            this.NetworkInfo.UseVisualStyleBackColor = true;
            // 
            // iList
            // 
            this.iList.AutoScroll = true;
            this.iList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.iList.Location = new System.Drawing.Point(9, 59);
            this.iList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iList.Name = "iList";
            this.iList.Size = new System.Drawing.Size(978, 236);
            this.iList.TabIndex = 10;
            this.iList.WrapContents = false;
            // 
            // label_route_ip
            // 
            this.label_route_ip.AutoSize = true;
            this.label_route_ip.Location = new System.Drawing.Point(291, 411);
            this.label_route_ip.Name = "label_route_ip";
            this.label_route_ip.Size = new System.Drawing.Size(0, 25);
            this.label_route_ip.TabIndex = 9;
            // 
            // label_ip
            // 
            this.label_ip.AutoSize = true;
            this.label_ip.Location = new System.Drawing.Point(42, 411);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(0, 25);
            this.label_ip.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(291, 337);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 25);
            this.label12.TabIndex = 7;
            this.label12.Text = "IP роутера";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 337);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 25);
            this.label11.TabIndex = 6;
            this.label11.Text = "Ваш IP";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(793, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "Скорость (Мб/с)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(560, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "Статус";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(284, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Имя сетевого интерфейса";
            // 
            // PingTest
            // 
            this.PingTest.Controls.Add(this.textBox_Log);
            this.PingTest.Controls.Add(this.labelRouterIp);
            this.PingTest.Controls.Add(this.labelPingDelay);
            this.PingTest.Controls.Add(this.labelPingCount);
            this.PingTest.Controls.Add(this.labelIP);
            this.PingTest.Controls.Add(this.checkbox_log);
            this.PingTest.Controls.Add(this.textbox_delay);
            this.PingTest.Controls.Add(this.textbox_pingCount);
            this.PingTest.Controls.Add(this.textBox_hosts);
            this.PingTest.Controls.Add(this.button_ping);
            this.PingTest.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PingTest.Location = new System.Drawing.Point(4, 29);
            this.PingTest.Name = "PingTest";
            this.PingTest.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.PingTest.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PingTest.Size = new System.Drawing.Size(1423, 587);
            this.PingTest.TabIndex = 1;
            this.PingTest.Text = "PingTest";
            this.PingTest.UseVisualStyleBackColor = true;
            // 
            // textBox_Log
            // 
            this.textBox_Log.Location = new System.Drawing.Point(622, 37);
            this.textBox_Log.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Log.Multiline = true;
            this.textBox_Log.Name = "textBox_Log";
            this.textBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Log.Size = new System.Drawing.Size(649, 501);
            this.textBox_Log.TabIndex = 10;
            this.textBox_Log.WordWrap = false;
            // 
            // labelRouterIp
            // 
            this.labelRouterIp.AutoSize = true;
            this.labelRouterIp.Location = new System.Drawing.Point(6, 247);
            this.labelRouterIp.Name = "labelRouterIp";
            this.labelRouterIp.Size = new System.Drawing.Size(0, 25);
            this.labelRouterIp.TabIndex = 9;
            // 
            // labelPingDelay
            // 
            this.labelPingDelay.AutoSize = true;
            this.labelPingDelay.Location = new System.Drawing.Point(47, 192);
            this.labelPingDelay.Name = "labelPingDelay";
            this.labelPingDelay.Size = new System.Drawing.Size(385, 25);
            this.labelPingDelay.TabIndex = 8;
            this.labelPingDelay.Text = "Задержка перед отправкой пакетов";
            // 
            // labelPingCount
            // 
            this.labelPingCount.AutoSize = true;
            this.labelPingCount.Location = new System.Drawing.Point(47, 96);
            this.labelPingCount.Name = "labelPingCount";
            this.labelPingCount.Size = new System.Drawing.Size(224, 25);
            this.labelPingCount.TabIndex = 7;
            this.labelPingCount.Text = "Количество пакетов";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(47, 5);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(356, 25);
            this.labelIP.TabIndex = 6;
            this.labelIP.Text = "IP адреса хостов (через запятую)";
            // 
            // checkbox_log
            // 
            this.checkbox_log.AutoSize = true;
            this.checkbox_log.Location = new System.Drawing.Point(251, 311);
            this.checkbox_log.Name = "checkbox_log";
            this.checkbox_log.Size = new System.Drawing.Size(279, 29);
            this.checkbox_log.TabIndex = 5;
            this.checkbox_log.Text = "Записывать лог в файл";
            this.checkbox_log.UseVisualStyleBackColor = true;
            // 
            // textbox_delay
            // 
            this.textbox_delay.Location = new System.Drawing.Point(47, 224);
            this.textbox_delay.Name = "textbox_delay";
            this.textbox_delay.Size = new System.Drawing.Size(129, 34);
            this.textbox_delay.TabIndex = 3;
            this.textbox_delay.Text = "0";
            // 
            // textbox_pingCount
            // 
            this.textbox_pingCount.Location = new System.Drawing.Point(47, 128);
            this.textbox_pingCount.Name = "textbox_pingCount";
            this.textbox_pingCount.Size = new System.Drawing.Size(129, 34);
            this.textbox_pingCount.TabIndex = 2;
            this.textbox_pingCount.Text = "0";
            // 
            // textBox_hosts
            // 
            this.textBox_hosts.Location = new System.Drawing.Point(47, 37);
            this.textBox_hosts.Name = "textBox_hosts";
            this.textBox_hosts.PlaceholderText = "vk.com, google.com";
            this.textBox_hosts.Size = new System.Drawing.Size(545, 34);
            this.textBox_hosts.TabIndex = 1;
            // 
            // button_ping
            // 
            this.button_ping.Location = new System.Drawing.Point(31, 300);
            this.button_ping.Name = "button_ping";
            this.button_ping.Size = new System.Drawing.Size(177, 49);
            this.button_ping.TabIndex = 0;
            this.button_ping.Text = "Старт";
            this.button_ping.UseVisualStyleBackColor = true;
            this.button_ping.Click += new System.EventHandler(this.button_ping_Click);
            // 
            // ProcessMonitor
            // 
            this.ProcessMonitor.Controls.Add(this.label4);
            this.ProcessMonitor.Controls.Add(this.label3);
            this.ProcessMonitor.Controls.Add(this.label2);
            this.ProcessMonitor.Controls.Add(this.label1);
            this.ProcessMonitor.Controls.Add(this.button_monitor_start);
            this.ProcessMonitor.Controls.Add(this.processList);
            this.ProcessMonitor.Font = new System.Drawing.Font("Times New Roman", 12.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProcessMonitor.Location = new System.Drawing.Point(4, 29);
            this.ProcessMonitor.Margin = new System.Windows.Forms.Padding(50, 51, 50, 51);
            this.ProcessMonitor.Name = "ProcessMonitor";
            this.ProcessMonitor.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.ProcessMonitor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ProcessMonitor.Size = new System.Drawing.Size(1423, 587);
            this.ProcessMonitor.TabIndex = 0;
            this.ProcessMonitor.Text = "ProcessMonitor";
            this.ProcessMonitor.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1187, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Получено кбайт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(771, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Отправлено кбайт";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Имя процесса";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID Процесса";
            // 
            // button_monitor_start
            // 
            this.button_monitor_start.Location = new System.Drawing.Point(8, 5);
            this.button_monitor_start.Name = "button_monitor_start";
            this.button_monitor_start.Size = new System.Drawing.Size(155, 45);
            this.button_monitor_start.TabIndex = 1;
            this.button_monitor_start.Text = "Запустить";
            this.button_monitor_start.UseVisualStyleBackColor = true;
            this.button_monitor_start.Click += new System.EventHandler(this.button_monitor_start_Click);
            // 
            // processList
            // 
            this.processList.AutoScroll = true;
            this.processList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.processList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.processList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.processList.Location = new System.Drawing.Point(3, 113);
            this.processList.Margin = new System.Windows.Forms.Padding(3, 27, 3, 3);
            this.processList.Name = "processList";
            this.processList.Size = new System.Drawing.Size(1417, 471);
            this.processList.TabIndex = 0;
            this.processList.WrapContents = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.ProcessMonitor);
            this.tabControl1.Controls.Add(this.PingTest);
            this.tabControl1.Controls.Add(this.NetworkInfo);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1431, 620);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 619);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "NetworkMonitor";
            this.NetworkInfo.ResumeLayout(false);
            this.NetworkInfo.PerformLayout();
            this.PingTest.ResumeLayout(false);
            this.PingTest.PerformLayout();
            this.ProcessMonitor.ResumeLayout(false);
            this.ProcessMonitor.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabPage NetworkInfo;
        private FlowLayoutPanel iList;
        private Label label_route_ip;
        private Label label_ip;
        private Label label12;
        private Label label11;
        private Label label7;
        private Label label6;
        private Label label5;
        private TabPage PingTest;
        private TextBox textBox_Log;
        private Label labelRouterIp;
        private Label labelPingDelay;
        private Label labelPingCount;
        private Label labelIP;
        private CheckBox checkbox_log;
        private TextBox textbox_delay;
        private TextBox textbox_pingCount;
        private TextBox textBox_hosts;
        private Button button_ping;
        private TabPage ProcessMonitor;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button button_monitor_start;
        private FlowLayoutPanel processList;
        private TabControl tabControl1;
    }
}