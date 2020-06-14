using System.Net.Sockets;

public class Client : IClient
{
    public uint _id { get; set; }
    public TcpClient _tcpClient { get; set; }
    public string _username { get; set; }
    
    public Client(TcpClient tcpClient)
    {
        _tcpClient = tcpClient;
    }
    
    public Client(TcpClient tcpClient, string username)
    {
        _tcpClient = tcpClient;
        _username = username;
    }
    
    public Client(uint id, TcpClient tcpClient)
    {
        _id = id;
        _tcpClient = tcpClient;
    }

    public Client(uint id, TcpClient tcpClient, string username)
    {
        _id = id;
        _tcpClient = tcpClient;
        _username = username;
    }
}