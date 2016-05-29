/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that makes the planet oblaaaaaate!
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexHeightOblate : PQSMod
    {
        /// <summary>
        /// The maximum height that should get added to the PQS
        /// </summary>
        public Double height;

        /// <summary>
        /// The power of the oblateness
        /// </summary>
        public Double pow;

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            Double a = Math.Sin(Math.PI * data.v);
            a = Math.Pow(a, pow);
            data.vertHeight += a * height;
        }
    }
}