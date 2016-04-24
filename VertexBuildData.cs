/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using System.Drawing;
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
        public Int32 vertIndex { get; set; }
        public Double vertHeight { get; set; }
        public Color vertColor { get; set; }
        public Double u { get; set; }
        public Double v { get; set; }
        public Double u2 { get; set; }
        public Double v2 { get; set; }
        public Double gnomonicU { get; set; }
        public Double gnomonicV { get; set; }
        public Boolean allowScatter { get; set; }
        public Double latitude { get; set; }
        public Double longitude { get; set; }
    }
}
