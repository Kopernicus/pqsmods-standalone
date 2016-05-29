/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using ProceduralQuadSphere.Unity;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that modifies coast lines.
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexDefineCoastLine : PQSMod
    {
        /// <summary>
        /// The ocean radius offset
        /// </summary>
        public Double oceanRadiusOffset;

        /// <summary>
        /// The depth offset
        /// </summary>
        public Double depthOffset;

        /// <summary>
        /// The ocean radius
        /// </summary>
        private Double oceanRadius;

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            oceanRadius = sphere.radius + oceanRadiusOffset;
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            if (data.vertHeight < oceanRadius)
                data.vertHeight += depthOffset;
        }

        /// <summary>
        /// Gets the minimum height of the mod
        /// </summary>
        public override Double GetVertexMinHeight()
        {
            return this.depthOffset;
        }
    }
}
