namespace COMP3404_Client.Tests;

using COMP3404_Client;
using System.Speech.Synthesis;

public class TTSTests
{
    [Fact]
    public void SettingsTestDefaultValuesAreKept()
    {
        //Arrange
        TTSSettings ttsSettings = new TTSSettings(true, 100, 100, System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult);
        TTS tts = new TTS(ttsSettings);
        //Act 

        //Assert
        Assert.True(ttsSettings.Enabled);
        Assert.Equal(100, ttsSettings.Rate);
        Assert.Equal(100, ttsSettings.Volume);
        Assert.Equal(VoiceGender.Male, ttsSettings.Gender);
        Assert.Equal(VoiceAge.Adult, ttsSettings.Age);

    }

    [Fact]
    public void SettingsTestDefaultValuesAreOverwritten()
    {
        //Arrange
        TTSSettings ttsSettings = new TTSSettings(true, 100, 100, System.Speech.Synthesis.VoiceGender.Male, System.Speech.Synthesis.VoiceAge.Adult);
        TTS tts = new TTS(ttsSettings);

        //Act 
        ttsSettings.Enabled = false;
        ttsSettings.Rate = 101;
        ttsSettings.Volume = 99;
        ttsSettings.Gender = VoiceGender.Female;
        ttsSettings.Age = VoiceAge.Child;

        //Assert
        Assert.False(ttsSettings.Enabled);
        Assert.NotEqual(100, ttsSettings.Rate);
        Assert.NotEqual(100, ttsSettings.Volume);
        Assert.NotEqual(VoiceGender.Male, ttsSettings.Gender);
        Assert.NotEqual(VoiceAge.Adult, ttsSettings.Age);

    }
}