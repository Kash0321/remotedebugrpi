using System;
using System.Runtime.InteropServices;
using TestPi.Sensoring;
using TestPi.Lighting;
using System.Threading;

namespace TestPi.ConsoleApp
{
    class Program
    {
        static ICpuSensors CpuSensors { get; set; } = new CpuSensors();

        static ILedArrayController LedArrayController { get; set; } = new ExplorerHATLedArrayController();

        static void Main(string[] args)
        {
            // Time variables
            var lightTimeInMilliseconds = 1000;
            var dimTimeInMilliseconds = 100;
            var fastBlinkingTimeInMilliseconds = 100;
            var fastBlinkingCount = 5;
            var progressSwitchOnOffTimeInMilliseconds = 1000;

            // Priting OS INFO
            Console.WriteLine( "**************************************************************************************");
            Console.WriteLine($"   Framework: {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"          OS: {RuntimeInformation.OSDescription}");
            Console.WriteLine($"     OS Arch: {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"    CPU Arch: {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine( "**************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine( "Now entering IoT loop...");

            while (true)
            {
                LedArrayController.Blink("blue", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("yellow", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("red", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("green", lightTimeInMilliseconds, dimTimeInMilliseconds);
                
                LedArrayController.Blink(1, fastBlinkingTimeInMilliseconds, fastBlinkingTimeInMilliseconds, fastBlinkingCount);
                LedArrayController.Blink(2, fastBlinkingTimeInMilliseconds, fastBlinkingTimeInMilliseconds, fastBlinkingCount);
                LedArrayController.Blink(3, fastBlinkingTimeInMilliseconds, fastBlinkingTimeInMilliseconds, fastBlinkingCount);
                LedArrayController.Blink(4, fastBlinkingTimeInMilliseconds, fastBlinkingTimeInMilliseconds, fastBlinkingCount);

                LedArrayController.SwitchOn(1);
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);
                LedArrayController.SwitchOn(2);
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);
                LedArrayController.SwitchOn(3);
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);
                LedArrayController.SwitchOn(4);
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);

                LedArrayController.SwitchOff("green");
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);
                LedArrayController.SwitchOff("red");
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);
                LedArrayController.SwitchOff("yellow");
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);
                LedArrayController.SwitchOff("blue");
                Thread.Sleep(progressSwitchOnOffTimeInMilliseconds);
                
                for (int i = 0; i < 5; i++)
                {
                    LedArrayController.SwitchOn(1);
                    LedArrayController.SwitchOn(2);
                    LedArrayController.SwitchOn(3);
                    LedArrayController.SwitchOn(4);
                    Thread.Sleep(lightTimeInMilliseconds);

                    LedArrayController.SwitchOff(1);
                    LedArrayController.SwitchOff(2);
                    LedArrayController.SwitchOff(3);
                    LedArrayController.SwitchOff(4);
                    Thread.Sleep(dimTimeInMilliseconds);
                }

                Console.WriteLine($"The CPU temperature (Celsius) is {CpuSensors.GetCurrentTemperatureInCelsius()}");
            } 
        }
    }
}