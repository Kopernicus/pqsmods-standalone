﻿using System;
using PQS.Unity;

namespace LibNoise.Generator
{
    /// <summary>
    /// Provides a noise module that outputs a three-dimensional perlin noise. [GENERATOR]
    /// </summary>
    public class Perlin : ModuleBase
    {
        #region Fields

        private Double _frequency = 1.0;
        private Double _lacunarity = 2.0;
        private QualityMode _quality = QualityMode.Medium;
        private Int32 _octaveCount = 6;
        private Double _persistence = 0.5;
        private Int32 _seed;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Perlin.
        /// </summary>
        public Perlin()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of Perlin.
        /// </summary>
        /// <param name="frequency">The frequency of the first octave.</param>
        /// <param name="lacunarity">The lacunarity of the perlin noise.</param>
        /// <param name="persistence">The persistence of the perlin noise.</param>
        /// <param name="octaves">The number of octaves of the perlin noise.</param>
        /// <param name="seed">The seed of the perlin noise.</param>
        /// <param name="quality">The quality of the perlin noise.</param>
        public Perlin(Double frequency, Double lacunarity, Double persistence, Int32 octaves, Int32 seed,
            QualityMode quality)
            : base(0)
        {
            Frequency = frequency;
            Lacunarity = lacunarity;
            OctaveCount = octaves;
            Persistence = persistence;
            Seed = seed;
            Quality = quality;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the first octave.
        /// </summary>
        public Double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        /// <summary>
        /// Gets or sets the lacunarity of the perlin noise.
        /// </summary>
        public Double Lacunarity
        {
            get { return _lacunarity; }
            set { _lacunarity = value; }
        }

        /// <summary>
        /// Gets or sets the quality of the perlin noise.
        /// </summary>
        public QualityMode Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        /// <summary>
        /// Gets or sets the number of octaves of the perlin noise.
        /// </summary>
        public Int32 OctaveCount
        {
            get { return _octaveCount; }
            set { _octaveCount = Mathf.Clamp(value, 1, Utils.OctavesMaximum); }
        }

        /// <summary>
        /// Gets or sets the persistence of the perlin noise.
        /// </summary>
        public Double Persistence
        {
            get { return _persistence; }
            set { _persistence = value; }
        }

        /// <summary>
        /// Gets or sets the seed of the perlin noise.
        /// </summary>
        public Int32 Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }

        #endregion

        #region ModuleBase Members

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public override Double GetValue(Double x, Double y, Double z)
        {
            var value = 0.0;
            var cp = 1.0;
            x *= _frequency;
            y *= _frequency;
            z *= _frequency;
            for (var i = 0; i < _octaveCount; i++)
            {
                var nx = Utils.MakeInt32Range(x);
                var ny = Utils.MakeInt32Range(y);
                var nz = Utils.MakeInt32Range(z);
                var seed = (_seed + i) & 0xffffffff;
                var signal = Utils.GradientCoherentNoise3D(nx, ny, nz, seed, _quality);
                value += signal * cp;
                x *= _lacunarity;
                y *= _lacunarity;
                z *= _lacunarity;
                cp *= _persistence;
            }
            return value;
        }

        #endregion
    }
}