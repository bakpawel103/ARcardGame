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
            stream.SendNext(transform.localPosition);
        }
        else
        {
            transform.localPosition = (Vector3)stream.ReceiveNext();
            transform.parent = GameObject.FindGameObjectWithTag("PlayingField").transform;
        }
    }
}