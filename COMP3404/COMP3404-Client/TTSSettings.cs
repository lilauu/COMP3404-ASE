using System.Speech.Synthesis;

namespace COMP3404_Client
{
    internal class TTSSettings
    {
        public bool Enabled { get => enabled; }
        public int Rate { get => rate; }
        public int Volume { get => volume; }
        public VoiceGender Gender { get => gender; }
        public VoiceAge Age { get => age; }

        bool enabled;
        int rate;
        int volume;
        VoiceGender gender;
        VoiceAge age;


        public TTSSettings(bool pEnabled, int pRate, int pVolume, VoiceGender pGender, VoiceAge pAge)
        {
            enabled = pEnabled;
            rate = pRate;
            volume = pVolume;
            gender = pGender;
            age = pAge;
        }

        void UpdateSettings(bool pEnabled, int pRate, int pVolume, VoiceGender pGender, VoiceAge pAge)
        {
            enabled = pEnabled;
            rate = pRate;
            volume = pVolume;
            gender = pGender;
            age = pAge;
        }
    }
}