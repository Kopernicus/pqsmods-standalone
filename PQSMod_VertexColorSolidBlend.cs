/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using PQS.Unity;

namespace PQS
{
    /// <summary>
    /// A PQSMod that applies a solid color to the pqs color.
    /// Funnily, this mod has blend in it's name, while it doesn't support blend, unless the version withhout blend in the name.
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexColorSolidBlend : PQSMod
    {
        /// <summary>
        /// The color the mod applies to the surface
        /// </summary>
        public Color color;

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            data.vertColor = color;
        }
    }
}
