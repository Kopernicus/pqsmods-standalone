/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using PQS.Unity;

namespace PQS
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
        public double oceanRadiusOffset;

        /// <summary>
        /// The depth offset
        /// </summary>
        public double depthOffset;

        /// <summary>
        /// The ocean radius
        /// </summary>
        private double oceanRadius;

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
    }
}
