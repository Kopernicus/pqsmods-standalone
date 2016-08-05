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
    /// A mod that uses three simplexes to color the terrain
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexSimplexMultiChromatic : PQSMod
    {
        /// <summary>
        /// How much of the resulting color ends up in the terrain
        /// </summary>
        public Single blend;

        // R
        public Int32 redSeed;
        public Double redOctaves;
        public Double redPersistence;
        public Double redFrequency;

        // B
        public Int32 blueSeed;
        public Double blueOctaves;
        public Double bluePersistence;
        public Double blueFrequency;

        // G
        public Int32 greenSeed;
        public Double greenOctaves;
        public Double greenPersistence;
        public Double greenFrequency;

        // A
        public Int32 alphaSeed;
        public Double alphaOctaves;
        public Double alphaPersistence;
        public Double alphaFrequency;

        // The final channel simplexes
        private Simplex redSimplex;
        private Simplex blueSimplex;
        private Simplex greenSimplex;
        private Simplex alphaSimplex;

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            redSimplex = new Simplex(redSeed, redOctaves, redPersistence, redFrequency);
            blueSimplex = new Simplex(blueSeed, blueOctaves, bluePersistence, blueFrequency);
            greenSimplex = new Simplex(greenSeed, greenOctaves, greenPersistence, greenFrequency);
            alphaSimplex = new Simplex(alphaSeed, alphaOctaves, alphaPersistence, alphaFrequency);
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            Color c = new Color
            {
                r = (Single) redSimplex.noiseNormalized(data.directionFromCenter),
                g = (Single) blueSimplex.noiseNormalized(data.directionFromCenter),
                b = (Single) greenSimplex.noiseNormalized(data.directionFromCenter),
                a = (Single) alphaSimplex.noiseNormalized(data.directionFromCenter)
            };
            data.vertColor = Color.Lerp(data.vertColor, c, blend);
        }
    }
}