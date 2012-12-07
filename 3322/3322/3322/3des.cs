using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace _3322
{
    class _3des
    {

        /// <summary>
     /// 3DES 加密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
         /// <param name="EncryptKey1">密钥一</param>
        /// <param name="EncryptKey2">密钥二</param>
         /// <param name="EncryptKey3">密钥三</param>
         /// <returns>returns</returns>
         public static string DES3Encrypt(string EncryptString, string EncryptKey1, string EncryptKey2, string EncryptKey3)
        {
            string m_strEncrypt = "";
 
            try
             {
                 m_strEncrypt = DESEncrypt(EncryptString, EncryptKey3);

                m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey2);
 
                 m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey1);
             }
             catch (Exception ex) { throw ex; }
 
             return m_strEncrypt;
         }
        /// <summary>
       /// 3DES 解密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
       /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey1">密钥一</param>
         /// <param name="DecryptKey2">密钥二</param>
         /// <param name="DecryptKey3">密钥三</param>
        /// <returns>returns</returns>
         public static string DES3Decrypt(string DecryptString, string DecryptKey1, string DecryptKey2, string DecryptKey3)
         {
             string m_strDecrypt = "";
 
             try
             {
                m_strDecrypt = DESDecrypt(DecryptString, DecryptKey1);
 
                 m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey2);
 
                 m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey3);
             }
             catch (Exception ex) { throw ex; }
 
            return m_strDecrypt;
        }




         /// <summary>
         /// DES 加密(数据加密标准，速度较快，适用于加密大量数据的场合)
          /// </summary>
         /// <param name="EncryptString">待加密的密文</param>
        /// <param name="EncryptKey">加密的密钥</param>
         /// <returns>returns</returns>
         public static string DESEncrypt(string EncryptString, string EncryptKey)
         {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }
 
             if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
 
             if (EncryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); } 
             byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
 
             string m_strEncrypt = "";
 
             DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
 
            try
             {
                 byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
 
                 MemoryStream m_stream = new MemoryStream();
 
                 CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
 
                 m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
 
                 m_cstream.FlushFinalBlock();
 
                 m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
 
                 m_stream.Close(); m_stream.Dispose();
 
                 m_cstream.Close(); m_cstream.Dispose();
             }
             catch (IOException ex) { throw ex; }
             catch (CryptographicException ex) { throw ex; }
             catch (ArgumentException ex) { throw ex; }
             catch (Exception ex) { throw ex; }
             finally { m_DESProvider.Clear(); }
 
             return m_strEncrypt;
         }





        /// <summary>
          /// DES 解密(数据加密标准，速度较快，适用于加密大量数据的场合)
          /// </summary>
          /// <param name="DecryptString">待解密的密文</param>
          /// <param name="DecryptKey">解密的密钥</param>
          /// <returns>returns</returns>
          public static string DESDecrypt(string DecryptString, string DecryptKey)
          {
              if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }
  
              if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
  
              if (DecryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }
  
              byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
  
           string m_strDecrypt = "";

             DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
 
             try
             {
                 byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
 
                 MemoryStream m_stream = new MemoryStream();
 
                 CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
 
                 m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
 
                 m_cstream.FlushFinalBlock();
 
                 m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());

                 m_stream.Close(); m_stream.Dispose();
 
                 m_cstream.Close(); m_cstream.Dispose();
             }
             catch (IOException ex) { throw ex; }
             catch (CryptographicException ex) { throw ex; }
             catch (ArgumentException ex) { throw ex; }
             catch (Exception ex) { throw ex; }
             finally { m_DESProvider.Clear(); }
 
             return m_strDecrypt;
         }





    }
}
