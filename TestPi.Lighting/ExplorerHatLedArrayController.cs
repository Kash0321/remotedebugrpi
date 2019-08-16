using System;
using System.Device.Gpio;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace TestPi.Lighting
{
    /// <summary>
    /// Implementa <see cref="ILedArrayController" /> para el ExplorerHat de Pimoroni mediante 
    /// <see cref="https://github.com/dotnet/iot">DotNet IoT</see> 
    /// </summary>
    public class ExplorerHATLedArrayController : ILedArrayController
    {
        protected List<Led> LedArrayList { get; set; } = new List<Led>();
        protected GpioController GpioController { get; set; } = new GpioController();

        /// <inheritdoc />
        public IReadOnlyCollection<Led> LedArray 
        {
            get
            {
                return new ReadOnlyCollection<Led>(LedArrayList);
            }
        }

        /// <inheritdoc />
        public void Blink(string ledName, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1)
        {
            var led = LedArrayList.Where(l => l.Name == ledName).SingleOrDefault();

            Blink(led, lightTimeInMilliseconds, dimTimeInMilliseconds, steps);
        }

        /// <inheritdoc />
        public void Blink(int ledNumber, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1)
        {
            var led = LedArrayList.Where(l => l.Number == ledNumber).SingleOrDefault();

            Blink(led, lightTimeInMilliseconds, dimTimeInMilliseconds, steps);
        }

        /// <inheritdoc />
        public void Blink(Led led, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps)
        {            
            if (!GpioController.IsPinOpen(led.Pin) || GpioController.GetPinMode(led.Pin) != PinMode.Output)
            {
                GpioController.OpenPin(led.Pin, PinMode.Output);
            }

            for (int i = 0; i < steps; i++)
            {
                Console.WriteLine($"Light {led.Name} for {lightTimeInMilliseconds}ms");
                GpioController.Write(led.Pin, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"Dim {led.Name} for {dimTimeInMilliseconds}ms");
                GpioController.Write(led.Pin, PinValue.Low);
                Thread.Sleep(dimTimeInMilliseconds);
            }
        }

        /// <inheritdoc />
        public void SwitchOn(int ledNumber)
        {
            var led = LedArrayList.Where(l => l.Number == ledNumber).SingleOrDefault();

            SwitchOn(led);
        }

        /// <inheritdoc />

        public void SwitchOff(int ledNumber)
        {
            var led = LedArrayList.Where(l => l.Number == ledNumber).SingleOrDefault();

            SwitchOff(led);
        }

        /// <inheritdoc />
        public void SwitchOn(string ledName)
        {
            var led = LedArrayList.Where(l => l.Name == ledName).SingleOrDefault();

            SwitchOn(led);
        }

        /// <inheritdoc />
        public void SwitchOff(string ledName)
        {
            var led = LedArrayList.Where(l => l.Name == ledName).SingleOrDefault();

            SwitchOff(led);
        }

        void SwitchOn(Led led)
        {            
            if (!GpioController.IsPinOpen(led.Pin) || GpioController.GetPinMode(led.Pin) != PinMode.Output)
            {
                GpioController.OpenPin(led.Pin, PinMode.Output);
            }

            GpioController.Write(led.Pin, PinValue.High);
            Console.WriteLine($"{led.Name} switched ON");
        }

        void SwitchOff(Led led)
        {            
            if (!GpioController.IsPinOpen(led.Pin) || GpioController.GetPinMode(led.Pin) != PinMode.Output)
            {
                GpioController.OpenPin(led.Pin, PinMode.Output);
            }

            GpioController.Write(led.Pin, PinValue.Low);
            Console.WriteLine($"{led.Name} switched OFF");
        }

        /// <summary>
        /// Inicializa una instancia de <see cref="ExplorerHATLedArrayController" />.
        /// Configura los 4 leds incluidos en el Explorer HAT
        /// </summary>
        public ExplorerHATLedArrayController()
        { 
            LedArrayList = new List<Led>() 
            { 
                new Led(1, "blue", 4), 
                new Led(2, "yellow", 17), 
                new Led(3, "red", 27), 
                new Led(4, "green", 5) 
            };
        }
    }
}
