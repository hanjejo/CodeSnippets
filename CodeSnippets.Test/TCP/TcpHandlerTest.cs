using CodeSnippets.TCP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippets.TCP.Test
{
    [TestClass]
    public class TcpHandlerTests
    {
        private TcpHandler handler = new TcpHandler();

        [TestMethod]
        public void TestAddMessageHandler()
        {
            // Arrange

            // Act
            handler.AddMessageHandler("REVERSE", message => new string(message.Reverse().ToArray()));

            // Assert
            Assert.AreEqual("dlroW", handler.ProcessMessage("REVERSE World"));
            Assert.AreEqual("Invalid message format\n", handler.ProcessMessage("UNKNOWN Hello"));
        }
    }
}
