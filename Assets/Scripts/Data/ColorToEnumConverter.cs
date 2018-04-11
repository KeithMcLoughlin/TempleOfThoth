using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public enum Colour
    {
        Yellow, Red, Green, Blue, Black, Cyan, Magenta, Grey
    }
    static public class ColorToEnumConverter
    {
        static public Color ColourEnumToColor(Colour colour)
        {
            switch(colour)
            {
                case Colour.Yellow: { return Color.yellow; }
                case Colour.Red: { return Color.red; }
                case Colour.Magenta: { return Color.magenta; }
                case Colour.Grey: { return Color.grey; }
                case Colour.Green: { return Color.green; }
                case Colour.Cyan: { return Color.cyan; }
                case Colour.Blue: { return Color.blue; }
                case Colour.Black: { return Color.black; }
            }
            return Color.white;
        }

        static public Colour ColorToColourEnum(Color colour)
        {
            if (colour == Color.yellow) return Colour.Yellow;
            else if (colour == Color.red) return Colour.Red;
            else if (colour == Color.magenta) return Colour.Magenta;
            else if (colour == Color.grey) return Colour.Grey;
            else if (colour == Color.green) return Colour.Green;
            else if (colour == Color.cyan) return Colour.Cyan;
            else if (colour == Color.blue) return Colour.Blue;
            else if (colour == Color.black) return Colour.Black;
            else return Colour.Red; //just return a colour as default
        }
    }
}
