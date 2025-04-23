using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client
{
    internal interface ISaveLoadManager
    {
        void SaveDataToFile<T>(T data, string fileName);
        T LoadDataFromFile<T>(string fileName);

        void SaveDataToOnline();

        T LoadDataFromOnline<T>();
        void DeleteFileIfExists(string fileName);
    }
}
