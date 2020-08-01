using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public CardsList<Card> cardsInHands = new CardsList<Card>();

    public static HandManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cardsInHands.ListChanged += () => { UpdateHand(); };
    }

    public void UpdateHand()
    {
    }

    public void AddNewCard()
    {
        try
        {
            Card newCard = CardsManager.instance.cardsCollection.GetRandomCardOfType(CardType.WEAPON, CardType.ARMOUR);
            GameObject newCardGO =
                Instantiate(GameManager.instance.cardPreviewPref, transform.position, Quaternion.identity) as GameObject;
            newCardGO.gameObject.GetComponent<CardScript>().SetCard(newCard);
            newCardGO.gameObject.GetComponent<CardScript>().SetSprite();

            switch (newCard.cardType)
            {
                case CardType.ARMOUR:
                    newCardGO.transform.SetParent(transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform);
                    break;
                case CardType.ITEM:
                    newCardGO.transform.SetParent(transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform);
                    break;
                case CardType.WEAPON:
                    newCardGO.transform.SetParent(transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform);
                    break;
            }

            GameManager.instance.debugLog.GetComponent<Text>().text += "Added new card\n";

            UpdateHand();
        }
        catch (Exception exception)
        {
            GameManager.instance.debugLog.GetComponent<Text>().text += "AddNewCard: " + exception.Message;
            Debug.Log(exception);
        }
    }
}