using System;
using UnityEngine;

public class CardsStackManager : MonoBehaviour
{
    private GameObject card;
    private float speed;
    private GameObject camera;
    private bool moveCardToHand;

    private void Awake()
    {
        camera = GameObject.FindWithTag("Camera");
        card = GameObject.FindWithTag("Card");
        moveCardToHand = false;
    }

    private void OnMouseDown()
    {
        if (!moveCardToHand)
        {
            moveCardToHand = !moveCardToHand;
            GameManager.instance.SetCardVisibility(moveCardToHand);
        }
    }

    void Update () {
        if(moveCardToHand) {
            float step = speed * Time.deltaTime;
            card.transform.position = Vector2.MoveTowards(card.transform.position, camera.transform.position, step);
        }
    }
}