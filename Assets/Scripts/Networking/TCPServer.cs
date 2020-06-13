using System;
using System.Collections.Generic;
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;
using UnityEngine.UI;

public class TCPServer : MonoBehaviour {
	#region private members
	/// <summary>
	/// TCPListener to listen for incomming TCP connection
	/// requests.
	/// </summary>
	private TcpListener tcpListener;
	/// <summary>
	/// Background thread for TcpServer workload.
	/// </summary>
	private Thread tcpListenerThread; 
	/// <summary>
	/// Create handle to connected tcp client.
	/// </summary> 
	private List<TcpClient> connectedTcpClients;
	
	private string host;
	private int port;
	
	private Text debugText;
	private string debugString;
	#endregion

	void Start()
	{
		connectedTcpClients = new List<TcpClient>();
	}
		
	public void StartServer (string host, int port, Text debugText)
	{
		this.host = host;
		this.port = port;
		
		this.debugText = debugText;
		
		// Start TcpServer background thread
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests));
		tcpListenerThread.IsBackground = true;
		tcpListenerThread.Start();
	}

	void Update()
	{
		debugText.text = debugString;
	}
	
	/// <summary> 	
	/// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
	/// </summary> 	
	private void ListenForIncommingRequests () { 		
		try { 			
			// Create listener on localhost port 8052.
			tcpListener = new TcpListener(IPAddress.Any, port);
			tcpListener.Start();
			DebugInfo("[+][S] Server is listening");
			while (true)
			{
				TcpClient connectedTcpClient = tcpListener.AcceptTcpClient();
				connectedTcpClients.Add(connectedTcpClient);
				Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));

				clientThread.Start(connectedTcpClient);
			}
		}
		catch (SocketException socketException)
		{
			DebugInfo("[-][S] Socket exception: " + socketException.Message + " | " + socketException.StackTrace);
		}     
	}

	private void HandleClientComm(object client)
	{
		TcpClient tcpClient = (TcpClient) client;
		NetworkStream clientStream = tcpClient.GetStream();
		Byte[] bytes = new Byte[1024];

		while (true)
		{						
			int length;
			DebugInfo("\n[+][S] Accepted new client.");
			SendMessage(tcpClient);
			// Read incomming stream into byte arrary. 						
			while ((length = clientStream.Read(bytes, 0, bytes.Length)) != 0) { 							
				var incommingData = new byte[length]; 							
				Array.Copy(bytes, 0, incommingData, 0, length);  							
				// Convert byte array to string message. 							
				string clientMessage = Encoding.ASCII.GetString(incommingData);
				DebugInfo("[+][S] Client message received as: " + clientMessage);
			}
		}
	}

	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	
	private void SendMessage(TcpClient tcpClient) {
		if (tcpClient == null) {             
			return;
		}  		
		
		try { 			
			// Get a stream object for writing.
			NetworkStream stream = tcpClient.GetStream();
			if (stream.CanWrite) {
				string serverMessage = "[+][S] This is a message from your server.";
				// Convert string message to byte array.
				byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
				// Write byte array to socketConnection stream.
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
				DebugInfo("[+][S] Server sent his message - should be received by client");   
			}       
		} 		
		catch (SocketException socketException) {             
			DebugInfo("[-][S] Socket exception: " + socketException);
		} 	
	}

	private void DebugInfo(string info)
	{
		debugString += "\n" + info;
		Debug.Log(info);
	}
}