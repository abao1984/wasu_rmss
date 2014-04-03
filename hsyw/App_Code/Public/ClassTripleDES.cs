using System;
using System.Data;
using System.Configuration;
////using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
////using System.Xml.Linq;

/// <summary>
///Class1 的摘要说明
/// </summary>
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;


public class ClassTripleDES
{
    public static byte[] DESKey = new byte[] { 0x82, 0xBC, 0xA1, 0x6A, 0xF5, 0x87, 0x3B, 0xE6, 0x59, 0x6A, 0x32, 0x64, 0x7F, 0x3A, 0x2A, 0xBB, 0x2B, 0x68, 0xE2, 0x5F, 0x06, 0xFB, 0xB8, 0x2D, 0x67, 0xB3, 0x55, 0x19, 0x4E, 0xB8, 0xBF, 0xDD };

    public string DESEncrypt(string strSource)
    {
        return DESEncrypt(strSource, DESKey);
    }
    public string DESEncrypt(string strSource, byte[] key)
    {
        SymmetricAlgorithm sa = Rijndael.Create();
        sa.Key = key;
        sa.Mode = CipherMode.ECB;
        sa.Padding = PaddingMode.Zeros;
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] byt = Encoding.UTF8.GetBytes(strSource);
        cs.Write(byt, 0, byt.Length);
        cs.FlushFinalBlock();
        cs.Close();
        return Convert.ToBase64String(ms.ToArray());
    }

    public string DESDecrypt(string strSource)
    {
        return DESDecrypt(strSource, DESKey);
    }
    public string DESDecrypt(string strSource, byte[] key)
    {
        SymmetricAlgorithm sa = Rijndael.Create();
        sa.Key = key;
        sa.Mode = CipherMode.ECB;
        sa.Padding = PaddingMode.Zeros;
        ICryptoTransform ct = sa.CreateDecryptor();
        byte[] byt = Convert.FromBase64String(strSource);
        MemoryStream ms = new MemoryStream(byt);
        CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cs, Encoding.UTF8);
        return sr.ReadToEnd();
    }



}
