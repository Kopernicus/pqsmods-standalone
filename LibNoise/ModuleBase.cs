﻿using System;
using System.Xml.Serialization;
using XnaGeometry;
using Debug = System.Diagnostics.Debug;

namespace LibNoise
{

    #region Enumerations

    /// <summary>
    /// Defines a collection of quality modes.
    /// </summary>
    public enum QualityMode
    {
        Low,
        Medium,
        High,
    }

    #endregion

    /// <summary>
    /// Base class for noise modules.
    /// </summary>
    public abstract class ModuleBase : IDisposable
    {
        #region Fields

        private ModuleBase[] _modules;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Helpers.
        /// </summary>
        /// <param name="count">The number of source modules.</param>
        protected ModuleBase(Int32 count)
        {
            if (count > 0)
            {
                _modules = new ModuleBase[count];
            }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets or sets a source module by index.
        /// </summary>
        /// <param name="index">The index of the source module to aquire.</param>
        /// <returns>The requested source module.</returns>
        public virtual ModuleBase this[Int32 index]
        {
            get
            {
                Debug.Assert(_modules != null);
                Debug.Assert(_modules.Length > 0);
                if (index < 0 || index >= _modules.Length)
                {
                    throw new ArgumentOutOfRangeException("Index out of valid module range");
                }
                if (_modules[index] == null)
                {
                    throw new ArgumentNullException("Desired element is null");
                }
                return _modules[index];
            }
            set
            {
                Debug.Assert(_modules.Length > 0);
                if (index < 0 || index >= _modules.Length)
                {
                    throw new ArgumentOutOfRangeException("Index out of valid module range");
                }
                if (value == null)
                {
                    throw new ArgumentNullException("Value should not be null");
                }
                _modules[index] = value;
            }
        }

        #endregion

        #region Properties
        protected ModuleBase[] Modules
        {
            get { return _modules; }
        }

        /// <summary>
        /// Gets the number of source modules required by this noise module.
        /// </summary>
        public Int32 SourceModuleCount
        {
            get { return (_modules == null) ? 0 : _modules.Length; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public abstract Double GetValue(Double x, Double y, Double z);

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="coordinate">The input coordinate.</param>
        /// <returns>The resulting output value.</returns>
        public Double GetValue(Vector3 coordinate)
        {
            return GetValue(coordinate.X, coordinate.Y, coordinate.Z);
        }

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="coordinate">The input coordinate.</param>
        /// <returns>The resulting output value.</returns>
        public Double GetValue(ref Vector3 coordinate)
        {
            return GetValue(coordinate.X, coordinate.Y, coordinate.Z);
        }

        #endregion

        #region IDisposable Members

        [XmlIgnore]
#if !XBOX360 && !ZUNE
        [NonSerialized]
#endif
        private Boolean _disposed;

        /// <summary>
        /// Gets a value whether the object is disposed.
        /// </summary>
        public Boolean IsDisposed
        {
            get { return _disposed; }
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = Disposing();
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        /// <returns>True if the object is completely disposed.</returns>
        protected virtual Boolean Disposing()
        {
            if (_modules != null)
            {
                for (var i = 0; i < _modules.Length; i++)
                {
                    _modules[i].Dispose();
                    _modules[i] = null;
                }
                _modules = null;
            }
            return true;
        }

        #endregion
    }
}