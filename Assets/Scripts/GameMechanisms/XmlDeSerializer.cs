using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlDeSerializer
{
    public static void Serialize(object item, string path)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer.BaseStream, item);
        writer.Close();
    }
 
    public static T Deserialize<T>(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        var data = Resources.Load<TextAsset>(path).bytes;
        using (var reader = new MemoryStream(data))
        using (var textReader = new StreamReader(reader))
        {
            T deserialized = (T)serializer.Deserialize(textReader);
            return deserialized;
        }
    }
}