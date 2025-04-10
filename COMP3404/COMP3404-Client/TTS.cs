using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace COMP3404_Client;

/// <summary>
/// This class reads a string input out to the user
/// </summary>

    internal class TTS
    {
    //Should not be using the singleton pattern probably
    public static TTS instance;

        //Composed of TTSSettings
        #region Fields
        TTSSettings settings;
        SpeechSynthesizer synth;
        #endregion

        #region Constructors
        //Blank constructor
        public TTS(TTSSettings pSettings)
        {
            if (instance == null)
            {
                instance = this;
            }
            else return;

            //Create a new SpeechSynthesizer
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            settings = pSettings;
        }
        #endregion

        #region Methods
        //Speaks a string input
        public void Speak(string toSpeak)
        {
            if (settings.Enabled)
            {
                synth.Rate = settings.Rate;
                synth.Volume = settings.Volume;
                synth.SelectVoiceByHints(settings.Gender, settings.Age);
                synth.SpeakAsyncCancelAll();
                synth.SpeakAsync(toSpeak);
            }
    }

        //Sets the voice gender property of the SpeechSynthesizer
        public void SetGender(string gender)
        {
            switch (gender)
            {
                default:
                   
                    break;

                case ("Male"):
                    synth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
                    break;

                case ("Female"):
                    synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
                    break;

                case ("Neutral"):
                    synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult);
                    break;
            }
        }

        //Pauses the SpeechSynthesizer
        public void Pause()
        {
            synth.Pause();
        }

        //Resumes the SpeechSynthesizer
        public void Resume()
        {
            synth.Resume();
        }

        #endregion
    }
