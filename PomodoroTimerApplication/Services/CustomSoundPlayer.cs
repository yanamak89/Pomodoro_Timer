using System.Diagnostics;

namespace PomodoroTimerApplication;

public class CustomSoundPlayer
{
    private readonly string soundFilePath;

    public CustomSoundPlayer(string soundFilePath)
    {
        this.soundFilePath = soundFilePath;
    }

    public void PlaySound()
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
        string musicDirectory = "/Users/yanamakogon/RiderProjects/PomodoroTimerApplication/PomodoroTimerApplication/Resources/Meditation";

        string[] musicFiles = Directory.GetFiles(musicDirectory, "*.mp3");
        if (musicFiles.Length == 0)
        {
            Console.WriteLine("No meditation music files found in the directory.");
            return;
        }

        Random random = new Random();
        int randomIndex = random.Next(musicFiles.Length);
        string randomMusicFile = musicFiles[randomIndex];

        Console.WriteLine($"Playing meditation music: {Path.GetFileName(randomMusicFile)}");

        PlaySoundFile(randomMusicFile);
    }

    private void PlaySoundFile(string filePath)
    {
        var process = new ProcessStartInfo
        {
            FileName = "/usr/bin/afplay",
            Arguments = filePath,
            RedirectStandardOutput = false,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        Process.Start(process);
    }
}