using System;
using System.Runtime.InteropServices;
using TestPi.Sensoring;
using TestPi.Lighting;

namespace TestPi.ConsoleApp
{
    class Program
    {
        static ICpuSensors CpuSensors { get; set; } = new CpuSensors();

        static ILedArrayController LedArrayController { get; set; } = new ExplorerHATLedArrayController();

        static void Main(string[] args)
        {
            Console.WriteLine( "**************************************************************************************");
            Console.WriteLine($"   Framework: {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"          OS: {RuntimeInformation.OSDescription}");
            Console.WriteLine($"     OS Arch: {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"    CPU Arch: {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine( "**************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine( "Now entering IoT loop...");

            var lightTimeInMilliseconds = 1000;
            var dimTimeInMilliseconds = 100;

            while (true)
            {
                LedArrayController.Blink("green", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("red", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("yellow", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("blue", lightTimeInMilliseconds, dimTimeInMilliseconds);

                Console.WriteLine($"The CPU temperature (Celsius) is {CpuSensors.GetCurrentTemperatureInCelsius()}");
            } 
        }
    }
}