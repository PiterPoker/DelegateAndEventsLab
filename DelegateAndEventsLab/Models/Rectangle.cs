using MaxFinderWithEvents;

public class Rectangle
{
    private float length;
    private float width;

    public float Length
    {
        get { return length; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Length cannot be negative");
            length = value;
        }
    }

    public float Width
    {
        get { return width; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Width cannot be negative");
            width = value;
        }
    }

    public float Area
    {
        get { return Length * Width; }
    }

    public float Perimeter
    {
        get { return 2 * (Length + Width); }
    }

    public Rectangle(float length, float width)
    {
        Length = length;
        Width = width;
    }
}