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
        /// Color key used by Gradient.
        /// </summary>
        public struct GradientColorKey
        {
            /// <summary>
            /// Color of key.
            /// </summary>
            public Color color;

            /// <summary>
            /// Time of the key (0 - 1).
            /// </summary>
            public Single time;

            /// <summary>
            /// Gradient color key.
            /// </summary>
            /// <param name="col">Color of key.</param>
            /// <param name="time">Time of the key (0 - 1).</param>
            public GradientColorKey(Color color, Single time)
            {
                this.color = color;
                this.time = time;
            }
        }
    }
}