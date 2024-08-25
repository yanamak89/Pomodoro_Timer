using System;
using System.Threading;
using PomodoroTimerApplication;

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

        private CustomSoundPlayer soundPlayer;

        public TimerManager(CustomSoundPlayer soundPlayer)
        {
            this.soundPlayer = soundPlayer;
        }

        public void Start()
        {
            running = true;
            pomodoroCount++;
            Console.WriteLine("Timer started! Session: " + pomodoroCount);
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
                    Console.WriteLine($"Time remaining: {seconds / 60:D2}:{seconds % 60:D2}");
                    seconds--;
                }
                else
                {
                    timer.Dispose();
                    callback?.Invoke();
                }
            }, null, 0, 1000); // Timer interval set to 1 second
        }

        public void Pause()
        {
            paused = !paused;
            Console.WriteLine(paused ? "Timer paused" : "Timer resumed");
        }

        public void Restart()
        {
            paused = false;
            pomodoroCount = 0;
            Start();
            Console.WriteLine("Timer restarted");
        }

        public void Stop()
        {
            paused = true;
            running = false;
            timer?.Dispose();
            Console.WriteLine("Timer stopped");
        }

        private void StartBreak()
        {
            int breakDurationToUse = (pomodoroCount % 4 == 0) ? longBreakDuration : breakDuration;
            Console.WriteLine("Starting break");
            soundPlayer.PlayRandomMeditationMusic(breakDurationToUse / 1000);
            StartTimer(breakDurationToUse, OnBreakEndedHandler);
        }
        public void OnWorkEndedHandler()
        {
            Console.WriteLine("Work session ended! Time for a break.");
            StartBreak();
        }

        public void OnBreakEndedHandler()
        {
            soundPlayer.StopMusic();
            Console.WriteLine("Break ended! Time to get back to work.");
            OnWorkStarted?.Invoke();
            Start(); 
        }

        public void EndBreak()
        {
            Console.WriteLine("Ending break...");
            OnBreakEnded?.Invoke();
        }

        public void EndWork()
        {
            OnWorkEnded?.Invoke();
        }

        public void StartWork()
        {
            OnWorkStarted?.Invoke();
        }
    }
}
