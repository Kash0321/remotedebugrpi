using System;
using System.Device.Gpio;
using System.Threading;
using Iot.Device.CpuTemperature;
using System.Runtime.InteropServices;

namespace TestPi.ConsoleApp
{
    class Program
    {
        static CpuTemperature CPUTemperatureSensor { get; set; } = new CpuTemperature();

        static void Main(string[] args)
        {
            var pin = 17;
            var controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);

            var lightTimeInMilliseconds = 1000;
            var dimTimeInMilliseconds = 200;

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
                Console.WriteLine($"Light for {lightTimeInMilliseconds}ms");
                controller.Write(pin, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"Dim for {dimTimeInMilliseconds}ms");
                controller.Write(pin, PinValue.Low);
                Thread.Sleep(dimTimeInMilliseconds);
                if (CPUTemperatureSensor.IsAvailable)
                {
                    Console.WriteLine($"The CPU temperature is {CPUTemperatureSensor.Temperature.Celsius}");
                    //Thread.Sleep(5000);
                }
            } 
        }
    }
}