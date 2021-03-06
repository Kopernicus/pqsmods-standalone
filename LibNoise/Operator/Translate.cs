﻿using System;
using System.Diagnostics;

namespace LibNoise.Operator
{
    /// <summary>
    /// Provides a noise module that moves the coordinates of the input value before
    /// returning the output value from a source module. [OPERATOR]
    /// </summary>
    public class Translate : ModuleBase
    {
        #region Fields

        private Double _x = 1.0;
        private Double _y = 1.0;
        private Double _z = 1.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Translate.
        /// </summary>
        public Translate()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Translate.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Translate(ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Translate.
        /// </summary>
        /// <param name="x">The translation on the x-axis.</param>
        /// <param name="y">The translation on the y-axis.</param>
        /// <param name="z">The translation on the z-axis.</param>
        /// <param name="input">The input module.</param>
        public Translate(Double x, Double y, Double z, ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the translation on the x-axis.
        /// </summary>
        public Double X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Gets or sets the translation on the y-axis.
        /// </summary>
        public Double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Gets or sets the translation on the z-axis.
        /// </summary>
        public Double Z
        {
            get { return _z; }
            set { _z = value; }
        }

        #endregion

        #region ModuleBase Members

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public override Double GetValue(Double x, Double y, Double z)
        {
            Debug.Assert(Modules[0] != null);
            return Modules[0].GetValue(x + _x, y + _y, z + _z);
        }

        #endregion
    }
}