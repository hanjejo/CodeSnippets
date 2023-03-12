using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.TCP
{
    public class TcpServer
    {
        private readonly TcpListener listener;
        public TcpServer(IPAddress ip, int port)
        {
            listener = new TcpListener(ip, port);
        }

        public async Task StartAsync(TcpHandler clientHandler)
        {
            listener.Start();
            Console.WriteLine($"Server started");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("New client connected");

                await Task.Run(() => clientHandler.HandleClientAsync(client));
            }
        }

        public void Stop() {
            listener.Stop();
        }
    }
}
