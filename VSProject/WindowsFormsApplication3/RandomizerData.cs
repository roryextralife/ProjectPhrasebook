using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Summary description for Class1
/// </summary>
public static class RandomizerData
{
    public static byte[] items = new byte[0x0150];
    public static string shopData = "ShopItemAndPrice.dat";
    public static string itemShopLocations = "ItemShopLocations.csv";
    public static int[] iShopLocs = new int[4];
    public static string weaponShopLocations = "WeaponShopLocations.csv";
    public static int[] wShopLocs = new int[9];
    public static string armorShopLocations = "ArmorShopLocations.csv";
    public static int[] aShopLocs = new int[8];
    public static string magicShopLocations = "MagicShopLocations.csv";
    public static int[] mShopLocs = new int[6];

    public static void InitArrays()
    {
        items = File.ReadAllBytes(shopData);
        iShopLocs = StringToIntList(File.ReadAllText(itemShopLocations)).ToArray();
        wShopLocs = StringToIntList(File.ReadAllText(weaponShopLocations)).ToArray();
        aShopLocs = StringToIntList(File.ReadAllText(armorShopLocations)).ToArray();
        mShopLocs = StringToIntList(File.ReadAllText(magicShopLocations)).ToArray();
    }
    public static IEnumerable<int> StringToIntList(string str)
    {
        if (String.IsNullOrEmpty(str))
            yield break;

        foreach (var s in str.Split(','))
        {
            int num;
            if (int.TryParse(s, out num))
                yield return num;
        }
    }
}

