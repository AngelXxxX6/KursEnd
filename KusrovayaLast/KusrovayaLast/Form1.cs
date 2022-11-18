using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System;
using System.Text.RegularExpressions;
namespace KusrovayaLast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           
            InitializeComponent();

            Info();
        }
           
        void IpInfo()
        {
            NetExt.GetRouteIp();
            label_route_ip.Text = NetExt.ROUTER_IP;
            var host = Dns.GetHostEntry(Dns.GetHostName());
           

            string a = "";
           
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    a = ip.ToString();

                }
            }
            label_ip.Text = a;
        }
        void Info()
        {
            IpInfo();

            processList.Width = tabControl1.Width;
            processList.Padding = new Padding(0, 0, 0, 0);
            NetworkInterface[] vrem = NetworkInterface.GetAllNetworkInterfaces();
            var b = OperationalStatus.Up;
            List<NetworkInterface> networkInterfaces = new List<NetworkInterface>();
            for (int i = 0; i < vrem.Length; i++)
            {

                if (vrem[i].OperationalStatus == b)
                {

                    networkInterfaces.Add(vrem[i]);
                }

            }
            for (int i = 0; i < vrem.Length; i++)
            {

                if (vrem[i].OperationalStatus != b)
                {

                    networkInterfaces.Add(vrem[i]);
                }

            }
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                var a = new Panel();
                var name = new Label();
                var speed = new Label();
                var active = new Label();
                name.Text = networkInterface.Name;
                speed.Text = (networkInterface.Speed / 1000000).ToString();
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    active.Text = "Активен";
                }
                else
                {
                    active.Text = "Не активен";
                }



                name.AutoSize = true;
                speed.AutoSize = true;
                active.AutoSize = true;
                a.Width = iList.Width - 30;
                a.Height = 50;
                a.Controls.Add(name);
                a.Controls.Add(speed);
                a.Controls.Add(active);
                a.BorderStyle = BorderStyle.FixedSingle;


                speed.Location = new Point(800, speed.Location.Y);
                active.Location = new Point(550, active.Location.Y);
                name.Location = new Point(0, name.Location.Y);

                a.Padding = new Padding(0, 10, 0, 0);

                iList.Controls.Add(a);
            }

        }
            

        private void button_ping_Click(object sender, EventArgs e)
        {
           
                
                List<String> hosts = new List<String>();

            if (textBox_hosts.Text != "")
            {

                String[] s = textBox_hosts.Text.Split(",");

                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = s[i].Trim();
                    hosts.Add(s[i]);
                }
            }

                
            

                


                NetExt.NetCheck(hosts, int.Parse(textbox_pingCount.Text), int.Parse(textbox_delay.Text), checkbox_log.Checked,button_ping,textBox_Log);
            }
            
        
        

        private void button_monitor_start_Click(object sender, EventArgs e)
        {
             NetExt.ProcMon(processList,button_monitor_start);
           
           
        }
    }
}