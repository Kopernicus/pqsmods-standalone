/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

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
        public double atmosphereDepth;

        /// <summary>
        /// Whether the alpha value should get inverted
        /// </summary>
        public bool invert;

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            double h = (data.vertHeight - sphere.radius) / atmosphereDepth;
            if (invert)
                data.vertColor.a = (float)(1.0 - h);
            else
                data.vertColor.a = (float)h;
        }
    }
}
