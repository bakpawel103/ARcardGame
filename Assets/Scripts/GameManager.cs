using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool movingCard;
    public GameObject cardGO;
    public Vector3 cardStackPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        movingCard = false;
    }

    public void SetCardVisibility(bool visible)
    {
        cardGO.SetActive(visible);
    }
}