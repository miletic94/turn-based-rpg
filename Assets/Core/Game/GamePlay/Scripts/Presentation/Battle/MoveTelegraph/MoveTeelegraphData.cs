using UnityEngine;

public class MoveTelegraphData
{
    public string Text { get; }
    public Color Color { get; }
    public MoveTelegraphData(string text, Color color)
    {
        Text = text;
        Color = color;
    }
}