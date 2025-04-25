using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client.SaveLoadManagerScripts
{
    public interface ISaveLoadManager
    {
        void SaveDataToFile<T>(T data, string fileName);
        T LoadDataFromFile<T>(string fileName);

        void SaveDataToOnline<T>(T data);

        T LoadDataFromOnline<T>();
        void DeleteFileIfExists(string fileName);
    }
}
