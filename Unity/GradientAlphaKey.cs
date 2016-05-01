/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

namespace PQS
{
    namespace Unity
    {
        /// <summary>
        /// Alpha key used by Gradient.
        /// </summary>
        public struct GradientAlphaKey
        {
            /// <summary>
            /// Alpha channel of key.
            /// </summary>
            public float alpha;

            /// <summary>
            /// Time of the key (0 - 1).
            /// </summary>
            public float time;

            /// <summary>
            /// Gradient alpha key.
            /// </summary>
            /// <param name="alpha">Alpha of key (0 - 1).</param>
            /// <param name="time">Time of the key (0 - 1).</param>
            public GradientAlphaKey(float alpha, float time)
            {
                this.alpha = alpha;
                this.time = time;
            }
        }
    }
}