using System;

namespace LibNoise.Generator
{
    /// <summary>
    /// Provides a noise module that outputs Voronoi cells. [GENERATOR]
    /// </summary>
    public class VoronoiCached : ModuleBase
    {
        #region Fields

        private Double _displacement = 1.0;
        private Double _frequency = 1.0;
        private Int32 _seed;
        private Boolean _distance;
        private VoronoiCube[,,] cubes;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Voronoi.
        /// </summary>
        public VoronoiCached()
            : base(0)
        {
            ConstructCube();
        }

        /// <summary>
        /// Initializes a new instance of Voronoi.
        /// </summary>
        /// <param name="frequency">The frequency of the first octave.</param>
        /// <param name="displacement">The displacement of the ridged-multifractal noise.</param>
        /// <param name="seed">The seed of the ridged-multifractal noise.</param>
        /// <param name="distance">Indicates whether the distance from the nearest seed point is applied to the output value.</param>
        public VoronoiCached(Double frequency, Double displacement, Int32 seed, Boolean distance)
            : this()
        {
            Frequency = frequency;
            Displacement = displacement;
            Seed = seed;
            UseDistance = distance;
            Seed = seed;
            ConstructCube();
        }

        private void ConstructCube()
        {
            Int32 maxCorner = (Int32)(Frequency + 3);
            cubes = new VoronoiCube[maxCorner * 2, maxCorner * 2, maxCorner * 2];
            for (Int32 zcu = 0; zcu < maxCorner * 2; zcu++)
            {
                for (Int32 ycu = 0; ycu < maxCorner * 2; ycu++)
                {
                    for (Int32 xcu = 0; xcu < maxCorner * 2; xcu++)
                        cubes[xcu, ycu, zcu] = new VoronoiCube(xcu - maxCorner, ycu - maxCorner, zcu - maxCorner, Seed);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displacement value of the Voronoi cells.
        /// </summary>
        public Double Displacement
        {
            get { return _displacement; }
            set { _displacement = value; }
        }

        /// <summary>
        /// Gets or sets the frequency of the seed points.
        /// </summary>
        public Double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        /// <summary>
        /// Gets or sets the seed value used by the Voronoi cells.
        /// </summary>
        public Int32 Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }

        /// <summary>
        /// Gets or sets a value whether the distance from the nearest seed point is applied to the output value.
        /// </summary>
        public Boolean UseDistance
        {
            get { return _distance; }
            set { _distance = value; }
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
            Int32 maxCorner = (Int32)(Frequency + 3);
            x *= _frequency;
            y *= _frequency;
            z *= _frequency;
            var xi = (x > 0.0 ? (Int32) x : (Int32) x - 1);
            var iy = (y > 0.0 ? (Int32) y : (Int32) y - 1);
            var iz = (z > 0.0 ? (Int32) z : (Int32) z - 1);
            var md = 2147483647.0;
            Double xc = 0;
            Double yc = 0;
            Double zc = 0;
            for (var zcu = iz - 2; zcu <= iz + 2; zcu++)
            {
                for (var ycu = iy - 2; ycu <= iy + 2; ycu++)
                {
                    for (var xcu = xi - 2; xcu <= xi + 2; xcu++)
                    {
                        var cube = cubes[xcu + maxCorner, ycu + maxCorner, zcu + maxCorner];
                        var xd = cube.xp - x;
                        var yd = cube.yp - y;
                        var zd = cube.zp - z;
                        var d = xd * xd + yd * yd + zd * zd;
                        if (d < md)
                        {
                            md = d;
                            xc = cube.xp;
                            yc = cube.yp;
                            zc = cube.zp;
                        }
                    }
                }
            }
            Double v;
            if (_distance)
            {
                var xd = xc - x;
                var yd = yc - y;
                var zd = zc - z;
                v = (Math.Sqrt(xd * xd + yd * yd + zd * zd)) * Utils.Sqrt3 - 1.0;
            }
            else
            {
                v = 0.0;
            }
            return v + (_displacement * Utils.ValueNoise3D((Int32) (Math.Floor(xc)), (Int32) (Math.Floor(yc)),
                (Int32) (Math.Floor(zc)), 0));
        }

        #endregion

        private class VoronoiCube
        {
            public Double xp;

            public Double yp;

            public Double zp;

            public VoronoiCube(Int32 x, Int32 y, Int32 z, Int32 seed)
            {
                this.xp = (Double)x + Utils.ValueNoise3D(x, y, z, seed);
                this.yp = (Double)y + Utils.ValueNoise3D(x, y, z, seed + 1);
                this.zp = (Double)z + Utils.ValueNoise3D(x, y, z, seed + 2);
            }
        }
    }
}