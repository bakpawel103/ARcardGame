using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class Package
{
    public int packageType = 0;
    public Player player;
    public int rollSum;

    public static byte[] Serialize(object obj)
    {
        Package data = (Package) obj;

        // this.packageType
        byte[] packageTypeBytes = BitConverter.GetBytes(data.packageType);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(packageTypeBytes);
        }

        // this.player
        byte[] playerBytes = SerializePhotonPlayer(data.player);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(playerBytes);
        }

        // this.rollSum
        byte[] rollSumBytes = BitConverter.GetBytes(data.rollSum);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(rollSumBytes);
        }

        return JoinBytes(packageTypeBytes, playerBytes, rollSumBytes);
    }

    public static object Deserialze(byte[] bytes)
    {
        Package data = new Package();

        // this.packageType
        byte[] packageTypeBytes = new byte[4];
        Array.Copy(bytes, 0, packageTypeBytes, 0, packageTypeBytes.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(packageTypeBytes);
        }

        data.packageType = BitConverter.ToInt32(packageTypeBytes, 0);

        // this.player
        byte[] playerBytes = new byte[7];
        Array.Copy(bytes, 0, playerBytes, 0, playerBytes.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(playerBytes);
        }

        data.player = (Player) DeserializePhotonPlayer(playerBytes);

        // this.rollSum
        byte[] rollSumBytes = new byte[4];
        Array.Copy(bytes, 0, rollSumBytes, 0, rollSumBytes.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(rollSumBytes);
        }

        data.rollSum = BitConverter.ToInt32(rollSumBytes, 0);

        return data;
    }

    private static byte[] SerializePhotonPlayer(object data)
    {
        int ID = ((Player) data).ActorNumber;
        byte[] bytes = new byte[4];
        
        StreamBuffer outStream = new StreamBuffer();
        
        int off = 0;
        Protocol.Serialize(ID, bytes, ref off);
        outStream.Write(bytes, 0, 4);
        
        return bytes;
    }

    private static object DeserializePhotonPlayer(byte[] data)
    {
        int ID;
        byte[] memPlayer = new byte[4];
        
        StreamBuffer inStream = new StreamBuffer();
        
        inStream.Read(data, 0, data.Length);
        int off = 0;
        Protocol.Deserialize(out ID, new byte[4], ref off);

        if (PhotonNetwork.CurrentRoom != null)
        {
            Player player = PhotonNetwork.CurrentRoom.GetPlayer(ID);
            return player;
        }

        return null;
    }

    private static byte[] JoinBytes(params byte[][] arrays)
    {
        byte[] rv = new byte[arrays.Sum(a => a.Length)];
        int offset = 0;
        foreach (byte[] array in arrays)
        {
            System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
            offset += array.Length;
        }

        return rv;
    }
}