using System;
using UnityEngine;
using UnityEngine.UI;

public class CardsStackManager : MonoBehaviour
{
    private void OnMouseDown()
    {
        HandManager.instance.AddNewCard();
    }
}