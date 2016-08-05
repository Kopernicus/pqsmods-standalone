/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using LibNoise.Generator;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that changes the terrain using a Voronoi Mesh
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexVoronoi : PQSMod
    {
        /// <summary>
        /// The terrain deformation
        /// </summary>
        public double deformation = 1000;

        /// <summary>
        /// The seed for the voronoi mesh
        /// </summary>
        public int voronoiSeed;

        /// <summary>
        /// The displacement for the voronoi
        /// </summary>
        public double voronoiDisplacement = 1;

        /// <summary>
        /// The frequency for the voronoi
        /// </summary>
        public double voronoiFrequency = 1;

        /// <summary>
        /// Whether the the distance from the nearest seed point is applied to the output of the voronoi
        /// </summary>
        public bool voronoiEnableDistance;

        /// <summary>
        /// The voronoi instance
        /// </summary>
        public Voronoi voronoi;

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            voronoi = new Voronoi(voronoiFrequency, voronoiDisplacement, voronoiSeed, voronoiEnableDistance);
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            data.vertHeight += voronoi.GetValue(data.directionFromCenter) * deformation;
        }
    }
}