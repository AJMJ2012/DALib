using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;

namespace DALib.Functions;

public static class ColorFunctions
{
    [Obsolete("Use String.Colorize(Color) Instead")]
    public static string ColorString(string input, Color color) => input.Colorize(color);
    public static string Colorize(this string input, Color color) => string.Format("[c/{0}:{1}]", color.ToHex(), input);


    [Obsolete("Use Color.ToHex() Instead")]
    public static string RGBToHex(Color color) => color.ToHex();
    public static string ToHex(this Color color) => string.Format("{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);

    public static Color ShiftHue(this Color color, int degrees) => color.ToHSL().ShiftHue(degrees).ToRGB();
    public static Vector4 ShiftHue(this Vector4 hsl, int degrees) => hsl + new Vector4(degrees / 360f, 0f, 0f, 0f);

	public static Color MinColor(this Color color) {
		byte minColor = Math.Min(color.R, Math.Min(color.G, color.B));
		return new Color(minColor, minColor, minColor);
	}

	public static Color MaxColor(this Color color) {
		byte maxColor = Math.Max(color.R, Math.Max(color.G, color.B));
		return new Color(maxColor, maxColor, maxColor);
    }


    [Obsolete("Use Vector3.ToRGB() Instead")]
    public static Color HSLToRGB(Vector3 hsl) => new Vector4(hsl.X, hsl.Y, hsl.Z, 1f).ToRGB();
    public static Color ToRGB(this Vector3 hsl) => new Vector4(hsl.X, hsl.Y, hsl.Z, 1f).ToRGB();

    [Obsolete("Use Vector4.ToRGB() Instead")]
    public static Color HSLToRGB(Vector4 hsl) => hsl.ToRGB();
    public static Color ToRGB(this Vector4 hsl) {
        float h = hsl.X;
        float s = hsl.Y;
        float l = hsl.Z;
        float a = hsl.W;
        if (s == 0f)
        {
            return new Color(l, l, l, a);
		}
        float t1 = ((!(l < 0.5f)) ? ((l + s - l * s)) : (l * (1f + s)));
        float t2 = 2f * l - t1;
        float c1 = h + 1f / 3f;
        float c2 = h;
        float c3 = h - 1f / 3f;
        float r = HueToRGB(c1, t1, t2);
		float g = HueToRGB(c2, t1, t2);
        float b = HueToRGB(c3, t1, t2);
        return new Color(r, g, b, a);
	}

	private static float HueToRGB(float c, float t1, float t2) {
		if (c < 0f) {
			c += 1f;
		}
		if (c > 1f) {
			c -= 1f;
		}
		if (6f * c < 1f) {
			return t2 + (t1 - t2) * 6f * c;
		}
		if (2f * c < 1f) {
			return t1;
		}
		if (3f * c < 2f) {
			return t2 + (t1 - t2) * (2f / 3f - c) * 6f;
		}
		return t2;
    }


    [Obsolete("Use Color.ToHSL() Instead")]
    public static Vector4 RGBToHSL(Color color) => color.ToHSL();
    public static Vector4 ToHSL(this Color color) {
		float r = color.R / 255f;
		float g = color.G / 255f;
        float b = color.B / 255f;
        float a = color.A / 255f;

        float max = Math.Max(Math.Max(r, g), b);
        float min = Math.Min(Math.Min(r, g), b);
		float h = 0f;
        float s;
        float l = (max + min) / 2f;
		if (max == min) {
			h = (s = 0f);
		}
		else {
			float diff = max - min;
			s = (((double)l > 0.5) ? (diff / (2f - max - min)) : (diff / (max + min)));
			if (max == r) {
				h = (g - b) / diff + ((g < b) ? 6 : 0);
			}
			if (max == g) {
				h = (b - r) / diff + 2f;
			}
			if (max == b) {
				h = (r - g) / diff + 4f;
			}
			h /= 6f;
		}
		return new Vector4(h, s, l, a);
	}
}

