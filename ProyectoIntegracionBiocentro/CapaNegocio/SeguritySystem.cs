﻿using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CapaNegocio
{
    public class SeguritySystem
    {
        public SeguritySystem()
        {

        }
        public string seguridad(String clave,bool encriptar)
        {
            try
            {
                if (encriptar)
                {
                    return encryptStringToBytes_Aes(clave);
                }
                else
                {
                    return decryptStringFromBytes_Aes(clave);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("***Se produjo un error en el metodo encriptar =>" + ex.Message +"//"+ ex.InnerException.Message);
            }
            return null;
        }
        private string encryptStringToBytes_Aes(string plainText)
        {
            string EncryptionKey = "abc123";
            string encriptada = string.Empty;
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encriptada = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encriptada;

        }

        private string decryptStringFromBytes_Aes(string cipherText)
        {
            string EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("***Se produjo un error en el metodo decryptStringFromBytes_Aes =>" + ex.Message +" /// "+ ex.InnerException.Message);
            }
            return cipherText;
        }
    }
}
