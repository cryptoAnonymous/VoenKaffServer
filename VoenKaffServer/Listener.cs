﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VoenKaffServer
{
    public class Listener
    {
        private readonly DynamicParams _parameters;
        private readonly Thread _thread;
        private readonly FormStart _form;

        public Listener(FormStart form)
        {
            _parameters = new DynamicParams();
            _thread = new Thread(Listen);
            _form = form;
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Interrupt()
        {
            _thread.Interrupt();
        }

        private void Listen() {
            var ipPoint = new IPEndPoint(IPAddress.Parse(_parameters.Get().IpAdress),_parameters.Get().Port);

            var socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            var filenames = new List<string>();
            try
            {
                socket.Bind(ipPoint);

                socket.Listen(40);

                while (true)
                {
                    var handler = socket.Accept();

                    var builder = new StringBuilder();

                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (handler.Available > 0);

                    string response = builder.ToString();
                    switch (response)
                    {
                        case "Test connect":  
                        {
                            handler.Send(Encoding.Unicode.GetBytes("OK"));
                                break;
                        }
                        case "Update":
                        {
                            var directoryInfo = new DirectoryInfo(_parameters.Get().TestPath);
                            foreach (var test in directoryInfo.GetFiles("*.test"))
                            {
                                filenames.Add(test.Name);
                            }

                            var json = JsonConvert.SerializeObject(filenames);
                            handler.Send(Encoding.Unicode.GetBytes(json));
                            break;
                         }
                        default:
                        {
                            int index;
                            if (Int32.TryParse(response, out index))
                            {

                                var file = File.ReadAllText(_parameters.Get().TestPath + "\\" + filenames[index]);
                                handler.Send(Encoding.Unicode.GetBytes(file));
                            }
                            else
                            {
                                _form.AddResult(builder.ToString());
                            }
                            break;
                        }
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
