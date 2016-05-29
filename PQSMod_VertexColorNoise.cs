/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using LibNoise;
using LibNoise.Generator;
using System;
using PQS.Unity;

namespace PQS
{
    /// <summary>
    /// PQSMod that applies color based on a noise generator
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexColorNoise : PQSMod
    {
        public enum NoiseType
        {
            Perlin,
            RiggedMultifractal,
            Billow
        }

        /// <summary>
        /// The type of the noise that gets used.
        /// </summary>
        public NoiseType noiseType;

        #region Noise Values
        public Single blend;
        public Int32 seed;
        public Single frequency;
        public Single lacunarity;
        public Single persistance;
        public Int32 octaves;
        public QualityMode mode;
        private ModuleBase noiseMap;
        #endregion

        /// <summary>
        /// The color that gets created from the noise
        /// </summary>
        private Color c;

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            switch (noiseType)
            {
                case NoiseType.Perlin:
                    noiseMap = new Perlin(frequency, lacunarity, persistance, octaves, seed, mode);
                    break;
                case NoiseType.Billow:
                    noiseMap = new Billow(frequency, lacunarity, persistance, octaves, seed, mode);
                    break;
                case NoiseType.RiggedMultifractal:
                    noiseMap = new RidgedMultifractal(frequency, lacunarity, octaves, seed, mode);
                    break;
                default:
                    throw new ArgumentException("Noise Type seems to be something undefineable. You are a scrub.", nameof(noiseType));
            }
            c = Color.white;
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
           Single h = (Single)((noiseMap.GetValue(data.directionFromCenter) + 1.0) * 0.5);
            c.r = h; c.g = h; c.b = h;
            data.vertColor = Color.Lerp(data.vertColor, c, blend);
        }
    }
}