using System;
using Iot.Device.CpuTemperature;

namespace TestPi.Sensoring
{
    /// <summary>
    /// Implementación de la sensorización de la CPU mediante 
    /// <see cref="https://github.com/dotnet/iot">DotNet IoT</see> 
    /// </summary>
    public class CpuSensors : ICpuSensors
    {
        CpuTemperature CPUTemperatureSensor { get; set; }

        /// <summary>
        /// Inicializa una instancia de <see cref="CpuSensors" />
        /// </summary>
        public CpuSensors()
        {
            CPUTemperatureSensor = new CpuTemperature();
        }

        /// <inheritdoc />
        public double GetCurrentTemperatureInCelsius()
        {
            if (CPUTemperatureSensor.IsAvailable)
            {
                return CPUTemperatureSensor.Temperature.Celsius;
            }
            else return 0.0d;
        }
    }
}
