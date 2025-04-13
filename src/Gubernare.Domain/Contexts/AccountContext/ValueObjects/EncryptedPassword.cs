using System.Security.Cryptography;
using System.Text;
using Gubernare.Domain.Contexts.SharedContext.ValueObjects;

namespace Gubernare.Domain.Contexts.AccountContext.ValueObjects
{
    public class EncryptedPassword : ValueObject
    {
        public string Cipher { get; private set; } = string.Empty;

        protected EncryptedPassword() { }

        public EncryptedPassword(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Senha não pode ser vazia.", nameof(plainText));

            Cipher = Encrypt(plainText);
        }

        public string GetPlainText()
        {
            return Decrypt(Cipher);
        }

        //TODO: COLOCAR PARA GERAR MAIS TARDE AUTOMATICAMENTE E ARMAZENAR NO BANCO PARA CADA PASS, SERIA OVER ME PREUCUPAR
        // COM ISSO AGORA.
        private static readonly byte[] Key = HexToBytes(Configuration.Secrets.PasswordSaltKey);
        private static readonly byte[] Iv  = HexToBytes("0102030405060708090A0B0C0D0E0F10"); // 16 bytes em hex

        private static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;
            aes.Mode = CipherMode.CBC;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs, Encoding.UTF8))
            {
                sw.Write(plainText);
            }

            var encryptedBytes = ms.ToArray();
            var ivBase64       = Convert.ToBase64String(aes.IV);
            var cipherBase64   = Convert.ToBase64String(encryptedBytes);
            return $"{ivBase64}:{cipherBase64}";
        }

        public static string Decrypt(string cipher)
        {
            if (string.IsNullOrEmpty(cipher))
                return string.Empty;

            var parts = cipher.Split(':');
            if (parts.Length != 2)
                throw new FormatException("Formato inválido do texto criptografado.");

            var ivBytes  = Convert.FromBase64String(parts[0]);
            var encBytes = Convert.FromBase64String(parts[1]);

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV  = ivBytes;
            aes.Mode = CipherMode.CBC;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(encBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs, Encoding.UTF8);

            return sr.ReadToEnd();
        }

        // Converte de hexadecimal (64 dígitos) para bytes (32 bytes, AES-256).
        private static byte[] HexToBytes(string hex)
        {
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Tamanho de string hex inválido.");

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
    }
}
