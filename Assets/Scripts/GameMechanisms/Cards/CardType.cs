using System.Xml.Serialization;

public enum CardType
{
    [XmlEnum(Name = "Armour")]
    ARMOUR,
    [XmlEnum(Name = "Enemy")]
    ENEMY,
    [XmlEnum(Name = "Iteam")]
    ITEM,
    [XmlEnum(Name = "Weapon")]
    WEAPON
}