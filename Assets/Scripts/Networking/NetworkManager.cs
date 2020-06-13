using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public string host;
    public int port;
    public GameObject debugLogGO;
    
    private GameObject UICanvas;

    void Start()
    {
        UICanvas = GameObject.FindGameObjectWithTag("MenuUI");
    }

    public void StartServer()
    {
        gameObject.AddComponent<TCPServer>();
        gameObject.GetComponent<TCPServer>().StartServer(host, port, debugLogGO.gameObject.GetComponent<Text>());

        UICanvas.SetActive(false);
    }

    public void StartClient()
    {
        gameObject.AddComponent<TCPClient>();
        gameObject.GetComponent<TCPClient>().StartClient(host, port, debugLogGO.gameObject.GetComponent<Text>());

        UICanvas.SetActive(false);
    }

    public void StartServerAndClient()
    {
        StartServer();
        StartClient();
    }
}