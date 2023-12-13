using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace LethalCompanySaveModifier
{
    internal class CryptoLogic
    {
        public static string LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
        public static string GameSavePath = LocalLowPath + "\\ZeekerssRBLX\\Lethal Company\\";
        public static string GameSaveFile = GameSavePath;
        public static string OutputFile = "output.json";
        private static string password = "lcslime14a5";


        public static string Decrypt()
        {
            if (GameSavePath != GameSaveFile)
            {
                byte[] data = File.ReadAllBytes(GameSaveFile);

                byte[] IV = data.Take(16).ToArray();
                byte[] dataToDecrypt = data.Skip(16).ToArray();
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);


                using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, IV, 100, HashAlgorithmName.SHA1))
                using (Aes decAlg = Aes.Create())
                {
                    decAlg.Mode = CipherMode.CBC;
                    decAlg.Padding = PaddingMode.PKCS7;
                    decAlg.Key = key.GetBytes(16);
                    decAlg.IV = IV;

                    using (MemoryStream decryptionStreamBacking = new MemoryStream())
                    using (CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        decrypt.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                        decrypt.FlushFinalBlock();

                        return new UTF8Encoding(true).GetString(decryptionStreamBacking.ToArray());
                    }
                }
            }
            else
            {
                MessageBox.Show("No Save Selected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static byte[] Encrypt(string password, string data)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.KeySize = 128;

                aesAlg.GenerateIV();

                using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, aesAlg.IV, 100, HashAlgorithmName.SHA1))
                {
                    aesAlg.Key = key.GetBytes(16);

                    using (MemoryStream encryptionStream = new MemoryStream())
                    {
                        using (CryptoStream crypto = new CryptoStream(encryptionStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                            crypto.Write(dataBytes, 0, dataBytes.Length);
                        }

                        byte[] iv = aesAlg.IV;
                        byte[] encryptedData = encryptionStream.ToArray();

                        byte[] ivAndEncryptedData = new byte[iv.Length + encryptedData.Length];
                        Array.Copy(iv, 0, ivAndEncryptedData, 0, iv.Length);
                        Array.Copy(encryptedData, 0, ivAndEncryptedData, iv.Length, encryptedData.Length);

                        return ivAndEncryptedData;

                    }
                }
            }
        }

        public static void Modify()
        {
            var jsonData = File.ReadAllText(OutputFile);
            if (!(jsonData == null))
            {


                dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);
                if (!(jsonObject == null))
                {
                    foreach (var property in jsonObject)
                    {
                        if (property.Value.__type != null)
                        {
                            string type = property.Value.__type.ToString();
                            switch (type)
                            {
                                case "System.Int32[],mscorlib":
                                    break;

                                case "UnityEngine.Vector3[],UnityEngine.CoreModule":
                                    break;

                                case "int":
                                    break;

                                case "bool":
                                    break;
                            }
                        }
                    }
                }


                string modifiedJsonData = JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(OutputFile, modifiedJsonData);
            }
        }
    }
}
