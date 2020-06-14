using System;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    [Header("Preffered Server Configuration")]
    public string serverUsername;
    public string host;
    public int port;

    void Start()
    {
        GameManager.instance.uiCanvas = GameObject.FindGameObjectWithTag("MenuUI");
        GameManager.instance.mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        GameManager.instance.joinMenu = GameObject.FindGameObjectWithTag("JoinMenu");
        
        GameManager.instance.serverUsernameText = GameObject.FindGameObjectWithTag("ServerUsernameText");
        GameManager.instance.serverAddressText = GameObject.FindGameObjectWithTag("ServerAddressText");
        GameManager.instance.serverPortText = GameObject.FindGameObjectWithTag("ServerPortText");
        
        GameManager.instance.serverUsernameDebugText = GameObject.FindGameObjectWithTag("ServerUsernameDebugText");
        GameManager.instance.serverAddressDebugText = GameObject.FindGameObjectWithTag("ServerAddressDebugText");
        GameManager.instance.serverPortDebugText = GameObject.FindGameObjectWithTag("ServerPortDebugText");


        GameManager.instance.serverAddressText.GetComponent<Text>().text = host;
        GameManager.instance.serverPortText.GetComponent<Text>().text = port.ToString();
        
        GameManager.instance.uiCanvas.SetActive(true);
        GameManager.instance.mainMenu.SetActive(true);
        GameManager.instance.joinMenu.SetActive(false);
    }

    public void HostServer()
    {
        GameManager.instance.uiCanvas.SetActive(false);

        GameManager.instance.serverUsernameDebugText.GetComponent<Text>().text = serverUsername;
        GameManager.instance.serverAddressDebugText.GetComponent<Text>().text = host;
        GameManager.instance.serverPortDebugText.GetComponent<Text>().text = port.ToString();
        
        StartServerAndClient();
    }

    public void ShowJoinServerMenu()
    {
        GameManager.instance.mainMenu.SetActive(false);
        GameManager.instance.joinMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        GameManager.instance.mainMenu.SetActive(true);
        GameManager.instance.joinMenu.SetActive(false);
    }

    public void JoinServer()
    {
        if (LoggingFormIsValid())
        {
            serverUsername = GameManager.instance.serverUsernameText.GetComponent<Text>().text;
            host = GameManager.instance.serverAddressText.GetComponent<Text>().text;
            port = Convert.ToInt32(GameManager.instance.serverPortText.GetComponent<Text>().text);
            
            GameManager.instance.uiCanvas.SetActive(false);
            
            StartClient();
        }
    }

    private void StartServer()
    {
        gameObject.AddComponent<TCPServer>();
        gameObject.GetComponent<TCPServer>().StartServer(port);
    }

    private void StartClient()
    {
        gameObject.AddComponent<TCPClient>();
        gameObject.GetComponent<TCPClient>().StartClient(serverUsername, host, port);
    }

    private void StartServerAndClient()
    {
        StartServer();
        StartClient();
    }

    private static bool LoggingFormIsValid()
    {
        return GameManager.instance.serverUsernameText && GameManager.instance.serverAddressText && GameManager.instance.serverPortText &&
               GameManager.instance.serverUsernameText.GetComponent<Text>().text.Length > 0 &&
               GameManager.instance.serverAddressText.GetComponent<Text>().text.Length > 0 &&
               GameManager.instance.serverPortText.GetComponent<Text>().text.Length > 0;
    }
}