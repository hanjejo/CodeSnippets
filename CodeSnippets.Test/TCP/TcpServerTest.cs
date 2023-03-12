using CodeSnippets.TCP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.TCP.Test
{
    [TestClass]
    public class TcpServerTests
    {
        private const int port = 5000;
        private readonly TcpServer server = new TcpServer(IPAddress.Any, port);

        [ClassInitialize]
        public async Task SetUp()
        {
            await server.StartAsync(new TcpHandler());
        }

        [TestMethod]
        public void ConnectionTest()
        {
            // Arrange
            TcpClient client = new TcpClient();

            // Act
            client.Connect("127.0.0.1", port);

            // Assert
            Assert.AreEqual(client.Connected, true);
        }

        [ClassCleanup]
        public void Clean()
        {
            server.Stop();
        }
    }
}
