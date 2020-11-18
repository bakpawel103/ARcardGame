using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Photon.Pun;
using UnityEngine;
    
public class PieceInitializer : MonoBehaviourPun
{
    [SerializeField] private Color ownerColor;
    [SerializeField] private Color guestColor;
    private void Start()
    {
        bool isFirstPlayer = (bool)photonView.Owner.CustomProperties[BoardInitializer.firstPlayerFlag];
    
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
    
        for (int i=0;i<renderers.Length;i++)
        {
            if (isFirstPlayer)
            {
                renderers[i].material.color = ownerColor;
            }
            else
            {
                renderers[i].material.color = guestColor;
            }
        }
    
        if (!photonView.IsMine)
        {
            GetComponent<ObjectManipulator>().enabled = false;
            GetComponent<ClickRecognizer>().enabled = false;
        }
    }
}