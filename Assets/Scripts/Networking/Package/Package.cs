using System;
using System.Linq;

[System.Serializable]
public class Package
{
    public int packageType = 0;
    public int rollSum;
    
    public Package() { }
    
    public Package(int packageType, int rollSum) {
        this.packageType = packageType;
        this.rollSum = rollSum;
    }

    public static byte[] Serialize(object obj)
    {
        Package data = (Package) obj;

        return JoinBytes(SerializePackageType(data.packageType), SerializeRollSum(data.rollSum));
    }

    public static object Deserialze(byte[] bytes)
    {
        Package data = new Package();

        data.packageType = DeserializePackageType(bytes);
        data.rollSum = DeserializeRollSum(bytes);

        return data;
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

    public static byte[] SerializePackageType(int packageType)
    {
        byte[] packageTypeBytes = BitConverter.GetBytes(packageType);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(packageTypeBytes);
        }

        return packageTypeBytes;
    }

    public static byte[] SerializeRollSum(int rollSum)
    {
        byte[] rollSumBytes = BitConverter.GetBytes(rollSum);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(rollSumBytes);
        }

        return rollSumBytes;
    }

    public static int DeserializePackageType(byte[] bytes)
    {
        byte[] packageTypeBytes = new byte[4];
        Array.Copy(bytes, 0, packageTypeBytes, 0, packageTypeBytes.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(packageTypeBytes);
        }
        
        return BitConverter.ToInt32(packageTypeBytes, 0);
    }

    public static int DeserializeRollSum(byte[] bytes)
    {
        byte[] rollSumBytes = new byte[4];
        Array.Copy(bytes, 4, rollSumBytes, 0, rollSumBytes.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(rollSumBytes);
        }
        
        return BitConverter.ToInt32(rollSumBytes, 0);
    }
}