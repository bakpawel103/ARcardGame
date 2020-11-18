using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
public class BoardInitializer : MonoBehaviour
{
    public const string firstPlayerFlag = "FirstPlayer";
   
    public Transform mixedRealityPlayspace;
   
    private void Start()
    {
        bool firstPlayer = true;
        // check if we are the first or second player in the room
        // each player has a shared flag which is realized as a custom property
        for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++)
        {
            if ((bool)PhotonNetwork.PlayerListOthers[i].CustomProperties[firstPlayerFlag])
            {
                Debug.Log(PhotonNetwork.PlayerListOthers[i].ActorNumber + " is first player");
                firstPlayer = false;
                break;
            }
        }
   
        // set the flag for this player
        // we cannot just add the property to the player but need to re-assign the entire hashtable to distribute the changes
        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
        playerProperties.Add(firstPlayerFlag, firstPlayer);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
   
        float cellSize = 1 / 8f;
   
        // determine which side the player should be on
        float playerDirection;
        if (firstPlayer)
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
            for (int column = row % 2; column < 8; column+=2)
            {
                // create a networked insteance of the gamepiece
                // set the position and rotation to default values since we will first parent the object to the board
                // and then change the position relative to the board
                GameObject gamePiece = PhotonNetwork.Instantiate("GamePiece", Vector3.zero, Quaternion.identity, 0);
                // parent the piece to the board
                gamePiece.transform.parent = transform;
            
                Vector3 localPiecePosition = new Vector3(
                    playerDirection * (cellSize * column + cellSize / 2f),
                    0,
                    playerDirection * (cellSize * row + cellSize / 2f)
                    );
                gamePiece.transform.localPosition = leftBottomStartPosition + localPiecePosition;
            }
        }
   
        // the second player starts on the opposite side of the board
        if (!firstPlayer)
        {
            mixedRealityPlayspace.position = new Vector3(0, 2, 4);
            mixedRealityPlayspace.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}