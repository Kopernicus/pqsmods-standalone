using System;
using System.Diagnostics;

namespace LibNoise.Operator
{
    /// <summary>
    /// Provides a noise module that clamps the output value from a source module to a
    /// range of values. [OPERATOR]
    /// </summary>
    public class Clamp : ModuleBase
    {
        #region Fields

        private Double _min = -1.0;
        private Double _max = 1.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Clamp.
        /// </summary>
        public Clamp()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Clamp.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Clamp(ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Clamp.
        /// </summary>
        /// <param name="input">The input module.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public Clamp(Double min, Double max, ModuleBase input)
            : base(1)
        {
            Minimum = min;
            Maximum = max;
            Modules[0] = input;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the maximum to clamp to.
        /// </summary>
        public Double Maximum
        {
            get { return _max; }
            set { _max = value; }
        }

        /// <summary>
        /// Gets or sets the minimum to clamp to.
        /// </summary>
        public Double Minimum
        {
            get { return _min; }
            set { _min = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the bounds.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public void SetBounds(Double min, Double max)
        {
            Debug.Assert(min < max);
            _min = min;
            _max = max;
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
            if (_min > _max)
            {
                var t = _min;
                _min = _max;
                _max = t;
            }
            var v = Modules[0].GetValue(x, y, z);
            if (v < _min)
            {
                return _min;
            }
            if (v > _max)
            {
                return _max;
            }
            return v;
        }

        #endregion
    }
}