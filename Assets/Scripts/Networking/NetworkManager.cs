using System.Text.RegularExpressions;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public int maxRoomPlayer = 4;
    
     //Our player name
    string playerName = "Player 1";
    //Users are separated from each other by gameversion (which allows you to make breaking changes).
    string gameVersion = "0.9";
    //The list of created rooms
    List<RoomInfo> createdRooms = new List<RoomInfo>();
    //Use this name when creating a Room
    string roomName = "Room 1";
    Vector2 roomListScroll = Vector2.zero;
    bool joiningRoom = false;

    private GUIStyle buttonsStyle;
    private GUIStyle labelStyle;
    private GUIStyle textFieldStyle;
    private GUIStyle smallLabelStyle;
    private GUIStyle smallButtonsStyle;
    private GUIStyle windowStyle;

    private int spaceBetweenGUI = 20;

    // Use this for initialization
    void Start()
    {
        //This makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            //Set the App version before connecting
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            // Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + cause.ToString() + " ServerAddress: " + PhotonNetwork.ServerAddress);
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
    }

    void OnGUI()
    {
        buttonsStyle = new GUIStyle(GUI.skin.button);
        buttonsStyle.fontSize = 45;
        smallButtonsStyle = new GUIStyle(GUI.skin.button);
        smallButtonsStyle.fontSize = 40;
        labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 45;
        smallLabelStyle = new GUIStyle(GUI.skin.label);
        smallLabelStyle.fontSize = 40;
        textFieldStyle = new GUIStyle(GUI.skin.textField);
        textFieldStyle.fontSize = 45;
        windowStyle = new GUIStyle (GUI.skin.window); 
        // do whatever you want with this style, e.g.:
        windowStyle.padding = new RectOffset(25,25,25,25);
        
        GUI.Window(0, new Rect(10, Screen.height/2-Screen.height/3/2, Screen.width-20, Screen.height/2), LobbyWindow, "", windowStyle);
    }

    void LobbyWindow(int index)
    {
        GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                //Set player name and Refresh Room button
                GUILayout.Label("Player Name: ", labelStyle, GUILayout.Width(300));
                //Player name text field
                playerName = GUILayout.TextField(playerName, textFieldStyle, GUILayout.Width(300));
                
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.Space(spaceBetweenGUI);
        
        GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                //Room name text field
                roomName = GUILayout.TextField(roomName, textFieldStyle,GUILayout.Width(400));

                if (GUILayout.Button("Create Room", buttonsStyle,GUILayout.Width(325)))
                {
                    if (roomName != "")
                    {
                        joiningRoom = true;

                        RoomOptions roomOptions = new RoomOptions();
                        roomOptions.IsOpen = true;
                        roomOptions.IsVisible = true;
                        roomOptions.MaxPlayers = (byte)maxRoomPlayer;

                        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
                    }
                }
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.Space(spaceBetweenGUI);

        //Scroll through available rooms
        roomListScroll = GUILayout.BeginScrollView(roomListScroll,false, true);
            if (createdRooms.Count == 0)
            {
                GUILayout.Label("No Rooms were created yet...", smallLabelStyle);
            }
            else
            {
                for (int i = 0; i < createdRooms.Count; i++)
                {
                    GUILayout.BeginHorizontal("box");
                        GUILayout.Label(createdRooms[i].Name, labelStyle, GUILayout.Width(400));
                        GUILayout.Label(createdRooms[i].PlayerCount + "/" + createdRooms[i].MaxPlayers, labelStyle);

                        GUILayout.FlexibleSpace();

                        if (GUILayout.Button("Join Room", smallButtonsStyle))
                        {
                            joiningRoom = true;

                            //Set our Player name
                            PhotonNetwork.NickName = playerName;

                            //Join the Room
                            PhotonNetwork.JoinRoom(createdRooms[i].Name);
                        }
                    GUILayout.EndHorizontal();
                }
            }
        GUILayout.EndScrollView();
        
        GUILayout.Space(spaceBetweenGUI);
        
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUI.enabled = (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby || PhotonNetwork.NetworkClientState == ClientState.Disconnected) && !joiningRoom;
            if (GUILayout.Button("Refresh", buttonsStyle, GUILayout.Width(200)))
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
            }
        GUILayout.EndHorizontal();

        GUILayout.Space(spaceBetweenGUI);

        GUILayout.BeginHorizontal();
            GUILayout.Label("Status: " + Regex.Replace(PhotonNetwork.NetworkClientState.ToString(), "([A-Z])", " $1").Trim(), labelStyle);

            if (joiningRoom || !PhotonNetwork.IsConnected || PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
            {
                GUI.enabled = false;
            }
        GUILayout.EndHorizontal();

        if (joiningRoom)
        {
            GUI.enabled = true;
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, Screen.width, 40), "Connecting...", labelStyle);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
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
        PhotonNetwork.NickName = playerName;
        //Load the Scene called Game (Make sure it's added to build settings)
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }
}