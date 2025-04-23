using System.Speech.Synthesis;

namespace COMP3404_Client
{
    public class TTSSettings
    {
        public TTSSettings(bool pEnabled, int pRate, int pVolume, VoiceGender pGender, VoiceAge pAge)
        {
            Enabled = pEnabled;
            Rate = pRate;
            Volume = pVolume;
            Gender = pGender;
            Age = pAge;
        }


        public bool Enabled;
        public int Rate;
        public int Volume;
        public VoiceGender Gender;
        public VoiceAge Age;

    }
}
