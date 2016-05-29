/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PQS.KSP;
using PQS.Unity;
using XnaGeometry;

namespace PQS
{
    /// <summary>
    /// A fake implementation of the PQS, to support things like radius to be centralized 
    /// </summary>
    public class PQS
    {
        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        public Double radius { get; set; }

        /// <summary>
        /// The lowest point of the sphere.
        /// </summary>
        public Double radiusMin { get; set; }
        
        /// <summary>
        /// The highest point of the sphere.
        /// </summary>
        public Double radiusMax { get; set; }

        /// <summary>
        /// The delta of <see cref="radiusMin"/> and <see cref="radiusMax"/>
        /// </summary>
        public Double radiusDelta
        {
            get { return radiusMax - radiusMin; }
        }

        /// <summary>
        /// The mods attached to this sphere.
        /// </summary>
        public ReadOnlyCollection<PQSMod> mods { get; protected set; }

        /// <summary>
        /// Initializes a new PQS.
        /// </summary>
        /// <param name="radius">The radius of the sphere.</param>
        public PQS(Double radius)
        {
            this.radius = radius;
            mods = new ReadOnlyCollection<PQSMod>(new List<PQSMod>());
        }

        /// <summary>
        /// Setups the sphere.
        /// </summary>
        public void SetupSphere()
        {
            List<PQSMod> mods_ = new List<PQSMod>(mods);
            mods_.Sort((a, b) => a.order.CompareTo(b.order));
            if (!mods_.Any())
                return;
            for (Int32 i = 0; i < mods_.Count; i++)
            {
                if (!mods_[i].modEnabled)
                {
                    mods_.RemoveAt(i);
                }
                mods_[i].sphere = this;
            }
            mods = mods_.AsReadOnly();
            OnSetup();
            radiusMin = 0;
            radiusMax = 0;
            foreach (PQSMod mod in mods)
            {
                radiusMin += mod.GetVertexMinHeight();
                radiusMax += mod.GetVertexMaxHeight();
            }
        }

        /// <summary>
        /// Calls OnSetup in all Mods
        /// </summary>
        public void OnSetup()
        {
            foreach (PQSMod mod in mods)
                mod.OnSetup();
        }

        /// <summary>
        /// Call OnVertexBuildHeight in all Mods
        /// </summary>
        /// <param name="data">The data.</param>
        public void OnVertexBuildHeight(VertexBuildData data)
        {
            // Build VertexBuildData
            data.latitude = Math.Asin(data.directionFromCenter.Y);
            if (Double.IsNaN(data.latitude))
                data.latitude = Math.PI / 2;
            data.directionXZ = Vector3.Normalize(new Vector3(data.directionFromCenter.X, 0, data.directionFromCenter.Z));
            if (data.directionXZ.Length() <= 0)
                data.longitude = 0;
            else
                data.longitude = data.directionXZ.Z >= 0 ? Math.Asin(data.directionXZ.X / data.directionXZ.Length()) : Math.PI - Math.Asin(data.directionXZ.X / data.directionXZ.Length());
            data.v = data.latitude / Math.PI + 0.5;
            data.u = data.longitude / Math.PI * 0.5;

            foreach (PQSMod mod in mods)
                mod.OnVertexBuildHeight(data);
        }

        /// <summary>
        /// Call OnVertexBuild in all Mods
        /// </summary>
        /// <param name="data">The data.</param>
        public void OnVertexBuild(VertexBuildData data)
        {
            // Build VertexBuildData
            data.latitude = Math.Asin(data.directionFromCenter.Y);
            if (Double.IsNaN(data.latitude))
                data.latitude = Math.PI / 2;
            data.directionXZ = Vector3.Normalize(new Vector3(data.directionFromCenter.X, 0, data.directionFromCenter.Z));
            if (data.directionXZ.Length() <= 0)
                data.longitude = 0;
            else
                data.longitude = data.directionXZ.Z >= 0 ? Math.Asin(data.directionXZ.X / data.directionXZ.Length()) : Math.PI - Math.Asin(data.directionXZ.X / data.directionXZ.Length());
            data.v = data.latitude / Math.PI + 0.5;
            data.u = data.longitude / Math.PI * 0.5;

            foreach (PQSMod mod in mods)
                mod.OnVertexBuild(data);
        }

        /// <summary>
        /// Adds a new PQSMod of type T to the sphere
        /// </summary>
        public T AddPQSMod<T>() where T : PQSMod, new()
        {
            T mod = new T();
            List<PQSMod> m_ = new List<PQSMod>(mods);
            m_.Add(mod);
            mods = m_.AsReadOnly();
            return mod;
        }

        /// <summary>
        /// Adds a new PQSMod of type T to the sphere
        /// </summary>
        public T AddPQSMod<T>(String name) where T : PQSMod, new()
        {
            T mod = AddPQSMod<T>();
            mod.name = name;
            return mod;
        }

        /// <summary>
        /// Returns the first PQSMod with the given name, or null if nothing was found.
        /// </summary>
        public T GetPQSMod<T>(String name) where T : PQSMod, new()
        {
            if (mods.Any(m => m.name == name && m is T))
                return (T)mods.FirstOrDefault(m => m.name == name && m is T);
            else
                return default(T);
        }

        /// <summary>
        /// Returns the PQSMods with the given type, or null if nothing was found.
        /// </summary>
        public T[] GetPQSMods<T>() where T : PQSMod, new()
        {
            if (mods.Any(m => m is T))
                return (T[])mods.Where(m => m is T).ToArray();
            else
                return default(T[]);
        }

        /// <summary>
        /// Returns the PQSMods with the given name and type, or null if nothing was found.
        /// </summary>
        public T[] GetPQSMods<T>(String name) where T : PQSMod, new()
        {
            if (mods.Any(m => m.name == name && m is T))
                return (T[])mods.Where(m => m.name == name && m is T).ToArray();
            else
                return default(T[]);
        }

        /// <summary>
        /// Removes the given PQSMod from the sphere
        /// </summary>
        public void RemovePQSMod<T>(T mod) where T : PQSMod, new()
        {
            List<PQSMod> m_ = new List<PQSMod>(mods);
            m_.Remove(mod);
            mods = m_.AsReadOnly();
        }
    }
}
