/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */
 
using PQS.Unity;
using XnaGeometry;

namespace PQS
{
    /// <summary>
    /// A structure that defines the exact position that the <see cref="PQSMod"/>
    /// should build. Some parameters have been stripped from the KSP
    /// version because they can't be used without a complete PQS.
    /// </summary>
    public class VertexBuildData
    {
        public Vector3 globalV { get; set; }
        public Vector3 directionFromCenter { get; set; }
        public Vector3 directionD { get; set; }
        public Vector3 directionXZ { get; set; }
        public int vertIndex { get; set; }
        public double vertHeight { get; set; }
        public Color vertColor { get; set; }
        public double u { get; set; }
        public double v { get; set; }
        public double u2 { get; set; }
        public double v2 { get; set; }
        public double gnomonicU { get; set; }
        public double gnomonicV { get; set; }
        public bool allowScatter { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
