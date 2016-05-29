/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using ProceduralQuadSphere.Unity;
using XnaGeometry;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A structure that defines the exact position that the <see cref="PQSMod"/>
    /// should build. Some parameters have been stripped from the KSP
    /// version because they can't be used without a complete PQS.
    /// </summary>
    public class VertexBuildData
    {
        public Vector3 globalV;
        public Vector3 directionFromCenter;
        public Vector3 directionD;
        public Vector3 directionXZ;
        public Int32 vertIndex;
        public Double vertHeight;
        public Color vertColor;
        public Double u;
        public Double v;
        public Double u2;
        public Double v2;
        public Double gnomonicU;
        public Double gnomonicV;
        public Boolean allowScatter;
        public Double latitude;
        public Double longitude;
    }
}
