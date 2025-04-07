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
        public static TTS instance;

        #region Fields
        SpeechSynthesizer synth;
        ProgressBar progressBar;
        //RichTextBox subtitleTextbox;
        string message;
        #endregion

        #region Constructors
        //Blank constructor
        public TTS()
        {
            if (instance == null)
            {
                instance = this;
            }
            else return;

            //Create a new SpeechSynthesizer
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
        }

        //Constructor taking a progress bar
        public TTS(ProgressBar progressBar)
        {
            //Create a new SpeechSynthesizer
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            //Assign the progressbar
            this.progressBar = progressBar;

            //Assign the progress event of the SpeechSynthesizer
            synth.SpeakProgress += SpeakProgress;
            synth.SpeakCompleted += SpeakComplete;
        }

        //Constructor taking a richtextbox
        /*
        public TTS(RichTextBox subtitleTextbox)
        {
            //Create a new SpeechSynthesizer
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            //Assign the display textbox
            this.subtitleTextbox = subtitleTextbox;

            //Assign the progress event of the SpeechSynthesizer
            synth.SpeakProgress += SpeakProgress;
            synth.SpeakCompleted += SpeakComplete;
        }
        

        //Constructor taking in a progress bar and rich text box
        public TTS(ProgressBar progressBar, RichTextBox subtitleTextbox)
        {
            //Create a new SpeechSynthesizer
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            //Assign the progressbar and display textbox
            this.progressBar = progressBar;
            this.subtitleTextbox = subtitleTextbox;

            //Assign the progress event of the SpeechSynthesizer
            synth.SpeakProgress += SpeakProgress;
            synth.SpeakCompleted += SpeakComplete;
        }
        */
        #endregion

        #region Methods
        //Speaks a string input
        public void Speak(string toSpeak, int rate)
        {
            synth.Rate = rate;
            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(toSpeak);

            //Sets up the progress bar and subtitles
            //progressBar.Maximum = toSpeak.Length;
            //progressBar.Value = 0;

            message = toSpeak;
        }

        //Sets the voice gender property of the SpeechSynthesizer
        public void SetGender(string gender)
        {
            switch (gender)
            {
                default:
                    synth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
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

        public void SetVolume(int volume)
        {
            synth.Volume = volume;
        }

        #endregion

        #region Events

        //Called via the SpeakProgress event in the SpeechSynthesizer
        private void SpeakProgress(Object sender, SpeakProgressEventArgs e)
        {
            /*
            //If the progressBar is not null, increment it
            if(progressBar != null && progressBar.Value < progressBar.Maximum)
            {
                progressBar.Value = e.CharacterPosition +1;
            }
           
            //if the displayTextbox is not null, update it with the new word
            if(subtitleTextbox != null)
            {
                subtitleTextbox.Text = e.Text;
            }
            */
        }

        private void SpeakComplete(object sender, SpeakCompletedEventArgs e)
        {
            /*
            if (progressBar != null)
            {
                progressBar.Value = progressBar.Maximum;
            }

            if (subtitleTextbox != null)
            {
                subtitleTextbox.Text = string.Empty;
            }
            */
        }

        #endregion

    }
