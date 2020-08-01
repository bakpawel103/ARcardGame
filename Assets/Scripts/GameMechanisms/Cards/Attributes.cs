using System.Xml.Serialization;

[XmlRoot("Attributes")]
public class Attributes
{
    [XmlElement("Attack")] public int attack { get; set; }
    [XmlElement("Defence")] public int defence { get; set; }
}