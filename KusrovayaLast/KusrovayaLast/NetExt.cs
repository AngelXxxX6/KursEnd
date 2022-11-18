using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Diagnostics.NETCore.Client;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;
using System.Runtime.CompilerServices;
using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.EventPipe;
using Microsoft.Diagnostics.Tracing.Parsers.AspNet;
using System.Drawing.Drawing2D;

namespace KusrovayaLast
{
    public static class NetExt
    {
        struct net_state
        {
            public bool inet_ok; // Флаг доступности сети
            public bool http_ok; // Флаг теста http
            public Dictionary<String, int> avg_rtts; // Словарь пинга до хостов
            public double packet_loss; // Потеря пакетов
            public double packet_success; // Приятно покетов
            public double packet_total; // Всего пакетов
            public DateTime measure_time; // Дата, время
            public int router_rtt; // 
        }
        class UI
        {
            public Label procName, ProcID, procSent, procRecv;
           
            public UI(Label procName, Label procID, Label procSent, Label procRecv)
            {
                this.procName = procName;
                ProcID = procID;
                this.procSent = procSent;
                this.procRecv = procRecv;
            }
        }
        public class netProcess
        {
            public float sent;
            public float recv;
            public String name;
           public int id;
            public Panel group;
           public netProcess(float sent, float recv, String name, int id, Panel group)
            {
                this.sent = sent;
                this.recv = recv;
                this.name = name;
                this.id = id;
                this.group = group;
            }
        }
        #region PARAMS_PING
        static String HTTP_TEST_HOST; // HTTP сервер, соединение до которого будем тестировать
        static int HTTP_TEST_PORT; // Порт HTTP сервера
        static int HTTP_TIMEOUT; // Таймаут подключения
        static int PING_COUNT; // Количество пакетов пинга
        static int PING_DELAY; // Ожидание перед отправкой следующего пакета пинга
        static int PING_TIMEOUT; // Таймаут пинга
        static List<String> PING_HOSTS;  // Хосты, пинг до которых меряем
        static int MEASURE_DELAY; // Время между проверками
        public static String ROUTER_IP; // IP роутера
        static double MAX_PKT_LOSS; // Максимально допустимый Packet loss
        static String OUT_FILE; // Выходной файл CSV
        static bool WRITE_CSV; // Писать ли CSV
        static String CSV_PATTERN; // Шаблон для записи в CSV
                                   // Промежуточные переменные
        static bool prev_inet_ok = true;
        static DateTime first_fail_time;
        static long total_time = 0;
        static int pkt_sent = 0;
        static int success_pkts = 0;
        static int exited_threads = 0;
        static Dictionary<string, int> measure_results = new Dictionary<string, int>();
       public static bool StartPing = false;
       
        #endregion
        #region PARAMS_MON
        private static TraceEventSession m_EtwSession;
        static List<netProcess> netProcesses;
        static List<UI> uiPr;
        public static bool monStart = false;
        static Process[] processes;

        #endregion
        #region INFO
        static NetworkInterface[] nicArr;
        static List<NetworkInterface> currentInterfaces = new List<NetworkInterface>();

