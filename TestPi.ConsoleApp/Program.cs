using System;
using System.Runtime.InteropServices;
using TestPi.Sensoring;
using TestPi.Lighting;
using System.Threading;
using Serilog;
using Serilog.Formatting.Compact;


namespace TestPi.ConsoleApp
{
    class Program
    {
        static ICpuSensors CpuSensors { get; set; } = new CpuSensors();

        static ILedArrayController LedArrayController { get; set; } = new ExplorerHATLedArrayController();

        static void Main(string[] args)
        {
            // Logging configuration
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

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
                
                LedArrayController.Blink(lightTimeInMilliseconds, dimTimeInMilliseconds, 5);

                LedArrayController.Blink(fastBlinkingTimeInMilliseconds, fastBlinkingTimeInMilliseconds, 5);

                Log.Information("The CPU temperature (Celsius) is {Temperature}", CpuSensors.GetCurrentTemperatureInCelsius());
            } 
        }
    }
}