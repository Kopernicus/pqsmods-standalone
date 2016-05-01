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
    /// The colomap PQSMod. Gets the pixel data from a binary texture wrapper and returns it.
    /// </summary>
    public class PQSMod_VertexColor : PQSMod
    {
        /// <summary>
        /// The color map for the mod
        /// </summary>
        public MapSO vertexColorMap { get; set; }

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            /// Check whether the HeightMap exists
            if (vertexColorMap == null)
                throw new ArgumentNullException(nameof(vertexColorMap));
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            data.vertColor = vertexColorMap.GetPixelColor(data.u, data.v);
        }
    }
}