        #endregion
        public static void NetCheck(List<String> hosts, int pingCount, int pingDelay, bool log,Button button,TextBox textLog)
        {
            if(StartPing)
            {
                StartPing = false;
                PING_HOSTS.Clear();
                
                button.Invoke(() => button.Text = "Старт");
                return;
            }
            textLog.Clear();
            button.Invoke(() => button.Text = "Стоп");
           
            StartPing = true;
            
            var config = JsonConvert.DeserializeObject<Dictionary<String, Object>>(File.ReadAllText("setting.json"));
            
            GetRouteIp();
            HTTP_TEST_HOST = (String)config["http_test_host"];
            if (hosts.Count == 0)
            {
                PING_HOSTS = ((JArray)config["ping_hosts"]).ToObject<List<String>>();
            }
            else
            {
                PING_HOSTS = hosts;
            }
            HTTP_TEST_PORT = int.Parse((String)config["http_test_port"]);
            HTTP_TIMEOUT = int.Parse((String)config["http_timeout"]);
            if (pingCount == 0)
            {
                PING_COUNT = int.Parse((String)config["ping_count"]);
            }
            else
            {
                PING_COUNT = pingCount;
            }
            if (pingDelay == 0)
            {
                PING_TIMEOUT = int.Parse((String)config["ping_timeout"]);
            }
            else
            {
                PING_TIMEOUT = pingDelay;
            }
            PING_DELAY = int.Parse((String)config["ping_packet_delay"]);
            MEASURE_DELAY = int.Parse((String)config["measure_delay"]);
            OUT_FILE = (String)config["out_file"];
            WRITE_CSV = bool.Parse((String)config["w_csv"]);
            CSV_PATTERN = (String)config["out_format"];
            MAX_PKT_LOSS = double.Parse((String)config["nq_max_loss"]);
            if(log)
            {
                WRITE_CSV = true;
            }
            else
            {
                WRITE_CSV = false;
            }
           
            String CSV_HEADER = CSV_PATTERN
                    .Replace("FTIME", "Data")
                    
                    .Replace("AVGRTT", "Average ping (ms)")
                    .Replace("ROUTERRTT", "Ping to router (ms)")
                    .Replace("TOTAL", "Total packets")
                    .Replace("LOSS", "Packet loss, %")
                    .Replace("SUCCESS", "Packet success, %")
                    .Replace("HTTP", "HTTP conntection")
                    .Replace("STIME", "Time")
                    .Replace("R_IP", "Router IP");

            foreach (var host in PING_HOSTS)
            {
                CSV_HEADER = CSV_HEADER.Replace("RN", $"Ping to {host};RN");
            }
            CSV_HEADER = CSV_HEADER.Replace("RN", "\r\n");
            if (WRITE_CSV) // Если запись включена в настройках создать файл и записать заголовки.
            {
                // Если файла нету , создать и записать заголовки.
                if (!File.Exists(OUT_FILE)) File.WriteAllText(OUT_FILE, CSV_HEADER);
            }
            
                new Thread(() => Monit(textLog)).Start();
                Thread.Sleep(MEASURE_DELAY);
            
        }
        static void Save_log(net_state snapshot,TextBox log)
        {
            if (WRITE_CSV && StartPing) // Если запись логов включена
            {
                String rtts = "";
                int avg_rtt = 0;
                foreach (var ci in PING_HOSTS)
                {
                    rtts += $"{snapshot.avg_rtts[ci]};";
                    avg_rtt += snapshot.avg_rtts[ci];
                }
                avg_rtt = avg_rtt / PING_HOSTS.Count;
                File.AppendAllText(OUT_FILE, CSV_PATTERN
                    .Replace("FTIME", snapshot.measure_time.ToShortDateString())
                    
                    .Replace("AVGRTT", avg_rtt.ToString())
                    .Replace("ROUTERRTT", snapshot.router_rtt.ToString())
                    .Replace("TOTAL", snapshot.packet_total.ToString())
                    .Replace("SUCCESS", (snapshot.packet_success * 100).ToString())
                    .Replace("LOSS", (snapshot.packet_loss * 100).ToString())
                    .Replace("HTTP", snapshot.http_ok.ToString())
                    .Replace("STIME", snapshot.measure_time.ToShortTimeString())
                    .Replace("R_IP", ROUTER_IP)
                    .Replace("RN", $"{rtts}\r\n"));
            }
            
            if(snapshot.packet_total !=0)
                log.Invoke(() => log.Text += "Отпарвлено пакетов: " + snapshot.packet_total.ToString() + "; " + "Пакетов получено: " + snapshot.packet_success*snapshot.packet_total + Environment.NewLine);
            
        }
        static void Monit(TextBox log)
        {

            while (StartPing)
            {
                // Создаем экземпляр измерений.
                net_state snapshot = new net_state();
                snapshot.inet_ok = true;
                snapshot.measure_time = DateTime.Now;
                // Проверяем доступность роутера
                Ping ping = new Ping();
                var prr = ping.Send(ROUTER_IP, PING_TIMEOUT);
                // В CSV файле все поля должны быть заполнены. Если роутер не пингуется заполняем их параметром PING_TIMEOUT
                snapshot.router_rtt = prr.Status == IPStatus.Success ? (int)prr.RoundtripTime : PING_TIMEOUT;
                if (prr.Status != IPStatus.Success)
                {
                    snapshot.avg_rtts = new Dictionary<string, int>();
                    snapshot.http_ok = false;
                    snapshot.inet_ok = false;
                    snapshot.packet_loss = 1;
                    foreach (var ci in PING_HOSTS)
                    {
                        snapshot.avg_rtts.Add(ci, PING_TIMEOUT);
                    }
                    
                    Save_log(snapshot,log);
                    return;
                }
                snapshot.inet_ok = true;
                // Проверяем доступность HTTP
                try
                {
                    snapshot.http_ok = true;
                    TcpClient tc = new TcpClient();
                    tc.BeginConnect(HTTP_TEST_HOST, HTTP_TEST_PORT, null, null);
                    Thread.Sleep(HTTP_TIMEOUT);
                    // Если подключиться не удалось
                    if (!tc.Connected)
                    {
                        snapshot.http_ok = false;
                    }
                    tc.Dispose();
                }
                catch { snapshot.http_ok = false; snapshot.inet_ok = false; }
                // пингуем заданные хосты
                exited_threads = 0;
                pkt_sent = 0;
                success_pkts = 0;
                total_time = 0;
                measure_results = new Dictionary<string, int>();
                // Перебираем все хосты и запускаем пинг в отдельном потоке.
                foreach (var ci in PING_HOSTS)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(PingTest));
                    thread.Start(ci);
                }
                while (exited_threads < PING_HOSTS.Count) continue;
                //Анализируем результаты пинга
                snapshot.avg_rtts = measure_results;
                snapshot.packet_total = pkt_sent;
                snapshot.packet_loss = (double)(pkt_sent - success_pkts) / pkt_sent;
                snapshot.packet_success = (double)(success_pkts) / pkt_sent;
                snapshot.inet_ok = !(
                    snapshot.http_ok == false ||
                    ((double)total_time / success_pkts >= 0.75 * PING_TIMEOUT) ||
                    snapshot.packet_loss >= MAX_PKT_LOSS ||
                    snapshot.router_rtt == PING_TIMEOUT);

