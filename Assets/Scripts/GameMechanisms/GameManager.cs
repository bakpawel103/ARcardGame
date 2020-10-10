using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public GameObject cardPref;
    public GameObject cardPreviewPref;
<<<<<<< HEAD
=======
    public GameObject boardPref;
    public Vector3 cardStackPosition;

    public GameObject playerPrefab;
>>>>>>> d7442c1a3e288cfbbd5ea6cddc24f614252550f4

    [Header("UI GameObjects")]
    public GameObject debugLog;
    public GameObject debugLogGO;
    public GameObject uiCanvas;
    public GameObject scanningHelperGO;

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

    void Start()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            GameObject.FindGameObjectWithTag("PlayersCountText").GetComponent<Text>().text =
                $"{(int) PhotonNetwork.CurrentRoom.PlayerCount}/{(int) PhotonNetwork.CurrentRoom.MaxPlayers}";
        }
    }

    public void AddLog(string log)
    {
        Debug.Log(log);
        debugLog.GetComponent<Text>().text += log + "\n";
    }

    public void SwitchDebugLogPanel()
    {
        debugLogGO.SetActive(!debugLogGO.activeSelf);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            GameObject.FindGameObjectWithTag("PlayersCountText").GetComponent<Text>().text =
                $"{(int) PhotonNetwork.CurrentRoom.PlayerCount}/{(int) PhotonNetwork.CurrentRoom.MaxPlayers}";
        }

        GameManager.instance.AddLog("New user logged in: " + newPlayer.NickName);
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