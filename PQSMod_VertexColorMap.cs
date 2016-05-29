/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using ProceduralQuadSphere.Unity;
using ProceduralQuadSphere.KSP;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// The colomap PQSMod. Gets the pixel data from a binary texture wrapper and returns it.
    /// </summary>
    public class PQSMod_VertexColorMap : PQSMod
    {
        /// <summary>
        /// The color map for the mod
        /// </summary>
        public MapSO vertexColorMap;

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            // Check whether the ColorMap exists
            if (vertexColorMap == null)
                throw new ArgumentNullException(nameof(vertexColorMap));
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            data.vertColor = vertexColorMap.GetPixelColor(data.u, data.v);
        }
    }
}
