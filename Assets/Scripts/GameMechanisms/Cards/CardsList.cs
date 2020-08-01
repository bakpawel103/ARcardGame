using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[XmlRoot(ElementName="Cards")]
public class CardsList<T> : List<T> where T : Card
{
    public event ListChangedEventDelegate ListChanged;
    public delegate void ListChangedEventDelegate();
    
    public new void Add(T item)
    {
        base.Add(item);
        Notify();
    }
    
    private void Notify()
    {
        if (ListChanged != null && ListChanged.GetInvocationList().Any())
        {
            ListChanged();
        }
    }
    
    public void AddSprites()
    {
        foreach(T card in this)
        {
            card.sprite  = Resources.Load<Sprite>("Packages/Default/Cards/" + card.filename);
            // TODO Dodać previewSprite do wszystkich kart, oprócz Enemies
            // card.previewSprite  = Resources.Load<Sprite>("Packages/Default/Cards/" + card.previewFilename);
        }
    }
    
    public CardsList<T> GetCardsOfType(params CardType[] cardTypes)
    {
        CardsList<T> cardsOfType = new CardsList<T>();

        foreach (CardType cardType in cardTypes)
        {
            for (int cardsListIndex = 0; cardsListIndex < Count; cardsListIndex++)
            {
                if (this[cardsListIndex].CompareTo(new Card(cardType)) == 0)
                {
                    cardsOfType.Add(this[cardsListIndex]);
                }
            }
        }

        return cardsOfType;
    }

    public Card GetRandomCardOfType(params CardType[] cardTypes)
    {
        try
        {
            CardsList<T> cardsOfType = GetCardsOfType(cardTypes);
            return cardsOfType[new System.Random().Next(cardsOfType.Count)];
        }
        catch (Exception exception)
        {
            GameManager.instance.debugLog.GetComponent<Text>().text += "GetRandomCardOfType: " + exception.Message;
            return null;
        }
    }

    public override string ToString()
    {
        string toString = "";
        
        foreach (T cardInHand in this)
        {
            toString += cardInHand.ToString();
        }

        return toString;
    }
}