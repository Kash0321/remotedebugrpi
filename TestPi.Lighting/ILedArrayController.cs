using System.Collections.Generic;

namespace TestPi.Lighting
{
    public interface ILedArrayController
    {
        IReadOnlyCollection<Led> LedArray { get; }
        
        void Blink(string ledName, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1);

        void Blink(int ledNumber, int lightTimeInMilliseconds, int dimTimeInMilliseconds, int steps = 1);
    }
}
