/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using PQS.Unity;

namespace PQS
{
    /// <summary>
    /// A mod that sets a minimum terrain altitude
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_FlattenOcean : PQSMod
    {
        /// <summary>
        /// The radius of the lowest spot
        /// </summary>
        public double oceanRadius;

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            if (data.vertHeight < (oceanRadius + sphere.radius))
                data.vertHeight = (oceanRadius + sphere.radius);
        }
    }
}
