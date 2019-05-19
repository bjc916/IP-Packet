using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Packet
{
    public partial class Form2 : Form
    {
        // class constant definitions
        public const int BIT_RATE_LENGTH = 8;
        public byte _biteSniffed = new byte();
        public Delegate _DRH;
        public bool eventHandlers = false;
        // declare instance variables
        Form1 _frm1;
        // enum declarations used to provision combo boxes

        //iana port numbers
        public Form2(Form1 frm1)
        {
            //_DRH = DRH;
            _frm1 = frm1;
            InitializeComponent();
            // initialize all of the ComboBoxes shown in GUI
            //loadComPortComboBox();
        }

        //private void loadComPortComboBox()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            cmboIPaddresses.Items.Add(ip.ToString());
        //        }
        //    }

        //    //sort the COM port names stored in the comboBox control
        //    cmboIPaddresses.Sorted = true;
        //    if (cmboIPaddresses.Items.Count != 0)
        //    {
        //        cmboIPaddresses.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        cmboIPaddresses.Text = "";    // empty string
        //        throw new Exception("No network adapters with an IPv4 address in the system!");
        //    }
        //}   // end loadComPortComboBox() definition

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenConnection_Click_2(object sender, EventArgs e)
        {
            IPAddress IP = null;
            if(txtIPaddress.Text != "")
            {
                try
                {
                    IP = IPAddress.Parse(txtIPaddress.Text);
                }
                catch(Exception)
                {
                    MessageBox.Show("IP Entry Error");
                }

            }
            // if (cmboIPaddresses.Items.Count > 0)
            //{
            //    IP = IPAddress.Parse(cmboIPaddresses.SelectedItem.ToString());
            //}
            IPEndPoint localIP = new IPEndPoint(IP, Convert.ToInt32(txtPortNumber.Text));
            Socket client = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
       
            if (IP != null)
            {
                if (_frm1._transmitMode)
                {
                    try
                    {
                        client.Connect(localIP);

                        if (client.Connected)
                        {
                            _frm1._clientSocket = client;
                            _frm1._netwrkStream = new NetworkStream(client);
                            _frm1.toolStripStatusLabel1.Text = "Connected to " + localIP.Address;
                            _frm1.manageClientTCPconnection();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Connection failed. \n\n Try opening port in 'Receive' mode in another window.");
                    }
                }
                else
                {
                    _frm1.manageServerTCPconnection(localIP);
                }
            }
            this.Close();
        }
    }
}
