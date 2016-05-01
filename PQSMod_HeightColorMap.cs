/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System.Linq;
using PQS.Unity;

namespace PQS
{
    /// <summary>
    /// A mod that colors the terrain based on it's altitude
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_HeightColorMap : PQSMod
    {
        /// <summary>
        /// A definition for a landclass, basically a link between altitude and color.
        /// </summary>
        public class LandClass
        {
            /// <summary>
            /// The name of the landclass
            /// </summary>
            public string name;

            /// <summary>
            /// Where the landclass starts
            /// </summary>
            public double altStart;

            /// <summary>
            /// Where the landclass ends
            /// </summary>
            public double altEnd;

            /// <summary>
            /// The color of the surface in this area
            /// </summary>
            public Color color;

            /// <summary>
            /// Whether the color should get lerped into the next landclass
            /// </summary>
            public bool lerpToNext;

            /// <summary>
            /// The difference between <see cref="altStart"/> and <see cref="altEnd"/>
            /// </summary>
            public double fractalDelta
            {
                get { return altEnd - altStart; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="LandClass"/> class.
            /// </summary>
            public LandClass(string name, double fractalStart, double fractalEnd, Color baseColor, double colorNoiseAmount)
            {
                this.name = name;
                this.altStart = fractalStart;
                this.altEnd = fractalEnd;
                this.color = baseColor;
            }
        }

        /// <summary>
        /// The land classes the mod uses
        /// </summary>
        public LandClass[] landClasses;

        /// <summary>
        /// How much the colors should get lerped into each other
        /// </summary>
        public float blend;

        /// <summary>
        /// How many landclasses exist
        /// </summary>
        public int lcCount
        {
            get { return landClasses.Length; }
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            double vHeight = (data.vertHeight - sphere.radiusMin) / sphere.radiusDelta;
            int index;
            LandClass lcSelected = SelectLandClassByHeight(vHeight, out index);
            if (lcSelected.lerpToNext)
            {
                data.vertColor = Color.Lerp(data.vertColor,
                    Color.Lerp(lcSelected.color, landClasses[index + 1].color,
                        (float)((vHeight - lcSelected.altStart) / (lcSelected.altEnd - lcSelected.altStart))), blend);
            }
            else
            {
                data.vertColor = Color.Lerp(data.vertColor, lcSelected.color, blend);
            }
        }

        /// <summary>
        /// Selects a landclass based on it's height
        /// </summary>
        /// <param name="height">The height of the landclass area.</param>
        public LandClass SelectLandClassByHeight(double height, out int index)
        {
            for (int itr = 0; itr < lcCount; itr++)
            {
                index = itr;
                if (height >= landClasses[itr].altStart && height <= landClasses[itr].altEnd)
                    return landClasses[itr];
            }
            index = lcCount - 1;
            return landClasses.Last();
        }
    }
}