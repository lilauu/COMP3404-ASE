using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace COMP3404_Client
{
    internal class SaveLoadManager
    {
        public SaveLoadManager()
        {

        }

        public void SaveData(List<string> data)
        {
            //Create a new List and convert input data into JSON data
            List<String> JSONData = new List<String>();
            foreach (var item in data)
            {
                JSONData.Add(JsonSerializer.Serialize(item)+ "\n");
            }

            //Create the file path using the MyDocuments folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(path, "Data.txt");

            //Write the data into the path
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var item in JSONData)
                {
                    sw.Write(item);
                }

            }
        }

        public List<String> LoadData()
        {
            List<String> data = new List<String>();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(path, "Data.txt");

            if (File.Exists(filePath))
            {
                using(StreamReader sr = new StreamReader(filePath))
                {
                    while(sr.ReadLine() != null)
                    {
                        data.Add(sr.ReadLine());
                    }
                }
            }
            else return null;

            return data;
        }
    }
}
