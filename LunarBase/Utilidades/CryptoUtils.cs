using System.Security.Cryptography;

namespace LunarBase.Utilidades
{
    public static class CryptoUtils
    {
        // Método para gerar uma chave e um IV de forma segura
        public static (byte[], byte[]) GenerateKeyAndIV()
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256; // Tamanho da chave de 256 bits para segurança máxima
                aes.GenerateKey();
                aes.GenerateIV();
                return (aes.Key, aes.IV);
            }
        }

        // Método para criptografar uma data
        public static byte[] EncryptDateString(string dateString, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(dateString); // Escreve a data como string
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        // Método para descriptografar uma data
        public static string DecryptDateString(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd(); // Lê a data como string
                        }
                    }
                }
            }
        }
    }
}
