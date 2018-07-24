using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class GameForm : Form
    {
        BricksPanel bp;
        public bool player1;
        UdpClient server;
        UdpClient client;
        byte[] sendToClient;//the packet to be sent from server to client
        byte[] sendToServer;//the packet to be sent from client to server
        byte[] receiveFromServer;
        byte[] receiveFromClient;
        IPEndPoint endpoint;
        IPEndPoint RemoteIpEndPoint;
        List<String> values;
        List<String> values2;

        Thread t;

        bool communicating = true;


        //host form
        public GameForm(int hostPM)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            bp = new BricksPanel();
            initHost_Communication(hostPM);
        }

        //client form
        public GameForm(String hostIp, int hostPM)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            bp = new BricksPanel();
            initClient_Communication(hostIp, hostPM);
        }

        public void initHost_Communication(int hostPM)
        {
            server = new UdpClient(hostPM);
            RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, hostPM);
            receiveFromClient = server.Receive(ref RemoteIpEndPoint);
            t = new Thread(new ThreadStart(UDPserverThread));
            t.Start();
        }

        public void initClient_Communication(String hostIp, int hostPM)
        {
            client = new UdpClient(hostIp, hostPM);
            endpoint = new IPEndPoint(IPAddress.Parse(hostIp), hostPM);
            sendToServer = Encoding.ASCII.GetBytes("0");
            client.Send(sendToServer, sendToServer.Length);
            t = new Thread(new ThreadStart(UDPclientThread));
            t.Start();
        }



        public void UDPserverThread()
        {
            values = new List<String>();
            string temp1;
            while (communicating)
            {
                receiveFromClient = server.Receive(ref RemoteIpEndPoint);
                temp1 = Encoding.ASCII.GetString(receiveFromClient);
                values.Add(temp1);
                if (bp.InvokeRequired)
                {
                    MethodInvoker methodInvokerDelegate = delegate()
                    {
                        for (int i = 0; i < values.Count; i++)
                        {
                            switch (int.Parse(values[i]))
                            {
                                case 1000: Environment.Exit(0); break;//exit

                                case 1001: bp.restart(); values.RemoveAt(i); break;//restart
                                case 1002: bp.start = false; values.RemoveAt(i); break;//pause
                                case 1003: bp.start = true; values.RemoveAt(i); break;//resume
                                case 1004: if (bp.lvl < 10) bp.nextlvl(); values.RemoveAt(i); break;//cheat
                                case 1005:
                                    bp.start = true;
                                    if (bp.nbricks == 0) bp.nextlvl(); values.RemoveAt(i); break;//next level
                             //   case 1006: bp.ballx2 = int.Parse(Encoding.ASCII.GetString(server.Receive(ref RemoteIpEndPoint)));
                               //     bp.bally2 = int.Parse(Encoding.ASCII.GetString(server.Receive(ref RemoteIpEndPoint))); break;
                                default: bp.player2 = int.Parse(values[i]); values.RemoveAt(i); break;
                            }
                        }
                    };
                }
                else
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        switch (int.Parse(values[i]))
                        {
                            case 1000: Environment.Exit(0); break;
                            case 1001: bp.restart(); values.RemoveAt(i); break;//restart
                            case 1002: bp.start = false; values.RemoveAt(i); break;//pause
                            case 1003: bp.start = true; values.RemoveAt(i); break;//resume
                            case 1004: if (bp.lvl < 10) bp.nextlvl(); values.RemoveAt(i); break;//cheat
                            case 1005:
                                bp.start = true;
                                if (bp.nbricks == 0) bp.nextlvl(); values.RemoveAt(i); break;//next level
                         //   case 1006: bp.ballx2 = int.Parse(Encoding.ASCII.GetString(server.Receive(ref RemoteIpEndPoint)));
                           //     bp.bally2 = int.Parse(Encoding.ASCII.GetString(server.Receive(ref RemoteIpEndPoint))); break;
                            default: bp.player2 = int.Parse(values[i]); values.RemoveAt(i); break;
                        }
                    }
                }
                Thread.Yield();

            }


        }


        public void UDPclientThread()
        {
            values2 = new List<String>();
            String temp2;

            while (communicating)
            {
                receiveFromServer = client.Receive(ref endpoint);
                temp2 = Encoding.ASCII.GetString(receiveFromServer);
                values2.Add(temp2);
                if (bp.InvokeRequired)
                {
                    MethodInvoker methodInvokerDelegate = delegate()
                    {
                        for (int i = 0; i < values2.Count; i++)
                        {
                            switch (int.Parse(values2[i]))
                            {
                                case 1000: Environment.Exit(0); break;
                                case 1001: bp.restart(); values2.RemoveAt(i); break;//restart
                                case 1002: bp.start = false; values2.RemoveAt(i); break;//pause
                                case 1003: bp.start = true; values2.RemoveAt(i); break;//resume
                                case 1004: if (bp.lvl < 10) bp.nextlvl(); values2.RemoveAt(i); break;//cheat
                                case 1005:
                                    bp.start = true;
                                    if (bp.nbricks == 0) bp.nextlvl(); values2.RemoveAt(i); break;//next level
                     //           case 1006: bp.ballx = int.Parse(Encoding.ASCII.GetString(client.Receive(ref endpoint)));
                       //             bp.bally = int.Parse(Encoding.ASCII.GetString(client.Receive(ref endpoint)));break;
                                default: bp.player = int.Parse(values2[i]); values2.RemoveAt(i); break;
                            }
                        }
                    };
                }
                else
                {
                    for (int i = 0; i < values2.Count; i++)
                    {
                        switch (int.Parse(values2[i]))
                        {
                            case 1000: Environment.Exit(0); break;
                            case 1001: bp.restart(); values2.RemoveAt(i); break;//restart
                            case 1002: bp.start = false; values2.RemoveAt(i); break;//pause
                            case 1003: bp.start = true; values2.RemoveAt(i); break;//resume
                            case 1004: if (bp.lvl < 10) bp.nextlvl(); values2.RemoveAt(i); break;//cheat
                            case 1005:
                                bp.start = true;
                                if (bp.nbricks == 0) bp.nextlvl(); values2.RemoveAt(i); break;//next level
                 //           case 1006: bp.ballx = int.Parse(Encoding.ASCII.GetString(client.Receive(ref endpoint)));
                   //             bp.bally = int.Parse(Encoding.ASCII.GetString(client.Receive(ref endpoint))); break;
                            default: bp.player = int.Parse(values2[i]); values2.RemoveAt(i); break;
                        }
                    }
                }
                Thread.Yield();
            }
        }

        public void KeysPressed()
        {
            try
            {
                if (KeyInputManager.getKeyState(Keys.Escape))
                {
                    if (player1)
                    {
                        sendToClient = Encoding.ASCII.GetBytes("" + 1000);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                        Environment.Exit(0);
                    }
                    else
                    {
                        sendToServer = Encoding.ASCII.GetBytes("" + 1000);
                        client.Send(sendToServer, sendToServer.Length);
                        Environment.Exit(0);
                    }
                }
                if (KeyInputManager.getKeyState(Keys.Space))
                {
                    if (player1)
                    {
                        sendToClient = Encoding.ASCII.GetBytes("" + 1001);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                        bp.restart();
                    }
                    else
                    {
                        sendToServer = Encoding.ASCII.GetBytes("" + 1001);
                        client.Send(sendToServer, sendToServer.Length);
                        bp.restart();
                    }
                }

                if (KeyInputManager.getKeyState(Keys.P))
                {
                    if (player1)
                    {
                        sendToClient = Encoding.ASCII.GetBytes("" + 1002);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                        bp.start = false;
                    }
                    else
                    {
                        sendToServer = Encoding.ASCII.GetBytes("" + 1002);
                        client.Send(sendToServer, sendToServer.Length);
                        bp.start = false;
                    }
                }
                if (KeyInputManager.getKeyState(Keys.S))
                {
                    if (player1)
                    {
                        sendToClient = Encoding.ASCII.GetBytes("" + 1003);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                        bp.start = true;
                    }
                    else
                    {
                        sendToServer = Encoding.ASCII.GetBytes("" + 1003);
                        client.Send(sendToServer, sendToServer.Length);
                        bp.start = true;
                    }
                }

                if (KeyInputManager.getKeyState(Keys.ControlKey))
                {
                    if (player1)
                    {
                        sendToClient = Encoding.ASCII.GetBytes("" + 1004);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                        if (bp.lvl < 10) bp.nextlvl();

                    }
                    else
                    {
                        sendToServer = Encoding.ASCII.GetBytes("" + 1004);
                        client.Send(sendToServer, sendToServer.Length);
                        if (bp.lvl < 10) bp.nextlvl();

                    }

                }
                if (KeyInputManager.getKeyState(Keys.Enter) && bp.lvl < 10)
                {
                    if (player1)
                    {
                        sendToClient = Encoding.ASCII.GetBytes("" + 1005);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                       bp.start = true;
                        if (bp.nbricks == 0)
                            bp.nextlvl();

                    }
                    else
                    {
                        sendToServer = Encoding.ASCII.GetBytes("" + 1005);
                        client.Send(sendToServer, sendToServer.Length);
                        bp.start = true;                       
                        if (bp.nbricks == 0)
                            bp.nextlvl();

                    }

                }

                if (KeyInputManager.getKeyState(Keys.Right))
                {
                    if (player1)
                    {
                        if (bp.player < 561) bp.player += 20;
                        sendToClient = Encoding.ASCII.GetBytes("" + bp.player);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                    }
                    else
                    {
                        if (bp.player2 < 561) bp.player2 += 20;
                        sendToServer = Encoding.ASCII.GetBytes("" + bp.player2);
                        client.Send(sendToServer, sendToServer.Length);
                    }
                }
                if (KeyInputManager.getKeyState(Keys.Left))
                {
                    if (player1)
                    {
                        if (bp.player > 0) bp.player -= 20;
                        sendToClient = Encoding.ASCII.GetBytes("" + bp.player);
                        server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                    }
                    else
                    {
                        if (bp.player2 > 0) bp.player2 -= 20;
                        sendToServer = Encoding.ASCII.GetBytes("" + bp.player2);
                        client.Send(sendToServer, sendToServer.Length);
                    }
                }
            }
            catch (Exception e) { }

        }

        public void BallPosition()
        {
            if (player1)
            {
                sendToClient = Encoding.ASCII.GetBytes("" + 1006);
                server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                sendToClient = Encoding.ASCII.GetBytes("" + bp.ballx);
                server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
                sendToClient = Encoding.ASCII.GetBytes("" + bp.bally);
                server.Send(sendToClient, sendToClient.Length, RemoteIpEndPoint);
            }
            else
            {
                sendToServer = Encoding.ASCII.GetBytes("" + 1006);
                client.Send(sendToServer, sendToServer.Length);
                sendToServer = Encoding.ASCII.GetBytes("" + bp.ballx2);
                client.Send(sendToServer, sendToServer.Length);
                sendToServer = Encoding.ASCII.GetBytes("" + bp.bally2);
                client.Send(sendToServer, sendToServer.Length);
                
            }
        
        
        }

        public void endgame()
        {
            if (this.InvokeRequired)
            {
                MethodInvoker m = delegate()
                {
                    this.Close();

                };
            }
            else
            {
                this.Close();
                //     this.Dispose();
            }


            communicating = false;
            t.Abort();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //  bp.startGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bp.drawGame(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
            bp.updateGame();
            KeysPressed();
          //  BallPosition();


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyInputManager.setKeyState(e.KeyCode, true);

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            KeyInputManager.setKeyState(e.KeyCode, false);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //  bp.startGame();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  communicating = false;
            //  t.Abort();
            Environment.Exit(0);
        }
    }
}
