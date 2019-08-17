using System;
using System.Device.Gpio;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Serilog;

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
            var led = GetLed(ledName);

            Blink(led, lightTimeInMilliseconds, dimTimeInMilliseconds, steps);
        }

        /// <inheritdoc />
        public void Blink(int ledNumber, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1)
        {
            var led = GetLed(ledNumber);

            Blink(led, lightTimeInMilliseconds, dimTimeInMilliseconds, steps);
        }

        /// <inheritdoc />
        public void Blink(Led led, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps)
        {            
            for (int i = 0; i < steps; i++)
            {
                Log.Information("Light {Led} for {@lightTimeInMilliseconds}ms", led, lightTimeInMilliseconds);
                SwitchOn(led);
                Thread.Sleep(lightTimeInMilliseconds);
                Log.Information("Dim {Led} for {@dimTimeInMilliseconds}ms", led, dimTimeInMilliseconds);
                SwitchOff(led);
                Thread.Sleep(dimTimeInMilliseconds);
            }
        }

        /// <inheritdoc />
        public void SwitchOn(int ledNumber)
        {
            var led = GetLed(ledNumber);

            SwitchOn(led);
        }

        /// <inheritdoc />

        public void SwitchOff(int ledNumber)
        {
            var led = GetLed(ledNumber);

            SwitchOff(led);
        }

        /// <inheritdoc />
        public void SwitchOn(string ledName)
        {
            var led = GetLed(ledName);

            SwitchOn(led);
        }

        /// <inheritdoc />
        public void SwitchOff(string ledName)
        {
            var led = GetLed(ledName);

            SwitchOff(led);
        }

        void EnsureOpenPin(Led led, PinMode pinMode)
        {
            if (!GpioController.IsPinOpen(led.Pin) || GpioController.GetPinMode(led.Pin) != pinMode)
            {
                if (GpioController.IsPinOpen(led.Pin))
                {
                    GpioController.ClosePin(led.Pin);
                }
                GpioController.OpenPin(led.Pin, pinMode);
            }
        }

        void SwitchOn(Led led)
        {
            if (!led.IsOn)
            {
                EnsureOpenPin(led, PinMode.Output);
                GpioController.Write(led.Pin, PinValue.High);
                Log.Information("{Led} switched ON", led);
                led.IsOn = true;
            }
        }

        void SwitchOff(Led led)
        {
            if (led.IsOn)
            {
                EnsureOpenPin(led, PinMode.Output);
                GpioController.Write(led.Pin, PinValue.Low);
                Log.Information("{Led} switched OFF", led);
                led.IsOn = false;
            }
        }

        Led GetLed(string name)
        {
            var led = LedArrayList.Where(l => l.Name == name).SingleOrDefault();

            if (led is null)
            {
                var ex = new Exception($"Led array has not a led named '{name}'");
                Log.Error(ex, "Error getting led from array");
            }

            return led;
        }

        Led GetLed(int number)
        {
            var led = LedArrayList.Where(l => l.Number == number).SingleOrDefault();

            if (led is null)
            {
                var ex = new Exception($"Led array has not a led identified with #{number}");
                Log.Error(ex, "Error getting led from array");
            }

            return led;
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

            Log.Information("Led array (Pimoroni ExplorerHAT on Raspberry PI) initialized: {@LedArrayList}", LedArrayList);
        }
    }
}
