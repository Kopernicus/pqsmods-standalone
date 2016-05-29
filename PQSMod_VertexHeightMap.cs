/**
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
        public MapSO heightMap;

        /// <summary>
        /// How much should the calculated height get deformed
        /// </summary>
        public Double heightMapDeformity;

        /// <summary>
        /// A static offset that gets applied to all values
        /// </summary>
        public Double heightMapOffset;

        /// <summary>
        /// Whether the radius should influence the deformity parameter
        /// </summary>
        public Boolean scaleDeformityByRadius;

        /// <summary>
        /// A storage value for the final deformity
        /// </summary>
        private Double heightDeformity;

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            // Calculate the deformity
            if (scaleDeformityByRadius)
                heightDeformity = sphere.radius * heightMapDeformity;
            else
                heightDeformity = heightMapDeformity;

            // Check whether the HeightMap exists
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

        /// <summary>
        /// Gets the maximum height of the mod.
        /// </summary>
        public override Double GetVertexMaxHeight()
        {
            return heightMapOffset + heightDeformity;
        }

        /// <summary>
        /// Gets the minimum height of the mod.
        /// </summary>
        public override Double GetVertexMinHeight()
        {
            return heightMapOffset;
        }
    }
}
