using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using Microsoft.Win32;
using System.IO.Ports;
using System.Windows.Threading;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Packet
{
    public partial class Form1 : Form
    {
        // public System.IO.Ports.SerialPort serialPort1 = new System.IO.Ports.SerialPort();
        private string _fileName = "";
        private string _fileContent = "";
        private int fileIndex = 0; 
        private bool ConnectionAcked = false;
        private int _length = 0;
        private bool gotSOH = false;
        private bool gotLength = false;
        private bool gotMessage = false;
        public bool _transmitMode = true;
        private bool errorFree = true;
        private bool CRCwithRecovery = false;
        private bool CRCwithoutRecovery = false;
        private bool SOHwithRecovery = false;
        private bool callFileSelectWindow = false;
        private bool _fileSelected = false;
        private bool eot = false;
        private bool _gotFile = false;
        private bool _gotString = false;
        private int PacketIndex = 0;
        private int PacketLength = 0;
        private int PacketNumber = 0;
        private byte[] dummyBuffer = new byte[7];
        private byte[] EOT = new byte[1];
        private byte[] ENQ = new byte[1];
        private byte[] ACK = new byte[1];
        private byte[] NAK = new byte[1];
        private List<byte[]> fileContents = new List<byte[]>();
        private byte[] packetContents = new byte[5];
        private byte[] bufferedPacket = new byte[7];
        private int _sendAttempts = 0;
        public NetworkStream _netwrkStream;
        private BinaryWriter _writer;
        private BinaryReader _reader;
        public Socket _clientSocket;

        public TcpClient _client;


        public Encoding asciiEncoder = Encoding.ASCII;
        private CRC8 _crc8 = new CRC8();

        public Form1()
        {
            InitializeComponent();
            tstripCmboXmitRcv.SelectedItem = "Transmit";
            tstripCmboError.SelectedItem = "Error Free";
            rtxtAscii.Text = "Ascii View \n\n";
            rtxtHex.Text = "Hex View \n\n";
            EOT[0] = (byte)0x04;
            ENQ[0] = (byte)0x05;
            ACK[0] = (byte)0x06;
            NAK[0] = (byte)0x15;
            toolStripStatusLabel1.Text = "Connection Down";
            toolStripStatusLabel1.BackColor = Color.Red;
            tstripCheckConnection.Enabled = false;
        }

        public void manageClientTCPconnection()
        {
            tstripCheckConnection.Enabled = true;
            new Thread(() =>
            {
                while (_clientSocket.Connected)
                {
                    int count = _netwrkStream.Read(dummyBuffer, 0, 7);
                    byte[] PacketRecd = new byte[count];
                    Array.Copy(dummyBuffer, PacketRecd, count);
                    //only invoke if there are bytes to read
                    if (count != 0)
                    {
                        //send another packet to receiver bc packet was acked
                        if (PacketRecd[PacketIndex] == 0x06)
                        {
                            this.statusStrip1.Invoke((MethodInvoker)delegate {
                                txtPortStatus.Text = "Connected";
                                toolStripStatusLabel1.BackColor = Color.LightGreen;
                            });
                            tstripPortStatusOpen.Enabled = false;

                            rtxtAscii.AppendText("Recv:     ");
                            rtxtHex.AppendText("Recv:     ");
                            printPacket(PacketRecd[PacketIndex]);

                            //will not execute during ackNackConnection Check
                            if (ConnectionAcked)
                            {
                                sendFile();
                            }
                            if (eot)
                            {
                                MessageBox.Show("File sent successfully!");
                                eot = false;
                            }
                            ConnectionAcked = true;
                            _sendAttempts = 0;
                        }

                        //NAK 
                        if (PacketRecd[PacketIndex] == 0x15)
                        {
                            //indicate that the packet was received
                            rtxtAscii.AppendText("Recv:     ");
                            rtxtHex.AppendText("Recv:     ");
                            printPacket(PacketRecd[PacketIndex]);

                            if (CRCwithoutRecovery)
                            {
                                sendFile();
                            }

                            if (CRCwithRecovery || SOHwithRecovery)
                            {
                                //send packet 2 extra times
                                if (ConnectionAcked && _sendAttempts <= 1)
                                {
                                    //resend the packet
                                    rtxtAscii.AppendText("Send:     ");
                                    rtxtHex.AppendText("Send:     ");
                                    printPacket(bufferedPacket);
                                    _netwrkStream.Write(bufferedPacket, 0, bufferedPacket.Length);
                                    if (SOHwithRecovery)
                                    {
                                        reset();
                                    }
                                    var timer = new DispatcherTimer();
                                    timer.Interval = TimeSpan.FromMilliseconds(1000);
                                    timer.Start();
                                    _sendAttempts++;
                                }
                                if (_sendAttempts == 2)
                                {
                                    MessageBox.Show("3 NAKs Received");
                                    _sendAttempts = 0;
                                }
                            }
                        }
                    }
                    Array.Clear(PacketRecd, 0, PacketRecd.Length);
                }
            }).Start();

        }

        public void manageServerTCPconnection(IPEndPoint IP)
        {
            new Thread(async () =>
            {
                // Thread.CurrentThread.IsBackground = true;

                TcpListener server = new TcpListener(IP);
                server.Start();

                TcpClient client = await server.AcceptTcpClientAsync();

                if (client.Connected)
                {
                    _writer = new BinaryWriter(client.GetStream());
                    _reader = new BinaryReader(client.GetStream());
                    toolStripStatusLabel1.Text = "Connected to " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    this.statusStrip1.Invoke((MethodInvoker)delegate {
                        toolStripStatusLabel1.Text = "Connected to " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                        toolStripStatusLabel1.BackColor = Color.LightGreen;
                    });
                }
                while (client.Connected)
                {
                    int count = 0;

                    //only invoke if there are bytes to read
                    if ((count = _reader.Read(dummyBuffer, 0, 7)) != 0)
                    {
                        byte[] PacketRecd = new byte[count];
                        Array.Copy(dummyBuffer, PacketRecd, count);
                        rtxtAscii.Invoke(new EventHandler(delegate
                        {
                            gotMessage = false;

                            //get the bytes that were stored in the buffer
                            while (PacketIndex < count && gotMessage == false)
                            {
                                //get the SOH
                                if (gotSOH == false)
                                {
                                    //FOUND ENQ
                                    if (PacketRecd[PacketIndex] == 0x05)
                                    {
                                        txtPortStatus.Text = "Connected";
                                        _writer.Write(ACK, 0, 1);
                                        gotMessage = true;
                                    }
                                    else if (PacketRecd[PacketIndex] == 0x01)
                                    {
                                        gotSOH = true;
                                        PacketIndex++;
                                    }
                                    else
                                    {
                                        //corrupted SOH
                                        _writer.Write(NAK, 0, 1);
                                        gotMessage = true;
                                    }
                                }

                                //get the message (data)
                                if (PacketIndex >= 1 && PacketIndex != 6)
                                {
                                    //get the contents of the file
                                    if (PacketRecd[PacketIndex] == EOT[0])
                                    {
                                        callFileSelectWindow = true;
                                        //stop and go handshake
                                        //write to file
                                        byte[] temp = new byte[5];
                                        Array.Copy(PacketRecd, 1, temp, 0, PacketIndex - 1);
                                        fileContents.Add(temp);
                                    }
                                    PacketIndex++;
                                }

                                //if we received the whole message, GET THE CRC
                                if (PacketIndex == 6)
                                {
                                    //save 5 data bytes to global a list of bytes, representing the file contents
                                    byte[] temp = new byte[5];
                                    Array.Copy(PacketRecd, 1, temp, 0, 5);

                                    if (!callFileSelectWindow)
                                    {
                                        fileContents.Add(temp);
                                    }

                                    //CRC second pass - hopefully zero out CRC
                                    CRC8 crc8 = new CRC8(PacketRecd, this);
                                    byte CRC = crc8.GET_CRC();

                                    //stop and go handshake
                                    if (CRC != 0)
                                    {   //stop
                                        rtxtAscii.AppendText("Recv:     ");
                                        rtxtHex.AppendText("Recv:     ");
                                        printPacket(PacketRecd);
                                        _writer.Write(NAK, 0, 1);
                                        reset();
                                    }
                                    else
                                    {
                                        //go
                                        _writer.Write(ACK, 0, 1);

                                        //reset data
                                        rtxtAscii.AppendText("Recv:     ");
                                        rtxtHex.AppendText("Recv:     ");
                                        printPacket(PacketRecd);

                                        if (callFileSelectWindow)
                                        {
                                            SaveFileDialog file = new SaveFileDialog();
                                            file.ShowDialog();

                                            if (file.FileName != "")
                                            {
                                                ByteArrayToFile(file.FileName);
                                                PacketIndex = 5;
                                                fileContents.Clear();
                                            }
                                        }

                                        reset();
                                    }
                                }
                            }

                        }));
                    }
                }
            }).Start();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tstripCmboXmitRcv.Text == "Receive")
            {
                _transmitMode = false;
                tstripSendFile.Enabled = false;
                tstripLaunchFile.Enabled = false;
                tstripCmboError.Enabled = false;
                tstripCmboError.Visible = false;
            }
            else
            {
                _transmitMode = true;
                _transmitMode = true;
                tstripSendFile.Enabled = true;
                tstripLaunchFile.Enabled = true;
                tstripCmboError.Enabled = true;
                tstripCmboError.Visible = true;
            }
        }

        private void tstripPortStatusClose_Click(object sender, EventArgs e)
        {
          
        }

        private void tstripPortStatusOpen_Click(object sender, EventArgs e)
        {
            Form2 configIP = new Form2(this);
            configIP.Show();
        }

        private void tstripPortStatusClose_Click_1(object sender, EventArgs e)
        {
            //close the port
            //close the port
            _client.Close();

            if (!_client.Connected)
            {
                txtPortStatus.Text = "Not Connected";
            }
        }

        private void toolStripShowConfig_Click(object sender, EventArgs e)
        {
            //ShowConfig showCon = new Packet.ShowConfig(this);
            //showCon.Show();
        }

        private void tstripCheckConnection_Click(object sender, EventArgs e)
        {
            //set to false to prevent file from resending when check is clicked
            AckNackConnectionCheck();

            //reset packet send variables
            _gotFile = false;
            _gotString = false;
            eot = false;
        }

        private void tstripLaunchFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //read in from file to hex and ascii view
                //rtxtAscii.Text = File.ReadAllText(dlg.FileName);
                _fileName = dlg.FileName;
                _fileSelected = true;
            }
        }

        private void tstripSendFile_Click(object sender, EventArgs e)
        {
            sendFile();
        }

         private void tstripClearDisplay_Click(object sender, EventArgs e)
        {
            rtxtAscii.Clear();
            rtxtAscii.AppendText("Ascii View\n");
            rtxtHex.Clear();
            rtxtHex.AppendText("Hex View\n");
        }

        private bool AckNackConnectionCheck()
        {
            if (_clientSocket.Connected)
            {
                //initiate handshaking
                ConnectionAcked = false;
                _netwrkStream.Write(ENQ, 0, 1);
                rtxtAscii.AppendText("Send:     ");
                rtxtHex.AppendText("Send:     ");
                printPacket(ENQ);
                //timespan source: https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Start();
            }
            return true;  
        }

        public void reset()
        {
            gotMessage = true;
            //Array.Clear(PacketRecd, 0, _length);
            callFileSelectWindow = false;
            PacketIndex = 0;
            PacketLength = 0;
            gotSOH = false;
            gotLength = false;
            _length = 0;
            callFileSelectWindow = false;
        }

        private void printPacket(byte[] Packet)
        {
            for (int Ind = 0; Ind < Packet.Length; Ind++)
            {
                switch (Packet[Ind])
                {
                    case 0x01:
                        {//START OF HEADER
                            rtxtAscii.AppendText("'SOH'  ");
                            rtxtHex.AppendText("01   ");
                            break;
                        }
                    case 0x05:
                        {//ENQUIRE
                            rtxtAscii.AppendText("'ENQ'  ");
                            rtxtHex.AppendText("05   ");
                            break;
                        }
                    case 0x06:
                        {//ACKNOWLEDGE
                            rtxtAscii.AppendText("'ACK'  ");
                            rtxtHex.AppendText("06   ");
                            break;
                        }
                    case 0x15:
                        {//NEGATIVE ACK
                            rtxtAscii.AppendText("'NAK'  ");
                            rtxtHex.AppendText("15   ");

                            break;
                        }
                    case 0x0A:
                        {//LINEFEED
                            rtxtAscii.AppendText("'LF'  ");
                            rtxtHex.AppendText("0A   ");
                            break;
                        }
                    case 0x04:
                        {//END OF TRANSMISSION
                            rtxtAscii.AppendText("'EOT'  ");
                            rtxtHex.AppendText("04   ");
                            break;
                        }
                    case 0x0D:
                        {//CARIAGE RETURN
                            rtxtAscii.AppendText("'CR'  ");
                            rtxtHex.AppendText("0D   ");
                            break;
                        }
                    case 0xFF:
                        {//PADDING - FILL EMPTY PACKET SPACE THE PACKET
                            rtxtAscii.AppendText("'PD'  ");
                            rtxtHex.AppendText("FF   ");
                            break;
                        }
                    case 0x20:
                        {//SPACE CHARACTER
                            rtxtAscii.AppendText("'SP' ");
                            rtxtHex.AppendText("20   ");
                            break;
                        }
                    default:
                        {
                            string newstring = Convert.ToChar(Packet[Ind]).ToString();
                            rtxtAscii.AppendText(newstring + "   ");
                            rtxtHex.AppendText(Packet[Ind].ToString() + "   ");
                            break;
                        }
                }
            }
            rtxtAscii.AppendText("\n");
            rtxtHex.AppendText("\n");
        }

        //print overload to accomodate printing single bytes
        private void printPacket(byte Packet)
        {
            switch (Packet)
            {
                case 0x01:
                    {//START OF HEADER
                        rtxtAscii.AppendText("'SOH'  ");
                        rtxtHex.AppendText("01   ");
                        break;
                    }
                case 0x05:
                    {//ENQUIRE
                        rtxtAscii.AppendText("'ENQ'  ");
                        rtxtHex.AppendText("05   ");
                        break;
                    }
                case 0x06:
                    {//ACKNOWLEDGE
                        rtxtAscii.AppendText("'ACK'  ");
                        rtxtHex.AppendText("06   ");
                        break;
                    }
                case 0x15:
                    {//NEGATIVE ACK
                        rtxtAscii.AppendText("'NAK'  ");
                        rtxtHex.AppendText("15   ");

                        break;
                    }
                case 0x0A:
                    {//LINEFEED
                        rtxtAscii.AppendText("'LF'  ");
                        rtxtHex.AppendText("0A   ");
                        break;
                    }
                case 0x04:
                    {//END OF TRANSMISSION
                        rtxtAscii.AppendText("'EOT'  ");
                        rtxtHex.AppendText("04   ");
                        break;
                    }
                case 0x0D:
                    {//CARIAGE RETURN
                        rtxtAscii.AppendText("'CR'  ");
                        rtxtHex.AppendText("0D   ");
                        break;
                    }
                case 0xFF:
                    {//PADDING - FILL EMPTY PACKET SPACE THE PACKET
                        rtxtAscii.AppendText("'PD'  ");
                        rtxtHex.AppendText("FF   ");
                        break;
                    }
                case 0x20:
                    {//PADDING - FILL EMPTY PACKET SPACE THE PACKET
                        rtxtAscii.AppendText("'SP' ");
                        rtxtHex.AppendText("20   ");
                        break;
                    }
                default:
                    {
                        string newstring = Convert.ToChar(Packet).ToString();
                        rtxtAscii.AppendText(newstring + "   ");
                        rtxtHex.AppendText(Packet.ToString() + "   ");
                        break;
                    }
            }
            rtxtAscii.AppendText("\n");
            rtxtHex.AppendText("\n");
        }
        //modified from source:https://stackoverflow.com/questions/6397235/write-bytes-to-file
        public bool ByteArrayToFile(string fileName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    foreach (byte[] packetContent in fileContents)
                    {
                        fs.Write(packetContent, 0, packetContent.Length);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        public void sendFile()
        {
            if (_clientSocket.Connected)
            {
                //don't send if a file hasn't been selected
                if (_fileSelected)
                {
                    //stop transmission if file has been sent
                    if (!_gotFile)
                    {
                        //only read the file once
                        if (!_gotString)
                        {
                            _fileContent = System.IO.File.ReadAllText(_fileName);
                            _gotString = true;
                        }

                        //create the packet
                        byte[] Packet = new byte[7];

                        Packet[0] = 0xFF;

                        Packet[0] = 0x01; //SOH

                        //build the packet with file contents
                        for (int PacketInd = 1; PacketInd < (Packet.Length - 1); PacketInd++)
                        {
                            if (fileIndex != _fileContent.Length)
                            {
                                Packet[PacketInd] = (byte)_fileContent[fileIndex];
                                fileIndex++;
                            }
                            else
                            {
                                if (!eot)
                                {
                                    Packet[PacketInd] = EOT[0];
                                    eot = true;
                                    _gotFile = true;
                                }
                                else
                                {
                                    Packet[PacketInd] = 0xFF;
                                }
                            }
                        }

                        //get and assign the real crc
                        CRC8 crc8 = new CRC8(Packet, this);
                        Packet[Packet.Length - 1] = crc8.GET_CRC();

                        //buffer the last command
                        Array.Copy(Packet, bufferedPacket, Packet.Length);

                        //corrupt packets after buffered
                        if (PacketNumber == 3)
                        {
                            if (SOHwithRecovery)
                            {
                                Packet[0] = 0xFF; //SOH
                            }
                        }

                        if ((CRCwithoutRecovery || CRCwithRecovery) && PacketNumber == 3)
                        {
                            //corrupt the CRC
                            Packet[Packet.Length - 1] = 0xFF;
                        }


                        rtxtAscii.AppendText("Send:     ");
                        rtxtHex.AppendText("Send:     ");
                        printPacket(Packet);

                        //write the packet to the socket
                        _netwrkStream.Write(Packet, 0, 7);

                        PacketNumber++;
                        if (_gotFile)
                        {
                            PacketIndex = 0;
                            fileIndex = 0;
                            PacketNumber = 0;
                        }
                    }
                }
            }
        }
        
        public void resetErrorModeBools()
        {
            errorFree = false;
            CRCwithRecovery = false;
            CRCwithoutRecovery = false;
            SOHwithRecovery = false;
        }

        //set transmit mode
        private void tstripCmboError_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tstripCmboError.Text)
            {
                case "Error Free":
                    {
                        resetErrorModeBools();
                        errorFree = true;
                        break;
                    }
                case "CRC With Recovery":
                    {
                        resetErrorModeBools();
                        CRCwithRecovery = true;
                        break;
                    }
                case "CRC Without Recovery":
                    {
                        resetErrorModeBools();
                        CRCwithoutRecovery = true;

                        break;
                    }
                case "SOH With Recovery":
                    {
                        resetErrorModeBools();
                        SOHwithRecovery = true;
                        break;
                    }
            }
        }
    }
    }

