/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using PQS.Unity;
using XnaGeometry;

namespace PQS
{
    /// <summary>
    /// A mod that makes a specific area on the planet completely flat (tangent version)
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_FlattenAreaTangential : PQSMod
    {
        /// <summary>
        /// The outer radius of the flatten area
        /// </summary>
        public Double outerRadius;

        /// <summary>
        /// The inner radius of the flatten area
        /// </summary>
        public Double innerRadius;

        /// <summary>
        /// The position of the flatten area, relative to the planet
        /// </summary>
        public Vector3 position;

        /// <summary>
        /// The height of the surface
        /// </summary>
        public double flattenTo;

        /// <summary>
        /// The smooth start of the flattening
        /// </summary>
        public Double smoothStart;

        /// <summary>
        /// The smooth end of the flattening
        /// </summary>
        public Double smoothEnd;

        /// <summary>
        /// The normalized position
        /// </summary>
        private Vector3 positionN;

        /// <summary>
        /// The inner angle for the flatten area
        /// </summary>
        private Double iAngle;

        /// <summary>
        /// The outer angle for the flatten area
        /// </summary>
        private Double oAngle;

        /// <summary>
        /// Sets up the defaults for the Mod
        /// </summary>
        public override void OnSetup()
        {
            positionN = Vector3.Normalize(position);
            iAngle = Math.Atan(innerRadius / sphere.radius);
            oAngle = Math.Atan(outerRadius / sphere.radius);
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            Double angle = Math.Acos(Vector3.Dot(data.directionFromCenter, positionN));

            // Check for outer angle
            if (angle >= oAngle)
                return;

            // Check for inner angle
            if (angle < iAngle)
            {
                data.vertHeight = (sphere.radius + flattenTo) / Math.Cos(angle);
                return;
            }

            // Flatten
            Double delta = (angle - iAngle) / (oAngle - iAngle);
            data.vertHeight = MathX.CubicHermite((sphere.radius + flattenTo) / Math.Cos(angle), data.vertHeight, smoothStart, smoothEnd, delta);
        }
    }
}
