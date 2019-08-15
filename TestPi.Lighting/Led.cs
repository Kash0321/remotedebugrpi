public class Led
{
    public int Number { get; private set; }

    public string Name { get; private set; }

    public int Pin { get; private set; }

    public bool IsOn { get; set; }

    public Led(int number, string name, int pin)
    {
        Number = number;
        Name = name;
        Pin = pin;
        IsOn = false;
    }
}