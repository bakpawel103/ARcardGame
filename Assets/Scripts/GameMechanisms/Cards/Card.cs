using System;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Card")]
public class Card : ICard, IComparable
{
    [XmlElement("ID")] public int ID { get; set; }
    [XmlElement("Name")] public string name { get; set; }
    [XmlElement("Type")] public string type { get; set; }
    [XmlElement("CardType")] public CardType cardType { get; set; }
    [XmlElement("Attributes")] public Attributes attributes { get; set; }
    [XmlElement("Information")] public string information { get; set; }
    [XmlElement("CardFilename")] public string filename { get; set; }
    [XmlElement("CardPreviewFilename")] public string previewFilename { get; set; }
    [XmlElement("RootPath")] public string rootPath { get; set; }
    [XmlIgnore] public Sprite sprite { get; set; }
    [XmlIgnore] public Sprite previewSprite { get; set; }

    public Card()
    {
    }

    public Card(CardType cardType)
    {
        this.cardType = cardType;
    }

    public Card(int ID, string name, string type, CardType cardType, Attributes attributes, string information, string filename,
        string previewFilename, Sprite sprite)
    {
        this.ID = ID;
        this.name = name;
        this.type = type;
        this.cardType = cardType;
        this.attributes = attributes;
        this.information = information;
        this.filename = filename;
        this.previewFilename = previewFilename;
        this.sprite = sprite;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (obj is Card otherCard)
            return cardType.CompareTo(otherCard.cardType);
        else
            throw new ArgumentException("Object is not a Card type");
    }

    public override string ToString()
    {
        string toString = "";

        toString += ID + " " + name + " " + type + " " + cardType + " " + attributes.attack + " " + attributes.defence + " " + information +
                    " " + filename + " " + previewFilename + "\n";

        return toString;
    }
}