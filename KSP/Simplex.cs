/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using XnaGeometry;

namespace ProceduralQuadSphere
{
    namespace KSP
    {
        /// <summary>
        /// A Simplex. Never look at it.
        /// </summary>
        public class Simplex
        {
            private static readonly Int32[][] grad3;
            private static readonly Int32[] p;
            private Int32[] perm;
            private Double n0;
            private Double n1;
            private Double n2;
            private Double n3;
            private Double F3;
            private Double s;
            private Int32 i;
            private Int32 j;
            private Int32 k;
            private Double G3;
            private Double t;
            private Double X0;
            private Double Y0;
            private Double Z0;
            private Double x0;
            private Double y0;
            private Double z0;
            private Int32 i1;
            private Int32 j1;
            private Int32 k1;
            private Int32 i2;
            private Int32 j2;
            private Int32 k2;
            private Double x1;
            private Double y1;
            private Double z1;
            private Double x2;
            private Double y2;
            private Double z2;
            private Double x3;
            private Double y3;
            private Double z3;
            private Int32 ii;
            private Int32 jj;
            private Int32 kk;
            private Int32 gi0;
            private Int32 gi1;
            private Int32 gi2;
            private Int32 gi3;
            private Double t0;
            private Double t1;
            private Double t2;
            private Double t3;
            private Double itr;
            private Double total;
            private Double amplitude;
            private Double maxAmplitude;
            private Double f;

            /// <summary>
            /// The octaves of this simpley
            /// </summary>
            public Double octaves;

            /// <summary>
            /// The persistence of this simplex
            /// </summary>
            public Double persistence;

            /// <summary>
            /// How frequent this simplex is
            /// </summary>
            public Double frequency;

            /// <summary>
            /// Sets the seed of the simplex
            /// </summary>
            public Int32 seed
            {
                set
                {
                    Random random = new Random(value);
                    for (Int32 i = 0; i < 255; i++)
                    {
                        p[i] = random.Next(0, 255);
                    }
                    SetupPermTable();
                }
            }

            static Simplex()
            {
                grad3 = new[] {new[] {1, 1, 0}, new[] {-1, 1, 0}, new[] {1, -1, 0}, new[] {-1, -1, 0}, new[] {1, 0, 1}, new[] {-1, 0, 1}, new[] {1, 0, -1}, new[] {-1, 0, -1}, new[] {0, 1, 1}, new[] {0, -1, 1}, new[] {0, 1, -1}, new[] {0, -1, -1}};
                p = new[] {151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180};
            }

            public Simplex()
            {
                SetupPermTable();
            }

            public Simplex(Int32 seed)
            {
                this.seed = seed;
            }

            public Simplex(Int32 seed, Double octaves, Double persistence, Double frequency)
            {
                this.seed = seed;
                this.octaves = octaves;
                this.persistence = persistence;
                this.frequency = frequency;
            }

            private static Double dot(Int32[] g, Double x, Double y)
            {
                return g[0]*x + g[1]*y;
            }

            private static Double dot(Int32[] g, Double x, Double y, Double z)
            {
                return g[0]*x + g[1]*y + g[2]*z;
            }

            private static Double dot(Int32[] g, Double x, Double y, Double z, Double w)
            {
                return g[0]*x + g[1]*y + g[2]*z + g[3]*w;
            }

            private static Int32 fastfloor(Double x)
            {
                return (x <= 0 ? (Int32) x - 1 : (Int32) x);
            }

            public Double noise(Vector3 v3d)
            {
                return noise(v3d.X, v3d.Y, v3d.Z);
            }

            public Double noise(Double x, Double y, Double z)
            {
                total = 0;
                amplitude = 1;
                f = frequency;
                maxAmplitude = 0;
                itr = 0;
                while (itr < octaves)
                {
                    Simplex simplex = this;
                    simplex.total = simplex.total + @value(x*f, y*f, z*f)*amplitude;
                    Simplex simplex1 = this;
                    simplex1.f = simplex1.f*2;
                    Simplex simplex2 = this;
                    simplex2.maxAmplitude = simplex2.maxAmplitude + amplitude;
                    Simplex simplex3 = this;
                    simplex3.amplitude = simplex3.amplitude*persistence;
                    Simplex simplex4 = this;
                    simplex4.itr = simplex4.itr + 1;
                }
                return total/maxAmplitude;
            }

