using System;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Photon.Pun;
using UnityEngine;
    
public class PieceInitializer : MonoBehaviourPun
{
    public Vector3 pieceLocalPosition;
    
    [SerializeField] private Color ownerColor;
    [SerializeField] private Color guestColor;

    private bool pieceInitialized = false;
    
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

    private void Update()
    {
        if (!pieceInitialized && GameObject.FindGameObjectWithTag("PlayingField") != null && pieceLocalPosition != Vector3.zero)
        {
            pieceInitialized = true;
            
            transform.parent = GameObject.FindGameObjectWithTag("PlayingField").transform;
            
            gameObject.transform.localPosition = pieceLocalPosition;
        }
    }
}