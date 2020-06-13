using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TCPClient : MonoBehaviour {
	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread;
	
	private string host;
	private int port;
	
	private Text debugText;
	private string debugString;
	#endregion
	
	public void StartClient (string host, int port, Text debugText) {
		this.host = host;
		this.port = port;
		
		this.debugText = debugText;
		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		} 		
		catch (Exception e) {
			DebugInfo("[-][C] On client connect exception " + e);
		} 	
	}

	void Update()
	{
		debugText.text = debugString;
	}
	
	/// <summary>
	/// Runs in background clientReceiveThread; Listens for incomming data.
	/// </summary>
	private void ListenForData() {
		try {
			socketConnection = new TcpClient(host, port);
			//socketConnection.Connect("127.0.0.1", port);
			Byte[] bytes = new Byte[1024];
			DebugInfo("\n[+][C] Connected to the server.");
			SendMessage("Hello server!");
			while (true) {
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) {
					int length;
					// Read incomming stream into byte arrary.
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);					
						// Convert byte array to string message.
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						DebugInfo("[+][C] Received: " + serverMessage);
					}
				}
			}
		}
		catch (SocketException socketException) {
			DebugInfo("[-][C] Socket exception: " + socketException.Message + " | " + socketException.StackTrace);
		}
	}
	
	/// <summary>
	/// Send message to server using socket connection.
	/// </summary>
	private void SendMessage(string message) {
		if (socketConnection == null) {
			return;
		}
		try {
			// Get a stream object for writing.
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite) {
				// Convert string message to byte array.
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);
				// Write byte array to socketConnection stream.
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
			}
		}
		catch (SocketException socketException) {
			DebugInfo("[-][C] Socket exception: " + socketException.Message);
		}
	}

	private void DebugInfo(string info)
	{		
		debugString += "\n" + info;
		Debug.Log(info);
	}
}