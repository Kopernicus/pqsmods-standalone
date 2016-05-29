/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;

namespace PQS
{
    namespace Unity
    {
        /// <summary>
        /// Representation of RGBA colors in 32 bit format.
        /// </summary>
        public struct Color32
        {
            /// <summary>
            /// Red component of the color.
            /// </summary>
            public Byte r;

            /// <summary>
            /// Green component of the color.
            /// </summary>
            public Byte g;

            /// <summary>
            /// Blue component of the color.
            /// </summary>
            public Byte b;

            /// <summary>
            /// Alpha component of the color.
            /// </summary>
            public Byte a;

            /// <summary>
            /// Constructs a new Color with given r, g, b, a components.
            /// </summary>
            /// <param name="r"></param>
            /// <param name="g"></param>
            /// <param name="b"></param>
            /// <param name="a"></param>
            public Color32(Byte r, Byte g, Byte b, Byte a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            /// <summary>
            /// Linearly interpolates between colors a and b by t.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static Color32 Lerp(Color32 a, Color32 b, Single t)
            {
                t = Mathf.Clamp01(t);
                return new Color32((Byte)(a.r + (b.r - a.r) * t), (Byte)(a.g + (b.g - a.g) * t), (Byte)(a.b + (b.b - a.b) * t), (Byte)(a.a + (b.a - a.a) * t));
            }

            /// <summary>
            /// Linearly interpolates between colors a and b by t.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static Color32 LerpUnclamped(Color32 a, Color32 b, Single t)
            {
                return new Color32((Byte)(a.r + (b.r - a.r) * t), (Byte)(a.g + (b.g - a.g) * t), (Byte)(a.b + (b.b - a.b) * t), (Byte)(a.a + (b.a - a.a) * t));
            }

            public static implicit operator Color32(Color c)
            {
                return new Color32((Byte)(Mathf.Clamp01(c.r) * 255f), (Byte)(Mathf.Clamp01(c.g) * 255f), (Byte)(Mathf.Clamp01(c.b) * 255f), (Byte)(Mathf.Clamp01(c.a) * 255f));
            }

            public static implicit operator Color(Color32 c)
            {
                return new Color(c.r / 255f, c.g / 255f, c.b / 255f, c.a / 255f);
            }

            public static implicit operator System.Drawing.Color(Color32 c)
            {
                return System.Drawing.Color.FromArgb(c.a, c.r, c.g, c.b);
            }

            public static implicit operator Color32(System.Drawing.Color c)
            {
                return new Color32(c.R, c.G, c.B, c.A);
            }

            /// <summary>
            /// Returns a nicely formatted string of this color.
            /// </summary>
            /// <param name="format"></param>
            public override String ToString()
            {
                return String.Format("RGBA({0}, {1}, {2}, {3})", r, g, b, a);
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
