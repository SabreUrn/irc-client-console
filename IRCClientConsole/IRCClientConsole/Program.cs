using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRCClientConsole {
	class Program {
		static void Main(string[] args) {
			string ip = "localhost";
			int port = 40998;

			TcpClient clientSocket = new TcpClient(ip, port);
			Console.WriteLine("Client ready.");

			using (NetworkStream ns = clientSocket.GetStream())
			using (StreamReader sr = new StreamReader(ns))
			using (StreamWriter sw = new StreamWriter(ns) { NewLine = "\r\n", AutoFlush = true }) {
				//Console.CancelKeyPress += delegate {
				//	sw.Dispose();
				//	sr.Dispose();
				//	ns.Dispose();
				//	clientSocket.Dispose();
				//};

				sw.WriteLine("CAP LS 302");
				sw.WriteLine("CAP END");
				sw.WriteLine("NICK " + "kammie");
				sw.WriteLine("USER " + "kammie" + " 0 * " + "kammie junkie");
				sw.WriteLine("JOIN " + "test");

				string readMessage = "";
				while (true) {
					readMessage = sr.ReadLine();
					if(!String.IsNullOrWhiteSpace(readMessage)) {
						Console.WriteLine(readMessage);
					}
					Thread.Sleep(17);
				}
			}
		}

		private static TcpClient WaitForServer(string ip, int port) {
			TcpClient clientSocket = new TcpClient();
			bool serverFound = false;

			while (!serverFound) {
				try {
					clientSocket = new TcpClient(ip, port);
				} catch (SocketException) {
					Console.WriteLine("Cannot find server.");
					Console.WriteLine("Retrying in 5 seconds.");
					Thread.Sleep(5000);
				}
			}

			return clientSocket;
		}
	}
}
