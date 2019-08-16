using System.Collections.Generic;

namespace TestPi.Lighting
{
    /// <summary>
    /// Controla un array de leds, con nombre, identificador, pin conectado y estado
    /// </summary>
    public interface ILedArrayController
    {
        /// <summary>
        /// Array de leds controlables por este componente
        /// </summary>
        IReadOnlyCollection<Led> LedArray { get; }
        
        /// <summary>
        /// Hace parpadear el led
        /// </summary>
        /// <param name="ledName">Nombre asociado al led que ha de parpadear</param>
        /// <param name="lightTimeInMilliseconds">Milisegundos en los que el led está encendido en cada parpadeo</param>
        /// <param name="dimTimeInMilliseconds">Milisegundos en los que el led está apagado en cada parpadeo</param>
        /// <param name="steps">Número de parpadeos a ejecutar</param>
        void Blink(string ledName, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1);

        /// <summary>
        /// Hace parpadear el led
        /// </summary>
        /// <param name="ledNumber">Número asociado al led que ha de parpadear</param>
        /// <param name="lightTimeInMilliseconds">Milisegundos en los que el led está encendido en cada parpadeo</param>
        /// <param name="dimTimeInMilliseconds">Milisegundos en los que el led está apagado en cada parpadeo</param>
        /// <param name="steps">Número de parpadeos a ejecutar</param>
        void Blink(int ledNumber, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1);

        /// <summary>
        /// Enciende el led
        /// </summary>
        /// <param name="ledNumber">Número asociado al led que se ha de encender</param>
        void SwitchOn(int ledNumber);

        /// <summary>
        /// Apaga el led
        /// </summary>
        /// <param name="ledNumber">Número asociado al led que se ha de apagar</param>
        void SwitchOff(int ledNumber);

        /// <summary>
        /// Enciende el led
        /// </summary>
        /// <param name="ledName">Nombre asociado al led que se ha de encender</param>
        void SwitchOn(string ledName);

        /// <summary>
        /// Apaga el led
        /// </summary>
        /// <param name="ledName">Nombre asociado al led que se ha de apagar</param>
        void SwitchOff(string ledName);
    }
}
