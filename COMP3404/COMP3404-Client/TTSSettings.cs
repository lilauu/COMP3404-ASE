using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3404_Client
{
    internal class TTSSettings
    {
        public bool Enabled { get => enabled; }
        public int Rate { get => rate; }

        bool enabled;
        int rate;

        public TTSSettings(bool pEnabled, int pRate)
        {
            enabled = pEnabled;
            rate = pRate;
        }
    }
}
