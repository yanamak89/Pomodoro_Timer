using System.Diagnostics;

namespace PomodoroTimerApplication;

public class CustomSoundPlayer
{
    private readonly string soundFilePath;

    // public CustomSoundPlayer(string soundFilePath)
    // {
    //     this.soundFilePath = soundFilePath;
    // }

    //public void PlaySound()
    public void PlaySound(string soundFilePath)
    {
        var process = new ProcessStartInfo
        {
            FileName = "/usr/bin/afplay",
            Arguments = soundFilePath,
            RedirectStandardOutput = false,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        Process.Start(process);
    }

    public void PlayRandomMeditationMusic()
    {
        var random = new Random();
        string[] meditationTracks =
        {
            "/Users/yanamakogon/RiderProjects/PomodoroTimerApplication/PomodoroTimerApplication/Resources/meditation_music2wav.mp3",
            "/Users/yanamakogon/RiderProjects/PomodoroTimerApplication/PomodoroTimerApplication/Resources/meditation_music3wav.mp3",
            "/Users/yanamakogon/RiderProjects/PomodoroTimerApplication/PomodoroTimerApplication/Resources/meditation_music4wav.mp3",
        };
        string selectedTrack = meditationTracks[random.Next(meditationTracks.Length)];
        PlaySound(selectedTrack);
    }
}