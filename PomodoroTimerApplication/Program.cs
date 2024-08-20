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

            CustomSoundPlayer soundPlayer = new CustomSoundPlayer();
            MessageNotifier messageNotifier = new MessageNotifier();
            TimerManager timerManager = new TimerManager();
            
            timerManager.OnWorkEnded += () =>
            {
                soundPlayer.PlaySound(workEndSoundPath);
                messageNotifier.ShowMessage("Час на перерву!");

                soundPlayer.PlaySound(relaxVoiceSoundPath);
                System.Threading.Thread.Sleep(3000); // 3 секунди

                Console.WriteLine("Чи хочете ви слухати музику для медитації? (y/n)");
                var response = Console.ReadLine();
                if (response?.ToLower() == "y")
                {
                    soundPlayer.PlayRandomMeditationMusic();
                }

                // Переходимо до перерви, але не викликаємо новий цикл роботи негайно
                System.Threading.Thread.Sleep(10000); // Додаємо невеликий інтервал часу між циклами
                timerManager.EndBreak(); // Викликаємо завершення перерви
            };

            timerManager.OnBreakEnded += () =>
            {
                soundPlayer.PlaySound(workEndSoundPath);
                messageNotifier.ShowMessage("Перерва закінчена!");

                // Починаємо нову сесію роботи
                System.Threading.Thread.Sleep(10000); // Додаємо невеликий інтервал часу перед початком нової сесії
                timerManager.StartWork();
            };

            timerManager.OnWorkStarted += () =>
            {
                soundPlayer.PlaySound(workEndSoundPath);
                messageNotifier.ShowMessage("Час знову працювати! Починається нова сесія.");

                timerManager.Start();
            };

            // Показати інструкції користувачу
            Console.WriteLine("Натисніть 'S', щоб розпочати таймер, 'P' для паузи/продовження, 'R' для перезапуску, 'X' для зупинки, або 'Q' для виходу.");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.WriteLine($"Натиснуто клавішу: {keyInfo.Key}");

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
                        Console.WriteLine("Вихід з програми...");
                        return;
                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}