using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LethalCompanySaveModifier
{
    public partial class Form1 : Form
    {

        private const string password = "lcslime14a5";
        private static string LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
        private static string GameSavePath = LocalLowPath + "\\ZeekerssRBLX\\Lethal Company\\";
        private static string TestFile = GameSavePath + "LCSaveFile1";
        private static string OutputFile = "output.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            string decryptedData = Decrypt(password, File.ReadAllBytes(TestFile));
            File.WriteAllText(OutputFile, decryptedData);
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            byte[] encryptedData = Encrypt(password, File.ReadAllText(OutputFile));
            File.WriteAllBytes(TestFile, encryptedData);
        }

        private string Decrypt(string password, byte[] data)
        {
            byte[] IV = data.Take(16).ToArray();
            byte[] dataToDecrypt = data.Skip(16).ToArray();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            
            using(Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, IV, 100, HashAlgorithmName.SHA1))
            using(Aes decAlg = Aes.Create())
            {
                decAlg.Mode = CipherMode.CBC;
                decAlg.Padding = PaddingMode.PKCS7;
                decAlg.Key = key.GetBytes(16);
                decAlg.IV = IV;

                using(MemoryStream decryptionStreamBacking = new MemoryStream())
                using(CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    decrypt.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    decrypt.FlushFinalBlock(); 

                    return new UTF8Encoding(true).GetString(decryptionStreamBacking.ToArray());
                }
            }


        }
    
        private byte[] Encrypt(string password, string data)
        {
            using(Aes aesAlg = Aes.Create())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.KeySize = 128;

                aesAlg.GenerateIV();

                using(Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, aesAlg.IV, 100, HashAlgorithmName.SHA1))
                {
                    aesAlg.Key = key.GetBytes(16);

                    using(MemoryStream encryptionStream = new MemoryStream())
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

        private void Modify()
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
