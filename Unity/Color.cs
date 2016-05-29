/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using XnaGeometry;

namespace ProceduralQuadSphere
{
    namespace Unity
    {
        /// <summary>
        /// Representation of RGBA colors.
        /// </summary>
        public struct Color
        {
            /// <summary>
            /// Converts a byte into a floating point number
            /// </summary>
            public const Single Byte2Float = 0.003921569f;

            /// <summary>
            /// Converts a floating point number into a byte
            /// </summary>
            public const Single Float2Byte = 255f;

            /// <summary>
            /// Red component of the color.
            /// </summary>
            public Single r;

            /// <summary>
            /// Green component of the color.
            /// </summary>
            public Single g;

            /// <summary>
            /// Blue component of the color.
            /// </summary>
            public Single b;

            /// <summary>
            /// Alpha component of the color.
            /// </summary>
            public Single a;

            /// <summary>
            /// Solid black. RGBA is (0, 0, 0, 1).
            /// </summary>
            public static Color black
            {
                get { return new Color(0f, 0f, 0f, 1f); }
            }

            /// <summary>
            /// Solid blue. RGBA is (0, 0, 1, 1).
            /// </summary>
            public static Color blue
            {
                get { return new Color(0f, 0f, 1f, 1f); }
            }

            /// <summary>
            /// Completely transparent. RGBA is (0, 0, 0, 0).
            /// </summary>
            public static Color clear
            {
                get { return new Color(0f, 0f, 0f, 0f); }
            }

            /// <summary>
            /// Cyan. RGBA is (0, 1, 1, 1).
            /// </summary>
            public static Color cyan
            {
                get { return new Color(0f, 1f, 1f, 1f); }
            }

            /// <summary>
            /// Gray. RGBA is (0.5, 0.5, 0.5, 1).
            /// </summary>
            public static Color gray
            {
                get { return new Color(0.5f, 0.5f, 0.5f, 1f); }
            }

            /// <summary>
            /// The grayscale value of the color. (Read Only)
            /// </summary>
            public Single grayscale
            {
                get { return 0.299f * r + 0.587f * g + 0.114f * b; }
            }

            /// <summary>
            /// Solid green. RGBA is (0, 1, 0, 1).
            /// </summary>
            public static Color green
            {
                get { return new Color(0f, 1f, 0f, 1f); }
            }

            /// <summary>
            /// English spelling for gray. RGBA is the same (0.5, 0.5, 0.5, 1).
            /// </summary>
            public static Color grey
            {
                get { return new Color(0.5f, 0.5f, 0.5f, 1f); }
            }

            public Single this[Int32 index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: 
                            return r; 
                        case 1:
                            return g;
                        case 2:
                            return b;
                        case 3:
                            return a;
                    }
                    throw new IndexOutOfRangeException("Invalid Color index!");
                }
                set
                {
                    switch (index)
                    {
                        case 0:
                            r = value;
                            break;
                        case 1:
                            g = value;
                            break;
                        case 2:
                            b = value;
                            break;
                        case 3:
                            a = value;
                            break;                            
                        default:
                            throw new IndexOutOfRangeException("Invalid Color index!");
                    }
                }
            }

            /// <summary>
            /// Magenta. RGBA is (1, 0, 1, 1).
            /// </summary>
            public static Color magenta
            {
                get { return new Color(1f, 0f, 1f, 1f); }
            }

            /// <summary>
            /// Returns the maximum color component value: Max(r,g,b).
            /// </summary>
            public Single maxColorComponent
            {
                get { return Mathf.Max(Mathf.Max(r, g), b); }
            }

            /// <summary>
            /// Solid red. RGBA is (1, 0, 0, 1).
            /// </summary>
            public static Color red
            {
                get { return new Color(1f, 0f, 0f, 1f); }
            }

            /// <summary>
            /// Solid white. RGBA is (1, 1, 1, 1).
            /// </summary>
            public static Color white
            {
                get { return new Color(1f, 1f, 1f, 1f); }
            }

