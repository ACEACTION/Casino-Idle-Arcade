using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace AmirSaveLoadSystem
{
    public static class AesEncryptionHelper
    {
        private static readonly byte[] Key = new byte[32]
        {
            0x14, 0x6C, 0x2B, 0x32, 0x7E, 0x50, 0x74, 0x7E,
            0x46, 0x60, 0x6C, 0x31, 0x62, 0x63, 0x7E, 0x23,
            0x74, 0x31, 0x23, 0x3E, 0x79, 0x32, 0x3E, 0x20,
            0x79, 0x71, 0x4F, 0x73, 0x39, 0x2A, 0x5F, 0x5E
        };

        private static readonly byte[] Iv = new byte[16]
        {
            0x0F, 0x39, 0x5B, 0x14, 0x3C, 0x12, 0x4A, 0x24,
            0x6B, 0x17, 0x0F, 0x1C, 0x0A, 0x31, 0x3D, 0x2E
        };

        public static byte[] Encrypt(byte[] plainBytes)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            return PerformCryptography(plainBytes, encryptor);
        }

        public static byte[] Decrypt(byte[] encryptedBytes)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = Iv;
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            return PerformCryptography(encryptedBytes, decryptor);
        }

        private static byte[] PerformCryptography(byte[] bytes, ICryptoTransform cryptoTransform)
        {
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cs.Write(bytes, 0, bytes.Length);
            }

            return ms.ToArray();
        }
    }
    
    public static class SaveLoadSystem
    {
        public static void Save<T>(T data,string name = null , Action<Exception> onError = null , Action onSuccess = null)
        {
            name ??= typeof(T).Name.GetHashCode().ToString();
            var dataPath = Application.persistentDataPath + $"/{name}.bin";
            
            try
            {
                var binary = new BinaryFormatter();
                var stream = new FileStream(dataPath, FileMode.Create);
                binary.Serialize(stream, data);
                stream.Dispose();
                Debug.Log($"Save {name} To : {dataPath}");
                onSuccess?.Invoke();
            }
            catch (Exception error) {
                onError?.Invoke(error);
            }
       
        }
        public static void Load<T>(Action<T> data,string name = null , Action<Exception> onError = null , Action onSuccess = null)
        {
            name ??= typeof(T).Name.GetHashCode().ToString();
            var dataPath = Application.persistentDataPath + $"/{name}.bin";
            
            try
            {
                var binary = new BinaryFormatter();
                var stream = File.OpenRead(dataPath);
                Debug.Log($"Load {name} From : {dataPath}");
                data((T)binary.Deserialize(stream));
                stream.Dispose();
                onSuccess?.Invoke();
            }
            catch (Exception error) {
                Debug.LogException(error);
                onError?.Invoke(error);
            }
        }
        
        public static void SaveAes<T>(T data, string name = null, Action<Exception> onError = null, Action<string> onSuccess = null)
        {
            name ??= typeof(T).Name.GetHashCode().ToString();
            var dataPath = Application.persistentDataPath + $"/{name}.bin";

            try
            {
                var binary = new BinaryFormatter();
                using var ms = new MemoryStream();
                binary.Serialize(ms, data);
                var encryptedBytes = AesEncryptionHelper.Encrypt(ms.ToArray());
                File.WriteAllBytes(dataPath, encryptedBytes);
                onSuccess?.Invoke($"Save {name} To : {dataPath}");
            }
            catch (Exception error)
            {
                Debug.LogException(error);
                onError?.Invoke(error);
            }
        }

        public static void LoadAes<T>(Action<T> data, string name = null, Action<Exception> onError = null, Action<string> onSuccess = null)
        {
            name ??= typeof(T).Name.GetHashCode().ToString();
            var dataPath = Application.persistentDataPath + $"/{name}.bin";

            try
            {
                var encryptedBytes = File.ReadAllBytes(dataPath);
                var decryptedBytes = AesEncryptionHelper.Decrypt(encryptedBytes);
                using (MemoryStream stream = new MemoryStream(decryptedBytes))
                {
                    var binary = new BinaryFormatter();
                    data((T)binary.Deserialize(stream));
                }
                
                onSuccess?.Invoke($"Load {name} From : {dataPath}");
            }
            catch (Exception error)
            {
                Debug.LogException(error);
                onError?.Invoke(error);
            }
        }

        public static void DeleteFile(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("File deleted successfully.");
            }
            else
            {
                Debug.Log("File does not exist.");
            }
        }

    } 
}

