/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that moves the terrain upwards or downwards
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexHeightOffset : PQSMod
    {
        /// <summary>
        /// The terrain offset
        /// </summary>
        public Double offset;

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            data.vertHeight += offset;
        }
    }
}