﻿/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using PQS.KSP;
using PQS.Unity;

namespace PQS
{
    /// <summary>
    /// The heightmap PQSMod. Gets the pixel data from a binary texture wrapper and returns it.
    /// </summary>
    public class PQSMod_VertexHeightMap : PQSMod
    {
        /// <summary>
        /// The height map for the mod
        /// </summary>
        public MapSO heightMap { get; set; }

        /// <summary>
        /// How much should the calculated height get deformed
        /// </summary>
        public double heightMapDeformity { get; set; }

        /// <summary>
        /// A static offset that gets applied to all values
        /// </summary>
        public double heightMapOffset { get; set; }

        /// <summary>
        /// Whether the radius should influence the deformity parameter
        /// </summary>
        public bool scaleDeformityByRadius { get; set; }

        /// <summary>
        /// A storage value for the final deformity
        /// </summary>
        private double heightDeformity { get; set; }

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            /// Calculate the deformity
            if (scaleDeformityByRadius)
                heightDeformity = sphere.radius * heightMapDeformity;
            else
                heightDeformity = heightMapDeformity;

            /// Check whether the HeightMap exists
            if (heightMap == null)
                throw new ArgumentNullException(nameof(heightMap));
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            data.vertHeight += heightMapOffset + heightDeformity * heightMap.GetPixelFloat(data.u, data.v);
        }
    }
}