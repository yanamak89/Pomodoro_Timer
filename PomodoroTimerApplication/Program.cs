using PomodoroTimerApp.Services;
using PomodoroTimerApplication;

namespace PomodoroTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            string workEndSoundPath = "/Users/yanamakogon/RiderProjects/PomodoroTimerApplication/PomodoroTimerApplication/Resources/work_end.wav";
            string relaxVoiceSoundPath = "/Users/yanamakogon/RiderProjects/PomodoroTimerApplication/PomodoroTimerApplication/Resources/relax_voice.wav";
            
            CustomSoundPlayer soundPlayer = new CustomSoundPlayer(workEndSoundPath);
            CustomSoundPlayer relaxSoundPlayer = new CustomSoundPlayer(relaxVoiceSoundPath);
            MessageNotifier messageNotifier = new MessageNotifier();
            TimerManager timerManager = new TimerManager(soundPlayer);
            
            timerManager.OnWorkEnded += () =>
            {
               // soundPlayer = new CustomSoundPlayer(workEndSoundPath);
                soundPlayer.PlaySound();
                messageNotifier.ShowMessage("Time for a break!");
                relaxSoundPlayer.PlaySound();
                
                // If we want to play a relaxation voice after the work ends:
                // soundPlayer = new CustomSoundPlayer(relaxVoiceSoundPath);
                // soundPlayer.PlaySound();
                
                System.Threading.Thread.Sleep(3000);

                Console.WriteLine("Do you want to listen to meditation music? (y/n)");

                var responce = Console.ReadLine();
                if (responce?.ToLower() == "y")
                {
                    soundPlayer.PlayRandomMeditationMusic();
                }
                
                System.Threading.Thread.Sleep(10000); 
                timerManager.EndBreak(); 
            };

            timerManager.OnBreakEnded += () =>
            {
                soundPlayer.StopMusic();  // Stop meditation music when the break ends
                soundPlayer.PlaySound();
                messageNotifier.ShowMessage("Break is over!");

                System.Threading.Thread.Sleep(10000); 
                timerManager.StartWork();
            };

            timerManager.OnWorkStarted += () =>
            {
                soundPlayer.PlaySound();
                messageNotifier.ShowMessage("Time to work again! A new session is starting.");
                timerManager.Start();
            };

            Console.WriteLine("Press: " +
                              "\n'S' to start the timer, " +
                              "\n'P' to pause/resume, " +
                              "\n'R' to restart, " +
                              "\n'X' to stop, " +
                              "\n'Q' to quit.");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.WriteLine($"Key pressed: {keyInfo.Key}");

                switch (keyInfo.Key)
                {
                    case ConsoleKey.S: 
                        timerManager.Start();
                        break;
                    case ConsoleKey.P:
                        timerManager.Pause();
                        break;
                    case ConsoleKey.R:
                        timerManager.Restart();
                        break;
                    case ConsoleKey.X:
                        timerManager.Stop();
                        break;
                    case ConsoleKey.Q:
                        timerManager.Stop();
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }
    }
}