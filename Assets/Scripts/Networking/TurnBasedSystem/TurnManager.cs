using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviourPun
{
    public GameObject uiGO;
    public GameObject playingField;

    public int remainingSeconds;
    public int maximumTurnTime;
    
    [SerializeField]
    public int currentTurnPlayerId;
    
    UnityEvent TurnTimeEnded = new UnityEvent();

    public void InitializeTurnManager()
    {
        playingField = GameObject.FindGameObjectWithTag("PlayingField").gameObject;
        currentTurnPlayerId = PhotonNetwork.CurrentRoom.MasterClientId;
        uiGO.transform.Find("TurnText").GetComponent<TextMeshPro>().text = IsMyTurn() ? "Your Turn" : "Opponent's turn";
        
        RefreshFinishButton();
    }
    
    public void StartGame()
    {
        playingField.GetComponent<BoardInitializer>().InitializeBoardPieces();
        playingField.GetComponent<BoardInitializer>().SetPiecesInteractive(IsMyTurn());
        
        TurnTimeEnded.AddListener(FinishTurn);
        StartCoroutine(StartTurnTimer());
        StartTurn();
    }

    public void StartTurn()
    {
        remainingSeconds = maximumTurnTime;
        RefreshFinishButton();
    }

    public void FinishTurn()
    {
        if (currentTurnPlayerId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            BroadcastTurnFinished();
        }
    }

    public bool IsMyTurn()
    {
        return currentTurnPlayerId == PhotonNetwork.LocalPlayer.ActorNumber;
    }
    
    private void BroadcastTurnFinished()
    {
        photonView.RPC("ReceiveTurnFinished", RpcTarget.All, currentTurnPlayerId);
    }
    
    [PunRPC]
    private void ReceiveTurnFinished(int currentTurnPlayerId, PhotonMessageInfo info)
    {
        this.currentTurnPlayerId = PhotonNetwork.CurrentRoom.GetPlayer(currentTurnPlayerId).GetNext().ActorNumber;
        uiGO.transform.Find("TurnText").GetComponent<TextMeshPro>().text = IsMyTurn() ? "Your Turn" : "Opponent's turn";

        playingField.GetComponent<BoardInitializer>().SetPiecesInteractive(IsMyTurn());
        
        StartTurn();
    }

    private void RefreshFinishButton()
    {
        uiGO.transform.Find("FinishTurnButton").gameObject.SetActive(IsMyTurn());
    }
    
    IEnumerator StartTurnTimer(){
        while (true)
        {
            AddSecondToTurnTimer();
            yield return new WaitForSeconds(1);
        }
    }
    void AddSecondToTurnTimer(){
        remainingSeconds -= 1;
        uiGO.transform.Find("RemainingTimeText").GetComponent<TextMeshPro>().text = $"Time left: {remainingSeconds.ToString()}s";

        if (remainingSeconds <= 0)
        {
            TurnTimeEnded.Invoke();
        }
    }
}