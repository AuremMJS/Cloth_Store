using System;

public class Cloth : IEquatable<Cloth>
{
    public ClothType Type { get; set; }
    public ClothColor Color {  get; private set; }

    public Cloth(ClothType type,ClothColor color)
    {
        Type = type;
        Color = color;
    }

    // Checking for equality
    public bool Equals(Cloth other)
    {
        return Type == other.Type && Color == other.Color;
    }
}

public enum ClothType
{
    Shirt,
    Pants,
    Bag,
    Jacket
}

public enum ClothColor
{
    Black,
    Red,
    Green,
    Yellow,
    Blue,
    White,
    Orange,
    Brown,
    Gray
}