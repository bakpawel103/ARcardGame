using System;
using UnityEngine;
using UnityEngine.UI;

public class CardsStackManager : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.instance.debugLog.GetComponent<Text>().text += "Adding new card\n";
        HandManager.instance.AddNewCard();
    }
}