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
    }

    public void InitializePrefabs()
    {
        playingField.GetComponent<BoardInitializer>().InitializeBoardPieces();
        playingField.GetComponent<BoardInitializer>().SetPiecesInteractive(false);
        
        uiGO.transform.Find("SwitchChangeFieldBoardPositionButton").gameObject.SetActive(true);
        
        uiGO.transform.Find("StartGame").gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
    
    public void StartGame()
    {
        uiGO.transform.Find("StartGame").gameObject.SetActive(false);
        
        BroadcastGameStarted();
    }

    private void DisplayTurnText(string textToDisplay)
    {
        uiGO.transform.Find("TurnText").gameObject.SetActive(true);
        uiGO.transform.Find("TurnText").GetComponent<TextMeshPro>().text = textToDisplay;
        uiGO.transform.Find("TurnText").GetComponent<Animation>().Play();
        StartCoroutine(TurnTextCoroutine());
    }

    private IEnumerator TurnTextCoroutine()
    {
        yield return new WaitForSeconds(2.1f);
        uiGO.transform.Find("TurnText").gameObject.SetActive(false);
    }

    private void StartTurn()
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
    private void ReceiveTurnFinished(int currentTurnPlayerId)
    {
        this.currentTurnPlayerId = PhotonNetwork.CurrentRoom.GetPlayer(currentTurnPlayerId).GetNext().ActorNumber;

        DisplayTurnText(IsMyTurn() ? "Your Turn" : "Opponent's turn");

        playingField.GetComponent<BoardInitializer>().SetPiecesInteractive(IsMyTurn());
        
        StartTurn();
    }
    
    private void BroadcastGameStarted()
    {
        photonView.RPC("ReceiveGameStarted", RpcTarget.All, currentTurnPlayerId);
    }
    
    [PunRPC]
    private void ReceiveGameStarted(int currentTurnPlayerId)
    {
        this.currentTurnPlayerId = PhotonNetwork.CurrentRoom.GetPlayer(currentTurnPlayerId).GetNext().ActorNumber;

        TurnTimeEnded.AddListener(FinishTurn);
        StartCoroutine(StartTurnTimer());
        
        uiGO.transform.Find("RemainingTimeText").gameObject.SetActive(true);
        
        DisplayTurnText(IsMyTurn() ? "Your Turn" : "Opponent's turn");

        playingField.GetComponent<BoardInitializer>().SetPiecesInteractive(IsMyTurn());
        
        StartTurn();
    }

    private void RefreshFinishButton()
    {
        uiGO.transform.Find("FinishTurn").gameObject.SetActive(IsMyTurn());
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