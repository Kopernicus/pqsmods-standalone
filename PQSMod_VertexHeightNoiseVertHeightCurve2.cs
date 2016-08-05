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
    public class PQSMod_VertexHeightNoiseVertHeightCurve2 : PQSMod
    {
        /// <summary>
        /// The float curve
        /// </summary>
        public Curve simplexCurve;

        /// <summary>
        /// The resulting Simplex
        /// </summary>
        private Simplex simplex;

        /// <summary>
        /// The resulting ridged noises
        /// </summary>
        private RidgedMultifractal ridgedAdd;
        private RidgedMultifractal ridgedSub;

        #region Noise Values
        public Single deformity;
        public Double simplexHeightStart;
        public Double simplexHeightEnd;
        public Int32 simplexSeed;
        public Double simplexOctaves;
        public Double simplexPersistence;
        public Double simplexFrequency;
        public Int32 ridgedAddSeed;
        public Single ridgedAddFrequency;
        public Single ridgedAddLacunarity;
        public Int32 ridgedAddOctaves;
        public Int32 ridgedSubSeed;
        public Single ridgedSubFrequency;
        public Single ridgedSubLacunarity;
        public Int32 ridgedSubOctaves;
        public QualityMode ridgedMode;
        #endregion

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
            ridgedAdd = new RidgedMultifractal(ridgedAddFrequency, ridgedAddLacunarity, ridgedAddOctaves, ridgedAddSeed, ridgedMode);
            ridgedSub = new RidgedMultifractal(ridgedSubFrequency, ridgedSubLacunarity, ridgedSubOctaves, ridgedSubSeed, ridgedMode);
            simplex = new Simplex(simplexSeed, simplexOctaves, simplexPersistence, simplexFrequency);
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            Double h = data.vertHeight - sphere.radiusMin;
            Single t;
            if (h <= this.simplexHeightStart)
                t = 0f;
            else if (h < simplexHeightEnd)
                t = (Single)((h - simplexHeightStart) * (1 / (simplexHeightEnd - simplexHeightStart)));
            else
                t = 1f;
            Double s = simplex.noiseNormalized(data.directionFromCenter) * simplexCurve.Evaluate(t);
            if (s == 0f)
                return;
            Double r = ridgedAdd.GetValue(data.directionFromCenter) - ridgedSub.GetValue(data.directionFromCenter);
            if (r < -1)
                r = -1;
            if (r > 1)
                r = 1;
            data.vertHeight += (r + 1) * 0.5 * deformity * s;
        }

        public enum NoiseType
        {
            Perlin,
            RiggedMultifractal,
            Billow
        }
    }
}