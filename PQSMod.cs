/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;

namespace PQS
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

        #region Virtuals
        public virtual void OnSetup() { }
        public virtual void OnPostSetup() { }
        public virtual void OnSphereReset() { }
        public virtual void OnSphereActive() { }
        public virtual void OnSphereInactive() { }
        public virtual Boolean OnSphereStart() { return false; }
        public virtual void OnSphereStarted() { }
        public virtual void OnSphereTransformUpdate() { }
        public virtual void OnPreUpdate() { }
        public virtual void OnUpdateFinished() { }
        public virtual void OnVertexBuild(VertexBuildData data) { }
        public virtual void OnVertexBuildHeight(VertexBuildData data) { }
        public virtual Double GetVertexMaxHeight() { return 0d; }
        public virtual double GetVertexMinHeight() { return 0d; }
        public virtual void OnMeshBuild() { }
        #endregion
    }
}
