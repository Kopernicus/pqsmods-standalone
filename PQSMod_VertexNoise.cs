/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using LibNoise.Generator;
using LibNoise.Operator;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that combines all existing noises 
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexNoise : PQSMod
    {
        #region Noise Values
        public Int32 seed;
        public Single noiseDeformity;
        public Int32 noisePasses;
        public Single smoothness;
        public Single falloff;
        public Single mesaVsPlainsBias;
        public Single plainsVsMountainSmoothness;
        public Single plainsVsMountainThreshold;
        public Single plainSmoothness;
        private Select terrainHeightMap;
        #endregion

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            Billow billow = new Billow()
            {
                Seed = seed,
                Frequency = 2 * (1f / smoothness)
            };
            ScaleBias scaleBia = new ScaleBias((1f / plainSmoothness), mesaVsPlainsBias, billow);
            RidgedMultifractal riggedMultifractal = new RidgedMultifractal
            {
                Seed = seed,
                OctaveCount = noisePasses,
                Frequency = 1 / smoothness
            };
            Perlin perlin = new Perlin
            {
                Seed = seed,
                OctaveCount = noisePasses,
                Frequency = 1 / plainsVsMountainSmoothness,
                Persistence = 1 / falloff
            };
            terrainHeightMap = new Select(0, 1, plainsVsMountainThreshold, scaleBia, riggedMultifractal)
            {
                Controller = perlin
            };
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            data.vertHeight  += terrainHeightMap.GetValue(data.directionFromCenter * sphere.radius) * noiseDeformity;
        }

        /// <summary>
        /// Gets the maximum height of the mod.
        /// </summary>
        public override Double GetVertexMaxHeight()
        {
            return noiseDeformity;
        }

        /// <summary>
        /// Gets the minimum height of the mod.
        /// </summary>
        public override Double GetVertexMinHeight()
        {
            return 0;
        }
    }
}