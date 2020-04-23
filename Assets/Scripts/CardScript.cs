using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Tapped");
                if (raycastHit.collider.CompareTag("Card"))
                {
                    Debug.Log("Tapped Card");
                    if (!HandManager.handShown)
                    {
                        Debug.Log("Showing");
                        HandManager.instance.ShowHand();
                    }
                }
            }
            else
            {
                Debug.Log("Tapped World");
                if (HandManager.handShown)
                {
                    Debug.Log("Hiding");
                    HandManager.instance.HideHand();
                }
            }
        }
    }
}
