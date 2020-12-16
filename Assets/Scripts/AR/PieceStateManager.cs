using System;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Photon.Pun;
using UnityEngine;
   
public class PieceStateManager : MonoBehaviourPun
{
    public GameObject topStone;
    
    private PieceStateSynchronizer stateSynchronizer;
    private TurnManager turnManager;

    public bool IsKing
    {
        get => topStone.activeSelf;
        private set => topStone.SetActive(value);
    }
   
    public void SetIsKingLocal(bool value)
    {
        if (turnManager.IsMyTurn())
        {
            IsKing = value;
            if (stateSynchronizer == null)
            {
                stateSynchronizer = GetComponent<PieceStateSynchronizer>();
            }

            stateSynchronizer.BroadcastStoneState();
        }
    }

    public void SetIsKingRemote(bool value)
    {
        if (turnManager.IsMyTurn()) {
            IsKing = value;
        }
    }

    private void Awake()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    private void Start()
    {
        topStone.SetActive(false);
    }
   
    public void ToggleState()
    {
        SetIsKingLocal(!IsKing);
    }

    public void ChangeInteractionWithPiece(bool interaction)
    {
        if (photonView.IsMine)
        {
            gameObject.GetComponent<ObjectManipulator>().enabled = interaction;
            gameObject.GetComponent<ClickRecognizer>().enabled = interaction;
        }
    }
}