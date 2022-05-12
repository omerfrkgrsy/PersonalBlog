using PersonalBlog.Core.Models;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace PersonalBlog.Core.Security;

public class Encryption: IEncryption
{
    public readonly IOptions<PersonalBlogConfig> _personalBlogConfig;        
    public Encryption(IOptions<PersonalBlogConfig> personalBlogConfig)
    {
        _personalBlogConfig = personalBlogConfig;
    }

    #region Utilty

    private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
    {
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                var toEncrypt = Encoding.Unicode.GetBytes(data);
                cs.Write(toEncrypt, 0, toEncrypt.Length);
                cs.FlushFinalBlock();
            }

            return ms.ToArray();
        }
    }

    private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
    {
        using (var ms = new MemoryStream(data))
        {
            using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                using (var sr = new StreamReader(cs, Encoding.Unicode))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    #endregion

    public string DecryptText(string text, string privateKey = "")
    {
        try
        {
            if (string.IsNullOrEmpty(text) || text == "null")
                return string.Empty;

            if (string.IsNullOrEmpty(privateKey))
                privateKey = _personalBlogConfig.Value.PrivateKey;

            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(privateKey.Substring(0, 16));
                provider.IV = Encoding.ASCII.GetBytes(privateKey.Substring(8, 8));

                var buffer = Convert.FromBase64String(text);
                return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
            }
        }
        catch
        {
            //throw new InvalidTokenException();
            return text;
        }
    }

    public string EncryptText(string text, string privateKey = "")
    {
        try
        {
            if (string.IsNullOrEmpty(text) || text == "null")
                return string.Empty;

            if (string.IsNullOrEmpty(privateKey))
                privateKey = _personalBlogConfig.Value.PrivateKey;

            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(privateKey.Substring(0, 16));
                provider.IV = Encoding.ASCII.GetBytes(privateKey.Substring(8, 8));

                var encryptedBinary = EncryptTextToMemory(text, provider.Key, provider.IV);
                return Convert.ToBase64String(encryptedBinary);
            }
        }
        catch
        {
            return text;
        }
    }
}