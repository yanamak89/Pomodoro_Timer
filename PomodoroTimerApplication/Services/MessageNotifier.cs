using System.Diagnostics;

namespace PomodoroTimerApplication;

public class MessageNotifier
{
    public void ShowMessage(string message)
    {
        Console.WriteLine("=== Pomodoro Timer ===");
        Console.WriteLine(message);
        Console.WriteLine("======================");
    }
}