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
		private static NetworkStream _ns;
		private static StreamReader _sr;
		private static StreamWriter _sw;

		static void Main(string[] args) {
			string ip = "localhost";
			int port = 40998;

			TcpClient clientSocket = new TcpClient(ip, port);
			Console.WriteLine("Client ready.");

			_ns = clientSocket.GetStream();
			_sr = new StreamReader(_ns);
			_sw = new StreamWriter(_ns) { NewLine = "\r\n", AutoFlush = true };

			//using (NetworkStream ns = clientSocket.GetStream())
			//using (StreamReader sr = new StreamReader(ns))
			//using (StreamWriter sw = new StreamWriter(ns) { NewLine = "\r\n", AutoFlush = true }) {





			//sw.WriteLine("CAP LS 302");
			//sw.WriteLine("CAP END");
			//Console.WriteLine(_sr.ReadLine());
			//Console.WriteLine(_sr.ReadLine());
			//_sw.WriteLine("NICK " + "kammie");
			//ListenForPing();
			//_sw.WriteLine("USER " + "kammie" + " 0 * " + "kammie junkie");
			//Console.WriteLine(_sr.ReadLine());
			//_sw.WriteLine("JOIN " + "#test");

			//

			var read = Task.Factory.StartNew(() => ReadMessage());
			var write = Task.Factory.StartNew(() => WriteMessage());

			while (true) ;

			//string readMessage = "";
			//while (true) {
			//	readMessage = _sr.ReadLine();
			//	if (!String.IsNullOrWhiteSpace(readMessage)) {
			//		Console.WriteLine(readMessage);
			//	}
			//	Thread.Sleep(17);
			//}
			//}


		}

		private static async void ReadMessage() {
			string readMessage = "";
			while(true) {
				readMessage = await _sr.ReadLineAsync();
				if(!String.IsNullOrWhiteSpace(readMessage)) {
					Console.WriteLine(readMessage);

					//if(readMessage.Contains("PING")) {
					//	string pingResponse = "PONG " + readMessage.Split(' ')[1];
					//	Console.WriteLine(pingResponse);
					//	_sw.WriteLine(pingResponse);
					//}
				}
			}
		}

		private static async void WriteMessage() {
			while(true) {
				string message = Console.ReadLine();
				await _sw.WriteLineAsync(message);
			}
		}

		//PING :4F0FD21D
		private static void ListenForPing() {
			string message = _sr.ReadLine();
			Console.WriteLine(message);
			if (message.Contains("PING")) {
				string response = "PONG " + message.Split(' ')[1];
				response = response.Substring(0, response.Length - 2);
				Console.WriteLine(response);
				_sw.WriteLine(response);

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
