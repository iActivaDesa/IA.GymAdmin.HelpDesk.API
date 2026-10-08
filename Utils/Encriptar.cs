using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace IA.GymAdmin.HelpDesk.API.Utils
{
    public class Encriptar
    {
        public async Task<string> EncriptarPassword(string password)
        {
            return await Task.Run(() => EncryptTripleDES(password, "c"));
        }

        private static string EncryptTripleDES(string plainText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(plainText))
                    return string.Empty;

                var uEncode = new UnicodeEncoding();
                byte[] plainBytes = uEncode.GetBytes(plainText);
                
                using (var memoryStream = new MemoryStream())
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
                    
                    using (var cryptoStream = new CryptoStream(memoryStream, tdes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Encryptar error: {ex.Message}");
                return plainText;
            }
        }
    }
}
