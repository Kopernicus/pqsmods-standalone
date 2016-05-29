using System;
using System.Collections.Generic;
using PQS.Unity;
using Debug = System.Diagnostics.Debug;

namespace LibNoise.Operator
{
    /// <summary>
    /// Provides a noise module that maps the output value from a source module onto an
    /// arbitrary function curve. [OPERATOR]
    /// </summary>
    public class Curve : ModuleBase
    {
        #region Fields

        private readonly List<KeyValuePair<Double, Double>> _data = new List<KeyValuePair<Double, Double>>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Curve.
        /// </summary>
        public Curve()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Curve.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Curve(ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of control points.
        /// </summary>
        public Int32 ControlPointCount
        {
            get { return _data.Count; }
        }

        /// <summary>
        /// Gets the list of control points.
        /// </summary>
        public List<KeyValuePair<Double, Double>> ControlPoints
        {
            get { return _data; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a control point to the curve.
        /// </summary>
        /// <param name="input">The curves input value.</param>
        /// <param name="output">The curves output value.</param>
        public void Add(Double input, Double output)
        {
            var kvp = new KeyValuePair<Double, Double>(input, output);
            if (!_data.Contains(kvp))
            {
                _data.Add(kvp);
            }
            _data.Sort(
                delegate(KeyValuePair<Double, Double> lhs, KeyValuePair<Double, Double> rhs)
                {
                    return lhs.Key.CompareTo(rhs.Key);
                });
        }

        /// <summary>
        /// Clears the control points.
        /// </summary>
        public void Clear()
        {
            _data.Clear();
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
            Debug.Assert(ControlPointCount >= 4);
            var smv = Modules[0].GetValue(x, y, z);
            Int32 ip;
            for (ip = 0; ip < _data.Count; ip++)
            {
                if (smv < _data[ip].Key)
                {
                    break;
                }
            }
            var i0 = Mathf.Clamp(ip - 2, 0, _data.Count - 1);
            var i1 = Mathf.Clamp(ip - 1, 0, _data.Count - 1);
            var i2 = Mathf.Clamp(ip, 0, _data.Count - 1);
            var i3 = Mathf.Clamp(ip + 1, 0, _data.Count - 1);
            if (i1 == i2)
            {
                return _data[i1].Value;
            }
            //double ip0 = _data[i1].Value;
            //double ip1 = _data[i2].Value;
            var ip0 = _data[i1].Key;
            var ip1 = _data[i2].Key;
            var a = (smv - ip0) / (ip1 - ip0);
            return Utils.InterpolateCubic(_data[i0].Value, _data[i1].Value, _data[i2].Value,
                _data[i3].Value, a);
        }

        #endregion
    }
}