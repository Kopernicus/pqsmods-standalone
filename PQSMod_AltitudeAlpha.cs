/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using PQS.Unity;

namespace PQS
{
    /// <summary>
    /// A mod that modifies alpha based on the altitude
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_AltitudeAlpha : PQSMod
    {
        /// <summary>
        /// The depth of the atmosphere
        /// </summary>
        public Double atmosphereDepth;

        /// <summary>
        /// Whether the alpha value should get inverted
        /// </summary>
        public Boolean invert;

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            Double h = (data.vertHeight - sphere.radius) / atmosphereDepth;
            if (invert)
                data.vertColor.a = (Single)(1.0 - h);
            else
                data.vertColor.a = (Single)h;
        }
    }
}
