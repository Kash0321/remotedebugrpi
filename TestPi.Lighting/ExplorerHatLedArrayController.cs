using System;
using System.Device.Gpio;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace TestPi.Lighting
{
    /// <summary>
    /// Implementa <see cref="ILedArrayController" /> para el ExplorerHat de Pimoroni
    /// </summary>
    public class ExplorerHatLedArrayController : ILedArrayController
    {
        List<Led> LedArrayList { get; set; } = new List<Led>();
        GpioController GPIOController { get; set; } = new GpioController();

        public IReadOnlyCollection<Led> LedArray 
        {
            get
            {
                return new ReadOnlyCollection<Led>(LedArrayList);
            }
        }

        public void Blink(string ledName, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1)
        {
            var led = LedArrayList.Where(l => l.Name == ledName).SingleOrDefault();

            Blink(led, lightTimeInMilliseconds, dimTimeInMilliseconds, steps);
        }

        public void Blink(int ledNumber, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1)
        {
            var led = LedArrayList.Where(l => l.Number == ledNumber).SingleOrDefault();

            Blink(led, lightTimeInMilliseconds, dimTimeInMilliseconds, steps);
        }

        public void Blink(Led led, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps)
        {            
            if (!GPIOController.IsPinOpen(led.Pin) || GPIOController.GetPinMode(led.Pin) != PinMode.Output)
            {
                GPIOController.OpenPin(led.Pin, PinMode.Output);
            }

            for (int i = 0; i < steps; i++)
            {
                Console.WriteLine($"Light {led.Name} for {lightTimeInMilliseconds}ms");
                GPIOController.Write(led.Pin, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"Dim {led.Name} for {dimTimeInMilliseconds}ms");
                GPIOController.Write(led.Pin, PinValue.Low);
                Thread.Sleep(dimTimeInMilliseconds);
            }
        }

        public ExplorerHatLedArrayController()
        { 
            LedArrayList = new List<Led>() 
            { 
                new Led(1, "Blue", 4), 
                new Led(1, "Yellow", 17), 
                new Led(1, "Red", 27), 
                new Led(1, "Green", 5) 
            };
        }
    }
}
