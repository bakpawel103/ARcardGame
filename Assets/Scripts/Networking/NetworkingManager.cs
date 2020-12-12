using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using UnityEngine;

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    public GameObject turnManager;
    public GameObject ui;

    public void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    public void StartApplication()
    {
        turnManager.GetComponent<TurnManager>().InitializeTurnManager();
        
        turnManager.GetComponent<TurnManager>().StartGame();

        ui.SetActive(true);
        SwitchFieldBoardPosition();
        
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.CurrentRoom.IsOpen = true;
    }

    public void SwitchFieldBoardPosition()
    {
        GameObject.FindGameObjectWithTag("PlayingField").GetComponent<ObjectManipulator>().enabled =
            !GameObject.FindGameObjectWithTag("PlayingField").GetComponent<ObjectManipulator>().enabled;
        GameObject.FindGameObjectWithTag("PlayingField").GetComponent<BoundingBox>().enabled =
            !GameObject.FindGameObjectWithTag("PlayingField").GetComponent<BoundingBox>().enabled;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("Room is full");
            turnManager.GetComponent<TurnManager>().StartGame();
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log($"Player left this room {PhotonNetwork.CurrentRoom.PlayerCount}");
    }
}