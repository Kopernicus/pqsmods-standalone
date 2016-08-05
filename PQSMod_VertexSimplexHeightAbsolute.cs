/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using ProceduralQuadSphere.KSP;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that modifies height on a planet based on the output of a simplex. Absolute version.
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexSimplexHeightAbsolute : PQSMod
    {
        /// <summary>
        /// The seed for the simplex
        /// </summary>
        public Int32 seed;

        /// <summary>
        /// The deformity for the terrain
        /// </summary>
        public Double deformity;

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
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            data.vertHeight += (simplex.noise(data.directionFromCenter) + 1) * 0.5 * deformity;
        }

        /// <summary>
        /// Gets the maximum height of the mod.
        /// </summary>
        public override Double GetVertexMaxHeight()
        {
            return deformity;
        }

        /// <summary>
        /// Gets the minimum height of the mod.
        /// </summary>
        public override Double GetVertexMinHeight()
        {
            return -deformity;
        }
    }
}