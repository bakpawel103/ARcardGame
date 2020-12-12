using Photon.Pun;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviourPun
{
    public GameObject uiGO;
    public GameObject playingField;
    
    [SerializeField]
    public int currentTurnPlayerId;

    public void InitializeTurnManager()
    {
        playingField = GameObject.FindGameObjectWithTag("PlayingField").gameObject;
        currentTurnPlayerId = PhotonNetwork.CurrentRoom.MasterClientId;
        uiGO.transform.Find("TurnText").GetComponent<TextMeshPro>().text = IsMyTurn() ? "Your Turn" : "Opponent's turn";
        
        foreach (var piece in playingField.GetComponent<BoardInitializer>().piecesArray)
        {
            piece.GetComponent<PieceStateManager>().ChangeInteractionWithPiece(false);
        }
    }
    
    public void StartGame()
    {
        foreach (var piece in playingField.GetComponent<BoardInitializer>().piecesArray)
        {
            piece.GetComponent<PieceStateManager>().ChangeInteractionWithPiece(IsMyTurn());
        }
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

        foreach (var piece in playingField.GetComponent<BoardInitializer>().piecesArray)
        {
            piece.GetComponent<PieceStateManager>().ChangeInteractionWithPiece(IsMyTurn());
        }
    }

    private void RefreshFinishButton()
    {
        uiGO.transform.Find("FinishTurnButton").gameObject.SetActive(IsMyTurn());
    }
}