using System;
using UnityEngine;

public class CardsStackManager : MonoBehaviour
{
    private GameObject card;
    private GameObject camera;

    private void Awake()
    {
        camera = GameObject.FindWithTag("Camera");
    }

    private void OnMouseDown()
    {
        HandManager.instance.AddNewCard();
        Debug.Log("Adding new card");
    }
}