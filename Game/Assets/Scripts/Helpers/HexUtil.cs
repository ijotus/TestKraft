using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexUtil
{

    public static byte[] ToByteArray(String hexString)
    {
        byte[] retval = new byte[hexString.Length / 2];
        for (int i = 0; i < hexString.Length; i += 2)
            retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        return retval;
    }

    public static uint ColorToUint(Color32 color)
    {
        return (uint)((color.a << 24) | (color.r << 16) |
        (color.g << 8) | (color.b << 0));
    }
}
