/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using ProceduralQuadSphere.KSP;
using ProceduralQuadSphere.Unity;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A PQSMod that lerps a simplex color into the pqs color
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexSimplexNoiseColor : PQSMod
    {
        /// <summary>
        /// The seed for the simplex
        /// </summary>
        public Int32 seed;

        /// <summary>
        /// How much of the color should end up on the surface
        /// </summary>
        public Single blend;

        /// <summary>
        /// The starting color
        /// </summary>
        public Color colorStart;

        /// <summary>
        /// The color at the end
        /// </summary>
        public Color colorEnd;

        /// <summary>
        /// The octaves for the simplex
        /// </summary>
        public Double octaves;

        /// <summary>
        /// The persistence for the simplex
        /// </summary>
        public Double persistence;

        /// <summary>
        /// The frequency for the simplex
        /// </summary>
        public Double frequency;

        /// <summary>
        /// The final simplex
        /// </summary>
        private Simplex simplex;

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            simplex = new Simplex(seed, octaves, persistence, frequency);
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            Single n = (Single)((this.simplex.noise(data.directionFromCenter) + 1) / 2);
            data.vertColor = Color.Lerp(data.vertColor, Color.Lerp(colorStart, colorEnd, n), blend);
        }
    }
}