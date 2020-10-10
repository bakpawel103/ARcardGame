using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public static CardsManager instance;
    
    [XmlArray("Cards")]
    [XmlArrayItem("Card")]
    public CardsList<Card> cardsCollection;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        LoadCardsDataFromXml();
    }
    
    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToUpper(s[0]) + s.Substring(1).ToLower();
    }

    public void LoadCardsDataFromXml()
    {
        try
        {
            cardsCollection = new CardsList<Card>();
            cardsCollection = XmlDeSerializer.Deserialize<CardsList<Card>>("Packages/Default/Default");
            foreach (Card card in cardsCollection)
            {
                card.sprite = Resources.Load<Sprite>(card.rootPath + UppercaseFirst(card.cardType.ToString()) + "/" + card.filename);
                card.previewSprite = Resources.Load<Sprite>(card.rootPath + UppercaseFirst(card.cardType.ToString()) + "/" + card.previewFilename);
            }
        }
        catch (Exception exception)
        {
            GameManager.instance.AddLog("LoadCardsDataFromXml: " + exception.Message);
        }
    }
}