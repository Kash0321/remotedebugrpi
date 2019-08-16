namespace TestPi.Lighting
{
    /// <summary>
    /// Representa un led del dispositivo
    /// </summary>
    public class Led
    {
        /// <summary>
        /// Número del led dentro de un conjunto controlable de elementos
        /// </summary>
        /// <value></value>
        public int Number { get; private set; }

        /// <summary>
        /// Nombre del led dentro de un conjunto controlable de elementos
        /// </summary>
        /// <value></value>
        public string Name { get; private set; }

        /// <summary>
        /// Pin "Gpio" del dispositivo al que está conectado el led
        /// </summary>
        /// <value></value>
        public int Pin { get; private set; }

        /// <summary>
        /// Indica o establece si el led está encendido o no
        /// </summary>
        /// <value></value>
        public bool IsOn { get; set; }

        /// <summary>
        /// Inicializa una instancia de <see cref="Led" />
        /// </summary>
        /// <param name="number">Número del led dentro de un conjunto controlable de elementos</param>
        /// <param name="name">Nombre del led dentro de un conjunto controlable de elementos</param>
        /// <param name="pin">Pin "Gpio" del dispositivo al que está conectado el led</param>
        public Led(int number, string name, int pin)
        {
            Number = number;
            Name = name;
            Pin = pin;
            IsOn = false;
        }
    }
}