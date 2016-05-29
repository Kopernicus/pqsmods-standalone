using System;
using ProceduralQuadSphere.Unity;
using Debug = System.Diagnostics.Debug;

namespace LibNoise.Operator
{
    /// <summary>
    /// Provides a noise module that rotates the input value around the origin before
    /// returning the output value from a source module. [OPERATOR]
    /// </summary>
    public class Rotate : ModuleBase
    {
        #region Fields

        private Double _x;
        private Double _x1Matrix;
        private Double _x2Matrix;
        private Double _x3Matrix;
        private Double _y;
        private Double _y1Matrix;
        private Double _y2Matrix;
        private Double _y3Matrix;
        private Double _z;
        private Double _z1Matrix;
        private Double _z2Matrix;
        private Double _z3Matrix;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Rotate.
        /// </summary>
        public Rotate()
            : base(1)
        {
            SetAngles(0.0, 0.0, 0.0);
        }

        /// <summary>
        /// Initializes a new instance of Rotate.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Rotate(ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Rotate.
        /// </summary>
        /// <param name="x">The rotation around the x-axis.</param>
        /// <param name="y">The rotation around the y-axis.</param>
        /// <param name="z">The rotation around the z-axis.</param>
        /// <param name="input">The input module.</param>
        public Rotate(Double x, Double y, Double z, ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
            SetAngles(x, y, z);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the rotation around the x-axis in degree.
        /// </summary>
        public Double X
        {
            get { return _x; }
            set { SetAngles(value, _y, _z); }
        }

        /// <summary>
        /// Gets or sets the rotation around the y-axis in degree.
        /// </summary>
        public Double Y
        {
            get { return _y; }
            set { SetAngles(_x, value, _z); }
        }

        /// <summary>
        /// Gets or sets the rotation around the z-axis in degree.
        /// </summary>
        public Double Z
        {
            get { return _x; }
            set { SetAngles(_x, _y, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the rotation angles.
        /// </summary>
        /// <param name="x">The rotation around the x-axis.</param>
        /// <param name="y">The rotation around the y-axis.</param>
        /// <param name="z">The rotation around the z-axis.</param>
        private void SetAngles(Double x, Double y, Double z)
        {
            var xc = Math.Cos(x * Mathf.Deg2Rad);
            var yc = Math.Cos(y * Mathf.Deg2Rad);
            var zc = Math.Cos(z * Mathf.Deg2Rad);
            var xs = Math.Sin(x * Mathf.Deg2Rad);
            var ys = Math.Sin(y * Mathf.Deg2Rad);
            var zs = Math.Sin(z * Mathf.Deg2Rad);
            _x1Matrix = ys * xs * zs + yc * zc;
            _y1Matrix = xc * zs;
            _z1Matrix = ys * zc - yc * xs * zs;
            _x2Matrix = ys * xs * zc - yc * zs;
            _y2Matrix = xc * zc;
            _z2Matrix = -yc * xs * zc - ys * zs;
            _x3Matrix = -ys * xc;
            _y3Matrix = xs;
            _z3Matrix = yc * xc;
            _x = x;
            _y = y;
            _z = z;
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
            var nx = (_x1Matrix * x) + (_y1Matrix * y) + (_z1Matrix * z);
            var ny = (_x2Matrix * x) + (_y2Matrix * y) + (_z2Matrix * z);
            var nz = (_x3Matrix * x) + (_y3Matrix * y) + (_z3Matrix * z);
            return Modules[0].GetValue(nx, ny, nz);
        }

        #endregion
    }
}