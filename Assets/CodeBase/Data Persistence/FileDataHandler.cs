using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

namespace CodeBase.DataPersistence
{
    public class FileDataHandler
    {
        private readonly string _fullPath = "";
        private readonly bool _useEncryption = false;
        private const string _encryptionCodeWord = "myLittleCrocodile";

        public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
        {
            _fullPath = Path.Combine(dataDirPath, dataFileName);
            _useEncryption = useEncryption;
        }

        public T Load<T>() where T : class
        {
            T loadedData = null;

            if (File.Exists(_fullPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new(_fullPath, FileMode.Open))
                    {
                        using StreamReader reader = new(stream);
                        dataToLoad = reader.ReadToEnd();
                    }

                    if (_useEncryption)
                        dataToLoad = EncryptDecrypt(dataToLoad);

                    loadedData = JsonConvert.DeserializeObject<T>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occured when trying to load data from file: " + _fullPath + "\n" + e);
                }
            }
            return loadedData;
        }

        public void Save<T>(T data) where T : class
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

                string dataToStore = JsonConvert.SerializeObject(data);

                if (_useEncryption)
                    dataToStore = EncryptDecrypt(dataToStore);

                using FileStream stream = new(_fullPath, FileMode.Create);
                using StreamWriter writer = new(stream);
                writer.Write(dataToStore);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to save data to file: " + _fullPath + "\n" + e);
            }
        }

        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";

            for (int i = 0; i < data.Length; i++)
                modifiedData += (char)(data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);

            return modifiedData;
        }
    }
}