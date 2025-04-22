using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client
{
    internal interface IChatPersistence
    {
        void SaveChat();
        void LoadChat();
    }
}
