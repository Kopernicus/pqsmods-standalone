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
    /// A mod that modifies color on a planet based on the output of a simplex
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexSimplexColorRGB : PQSMod
    {
        /// <summary>
        /// The seed of the simplex
        /// </summary>
        public Int32 seed;

        /// <summary>
        /// How much of the color should end up on the terrain
        /// </summary>
        public Single blend;

        /// <summary>
        /// The blend value for the R channel
        /// </summary>
        public Single rBlend;

        /// <summary>
        /// The blend value for the G channel
        /// </summary>
        public Single gBlend;

        /// <summary>
        /// The blend value for the B channel
        /// </summary>
        public Single bBlend;

        /// <summary>
        /// The octaves of the simplex
        /// </summary>summary>
        public Double octaves;

        /// <summary>
        /// The persistence of the simplex
        /// </summary>
        public Double persistence;

        /// <summary>
        /// The frequency of the simplex
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
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            Single n = (Single)((this.simplex.noise(data.directionFromCenter) + 1) * 0.5);
            Color c = new Color(n * rBlend, n * gBlend, n * bBlend);
            data.vertColor = Color.Lerp(data.vertColor, c, blend);
        }
    }
}