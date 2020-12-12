using Photon.Pun;

public class PieceStateSynchronizer : MonoBehaviourPun
{
    PieceStateManager pieceStateManager;
   
    private float startTime;
   
    private void Start()
    {
        pieceStateManager = GetComponent<PieceStateManager>();
    }
    
    public void BroadcastStoneState()
    {
        if (pieceStateManager == null)
        {
            pieceStateManager = GetComponent<PieceStateManager>();
        }
   
        photonView.RPC("ReceiveStoneState", RpcTarget.Others, pieceStateManager.IsKing);
    }
    
    [PunRPC]
    private void ReceiveStoneState(bool isKing, PhotonMessageInfo info)
    {
        if (pieceStateManager == null)
        {
            pieceStateManager = GetComponent<PieceStateManager>();
        }
        
        pieceStateManager.SetIsKingRemote(isKing);
    }
}
