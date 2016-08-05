/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using LibNoise;
using LibNoise.Generator;
using System;
using Microsoft.Xna.Framework;
using ProceduralQuadSphere.KSP;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that applies a noise on top of the output of a float curve
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexHeightNoiseVertHeightCurve : PQSMod
    {
        /// <summary>
        /// The type of the noise
        /// </summary>
        public NoiseType noiseType;

        #region Noise Values
        public Single deformity;
        public Int32 seed;
        public Single frequency;
        public Single lacunarity;
        public Single persistance;
        public Int32 octaves;
        public QualityMode mode;
        private ModuleBase noiseMap;
        #endregion

        /// <summary>
        /// The float curve
        /// </summary>
        public Curve curve;

        /// <summary>
        /// The height where the noise starts
        /// </summary>
        public Single heightStart;

        /// <summary>
        /// The height where the noise ends
        /// </summary>
        public Single heightEnd;

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
            return 0;
        }

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
                case NoiseType.RiggedMultifractal:
                    noiseMap = new RidgedMultifractal(frequency, lacunarity, octaves, seed, mode);
                    break;
                case NoiseType.Billow:
                    noiseMap = new Billow(frequency, lacunarity, persistance, octaves, seed, mode);
                    break;
                default:
                    throw new ArgumentException("Noise Type seems to be something undefineable. You are a scrub.", nameof(noiseType));
            }
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            Double h = data.vertHeight - sphere.radiusMin;
            Single t = 0;
            if (h <= heightStart)
                t = 0f;
            else if (h < heightEnd)
                t = (Single)(h - heightStart) / (heightEnd - heightStart);
            else
                t = 1f;
            Double n = noiseMap.GetValue(data.directionFromCenter);
            if (n < -1)
                n = -1;
            if (n > 1)
                n = 1;
            data.vertHeight += (n + 1) * 0.5 * deformity * curve.Evaluate(t);
        }

        public enum NoiseType
        {
            Perlin,
            RiggedMultifractal,
            Billow
        }
    }
}