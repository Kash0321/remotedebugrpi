using System;
using System.Runtime.InteropServices;
using TestPi.Sensoring;
using TestPi.Lighting;

namespace TestPi.ConsoleApp
{
    class Program
    {
        static ICpuSensors CpuSensors { get; set; } = new CpuSensors();

        static ILedArrayController LedArrayController { get; set; } = new ExplorerHatLedArrayController();

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
                LedArrayController.Blink("Green", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("Red", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("Yellow", lightTimeInMilliseconds, dimTimeInMilliseconds);
                LedArrayController.Blink("Blue", lightTimeInMilliseconds, dimTimeInMilliseconds);

                Console.WriteLine($"The CPU temperature (Celsius) is {CpuSensors.GetCurrentTemperatureInCelsius()}");
            } 
        }
    }
}