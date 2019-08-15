using System;
using System.Device.Gpio;
using System.Threading;
using System.Runtime.InteropServices;
using TestPi.Sensoring;

namespace TestPi.ConsoleApp
{
    class Program
    {
        static ICpuSensors CpuSensors { get; set; } = new CpuSensors();

        static void Main(string[] args)
        {
            var led1 = 4;
            var led2 = 17;
            var led3 = 27;
            var led4 = 5;
            var controller = new GpioController();
            var lightTimeInMilliseconds = 1000;
            var dimTimeInMilliseconds = 200;

            controller.OpenPin(led1, PinMode.Output);
            controller.OpenPin(led2, PinMode.Output);
            controller.OpenPin(led3, PinMode.Output);
            controller.OpenPin(led4, PinMode.Output);

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
                Console.WriteLine($"Light 1 for {lightTimeInMilliseconds}ms");
                controller.Write(led1, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"Dim 1 for {dimTimeInMilliseconds}ms");
                controller.Write(led1, PinValue.Low);
                Thread.Sleep(dimTimeInMilliseconds);
                
                Console.WriteLine($"Light 2 for {lightTimeInMilliseconds}ms");
                controller.Write(led2, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"Dim 2 for {dimTimeInMilliseconds}ms");
                controller.Write(led2, PinValue.Low);
                Thread.Sleep(dimTimeInMilliseconds);
                
                Console.WriteLine($"Light 3 for {lightTimeInMilliseconds}ms");
                controller.Write(led3, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"Dim 3 for {dimTimeInMilliseconds}ms");
                controller.Write(led3, PinValue.Low);
                Thread.Sleep(dimTimeInMilliseconds);
                
                Console.WriteLine($"Light 4 for {lightTimeInMilliseconds}ms");
                controller.Write(led4, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"Dim 4 for {dimTimeInMilliseconds}ms");
                controller.Write(led4, PinValue.Low);
                Thread.Sleep(dimTimeInMilliseconds);

                Console.WriteLine($"The CPU temperature (Celsius) is {CpuSensors.GetCurrentTemperatureInCelsius()}");
            } 
        }
    }
}