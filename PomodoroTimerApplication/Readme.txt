# Pomodoro Timer Console Application

This Pomodoro Timer is a C# console application designed to help users manage their time effectively using the Pomodoro technique. The application runs in the background and provides sound notifications and message boxes to keep users informed about their work and break intervals. It also allows users to control the timer through keyboard inputs.

## Features

- **Timer:** The application follows the Pomodoro method with 25 minutes of work followed by a 5-minute break. After every 4 Pomodoros, a longer break of 15-30 minutes is suggested.
- **Sound Notification:** Every 30 minutes, a sound notification is played to indicate the end of a Pomodoro session.
- **MessageBox Notification:** After the sound, a MessageBox appears on the screen, requiring the user to click a button with the mouse to acknowledge the notification.
- **Keyboard Controls:** The application tracks keyboard inputs and allows the user to:
  - Pause the timer (`p` key)
  - Restart the timer (`r` key)
  - Stop the timer (`s` key)
  - Exit the application (`q` key)

## Prerequisites

- .NET Core SDK installed on your system.
- The application is designed to run on Windows due to its use of `System.Media.SoundPlayer` and `System.Windows.Forms.MessageBox`.

## Installation

1. **Clone the repository:**

   ```
   git clone https://github.com/your-username/pomodoro-timer.git
   cd pomodoro-timer
   ```
   
2. **Build the application:**

   ```
   dotnet build
   ```
   
3. **Run the application::**

   ```
   dotnet run
   ```
   
   
## Customization

You can customize the timer intervals and the sound file by modifying the respective variables in the Program.cs file.

**Work Duration**: Default is set to 25 minutes.
**Break Duration**: Default is set to 5 minutes.
**Long Break Duration**: Default is set to 15 minutes.
**Sound File**: Update the path in the PlaySound() method to point to a different .wav file if desired.