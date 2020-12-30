using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
   
public class BoardInitializer : MonoBehaviour
{
    public Transform mixedRealityPlayspace;

    public List<GameObject> piecesArray;
   
    private void Start()
    {
        mixedRealityPlayspace = GameObject.FindGameObjectWithTag("MixedRealityPlayspace").transform;  
    }

    public void SetPiecesInteractive(bool interactive)
    {
        foreach (var piece in piecesArray)
        {
            piece.GetComponent<PieceStateManager>().ChangeInteractionWithPiece(interactive);
        }
    }

    public void InitializeBoardPieces()
    {
        float cellSize = 1 / 8f;
   
        // determine which side the player should be on
        float playerDirection;
        if (PhotonNetwork.IsMasterClient)
        {
            playerDirection = 1;
        }
        else
        {
            playerDirection = -1;
        }
   
        // place the playing pieces
        Vector3 leftBottomStartPosition = new Vector3(playerDirection * -0.5f, 0.5f, playerDirection * -0.5f);
   
        for (int row = 0; row < 3; row++)
        {
            for (int column = row % 2; column < 8; column += 2)
            {
                // create a networked instance of the gamepiece
                // set the position and rotation to default values since we will first parent the object to the board
                // and then change the position relative to the board
                GameObject gamePiece = PhotonNetwork.Instantiate("GamePiece", Vector3.zero, Quaternion.identity);
                // parent the piece to the board
                gamePiece.transform.parent = gameObject.transform;
            
                Vector3 localPiecePosition = new Vector3(
                    playerDirection * (cellSize * column + cellSize / 2f),
                    0,
                    playerDirection * (cellSize * row + cellSize / 2f)
                    );
                gamePiece.transform.localPosition = leftBottomStartPosition + localPiecePosition;
                
                piecesArray.Add(gamePiece);
            }
        }
   
        // the second player starts on the opposite side of the board
        if (!PhotonNetwork.IsMasterClient)
        {
            if (mixedRealityPlayspace == null)
            {
                mixedRealityPlayspace = GameObject.FindGameObjectWithTag("MixedRealityPlayspace").transform;
            }
            
            mixedRealityPlayspace.position = new Vector3(0, 2, 4);
            mixedRealityPlayspace.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}