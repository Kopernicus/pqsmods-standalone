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
    /// Colormap PQSMod that supports blending the generated color into the one the PQS already has.
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexColorMapBlend : PQSMod
    {
        /// <summary>
        /// The color map for the mod
        /// </summary>
        public MapSO vertexColorMap;

        /// <summary>
        /// How much of the current value should get blended into the new one
        /// </summary>
        public Single blend;

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
            data.vertColor = Color.Lerp(data.vertColor, vertexColorMap.GetPixelColor(data.u, data.v), blend);
        }
    }
}
