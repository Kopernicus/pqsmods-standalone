/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
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
            public String name;

            /// <summary>
            /// Where the landclass starts
            /// </summary>
            public Double altStart;

            /// <summary>
            /// Where the landclass ends
            /// </summary>
            public Double altEnd;

            /// <summary>
            /// The color of the surface in this area
            /// </summary>
            public Color color;

            /// <summary>
            /// Whether the color should get lerped into the next landclass
            /// </summary>
            public Boolean lerpToNext;

            /// <summary>
            /// The difference between <see cref="altStart"/> and <see cref="altEnd"/>
            /// </summary>
            public Double fractalDelta
            {
                get { return altEnd - altStart; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="LandClass"/> class.
            /// </summary>
            public LandClass(String name, Double fractalStart, Double fractalEnd, Color baseColor)
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
        public Single blend;

        /// <summary>
        /// How many landclasses exist
        /// </summary>
        public Int32 lcCount
        {
            get { return landClasses.Length; }
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            Double vHeight = (data.vertHeight - sphere.radiusMin) / sphere.radiusDelta;
            Int32 index;
            LandClass lcSelected = SelectLandClassByHeight(vHeight, out index);
            if (lcSelected.lerpToNext)
            {
                data.vertColor = Color.Lerp(data.vertColor,
                    Color.Lerp(lcSelected.color, landClasses[index + 1].color,
                        (Single)((vHeight - lcSelected.altStart) / (lcSelected.altEnd - lcSelected.altStart))), blend);
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
        public LandClass SelectLandClassByHeight(Double height, out Int32 index)
        {
            for (Int32 itr = 0; itr < lcCount; itr++)
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