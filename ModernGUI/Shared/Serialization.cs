using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModernGUI.Shared
{
    public static class Serialization
    {
        public static string SerializationKey = "mvqtVXJxTaXkMfot";

        public static byte[] Object_ToByteArray(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return EncryptByteArray(ms.ToArray());
            }
        }

        public static object ByteArrayTo_Object(byte[] arrBytes)
        {
            arrBytes = DecryptByteArray(arrBytes);
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                object obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        #region EncryptionMethods for Serialization
        private static byte[] EncryptByteArray(byte[] bytesToEncrypt)
        {
            byte[] ivSeed = Guid.NewGuid().ToByteArray();

            var rfc = new Rfc2898DeriveBytes(SerializationKey, ivSeed);
            byte[] Key = rfc.GetBytes(16);
            byte[] IV = rfc.GetBytes(16);

            byte[] encrypted;
            using (MemoryStream mstream = new MemoryStream())
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mstream, aesProvider.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    }
                }
                encrypted = mstream.ToArray();
            }

            var messageLengthAs32Bits = Convert.ToInt32(bytesToEncrypt.Length);
            var messageLength = BitConverter.GetBytes(messageLengthAs32Bits);

            encrypted = encrypted.Prepend(ivSeed);
            encrypted = encrypted.Prepend(messageLength);

            return encrypted;
        }

        private static byte[] DecryptByteArray(byte[] bytesToDecrypt)
        {
            (byte[] messageLengthAs32Bits, byte[] bytesWithIv) = bytesToDecrypt.Shift(4); // get the message length
            (byte[] ivSeed, byte[] encrypted) = bytesWithIv.Shift(16);                    // get the initialization vector

            var length = BitConverter.ToInt32(messageLengthAs32Bits, 0);

            var rfc = new Rfc2898DeriveBytes(SerializationKey, ivSeed);
            byte[] Key = rfc.GetBytes(16);
            byte[] IV = rfc.GetBytes(16);

            byte[] decrypted;
            using (MemoryStream mStream = new MemoryStream(encrypted))
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    aesProvider.Padding = PaddingMode.None;
                    using (CryptoStream cryptoStream = new CryptoStream(mStream, aesProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        cryptoStream.Read(encrypted, 0, length);
                    }
                }
                decrypted = mStream.ToArray().Take(length).ToArray();
            }
            return decrypted;
        }

        #region Extension Methods
        private static byte[] Prepend(this byte[] bytes, byte[] bytesToPrepend)
        {
            var tmp = new byte[bytes.Length + bytesToPrepend.Length];
            bytesToPrepend.CopyTo(tmp, 0);
            bytes.CopyTo(tmp, bytesToPrepend.Length);
            return tmp;
        }

        private static (byte[] left, byte[] right) Shift(this byte[] bytes, int size)
        {
            var left = new byte[size];
            var right = new byte[bytes.Length - size];

            Array.Copy(bytes, 0, left, 0, left.Length);
            Array.Copy(bytes, left.Length, right, 0, right.Length);

            return (left, right);
        }
        #endregion
        #endregion
    }
}