            public Double noiseNormalized(Vector3 v3d)
            {
                return (noise(v3d.X, v3d.Y, v3d.Z) + 1)*0.5;
            }

            public Double noiseNormalized(Double x, Double y, Double z)
            {
                return (noise(x, y, z) + 1)*0.5;
            }

            private void SetupPermTable()
            {
                perm = new Int32[512];
                for (Int32 i = 0; i < 512; i++)
                {
                    perm[i] = p[i & 255];
                }
            }

            private Double @value(Double xin, Double yin, Double zin)
            {
                F3 = 0.333333333333333;
                s = (xin + yin + zin)*F3;
                i = fastfloor(xin + s);
                j = fastfloor(yin + s);
                k = fastfloor(zin + s);
                G3 = 0.166666666666667;
                t = (i + j + k)*G3;
                X0 = i - t;
                Y0 = j - t;
                Z0 = k - t;
                x0 = xin - X0;
                y0 = yin - Y0;
                z0 = zin - Z0;
                if (x0 >= y0)
                {
                    if (y0 >= z0)
                    {
                        i1 = 1;
                        j1 = 0;
                        k1 = 0;
                        i2 = 1;
                        j2 = 1;
                        k2 = 0;
                    }
                    else if (x0 < z0)
                    {
                        i1 = 0;
                        j1 = 0;
                        k1 = 1;
                        i2 = 1;
                        j2 = 0;
                        k2 = 1;
                    }
                    else
                    {
                        i1 = 1;
                        j1 = 0;
                        k1 = 0;
                        i2 = 1;
                        j2 = 0;
                        k2 = 1;
                    }
                }
                else if (y0 < z0)
                {
                    i1 = 0;
                    j1 = 0;
                    k1 = 1;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                }
                else if (x0 >= z0)
                {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 1;
                    j2 = 1;
                    k2 = 0;
                }
                else
                {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                }
                x1 = x0 - i1 + G3;
                y1 = y0 - j1 + G3;
                z1 = z0 - k1 + G3;
                x2 = x0 - i2 + 2*G3;
                y2 = y0 - j2 + 2*G3;
                z2 = z0 - k2 + 2*G3;
                x3 = x0 - 1 + 3*G3;
                y3 = y0 - 1 + 3*G3;
                z3 = z0 - 1 + 3*G3;
                ii = i & 255;
                jj = j & 255;
                kk = k & 255;
                gi0 = perm[ii + perm[jj + perm[kk]]]%12;
                gi1 = perm[ii + i1 + perm[jj + j1 + perm[kk + k1]]]%12;
                gi2 = perm[ii + i2 + perm[jj + j2 + perm[kk + k2]]]%12;
                gi3 = perm[ii + 1 + perm[jj + 1 + perm[kk + 1]]]%12;
                t0 = 0.6 - x0*x0 - y0*y0 - z0*z0;
                if (t0 >= 0)
                {
                    Simplex simplex = this;
                    simplex.t0 = simplex.t0*t0;
                    n0 = t0*t0*dot(grad3[gi0], x0, y0, z0);
                }
                else
                {
                    n0 = 0;
                }
                t1 = 0.6 - x1*x1 - y1*y1 - z1*z1;
                if (t1 >= 0)
                {
                    Simplex simplex1 = this;
                    simplex1.t1 = simplex1.t1*t1;
                    n1 = t1*t1*dot(grad3[gi1], x1, y1, z1);
                }
                else
                {
                    n1 = 0;
                }
                t2 = 0.6 - x2*x2 - y2*y2 - z2*z2;
                if (t2 >= 0)
                {
                    Simplex simplex2 = this;
                    simplex2.t2 = simplex2.t2*t2;
                    n2 = t2*t2*dot(grad3[gi2], x2, y2, z2);
                }
                else
                {
                    n2 = 0;
                }
                t3 = 0.6 - x3*x3 - y3*y3 - z3*z3;
                if (t3 >= 0)
                {
                    Simplex simplex3 = this;
                    simplex3.t3 = simplex3.t3*t3;
                    n3 = t3*t3*dot(grad3[gi3], x3, y3, z3);
                }
                else
                {
                    n3 = 0;
                }
                return 32*(n0 + n1 + n2 + n3);
            }
        }
    }
}