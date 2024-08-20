using System;
using System.Threading;

namespace PomodoroTimerApp.Services
{
    public class TimerManager
    {
        private bool paused = false;
        private bool running = true;
        private Timer timer;
        private int workDuration = 1 * 60 * 1000;  // 25 хвилин
        private int breakDuration = 1 * 60 * 1000;  // 5 хвилин
        private int longBreakDuration = 2 * 60 * 1000; // 15 хвилин
        private int pomodoroCount = 0;

        public event Action OnWorkEnded;
        public event Action OnBreakEnded;
        public event Action OnWorkStarted;

        public void Start()
        {
            running = true;
            pomodoroCount++;
            Console.WriteLine("Таймер почався! Сесія: " + pomodoroCount);
            StartTimer(workDuration, OnWorkEnded);
        }

        private void StartTimer(int duration, Action callback)
        {
            int seconds = duration / 1000;
            timer = new Timer(state =>
            {
                if (paused)
                {
                    return;
                }

                if (seconds > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Залишилося часу: {seconds / 60:D2}:{seconds % 60:D2}");
                    seconds--;
                }
                else
                {
                    callback?.Invoke();
                    timer.Dispose();
                }
            }, null, 0, 1000); // Запуск таймера з інтервалом в 1 секунду
        }

        public void Pause()
        {
            paused = !paused;
            Console.WriteLine(paused ? "Таймер на паузі" : "Таймер продовжено");
        }

        public void Restart()
        {
            paused = false;
            pomodoroCount = 0;
            Start();
            Console.WriteLine("Таймер перезапущено");
        }

        public void Stop()
        {
            paused = true;
            running = false;
            timer?.Dispose();
            Console.WriteLine("Таймер зупинено");
        }

        private void StartBreak()
        {
            int breakDurationToUse = (pomodoroCount % 4 == 0) ? longBreakDuration : breakDuration;
            Console.WriteLine("Початок перерви");
            StartTimer(breakDurationToUse, OnBreakEnded);
        }

        public void OnWorkEndedHandler()
        {
            Console.WriteLine("Час на перерву!");
            StartBreak();
        }

        public void OnBreakEndedHandler()
        {
            Console.WriteLine("Перерва закінчилася, повертаємось до роботи!");
            OnWorkStarted?.Invoke();
            Start(); // Після перерви запускається новий цикл роботи
        }
        
        public void EndWork()
        {
            OnWorkEnded?.Invoke();
        }

        public void EndBreak()
        {
            OnBreakEnded?.Invoke();
        }

        public void StartWork()
        {
            OnWorkStarted?.Invoke();
        }
    }
}
