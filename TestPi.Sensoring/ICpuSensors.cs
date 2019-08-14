namespace TestPi.Sensoring
{
    /// <summary>
    /// Sensorización de la CPU (temperatura, etc...)
    /// </summary>
    public interface ICpuSensors
    {
        /// <summary>
        /// Obtiene la temperatura actual del procesador
        /// </summary>
        /// <returns>Temperatura en grados celsius</returns>
        double GetCurrentTemperatureInCelsius();
    }
}