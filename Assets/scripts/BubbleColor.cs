// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which handles colour mixing and presentation of the colours
/// </summary>
[ExecuteInEditMode]
public class BubbleColor : MonoBehaviour
{
    [Serializable]
    public struct RYB
    {
        public float Red;
        public float Yellow;
        public float Blue;

        public RYB(float red, float yellow, float blue)
        {
            this.Red = red;
            this.Blue = blue;
            this.Yellow = yellow;
        }

        public bool Equals(RYB o)
        {
            return o.Red == Red && o.Yellow == Yellow && o.Blue == Blue;
        }

        public override string ToString()
        {
            return "[r=" + Red + ",y=" + Yellow + ",b=" + Blue + "]";
        }
    }
    public RYB color;

    public Color colorRGB;

    //is called on editor change
    void OnValidate()
    {
        UpdateColor();
    }

    public Color getColor()
    {
        return colorRGB;
    }

    private Color ColorTransformRYBtoRGB(float red, float yellow, float blue)
    {
        //trilinear interpolation
        //http://math.stackexchange.com/questions/305395/ryb-and-rgb-color-space-conversion
        Func<float, float, float, Color> f = null;
        f = (r, y, b) =>
        {
            r = Mathf.Clamp(r, 0, 1);
            b = Mathf.Clamp(b, 0, 1);
            y = Mathf.Clamp(y, 0, 1);

            if (r == 0 && y == 0 && b == 0) return new Color(1, 1, 1);
            if (r == 0 && y == 0 && b == 1) return new Color(0.163f, 0.373f, 0.6f);
            if (r == 0 && y == 1 && b == 0) return new Color(1, 1, 0);
            if (r == 0 && y == 1 && b == 1) return new Color(0, 0.66f, 0.2f);
            if (r == 1 && y == 0 && b == 0) return new Color(1, 0, 0);
            if (r == 1 && y == 0 && b == 1) return new Color(0.5f, 0, 0.5f); //purple
            if (r == 1 && y == 1 && b == 0) return new Color(1, 0.5f, 0);
            if (r == 1 && y == 1 && b == 1) return new Color(0.2f, 0.094f, 0);
            return
               f(0, 0, 0) * (1 - r) * (1 - y) * (1 - b) +
               f(0, 0, 1) * (1 - r) * (1 - y) * b +
               f(0, 0, 1) * (1 - r) * (1 - y) * b +
               f(0, 1, 0) * (1 - r) * y * (1 - b) +
               f(1, 0, 0) * r * (1 - y) * (1 - b) +
               f(0, 1, 1) * (1 - r) * y * b +
               f(1, 0, 1) * r * (1 - y) * b +
               f(1, 1, 0) * r * y * (1 - b) +
               f(1, 1, 1) * r * y * b;
        };
        var c = f(red, yellow, blue);
        return new Color(c.r, c.g, c.b); //making sure alpha is at 1

    }

    public static BubbleColor operator +(BubbleColor c1, BubbleColor c2)
    {
        c1.color.Red += c2.color.Red;
        c1.color.Blue += c2.color.Blue;
        c1.color.Yellow += c2.color.Yellow;

        c1.color = clamp(c1.color);
        return c1;
    }

    private static RYB clamp(RYB c)
    {
        c.Blue = Mathf.Clamp01(c.Blue);
        c.Red = Mathf.Clamp01(c.Red);
        c.Yellow = Mathf.Clamp01(c.Yellow);

        return c;
    }

    internal void UpdateColor()
    {
        colorRGB = ColorTransformRYBtoRGB(color.Red, color.Yellow, color.Blue);
 
        if (GetComponent<BubbleTouchScript>() != null)
        {
            GetComponent<BubbleTouchScript>().UpdateColor(colorRGB);
        }
    }

    public override string ToString()
    {
        return color.ToString();
    }

    public override bool Equals(object other)
    {
        var o = other as BubbleColor;
        return o != null && color.Equals(o.color);
    }
}
