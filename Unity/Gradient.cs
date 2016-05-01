/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using System.Linq;

namespace PQS
{
    namespace Unity
    {
        /// <summary>
        ///   <para>Gradient used for animating colors.</para>
        /// </summary>
        public sealed class Gradient
        {
            /// <summary>
            /// All alpha keys defined in the gradient.
            /// </summary>
            public GradientAlphaKey[] alphaKeys { get; set; }

            /// <summary>
            /// All color keys defined in the gradient.
            /// </summary>
            public GradientColorKey[] colorKeys { get; set; }

            /// <summary>
            ///   <para>Create a new Gradient object.</para>
            /// </summary>
            public Gradient()
            {
                colorKeys = new[] { new GradientColorKey(Color.white, 0), new GradientColorKey(Color.black, 1) };
                alphaKeys = new[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(0, 1) };
            }

            /// <summary>
            /// Calculate color at a given time.
            /// </summary>
            /// <param name="time">Time of the key (0 - 1).</param>
            public Color Evaluate(float time)
            {
                Color color = Color.black;
                if (colorKeys.Count(k => k.time == time) == 1)
                {
                    color = colorKeys.FirstOrDefault(k => k.time == time).color;
                }
                else
                {
                    GradientColorKey key1 = colorKeys.OrderBy(k => k.time).LastOrDefault(k => k.time < time);
                    GradientColorKey key2 = colorKeys.OrderBy(k => k.time).FirstOrDefault(k => k.time > time);
                    color = Color.Lerp(key1.color, key2.color, (time - key1.time)/(key2.time - key1.time));
                }
                float alpha = 1;
                if (alphaKeys.Count(k => k.time == time) == 1)
                {
                    alpha = alphaKeys.FirstOrDefault(k => k.time == time).alpha;
                }
                else
                {
                    GradientAlphaKey key1 = alphaKeys.OrderBy(k => k.time).LastOrDefault(k => k.time < time);
                    GradientAlphaKey key2 = alphaKeys.OrderBy(k => k.time).FirstOrDefault(k => k.time > time);
                    alpha = Mathf.Lerp(key1.alpha, key2.alpha, (time - key1.time) / (key2.time - key1.time));
                }
                color.a = alpha;
                return color;
            }

            /// <summary>
            /// Setup Gradient with an array of color keys and alpha keys.
            /// </summary>
            /// <param name="colorKeys">Color keys of the gradient (maximum 8 color keys).</param>
            /// <param name="alphaKeys">Alpha keys of the gradient (maximum 8 alpha keys).</param>
            public void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys)
            {
                if (colorKeys.Length > 8)
                    throw new ArgumentException(nameof(colorKeys), "There are more than 8 color keys.");
                if (alphaKeys.Length > 8)
                    throw new ArgumentException(nameof(alphaKeys), "There are more than 8 alpha keys.");

                this.colorKeys = colorKeys;
                this.alphaKeys = alphaKeys;
            }

        }
    }
}