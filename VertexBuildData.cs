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
        public Vector3 globalV;
        public Vector3 directionFromCenter;
        public Vector3 directionD;
        public Vector3 directionXZ;
        public int vertIndex;
        public double vertHeight;
        public Color vertColor;
        public double u;
        public double v;
        public double u2;
        public double v2;
        public double gnomonicU;
        public double gnomonicV;
        public bool allowScatter;
        public double latitude;
        public double longitude;
    }
}
