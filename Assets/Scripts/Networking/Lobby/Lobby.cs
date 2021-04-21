using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lobby : MonoBehaviourPunCallbacks
{
    #region Public Properties

    public GameObject roomsListButtonPrefab;
    public GameObject roomsListGOContent;

    #endregion

    #region Private Properties

    private string gameVersion = "1";
    private List<RoomInfo> createdRooms = new List<RoomInfo>();
    private List<GameObject> roomsListGO = new List<GameObject>();

    #endregion

    #region Lifecycles

    void Start()
    {
        //This makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 40;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        GenerateRoomsList();
    }

    #endregion

    #region Public Methods

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom($"Room{Random.Range(0, 1000)}",
            new RoomOptions {IsVisible = false, IsOpen = false, MaxPlayers = 2}, TypedLobby.Default);
    }

    public void RefreshRooms()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        GenerateRoomsList();
    }

    public void GenerateRoomsList()
    {
        DeleteAllChildrenFromGameObject(roomsListGOContent);

        for (int i = 0; i < createdRooms.Count; i++)
        {
            GameObject itemPrefabInstance =
                Instantiate(roomsListButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            var i1 = i;
            itemPrefabInstance.GetComponent<Interactable>().OnClick.AddListener(() =>
            {
                PhotonNetwork.NickName = $"Player{Random.Range(0, 1000)}";
                PhotonNetwork.JoinRoom(createdRooms[i1].Name);
            });

            if (itemPrefabInstance != null)
            {
                itemPrefabInstance.transform.GetChild(3).GetComponent<TextMeshPro>().text = createdRooms[i].Name;

                itemPrefabInstance.transform.SetParent(roomsListGOContent.transform, false);
            }

            roomsListGO.Add(itemPrefabInstance);
        }

        roomsListGOContent.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    private void DeleteAllChildrenFromGameObject(GameObject gameObject)
    {
        roomsListGO.Clear();
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion

    #region Overrides Methods

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"OnFailedToConnectToPhoton. StatusCode: {cause} ServerAddress: {PhotonNetwork.ServerAddress}");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        GameObject.FindGameObjectWithTag("ProgressIndicator").SetActive(false);
        Views.instance.NavigateToView(0);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("We have received the Room list");
        createdRooms = roomList;

        if (createdRooms.Count > 0)
            GenerateRoomsList();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(
            "OnCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.NickName = $"Player{Random.Range(0, 1000)}";
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    #endregion
}