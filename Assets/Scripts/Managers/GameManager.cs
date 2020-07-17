using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public GameObject cardStackPref;
    public GameObject cardPref;
    public Vector3 cardStackPosition;

    [Header("UI GameObjects")]
    public GameObject debugLog;
    public GameObject uiCanvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        GameObject.FindGameObjectWithTag("PlayersCountText").GetComponent<Text>().text = $"{(int)PhotonNetwork.CurrentRoom.PlayerCount}/{(int) PhotonNetwork.CurrentRoom.MaxPlayers}";
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
}