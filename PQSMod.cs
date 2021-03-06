﻿/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// Implementation of the base PQSMod type. Does nothing on it's own.
    /// </summary>
    public abstract class PQSMod
    {
        /// <summary>
        /// Whether the mod should be taken into account for building something
        /// </summary>
        public Boolean modEnabled = true;

        /// <summary>
        /// The position of the mod in the build queue
        /// </summary>
        public Int32 order = 100;

        /// <summary>
        /// The sphere the PQSMod is attached to
        /// </summary>
        public PQS sphere;

        /// <summary>
        /// The name of the PQSMod
        /// </summary>
        public String name { get; set; }

        #region Virtuals
        public virtual void OnSetup() { }
        public virtual void OnVertexBuild(VertexBuildData data) { }
        public virtual void OnVertexBuildHeight(VertexBuildData data) { }
        public virtual Double GetVertexMaxHeight() { return 0; }
        public virtual Double GetVertexMinHeight() { return 0; }
        #endregion
    }
}
