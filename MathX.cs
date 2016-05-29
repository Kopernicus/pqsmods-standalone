/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;

namespace PQS
{
    /// <summary>
    /// Custom Math operations for PQSMods
    /// </summary>
    public static class MathX
    {
        /// <summary>
        /// Cubic Interpolation
        /// </summary>
        /// <returns></returns>
        public static Double CubicHermite(Double start, Double end, Double startTangent, Double endTangent, Double t)
        {
            Double timeSqr = t * t;
            Double timeCub = t * t * t;
            return start * (2 * timeCub - 3 * timeSqr + 1) + startTangent * (timeCub - 2 * timeSqr + t) + end * (-2 * timeCub + 3 * timeSqr) + endTangent * (timeCub - timeSqr);
        }
    }
}