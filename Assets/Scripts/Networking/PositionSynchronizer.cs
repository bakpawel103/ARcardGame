using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PositionSynchronizer : MonoBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameObject.transform.localPosition);
        }
        else
        {
            Vector3 receivedPosition = (Vector3) stream.ReceiveNext();
            gameObject.GetComponent<PieceInitializer>().pieceLocalPosition = receivedPosition;
            if(GameObject.FindGameObjectWithTag("PlayingField") != null)
            {
                gameObject.transform.localPosition = receivedPosition;
            }
        }
    }
}