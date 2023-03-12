using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.TCP
{
    public class TcpHandler
    {
        private readonly Dictionary<string, Func<string, string>> _handler = new Dictionary<string, Func<string, string>>();

        public void AddMessageHandler(string messageType, Func<string, string> handler)
        {
            _handler[messageType] = handler;
        }

        public async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                string response = ProcessMessage(message);
                byte[] responseData = Encoding.ASCII.GetBytes(response);
                await stream.WriteAsync(responseData, 0, responseData.Length);
            }

            client.Close();
        }

        public string ProcessMessage(string message)
        {
            string[] tokens = message.Split(' ');
            if (tokens.Length < 2 || !_handler.ContainsKey(tokens[0]))
            {
                return "Invalid message format\n";
            }

            string messageType = tokens[0];
            string messageData = message.Substring(messageType.Length + 1);
            return _handler[messageType](messageData);
        }
    }
}
