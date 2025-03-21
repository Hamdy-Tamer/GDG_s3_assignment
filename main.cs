// See https://aka.ms/new-console-template for more information

using System;
using System.IO;

namespace SmartHomeSystem
{
    // Enum to define device commands
    public enum DeviceCommand
    {
        TurnOn,
        TurnOff,
        SetTemperature,
        ActivateAlarm
    }

    // Define delegate
    public delegate void CommandHandler(DeviceCommand command);

    public class SmartHomeController
    {
        // Declare event
        public event CommandHandler OnCommandReceived;

        public void ExecuteCommand(DeviceCommand command)
        {
            Console.WriteLine($"\n[Controller] Executing command: {command}");
            LogEvent($"Command executed: {command}");

            // Invoke the event if it has subscribers
            OnCommandReceived?.Invoke(command);
        }

        private void LogEvent(string message)
        {
            File.AppendAllText("SmartHomeLog.txt", message + "\n");
        }
    }

    public class Light
    {
        public void HandleCommand(DeviceCommand command)
        {
            if (command == DeviceCommand.TurnOn)
                Console.WriteLine("Light turned ON.");
            else if (command == DeviceCommand.TurnOff)
                Console.WriteLine("Light turned OFF.");
        }
    }

    public class AC
    {
        public void HandleCommand(DeviceCommand command)
        {
            if (command == DeviceCommand.SetTemperature)
                Console.WriteLine("AC temperature set.");
            else if (command == DeviceCommand.TurnOff)
                Console.WriteLine("AC turned OFF.");
        }
    }

    public class Alarm
    {
        public void HandleCommand(DeviceCommand command)
        {
            if (command == DeviceCommand.ActivateAlarm)
                Console.WriteLine("Alarm ACTIVATED!");
            else if (command == DeviceCommand.TurnOff)
                Console.WriteLine("Alarm turned OFF.");
        }
    }

    // Bonus: Fan class
    public class Fan
    {
        public void HandleCommand(DeviceCommand command)
        {
            if (command == DeviceCommand.TurnOn)
                Console.WriteLine(" Fan turned ON.");
            else if (command == DeviceCommand.TurnOff)
                Console.WriteLine(" Fan turned OFF.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create controller and devices
            SmartHomeController controller = new SmartHomeController();
            Light light = new Light();
            AC ac = new AC();
            Alarm alarm = new Alarm();
            Fan fan = new Fan();

            // Subscribe devices to controller event
            controller.OnCommandReceived += light.HandleCommand;
            controller.OnCommandReceived += ac.HandleCommand;
            controller.OnCommandReceived += alarm.HandleCommand;
            controller.OnCommandReceived += fan.HandleCommand;

            // Simulate commands
            controller.ExecuteCommand(DeviceCommand.TurnOn);
            controller.ExecuteCommand(DeviceCommand.SetTemperature);
            controller.ExecuteCommand(DeviceCommand.ActivateAlarm);
            controller.ExecuteCommand(DeviceCommand.TurnOff);
        }
    }
}
