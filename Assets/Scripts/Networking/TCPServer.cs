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
	private TcpListener tcpListener;
	private Thread tcpListenerThread;
	
	private List<Client> connectedClients;

	private string debugString;
	#endregion

	public void StartServer (int port)
	{
		connectedClients = new List<Client>();
		tcpListener = new TcpListener(IPAddress.Any, port);
		tcpListener.Start();
		DebugInfo("[+][S] Server is listening");
		
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests));
		tcpListenerThread.IsBackground = true;
		tcpListenerThread.Start();
	}

	void Update()
	{
		GameManager.instance.debugLog.GetComponent<Text>().text = debugString;
	}
	
	private void ListenForIncommingRequests () {
		try {
			while (true)
			{
				TcpClient connectedTcpClient = tcpListener.AcceptTcpClient();
				DebugInfo("\n[+][S] Accepted new client.");
				Client client = new Client((uint) connectedClients.Count, connectedTcpClient);
				connectedClients.Add(client);
				Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));

				clientThread.Start(client);
			}
		}
		catch (SocketException socketException)
		{
			DebugInfo("[-][S] Socket exception: " + socketException.Message + " | " + socketException.StackTrace);
		}     
	}

	private void HandleClientComm(object clientObj)
	{
		Client client = (Client) clientObj;
		NetworkStream clientStream = client._tcpClient.GetStream();
		byte[] bytes = new byte[1024];
		
		SendMessage(client._tcpClient, CreateSendingPackageString(PackageType.USER_ID, new Package(client._id)));

		while (true)
		{
			int length;
			while ((length = clientStream.Read(bytes, 0, bytes.Length)) != 0) {
				var incommingData = new byte[length];
				Array.Copy(bytes, 0, incommingData, 0, length);
				string clientMessage = Encoding.ASCII.GetString(incommingData);
				DebugInfo("[+][S] Received: " + clientMessage);
			}
		}
	}
	
	private void ReadPackageString(string packageString)
	{
		string[] decodedPackageArray = packageString.Split(';');

		if (int.Parse(decodedPackageArray[0]).Equals((int) PackageType.USERNAME))
		{
			Client foundClient = connectedClients.Find(client => client._id == uint.Parse(decodedPackageArray[1]));
			if (foundClient != null)
			{
				foundClient._username = decodedPackageArray[2];
			}
		}
	}
	
	private string CreateSendingPackageString(PackageType packageType, Package package)
	{
		string packageString = "";
			
		if (packageType.Equals(PackageType.USER_ID))
		{
			packageString = packageType + ";" + package.id;
		}

		return packageString;
	}

	private void SendMessage(TcpClient tcpClient, string message) {
		if (tcpClient == null) {             
			return;
		}  		
		
		try {
			NetworkStream stream = tcpClient.GetStream();
			if (stream.CanWrite) {
				byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(message);
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
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