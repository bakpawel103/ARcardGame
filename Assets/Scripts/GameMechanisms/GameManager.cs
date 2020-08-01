using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public GameObject cardStackPref;
    public GameObject cardPref;
    public GameObject cardPreviewPref;
    public Vector3 cardStackPosition;

    public GameObject playerPrefab;

    [Header("UI GameObjects")]
    public GameObject debugLog;
    public GameObject uiCanvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddLog(string log)
    {
        debugLog.GetComponent<Text>().text += log + "\n";
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void SendStringMessage()
    {
        photonView.RPC("RpcWithString", RpcTarget.Others, "jup", "and jup.");
    }
    
    [PunRPC]
    void RpcWithString(string a, string b, PhotonMessageInfo info)
    {
        debugLog.GetComponent<Text>().text += $"Chat message from {info.Sender.NickName}: {a} {b}";
    }
    
    public void SendArrayObjectMessage()
    {
        object[] objectArray = { 1, 2, 3, 4, 5, 6 };
        photonView.RPC("RpcWithObjectArray", RpcTarget.Others, objectArray as object);
    }
    
    [PunRPC]
    void RpcWithObjectArray(object[] objectArray, PhotonMessageInfo info)
    {
        debugLog.GetComponent<Text>().text += $"RpcWithObjectArray from {info.Sender.NickName}: {objectArray}";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            GameObject.FindGameObjectWithTag("PlayersCountText").GetComponent<Text>().text =
                $"{(int) PhotonNetwork.CurrentRoom.PlayerCount}/{(int) PhotonNetwork.CurrentRoom.MaxPlayers}";
        }

        debugLog.GetComponent<Text>().text += "New user logged in: " + newPlayer.NickName;
        if (newPlayer.IsMasterClient)
        {
            //Debug.Log($"Logged in user: {newPlayer.NickName} and isMasterClient {newPlayer.IsMasterClient}");
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            //PhotonNetwork.Instantiate("Player", new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
        else
        {
            //Debug.Log($"Logged in user: {newPlayer.NickName} and isMasterClient {newPlayer.IsMasterClient}");
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            //PhotonNetwork.Instantiate("Player", new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
    }
}