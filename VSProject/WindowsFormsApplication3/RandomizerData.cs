using System;
using System.IO;
/// <summary>
/// Summary description for Class1
/// </summary>
public static class RandomizerData
{
    public static byte[] items = new byte[0x0160];
    public static string shopData = "ShopItemAndPrice.dat";
    public static void InitArrays()
    {
        items = File.ReadAllBytes(shopData);

    }
}

