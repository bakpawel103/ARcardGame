using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Photon.Pun;
using UnityEngine;
    
public class PieceInitializer : MonoBehaviourPun
{
    [SerializeField] private Color ownerColor;
    [SerializeField] private Color guestColor;
    
    private void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
    
        for (int i = 0; i < renderers.Length; i++)
        {
            if (photonView.IsMine)
            {
                if (PhotonNetwork.IsMasterClient)
                    renderers[i].material.color = ownerColor;
                else
                    renderers[i].material.color = guestColor;
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                    renderers[i].material.color = guestColor;
                else
                    renderers[i].material.color = ownerColor;
            }
        }
    
        if (!photonView.IsMine)
        {
            GetComponent<ObjectManipulator>().enabled = false;
            GetComponent<ClickRecognizer>().enabled = false;
        }
    }
}