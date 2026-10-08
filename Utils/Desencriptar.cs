using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace IA.GymAdmin.HelpDesk.API.Utils
{
    public class Desencriptar
    {
        public async Task<string> DesencriptarPassword(string encryptedPassword)
        {
            return await Task.Run(() => DecryptTripleDES(encryptedPassword, "c"));
        }

        private static string DecryptTripleDES(string encryptedText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptedText))
                    return string.Empty;

                byte[] buffer = Convert.FromBase64String(encryptedText);
                using (var stream = new MemoryStream(buffer))
                using (var tdes = TripleDES.Create())
                {
                    byte[] rgbSalt = new byte[0];
#pragma warning disable SYSLIB0023
                    var pdb = new PasswordDeriveBytes(key ?? "c", rgbSalt);
#pragma warning restore SYSLIB0023
                    byte[] keyBytes = pdb.GetBytes(24);
                    byte[] ivBytes = pdb.GetBytes(8);
                    
                    tdes.Key = keyBytes;
                    tdes.IV = ivBytes;
                    
                    using (var cryptoStream = new CryptoStream(stream, tdes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var streamReader = new StreamReader(cryptoStream, Encoding.Unicode))
                    {
                        string result = streamReader.ReadToEnd();
                        return result.TrimEnd('\0');
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Desencryptar error: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
