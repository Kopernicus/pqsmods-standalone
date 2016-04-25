/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        public double radius { get; set; }

        /// <summary>
        /// The mods attached to this sphere.
        /// </summary>
        public ReadOnlyCollection<PQSMod> mods { get; protected set; }

        /// <summary>
        /// Initializes a new PQS.
        /// </summary>
        /// <param name="radius">The radius of the sphere.</param>
        public PQS(double radius)
        {
            this.radius = radius;
            mods = new ReadOnlyCollection<PQSMod>(new List<PQSMod>());
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
        public T AddPQSMod<T>(string name) where T : PQSMod, new()
        {
            T mod = AddPQSMod<T>();
            mod.name = name;
            return mod;
        }

        /// <summary>
        /// Returns the first PQSMod with the given name, or null if nothing was found.
        /// </summary>
        public T GetPQSMod<T>(string name) where T : PQSMod, new()
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
        public T[] GetPQSMods<T>(string name) where T : PQSMod, new()
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
