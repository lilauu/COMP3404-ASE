using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace COMP3404_Client.SaveLoadManagerScripts
{
    /// <summary>
    /// This class saves and loads data both to and from file and online
    /// </summary>
    public class SaveLoadManager : ISaveLoadManager
    {
        /// <summary>
        /// Saves the data to file in MyDocuments/COMP3404
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        public void SaveDataToFile<T>(T data, string fileName)
        {
            //Create the file path using the MyDocuments folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            path = Path.Combine(path, "COMP3404");

            if (!Path.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, fileName);

            //Write the data into the path
            File.WriteAllText(filePath, JsonSerializer.Serialize(data, options:new() { WriteIndented = true, }));
        }
        /// <summary>
        /// Loads the data with the passed in name from MyDocuments/COMP3404
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public T LoadDataFromFile<T>(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            path = Path.Combine(path, "COMP3404");

            string filePath = Path.Combine(path, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"{filePath}");

            var text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text) ?? throw new Exception("Failed to parse JSON, what the fuck");
        }
        /// <summary>
        /// Deletes any file with a specified name in MyDocuments/COMP3404
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteFileIfExists(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(path, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        /// <summary>
        /// Saves data to the online database
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SaveDataToOnline()
        {
            //Save the data to the online database
            throw new NotImplementedException();
        }
        /// <summary>
        /// Loads data from the online database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T LoadDataFromOnline<T>()
        {
            //Load the data from the online database
            throw new NotImplementedException();
        }




    }
}