            /// <summary>
            /// Yellow. RGBA is (1, 0.92, 0.016, 1), but the color is nice to look at!
            /// </summary>
            public static Color yellow
            {
                get { return new Color(1f, 0.921568632f, 0.0156862754f, 1f); }
            }

            /// <summary>
            /// Constructs a new Color with given r,g,b,a components.
            /// </summary>
            /// <param name="r">Red component.</param>
            /// <param name="g">Green component.</param>
            /// <param name="b">Blue component.</param>
            /// <param name="a">Alpha component.</param>
            public Color(Single r, Single g, Single b, Single a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            /// <summary>
            /// Constructs a new Color with given r,g,b components and sets a to 1.
            /// </summary>
            /// <param name="r">Red component.</param>
            /// <param name="g">Green component.</param>
            /// <param name="b">Blue component.</param>
            public Color(Single r, Single g, Single b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = 1f;
            }

            internal Color AlphaMultiplied(Single multiplier)
            {
                return new Color(r, g, b, a * multiplier);
            }

            public override Boolean Equals(Object other)
            {
                if (!(other is Color))
                {
                    return false;
                }
                Color color = (Color)other;
                return (r.Equals(color.r) && g.Equals(color.g) && b.Equals(color.b) && a.Equals(color.a));
            }

            public override Int32 GetHashCode()
            {
                return r.GetHashCode() ^ g.GetHashCode() ^ b.GetHashCode() ^ a.GetHashCode();
            }

            /// <summary>
            /// Linearly interpolates between colors a and b by t.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static Color Lerp(Color a, Color b, Single t)
            {
                t = Mathf.Clamp01(t);
                return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
            }

            /// <summary>
            /// Linearly interpolates between colors a and b by t.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static Color LerpUnclamped(Color a, Color b, Single t)
            {
                return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
            }

            public static Color operator +(Color a, Color b)
            {
                return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
            }

            public static Color operator /(Color a, Single b)
            {
                return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
            }

            public static Boolean operator ==(Color lhs, Color rhs)
            {
                return lhs == rhs;
            }

            public static implicit operator Vector4(Color c)
            {
                return new Vector4(c.r, c.g, c.b, c.a);
            }

            public static implicit operator Color(Vector4 v)
            {
                return new Color((Single)v.X, (Single)v.Y, (Single)v.Z, (Single)v.W);
            }

            public static implicit operator System.Drawing.Color(Color c)
            {
                return System.Drawing.Color.FromArgb((Int32)(Float2Byte * c.a), (Int32)(Float2Byte * c.r), (Int32)(Float2Byte * c.g), (Int32)(Float2Byte * c.b));
            }

            public static implicit operator Color(System.Drawing.Color c)
            {
                return new Color(Byte2Float * c.R, Byte2Float * c.G, Byte2Float * c.B, Byte2Float * c.A);
            }

            public static Boolean operator !=(Color lhs, Color rhs)
            {
                return !(lhs == rhs);
            }

            public static Color operator *(Color a, Color b)
            {
                return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
            }

            public static Color operator *(Color a, Single b)
            {
                return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
            }

            public static Color operator *(Single b, Color a)
            {
                return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
            }

            public static Color operator -(Color a, Color b)
            {
                return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
            }

            internal Color RGBMultiplied(Single multiplier)
            {
                return new Color(r * multiplier, g * multiplier, b * multiplier, a);
            }

            internal Color RGBMultiplied(Color multiplier)
            {
                return new Color(r * multiplier.r, g * multiplier.g, b * multiplier.b, a);
            }

            /// <summary>
            /// Returns a nicely formatted string of this color.
            /// </summary>
            /// <param name="format"></param>
            public override String ToString()
            {
                return String.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", r, g, b, a);
            }

            /// <summary>
            /// Returns a nicely formatted string of this color.
            /// </summary>
            /// <param name="format"></param>
            public String ToString(String format)
            {
                return String.Format("RGBA({0}, {1}, {2}, {3})", r.ToString(format), g.ToString(format), b.ToString(format), a.ToString(format));
            }
        }
    }
}
