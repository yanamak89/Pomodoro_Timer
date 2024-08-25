using System.Diagnostics;

namespace PomodoroTimerApplication
{
    public class CustomSoundPlayer
    {
        private readonly string soundFilePath;
        private Process currentMusicProcess;

        public CustomSoundPlayer(string soundFilePath)
        {
            this.soundFilePath = soundFilePath;
        }

        public void PlaySound()
        {
            PlaySoundFile(soundFilePath);
        }

        public void PlaySound(int durationInSeconds = 0)
        {
            PlaySoundFile(soundFilePath, durationInSeconds);
        }

        public void PlayRandomMeditationMusic(int durationInSeconds = 0)
        {
            string musicDirectory = "/Users/yanamakogon/RiderProjects/PomodoroTimerApplication/PomodoroTimerApplication/Resources/Meditation";

            if (!Directory.Exists(musicDirectory))
            {
                Console.WriteLine($"Directory not found: {musicDirectory}");
                return;
            }

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
            PlaySoundFile(randomMusicFile, durationInSeconds);
        }


        private void PlaySoundFile(string filePath, int durationInSeconds = 0)
        {
            // Properly escape and quote the file path
            string escapedFilePath = "\"" + filePath.Replace("\"", "\\\"") + "\"";
            Console.WriteLine($"Attempting to play sound file: {escapedFilePath} for {durationInSeconds} seconds");
    
            var process = new ProcessStartInfo
            {
                FileName = "/usr/bin/afplay",
                Arguments = durationInSeconds > 0 ? $"-t {durationInSeconds} {escapedFilePath}" : escapedFilePath,
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            try
            {
                currentMusicProcess = Process.Start(process);
                if (currentMusicProcess == null)
                {
                    throw new InvalidOperationException("Failed to start the sound process.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting sound process: {ex.Message}");
                currentMusicProcess = null;
            }
        }

        public void StopMusic()
        {
            if (currentMusicProcess != null && !currentMusicProcess.HasExited)
            {
                currentMusicProcess.Kill();
                currentMusicProcess = null;
            }
        }
    }
}
