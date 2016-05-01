/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using PQS.Unity;

namespace PQS
{
    /// <summary>
    /// A PQSMod that lerps a solid color into the pqs color
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexColorSolid : PQSMod
    {
        /// <summary>
        /// The color the mod applies to the surface
        /// </summary>
        public Color color;

        /// <summary>
        /// How much of the color should end up on the surface
        /// </summary>
        public float blend;

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            data.vertColor = Color.Lerp(data.vertColor, color, blend);
        }
    }
}