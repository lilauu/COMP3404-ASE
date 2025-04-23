﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace COMP3404_Client.SaveLoadManager
{
    public class SaveLoadManager : ISaveLoadManager
    {
        public SaveLoadManager()
        {

        }

        public void SaveDataToFile<T>(T data, string fileName)
        {
            //Create the file path using the MyDocuments folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(path, fileName);

            //Write the data into the path
            File.WriteAllText(filePath, JsonSerializer.Serialize(data));

        }

        public T LoadDataFromFile<T>(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(path, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"{filePath}");

            var text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text) ?? throw new Exception("Failed to parse JSON, what the fuck");
        }
        public void DeleteFileIfExists(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(path, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void SaveDataToOnline()
        {
            //Save the data to the online database
            throw new NotImplementedException();
        }

        public T LoadDataFromOnline<T>()
        {
            //Load the data from the online database
            throw new NotImplementedException();
        }




    }
}
