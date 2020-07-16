using System.Text.RegularExpressions;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public int maxRoomPlayer = 4;
    public GameObject scrollViewItemItemPrefab;

    private GameObject playerName;
    private string gameVersion = "0.9";
    private List<RoomInfo> createdRooms = new List<RoomInfo>();
    private GameObject roomName;
    private GameObject scrollViewContentGO;
    private bool joiningRoom = false;

    private GUIStyle lobbyGUISkin;

    private int spaceBetweenGUI = 20;

    void Start()
    {
        playerName = GameObject.FindGameObjectWithTag("PlayerNameInput");
        roomName = GameObject.FindGameObjectWithTag("RoomNameInput");
        scrollViewContentGO = GameObject.FindGameObjectWithTag("ScrollViewContent");

        //This makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            //Set the App version before connecting
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            // Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
            PhotonNetwork.ConnectUsingSettings();
        }

        GenerateScrollView();
    }

    void Update()
    {
    }

    public void CreateJoinRoom()
    {
        Debug.Log(roomName);
        if (roomName.GetComponent<InputField>().text != "")
        {
            joiningRoom = true;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = (byte) maxRoomPlayer;

            PhotonNetwork.JoinOrCreateRoom(roomName.GetComponent<InputField>().text, roomOptions, TypedLobby.Default);
        }
    }

    public void RefreshRooms()
    {
        if (PhotonNetwork.IsConnected)
        {
            //Re-join Lobby to get the latest Room list
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
        else
        {
            //We are not connected, estabilish a new connection
            PhotonNetwork.ConnectUsingSettings();
        }

        GenerateScrollView();
    }

    private void GenerateScrollView()
    {
        DeleteAllChildrenFromGameObject(scrollViewContentGO);

        for (int i = 0; i < createdRooms.Count; i++)
        {
            GameObject itemPrefabInstance = Instantiate(scrollViewItemItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            if (itemPrefabInstance != null)
            {
                itemPrefabInstance.transform.GetChild(0).GetComponent<Text>().text = createdRooms[i].Name;
                itemPrefabInstance.transform.GetChild(1).GetComponent<Text>().text =
                    createdRooms[i].PlayerCount + "/" + createdRooms[i].MaxPlayers;

                var i1 = i;
                itemPrefabInstance.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log(i1);
                    joiningRoom = true;
                    PhotonNetwork.NickName = playerName.GetComponent<InputField>().text;
                    PhotonNetwork.JoinRoom(createdRooms[i1].Name);
                });
                itemPrefabInstance.transform.SetParent(scrollViewContentGO.transform, false);
            }
        }
    }

    private void DeleteAllChildrenFromGameObject(GameObject gameObject)
    {
        if (gameObject.transform.GetChildCount() > 0)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + cause.ToString() + " ServerAddress: " +
                  PhotonNetwork.ServerAddress);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        //After we connected to Master server, join the Lobby
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("We have received the Room list");
        //After this callback, update the room list
        createdRooms = roomList;
        GenerateScrollView();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(
            "OnCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
        joiningRoom = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        //Set our player name
        PhotonNetwork.NickName = playerName.GetComponent<InputField>().text;
        //Load the Scene called Game (Make sure it's added to build settings)
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }
}