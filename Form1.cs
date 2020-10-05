using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Management;

namespace IP_check
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();



            //========== IPv4 (Internal IP) ==========
            string localIP = "Not available, please check your network seetings!";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    this.textBox1.Text = ip.ToString();
                }
            }
            //========== IPv4 (Internal IP) ==========


            //========== External IP ==========
            string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim(); //http://icanhazip.com

            if (String.IsNullOrWhiteSpace(externalip))
            {
                externalip = null;//null경우 Get Internal IP를 가져오게 한다.
            }
            this.textBox2.Text = externalip;
            //return externalip;
            //========== External IP ==========


            //========== MAC ===========
            string MacAddress = "";
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                System.Net.NetworkInformation.PhysicalAddress pa = adapter.GetPhysicalAddress();
                if (pa != null && !pa.ToString().Equals(""))
                {
                    MacAddress = pa.ToString();
                    break;
                }
            }
            //return MacAddress;
            this.textBox3.Text = MacAddress;
            //========== MAC ===========



            //========== User name ==========
            var input = WindowsIdentity.GetCurrent().Name;
            string[] tab = input.Split('\\');
            var result = tab[1];               //var result = tab[1] + "@" + tab[0];
            this.textBox4.Text = result;
            //========== User name ==========



            //========== Host name ==========
            String hostName = Dns.GetHostName();
            Console.WriteLine("Computer name :" + hostName);
            this.textBox5.Text = hostName;
            //========== Host name ==========

        }




        //=========== 참고 ============
        // 호스트 이름으로 IP를 구한다
        //IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        //IPAddress[] addr = ipEntry.AddressList;

        //    for (int i = 0; i<addr.Length; i++)
        //    {
        //        Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
        //        this.textBox6.Text = addr[i].ToString();
        //}
        //=========== 참고 ============




        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox3.Text);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox4.Text);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox5.Text);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://92rkskekfk.tistory.com/12");
        }



        private void button6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("IPv4 : " + textBox1.Text + "\r\n" + "External IP : " + textBox2.Text + "\r\n" + "MAC Address : " + textBox3.Text + "\r\n" + "User name : " + textBox4.Text + "\r\n" + "Host name : " + textBox5.Text);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();
            cmd.FileName = @"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
            cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;        // cmd창에서 데이터를 가져오기
            cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
            cmd.RedirectStandardError = true;          // cmd창에서 오류 내용 가져오기

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();
            //process.StandardInput.Write(this.textBox6.Text + Environment.NewLine);
            process.StandardInput.Write("netsh wlan disconnect" + Environment.NewLine);
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();
            StringBuilder sb = new StringBuilder();
            sb.Append("[Result Info]" + DateTime.Now + "\r\n");
            sb.Append(result);
            sb.Append("\r\n");

            //textBox7.Text = sb.ToString();
            process.WaitForExit();
            process.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();
            cmd.FileName = @"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
            cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;        // cmd창에서 데이터를 가져오기
            cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
            cmd.RedirectStandardError = true;          // cmd창에서 오류 내용 가져오기

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();
            process.StandardInput.Write("netsh wlan connect name = " + this.textBox6.Text + Environment.NewLine);
            //netsh wlan connect name = "WifiNetWorkName"
            // 명령어를 보낼때는 꼭 마무리를 해줘야 한다. 그래서 마지막에 NewLine가 필요하다
            process.StandardInput.Close();

            string result = process.StandardOutput.ReadToEnd();
            StringBuilder sb = new StringBuilder();
            sb.Append("[Result Info]" + DateTime.Now + "\r\n");
            sb.Append(result);
            sb.Append("\r\n");

            //textBox7.Text = sb.ToString();
            process.WaitForExit();
            process.Close();

            //netsh wlan disconnect

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Windows\System32\ncpa.cpl");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();
            cmd.FileName = @"cmd";    //@"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
            cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;        // cmd창에서 데이터를 가져오기
            cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
            cmd.RedirectStandardError = true;          // cmd창에서 오류 내용 가져오기

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();

            //process.StandardInput.Write(@"getmac /v" + Environment.NewLine);
            process.StandardInput.Write(@"netsh wlan show profiles" + Environment.NewLine);
            
            //this.textBox7.Text = process.ToString();

            process.StandardInput.Close();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();

            MessageBox.Show(result);
        }
    }
}
