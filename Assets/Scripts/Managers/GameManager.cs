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
    public Vector3 cardStackPosition;

    public GameObject playerPrefab;

    [Header("UI GameObjects")] public GameObject debugLog;
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

    void Update()
    {
        GameObject.FindGameObjectWithTag("PlayersCountText").GetComponent<Text>().text =
            $"{(int) PhotonNetwork.CurrentRoom.PlayerCount}/{(int) PhotonNetwork.CurrentRoom.MaxPlayers}";
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

    public void SendTestMessage()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ChatMessage", RpcTarget.All, "jup", "and jup.");
    }
    
    [PunRPC]
    void ChatMessage(string a, string b)
    {
        Debug.Log(string.Format("ChatMessage {0} {1}", a, b));
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
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