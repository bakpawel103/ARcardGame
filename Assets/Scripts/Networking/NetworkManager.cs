using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public string host;
    public int port;
    public GameObject debugLogGO;
    
    private GameObject UICanvas;
    private GameObject MainMenu;
    private GameObject JoinMenu;
    private Text ServerAddressDebugText;
    private Text ServerPortDebugText;

    void Start()
    {
        UICanvas = GameObject.FindGameObjectWithTag("MenuUI");
        MainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        JoinMenu = GameObject.FindGameObjectWithTag("JoinMenu");
        ServerAddressDebugText = GameObject.FindGameObjectWithTag("ServerAddressDebugText").GetComponent<Text>();
        ServerPortDebugText = GameObject.FindGameObjectWithTag("ServerPortDebugText").GetComponent<Text>();
        
        UICanvas.SetActive(true);
        MainMenu.SetActive(true);
        JoinMenu.SetActive(false);
    }

    public void HostServer()
    {
        UICanvas.SetActive(false);

        ServerAddressDebugText.text = host;
        ServerPortDebugText.text = port.ToString();
        
        StartServerAndClient();
    }

    public void ShowJoinServerMenu()
    {
        MainMenu.SetActive(false);
        JoinMenu.SetActive(true);
    }

    public void JoinServer()
    {

        if (GameObject.FindGameObjectWithTag("ServerAddressText") &&
            GameObject.FindGameObjectWithTag("ServerPortText") &&
            GameObject.FindGameObjectWithTag("ServerAddressText").GetComponent<Text>().text.Length > 0 &&
            GameObject.FindGameObjectWithTag("ServerPortText").GetComponent<Text>().text.Length > 0) {
            
            host = GameObject.FindGameObjectWithTag("ServerAddressText").GetComponent<Text>().text;
            port = Convert.ToInt32(GameObject.FindGameObjectWithTag("ServerPortText").GetComponent<Text>().text);
            
            StartClient();
        }
    }

    private void StartServer()
    {
        gameObject.AddComponent<TCPServer>();
        gameObject.GetComponent<TCPServer>().StartServer(host, port, debugLogGO.gameObject.GetComponent<Text>());
    }

    private void StartClient()
    {
        gameObject.AddComponent<TCPClient>();
        gameObject.GetComponent<TCPClient>().StartClient(host, port, debugLogGO.gameObject.GetComponent<Text>());
    }

    private void StartServerAndClient()
    {
        StartServer();
        StartClient();
    }
}