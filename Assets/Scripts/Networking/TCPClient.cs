using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TCPClient : MonoBehaviour {
	#region private member

	private Client client;
	private Thread clientReceiveThread;
	
	private string debugString;
	#endregion
	
	public void StartClient (string username, string host, int port) {
		client = new Client(new TcpClient(), username);
		client._tcpClient.Connect(host, port);
		
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
		GameManager.instance.debugLog.GetComponent<Text>().text = debugString;
	}
	
	private void ListenForData() {
		try {
			Byte[] bytes = new Byte[1024];
			DebugInfo("\n[+][C] Connected to the server.");
			
			SendMessage(CreateSendingPackageString(PackageType.USER_ID, new Package(client._id, client._username)));
			
			while (true) {			
				using (NetworkStream stream = client._tcpClient.GetStream()) {
					int length;
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
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

	private void ReadPackageString(string packageString)
	{
		string[] decodedPackageArray = packageString.Split(';');

		if (int.Parse(decodedPackageArray[0]).Equals((int) PackageType.USER_ID))
		{
			client._id = uint.Parse(decodedPackageArray[1]);
		}
	}
	
	private string CreateSendingPackageString(PackageType packageType, Package package)
	{
		string packageString = "";
			
		if (packageType.Equals(PackageType.USERNAME))
		{
			packageString = packageType + ";" + package.id + ";" + package.username;
		}

		return packageString;
	}
	
	private void SendMessage(string message) {
		if (client._tcpClient == null) {
			return;
		}
		try {
			NetworkStream stream = client._tcpClient.GetStream();
			if (stream.CanWrite) {
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);
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