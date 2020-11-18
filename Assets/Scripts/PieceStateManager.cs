using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
public class PieceStateManager : MonoBehaviour
{
    public GameObject topStone;
    private PieceStateSynchronizer stateSynchronizer;
   
    public bool IsKing
    {
        get => topStone.activeSelf;
        private set => topStone.SetActive(value);
    }
   
    public void SetIsKingLocal(bool value)
    {
        IsKing = value;
        if (stateSynchronizer == null)
        {
            stateSynchronizer = GetComponent<PieceStateSynchronizer>();
        }
        stateSynchronizer.BroadcastStoneState();
    }
   
    public void SetIsKingRemote(bool value)
    {
        IsKing = value;
    }
   
    private void Start()
    {
        SetIsKingLocal(false);
    }
   
    public void ToggleState()
    {
        SetIsKingLocal(!IsKing);
    }
}