                Save_log(snapshot,log);
                if (prev_inet_ok && !snapshot.inet_ok)
                {
                    //Интернет был , но потом пропал
                    prev_inet_ok = false;
                    first_fail_time = DateTime.Now;
                }
                else if (!prev_inet_ok && snapshot.inet_ok)
                {
                    String t_s = new TimeSpan(DateTime.Now.Ticks - first_fail_time.Ticks).ToString(@"hh\:mm\:ss");
                    prev_inet_ok = true;
                }
            }
        }
       
      
      public static void GetRouteIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            int n = 0;

            string a = "";
            string a2 = "";
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    a = ip.ToString();

                }
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (n == 3)
                {
                    a2 += '1';
                    break;
                }
                else
                if (a[i] == '.')
                {
                    n++;
                }
                a2 += a[i];
            }
            ROUTER_IP = (String)a2;
        }
        

      public  static void ProcMon(FlowLayoutPanel panel, Button button)
        {
            if(monStart)
            {
                monStart = false;
                m_EtwSession.Dispose();
               
                button.Invoke(() => button.Text = "Старт");
                
                
                ClearMon();
                return;
            }
            
            button.Invoke(() => button.Text = "Стоп");
             netProcesses = new List<netProcess>();
            uiPr = new List<UI>();
           
            
           
            monStart = true;
            new Thread(() => ThreadProc(panel)).Start();
            new Thread(() => GetProces()).Start();




        }
        static void GetProces()
        {
            while (monStart)
            {
                processes = Process.GetProcesses();
            }
        }
        static void ClearMon()
        {
            
            for (int i = 0; i < netProcesses.Count; i++)
            {
                netProcesses[i].group.Dispose();
                uiPr[i].procName.Dispose();
                uiPr[i].ProcID.Dispose();
                uiPr[i].procSent.Dispose();
                uiPr[i].procRecv.Dispose();
            }
            netProcesses.Clear();
            uiPr.Clear();
            

        }
        static void ThreadProc(FlowLayoutPanel panel)
        {
            object locker = new();
            processes = Process.GetProcesses();
            try { 
            using (m_EtwSession = new TraceEventSession(KernelTraceEventParser.KernelSessionName))
            {
                m_EtwSession.StopOnDispose = true;
                m_EtwSession.EnableKernelProvider(KernelTraceEventParser.Keywords.NetworkTCPIP);

                m_EtwSession.Source.Kernel.TcpIpRecv += data =>
                {
                    foreach (var id in processes)
                    {
                        if (data.ProcessID == id.Id && monStart)
                        {
                            bool exist = false;
                            for (int i = 0; i < netProcesses.Count; i++)
                            {
                                if (netProcesses[i].name == data.ProcessName || netProcesses[i].id == data.ProcessID)
                                {
                                    netProcesses[i].recv += data.size;
                                    uiPr[i].procRecv.Invoke(() => uiPr[i].procRecv.Text = MathF.Round(netProcesses[i].recv / 1024,2).ToString());

                                    exist = true;
                                    break;
                                }
                            }
                            if (!exist)
                            {
                                var a = new Panel();
                                var name = new Label();
                                var idL = new Label();
                                var sent = new Label();
                                sent.TextAlign = ContentAlignment.MiddleRight;
                                var recv = new Label();
                                recv.TextAlign = ContentAlignment.MiddleRight;
                                name.AutoSize = true;
                                idL.AutoSize = true;
                                sent.AutoSize = true;
                                recv.AutoSize = true;
                                name.Text = data.ProcessName;

                                idL.Text = data.ProcessID.ToString();
                                sent.Text = "0";
                                recv.Text = data.size.ToString();
                                var b = new UI(name, idL, sent, recv);


                                a.Width = panel.Width - 30 ;
                                a.Height = 50;
                                a.Controls.Add(recv);
                                a.Controls.Add(sent);
                                a.Controls.Add(idL);
                                a.BorderStyle = BorderStyle.FixedSingle;
                                a.Controls.Add(name);

                                recv.Location = new Point(1200, recv.Location.Y);
                                sent.Location = new Point(800, sent.Location.Y);
                                idL.Location = new Point(0, idL.Location.Y);
                                name.Location = new Point(270, name.Location.Y);


                                uiPr.Add(b);
                                a.Padding = new Padding(0, 10, 0, 0);
                                panel.Invoke(() => panel.Controls.Add(a));




                                netProcesses.Add(new netProcess(0, MathF.Round(data.size / 1024,2), data.ProcessName, data.ProcessID, a));


                            }
                        }
                    }


                };

                m_EtwSession.Source.Kernel.TcpIpSend += data =>
                {
                    foreach (var id in processes)
                    {
                        if (data.ProcessID == id.Id && monStart)
                        {
                            bool exist = false;
                            for (int i = 0; i < netProcesses.Count; i++)
                            { if (netProcesses[i].name == data.ProcessName || netProcesses[i].id == data.ProcessID)
                                {
                                    netProcesses[i].sent += data.size;
                                    uiPr[i].procSent.Invoke(() => uiPr[i].procSent.Text = MathF.Round(netProcesses[i].sent / 1024,2).ToString());
                                    exist = true;
                                    break;
                                }
                            }
                            if (!exist)
                            {
                                var a = new Panel();
                                var name = new Label();
                                var idL = new Label();
                                var sent = new Label();
                                sent.TextAlign = ContentAlignment.MiddleRight;
                                var recv = new Label();
                                recv.TextAlign = ContentAlignment.MiddleRight;
                                name.AutoSize = true;
                                idL.AutoSize = true;
                                sent.AutoSize = true;
                                recv.AutoSize = true;
                                name.TextAlign = ContentAlignment.MiddleCenter;
                                name.Text = data.ProcessName;
                                idL.Text = data.ProcessID.ToString();
                                sent.Text = data.size.ToString();
                                recv.Text = "0";
                                var b = new UI(name, idL, sent, recv);
                                a.Width = panel.Width-30;

                                a.Height = 50;
                                a.Controls.Add(recv);
                                a.Controls.Add(sent);
                                a.Controls.Add(idL);
                                a.Controls.Add(name);
                                a.BorderStyle = BorderStyle.FixedSingle;
                                a.Padding = new Padding(0, 10, 0, 0);
                                recv.Location = new Point(1200, recv.Location.Y);
                                sent.Location = new Point(800, sent.Location.Y);
                                idL.Location = new Point(0, idL.Location.Y);
                                name.Location = new Point(270, name.Location.Y);

                                uiPr.Add(b);
                                panel.Invoke(() => panel.Controls.Add(a));

                                netProcesses.Add(new netProcess(MathF.Round(data.size / 1024, 2), 0, data.ProcessName, data.ProcessID, a));
                            }
                        }
                    }
                };

                m_EtwSession.Source.Process();
            }
            
            }
            catch (System.ObjectDisposedException)
            {

            }
        }
        
        static void PingTest(Object arg)
        {
            String host = (String)arg;
            int pkts_lost_row = 0;
            int local_success = 0;
            long local_time = 0;
            Ping ping = new Ping();
            // Запускаем пинг заданное количество раз.
            for (int i = 0; i < PING_COUNT; i++)
            {
                // Если потеряно 3 пакеты, записываем результаты и выходим из цикла
                if (pkts_lost_row == 3)
                {
                    measure_results.Add(host, (int)(local_time / (local_success == 0 ? 1 : local_success)));
                    exited_threads++;
                    return;
                }
                try
                {

                    var result = ping.Send(host, PING_TIMEOUT);

                    // Если пинг прошел
                    if (result.Status == IPStatus.Success)
                    {
                        pkts_lost_row = 0;
                        local_success++;
                        // RoundtripTime Возвращает количество миллисекунд, затраченных на отправку Эхо-запроса
                        local_time += result.RoundtripTime;
                        total_time += result.RoundtripTime;

                        pkt_sent++;
                        success_pkts++;

                    }
                    switch (result.Status)
                    {
                        case IPStatus.Success: break; //Already handled 
                        case IPStatus.BadDestination:
                            measure_results.Add(host, -1);
                            exited_threads++;
                            return;
                        case IPStatus.DestinationHostUnreachable:
                        case IPStatus.DestinationNetworkUnreachable:
                        case IPStatus.DestinationUnreachable:
                            measure_results.Add(host, -1);
                            exited_threads++;
                            return;
                        case IPStatus.TimedOut:
                            pkts_lost_row++;
                            pkt_sent++;
                           
                            break;
                        default:
                            measure_results.Add(host, -1);
                            exited_threads++;
                            return;
                    }
                }
                catch (Exception xc)
                {
                    exited_threads++;
                    measure_results.Add(host, -1);
                    return;
                }
            }
            measure_results.Add(host, (int)(local_time / (local_success == 0 ? 1 : local_success)));
            exited_threads++;
            return;
        }
    }
}
