#region License
/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
#if WINRT
using System.Runtime.Serialization;
#endif

namespace XnaGeometry
{
#if WINRT
    [DataContract]
#else
    [Serializable]
#endif
    public struct Quaternion : IEquatable<Quaternion>
    {
#if WINRT
        [DataMember]
#endif
        public Double X;
#if WINRT
        [DataMember]
#endif
        public Double Y;
#if WINRT
        [DataMember]
#endif
        public Double Z;
#if WINRT
        [DataMember]
#endif
        public Double W;

        static Quaternion identity = new Quaternion(0, 0, 0, 1);


        public Quaternion(Double x, Double y, Double z, Double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }


        public Quaternion(Vector3 vectorPart, Double scalarPart)
        {
            this.X = vectorPart.X;
            this.Y = vectorPart.Y;
            this.Z = vectorPart.Z;
            this.W = scalarPart;
        }

        public static Quaternion Identity
        {
            get { return identity; }
        }


        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
        {
            //Syderis
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }


        public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            //Syderis
            result.X = quaternion1.X + quaternion2.X;
            result.Y = quaternion1.Y + quaternion2.Y;
            result.Z = quaternion1.Z + quaternion2.Z;
            result.W = quaternion1.W + quaternion2.W;
        }

        //Funcion añadida Syderis
        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
        {
            Quaternion quaternion;
            Double x = value2.X;
            Double y = value2.Y;
            Double z = value2.Z;
            Double w = value2.W;
            Double num4 = value1.X;
            Double num3 = value1.Y;
            Double num2 = value1.Z;
            Double num = value1.W;
            Double num12 = (y * num2) - (z * num3);
            Double num11 = (z * num4) - (x * num2);
            Double num10 = (x * num3) - (y * num4);
            Double num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;

        }

        //Añadida por Syderis
        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Double x = value2.X;
            Double y = value2.Y;
            Double z = value2.Z;
            Double w = value2.W;
            Double num4 = value1.X;
            Double num3 = value1.Y;
            Double num2 = value1.Z;
            Double num = value1.W;
            Double num12 = (y * num2) - (z * num3);
            Double num11 = (z * num4) - (x * num2);
            Double num10 = (x * num3) - (y * num4);
            Double num9 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num12;
            result.Y = ((y * num) + (num3 * w)) + num11;
            result.Z = ((z * num) + (num2 * w)) + num10;
            result.W = (w * num) - num9;
        }

        //Añadida por Syderis
        public void Conjugate()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        //Añadida por Syderis
        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion quaternion;
            quaternion.X = -value.X;
            quaternion.Y = -value.Y;
            quaternion.Z = -value.Z;
            quaternion.W = value.W;
            return quaternion;
        }

        //Añadida por Syderis
        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, Double angle)
        {

            Quaternion quaternion;
            Double num2 = angle * 0.5f;
            Double num = (Double)Math.Sin((Double)num2);
            Double num3 = (Double)Math.Cos((Double)num2);
            quaternion.X = axis.X * num;
            quaternion.Y = axis.Y * num;
            quaternion.Z = axis.Z * num;
            quaternion.W = num3;
            return quaternion;

        }

        public static Quaternion CreateFromAngleAxis(Double angle, Vector3 axis)
        {
            Double X;
            Double Y;
            Double Z;
            Double W;
            Double magnitude = axis.Length();
            if (magnitude <= 0.0001)
            {
                W = 1;
                X = 0;
                Y = 0;
                Z = 0;
            }
            else
            {
                Double cos = Math.Cos(angle * (Math.PI / 180d) / 2);
                Double sin = Math.Sin(angle * (Math.PI / 180d) / 2);
                X = axis.X / magnitude * sin;
                Y = axis.Y / magnitude * sin;
                Z = axis.Z / magnitude * sin;
                W = cos;
            }
            return new Quaternion(X, Y, Z, W);
        }


        public static void CreateFromAxisAngle(ref Vector3 axis, Double angle, out Quaternion result)
        {
            Double num2 = angle * 0.5f;
            Double num = (Double)Math.Sin((Double)num2);
            Double num3 = (Double)Math.Cos((Double)num2);
            result.X = axis.X * num;
            result.Y = axis.Y * num;
            result.Z = axis.Z * num;
            result.W = num3;

        }


        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
            Double num8 = (matrix.M11 + matrix.M22) + matrix.M33;
            Quaternion quaternion = new Quaternion();
            if (num8 > 0f)
            {
                Double num = (Double)Math.Sqrt((Double)(num8 + 1f));
                quaternion.W = num * 0.5f;
                num = 0.5f / num;
                quaternion.X = (matrix.M23 - matrix.M32) * num;
                quaternion.Y = (matrix.M31 - matrix.M13) * num;
                quaternion.Z = (matrix.M12 - matrix.M21) * num;
                return quaternion;
            }
            if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                Double num7 = (Double)Math.Sqrt((Double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
                Double num4 = 0.5f / num7;
                quaternion.X = 0.5f * num7;
                quaternion.Y = (matrix.M12 + matrix.M21) * num4;
                quaternion.Z = (matrix.M13 + matrix.M31) * num4;
                quaternion.W = (matrix.M23 - matrix.M32) * num4;
                return quaternion;
            }
            if (matrix.M22 > matrix.M33)
            {
                Double num6 = (Double)Math.Sqrt((Double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
                Double num3 = 0.5f / num6;
                quaternion.X = (matrix.M21 + matrix.M12) * num3;
                quaternion.Y = 0.5f * num6;
                quaternion.Z = (matrix.M32 + matrix.M23) * num3;
                quaternion.W = (matrix.M31 - matrix.M13) * num3;
                return quaternion;
            }
            Double num5 = (Double)Math.Sqrt((Double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
            Double num2 = 0.5f / num5;
            quaternion.X = (matrix.M31 + matrix.M13) * num2;
            quaternion.Y = (matrix.M32 + matrix.M23) * num2;
            quaternion.Z = 0.5f * num5;
            quaternion.W = (matrix.M12 - matrix.M21) * num2;

            return quaternion;

        }


        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            Double num8 = (matrix.M11 + matrix.M22) + matrix.M33;
            if (num8 > 0f)
            {
                Double num = (Double)Math.Sqrt((Double)(num8 + 1f));
                result.W = num * 0.5f;
                num = 0.5f / num;
                result.X = (matrix.M23 - matrix.M32) * num;
                result.Y = (matrix.M31 - matrix.M13) * num;
                result.Z = (matrix.M12 - matrix.M21) * num;
            }
            else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                Double num7 = (Double)Math.Sqrt((Double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
                Double num4 = 0.5f / num7;
                result.X = 0.5f * num7;
                result.Y = (matrix.M12 + matrix.M21) * num4;
                result.Z = (matrix.M13 + matrix.M31) * num4;
                result.W = (matrix.M23 - matrix.M32) * num4;
            }
            else if (matrix.M22 > matrix.M33)
            {
                Double num6 = (Double)Math.Sqrt((Double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
                Double num3 = 0.5f / num6;
                result.X = (matrix.M21 + matrix.M12) * num3;
                result.Y = 0.5f * num6;
                result.Z = (matrix.M32 + matrix.M23) * num3;
                result.W = (matrix.M31 - matrix.M13) * num3;
            }
            else
            {
                Double num5 = (Double)Math.Sqrt((Double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
                Double num2 = 0.5f / num5;
                result.X = (matrix.M31 + matrix.M13) * num2;
                result.Y = (matrix.M32 + matrix.M23) * num2;
                result.Z = 0.5f * num5;
                result.W = (matrix.M12 - matrix.M21) * num2;
            }

        }

        public static Quaternion CreateFromYawPitchRoll(Double yaw, Double pitch, Double roll)
        {
            Quaternion quaternion;
            Double num9 = roll * 0.5f;
            Double num6 = (Double)Math.Sin((Double)num9);
            Double num5 = (Double)Math.Cos((Double)num9);
            Double num8 = pitch * 0.5f;
            Double num4 = (Double)Math.Sin((Double)num8);
            Double num3 = (Double)Math.Cos((Double)num8);
            Double num7 = yaw * 0.5f;
            Double num2 = (Double)Math.Sin((Double)num7);
            Double num = (Double)Math.Cos((Double)num7);
            quaternion.X = ((num * num4) * num5) + ((num2 * num3) * num6);
            quaternion.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
            quaternion.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
            quaternion.W = ((num * num3) * num5) + ((num2 * num4) * num6);
            return quaternion;
        }

        public static void CreateFromYawPitchRoll(Double yaw, Double pitch, Double roll, out Quaternion result)
        {
            Double num9 = roll * 0.5f;
            Double num6 = (Double)Math.Sin((Double)num9);
            Double num5 = (Double)Math.Cos((Double)num9);
            Double num8 = pitch * 0.5f;
            Double num4 = (Double)Math.Sin((Double)num8);
            Double num3 = (Double)Math.Cos((Double)num8);
            Double num7 = yaw * 0.5f;
            Double num2 = (Double)Math.Sin((Double)num7);
            Double num = (Double)Math.Cos((Double)num7);
            result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
            result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
            result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
            result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
        }

        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            Double x = quaternion1.X;
            Double y = quaternion1.Y;
            Double z = quaternion1.Z;
            Double w = quaternion1.W;
            Double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            Double num5 = 1f / num14;
            Double num4 = -quaternion2.X * num5;
            Double num3 = -quaternion2.Y * num5;
            Double num2 = -quaternion2.Z * num5;
            Double num = quaternion2.W * num5;
            Double num13 = (y * num2) - (z * num3);
            Double num12 = (z * num4) - (x * num2);
            Double num11 = (x * num3) - (y * num4);
            Double num10 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num13;
            quaternion.Y = ((y * num) + (num3 * w)) + num12;
            quaternion.Z = ((z * num) + (num2 * w)) + num11;
            quaternion.W = (w * num) - num10;
            return quaternion;

        }

        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            Double x = quaternion1.X;
            Double y = quaternion1.Y;
            Double z = quaternion1.Z;
            Double w = quaternion1.W;
            Double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            Double num5 = 1f / num14;
            Double num4 = -quaternion2.X * num5;
            Double num3 = -quaternion2.Y * num5;
            Double num2 = -quaternion2.Z * num5;
            Double num = quaternion2.W * num5;
            Double num13 = (y * num2) - (z * num3);
            Double num12 = (z * num4) - (x * num2);
            Double num11 = (x * num3) - (y * num4);
            Double num10 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num13;
            result.Y = ((y * num) + (num3 * w)) + num12;
            result.Z = ((z * num) + (num2 * w)) + num11;
            result.W = (w * num) - num10;

        }


        public static Double Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            return ((((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W));
        }


        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out Double result)
        {
            result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
        }


        public override Boolean Equals(Object obj)
        {
            Boolean flag = false;
            if (obj is Quaternion)
            {
                flag = this.Equals((Quaternion)obj);
            }
            return flag;
        }


        public Boolean Equals(Quaternion other)
        {
            return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }


        public override Int32 GetHashCode()
        {
            return (((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode()) + this.W.GetHashCode());
        }


        public static Quaternion Inverse(Quaternion quaternion)
        {
            Quaternion quaternion2;
            Double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            Double num = 1f / num2;
            quaternion2.X = -quaternion.X * num;
            quaternion2.Y = -quaternion.Y * num;
            quaternion2.Z = -quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;

        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            Double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            Double num = 1f / num2;
            result.X = -quaternion.X * num;
            result.Y = -quaternion.Y * num;
            result.Z = -quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public Double Length()
        {
            Double num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            return (Double)Math.Sqrt((Double)num);
        }


        public Double LengthSquared()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }


        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, Double amount)
        {
            Double num = amount;
            Double num2 = 1f - num;
            Quaternion quaternion = new Quaternion();
            Double num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f)
            {
                quaternion.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            }
            else
            {
                quaternion.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            Double num4 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            Double num3 = 1f / ((Double)Math.Sqrt((Double)num4));
            quaternion.X *= num3;
            quaternion.Y *= num3;
            quaternion.Z *= num3;
            quaternion.W *= num3;
            return quaternion;
        }


        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, Double amount, out Quaternion result)
        {
            Double num = amount;
            Double num2 = 1f - num;
            Double num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f)
            {
                result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            }
            else
            {
                result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            Double num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
            Double num3 = 1f / ((Double)Math.Sqrt((Double)num4));
            result.X *= num3;
            result.Y *= num3;
            result.Z *= num3;
            result.W *= num3;

        }


        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, Double amount)
        {
            Double num2;
            Double num3;
            Quaternion quaternion;
            Double num = amount;
            Double num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            Boolean flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                Double num5 = (Double)Math.Acos((Double)num4);
                Double num6 = (Double)(1.0 / Math.Sin((Double)num5));
                num3 = ((Double)Math.Sin((Double)((1f - num) * num5))) * num6;
                num2 = flag ? ((-Math.Sin((Double)(num * num5))) * num6) : (((Double)Math.Sin((Double)(num * num5))) * num6);
            }
            quaternion.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
            quaternion.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
            quaternion.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
            quaternion.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
            return quaternion;
        }


        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, Double amount, out Quaternion result)
        {
            Double num2;
            Double num3;
            Double num = amount;
            Double num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            Boolean flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                Double num5 = (Double)Math.Acos((Double)num4);
                Double num6 = (Double)(1.0 / Math.Sin((Double)num5));
                num3 = ((Double)Math.Sin((Double)((1f - num) * num5))) * num6;
                num2 = flag ? ((-Math.Sin((Double)(num * num5))) * num6) : (((Double)Math.Sin((Double)(num * num5))) * num6);
            }
            result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
            result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
            result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
            result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
        }


        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }


        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result.X = quaternion1.X - quaternion2.X;
            result.Y = quaternion1.Y - quaternion2.Y;
            result.Z = quaternion1.Z - quaternion2.Z;
            result.W = quaternion1.W - quaternion2.W;
        }


        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            Double x = quaternion1.X;
            Double y = quaternion1.Y;
            Double z = quaternion1.Z;
            Double w = quaternion1.W;
            Double num4 = quaternion2.X;
            Double num3 = quaternion2.Y;
            Double num2 = quaternion2.Z;
            Double num = quaternion2.W;
            Double num12 = (y * num2) - (z * num3);
            Double num11 = (z * num4) - (x * num2);
            Double num10 = (x * num3) - (y * num4);
            Double num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }


        public static Quaternion Multiply(Quaternion quaternion1, Double scaleFactor)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }


        public static void Multiply(ref Quaternion quaternion1, Double scaleFactor, out Quaternion result)
        {
            result.X = quaternion1.X * scaleFactor;
            result.Y = quaternion1.Y * scaleFactor;
            result.Z = quaternion1.Z * scaleFactor;
            result.W = quaternion1.W * scaleFactor;
        }


        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            Double x = quaternion1.X;
            Double y = quaternion1.Y;
            Double z = quaternion1.Z;
            Double w = quaternion1.W;
            Double num4 = quaternion2.X;
            Double num3 = quaternion2.Y;
            Double num2 = quaternion2.Z;
            Double num = quaternion2.W;
            Double num12 = (y * num2) - (z * num3);
            Double num11 = (z * num4) - (x * num2);
            Double num10 = (x * num3) - (y * num4);
            Double num9 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num12;
            result.Y = ((y * num) + (num3 * w)) + num11;
            result.Z = ((z * num) + (num2 * w)) + num10;
            result.W = (w * num) - num9;
        }


        public static Quaternion Negate(Quaternion quaternion)
        {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }


        public static void Negate(ref Quaternion quaternion, out Quaternion result)
        {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = -quaternion.W;
        }


        public void Normalize()
        {
            Double num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            Double num = 1f / ((Double)Math.Sqrt((Double)num2));
            this.X *= num;
            this.Y *= num;
            this.Z *= num;
            this.W *= num;
        }


        public static Quaternion Normalize(Quaternion quaternion)
        {
            Quaternion quaternion2;
            Double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            Double num = 1f / ((Double)Math.Sqrt((Double)num2));
            quaternion2.X = quaternion.X * num;
            quaternion2.Y = quaternion.Y * num;
            quaternion2.Z = quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;
        }


        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            Double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            Double num = 1f / ((Double)Math.Sqrt((Double)num2));
            result.X = quaternion.X * num;
            result.Y = quaternion.Y * num;
            result.Z = quaternion.Z * num;
            result.W = quaternion.W * num;
        }


        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }


        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            Double x = quaternion1.X;
            Double y = quaternion1.Y;
            Double z = quaternion1.Z;
            Double w = quaternion1.W;
            Double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            Double num5 = 1f / num14;
            Double num4 = -quaternion2.X * num5;
            Double num3 = -quaternion2.Y * num5;
            Double num2 = -quaternion2.Z * num5;
            Double num = quaternion2.W * num5;
            Double num13 = (y * num2) - (z * num3);
            Double num12 = (z * num4) - (x * num2);
            Double num11 = (x * num3) - (y * num4);
            Double num10 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num13;
            quaternion.Y = ((y * num) + (num3 * w)) + num12;
            quaternion.Z = ((z * num) + (num2 * w)) + num11;
            quaternion.W = (w * num) - num10;
            return quaternion;
        }


        public static Boolean operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
        }


        public static Boolean operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
            if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z))
            {
                return (quaternion1.W != quaternion2.W);
            }
            return true;
        }


        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            Double x = quaternion1.X;
            Double y = quaternion1.Y;
            Double z = quaternion1.Z;
            Double w = quaternion1.W;
            Double num4 = quaternion2.X;
            Double num3 = quaternion2.Y;
            Double num2 = quaternion2.Z;
            Double num = quaternion2.W;
            Double num12 = (y * num2) - (z * num3);
            Double num11 = (z * num4) - (x * num2);
            Double num10 = (x * num3) - (y * num4);
            Double num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }


        public static Quaternion operator *(Quaternion quaternion1, Double scaleFactor)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }


        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            Vector3 vector3d = new Vector3();
            Double num = rotation.X * 2;
            Double num1 = rotation.Y * 2;
            Double num2 = rotation.Z * 2;
            Double num3 = rotation.X * num;
            Double num4 = rotation.Y * num1;
            Double num5 = rotation.Z * num2;
            Double num6 = rotation.X * num1;
            Double num7 = rotation.X * num2;
            Double num8 = rotation.Y * num2;
            Double num9 = rotation.W * num;
            Double num10 = rotation.W * num1;
            Double num11 = rotation.W * num2;
            vector3d.X = (1 - (num4 + num5)) * point.X + (num6 - num11) * point.Y + (num7 + num10) * point.Z;
            vector3d.Y = (num6 + num11) * point.X + (1 - (num3 + num5)) * point.Y + (num8 - num9) * point.Z;
            vector3d.Z = (num7 - num10) * point.X + (num8 + num9) * point.Y + (1 - (num3 + num4)) * point.Z;
            return vector3d;
        }


        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;

        }


        public static Quaternion operator -(Quaternion quaternion)
        {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }


        public override String ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append(" Z:");
            sb.Append(this.Z);
            sb.Append(" W:");
            sb.Append(this.W);
            sb.Append("}");
            return sb.ToString();
        }

        internal Matrix ToMatrix()
        {
            Matrix matrix = Matrix.Identity;
            ToMatrix(out matrix);
            return matrix;
        }

        internal void ToMatrix(out Matrix matrix)
        {
            Quaternion.ToMatrix(this, out matrix);
        }

        internal static void ToMatrix(Quaternion quaternion, out Matrix matrix)
        {

            // source -> http://content.gpwiki.org/index.php/OpenGL:Tutorials:Using_Quaternions_to_represent_rotation#Quaternion_to_Matrix
            Double x2 = quaternion.X * quaternion.X;
            Double y2 = quaternion.Y * quaternion.Y;
            Double z2 = quaternion.Z * quaternion.Z;
            Double xy = quaternion.X * quaternion.Y;
            Double xz = quaternion.X * quaternion.Z;
            Double yz = quaternion.Y * quaternion.Z;
            Double wx = quaternion.W * quaternion.X;
            Double wy = quaternion.W * quaternion.Y;
            Double wz = quaternion.W * quaternion.Z;

            // This calculation would be a lot more complicated for non-unit length quaternions
            // Note: The constructor of Matrix4 expects the Matrix in column-major format like expected by
            //   OpenGL
            matrix.M11 = 1.0f - 2.0f * (y2 + z2);
            matrix.M12 = 2.0f * (xy - wz);
            matrix.M13 = 2.0f * (xz + wy);
            matrix.M14 = 0.0f;

            matrix.M21 = 2.0f * (xy + wz);
            matrix.M22 = 1.0f - 2.0f * (x2 + z2);
            matrix.M23 = 2.0f * (yz - wx);
            matrix.M24 = 0.0f;

            matrix.M31 = 2.0f * (xz - wy);
            matrix.M32 = 2.0f * (yz + wx);
            matrix.M33 = 1.0f - 2.0f * (x2 + y2);
            matrix.M34 = 0.0f;

            matrix.M41 = 2.0f * (xz - wy);
            matrix.M42 = 2.0f * (yz + wx);
            matrix.M43 = 1.0f - 2.0f * (x2 + y2);
            matrix.M44 = 0.0f;

            //return Matrix4( 1.0f - 2.0f * (y2 + z2), 2.0f * (xy - wz), 2.0f * (xz + wy), 0.0f,
            //		2.0f * (xy + wz), 1.0f - 2.0f * (x2 + z2), 2.0f * (yz - wx), 0.0f,
            //		2.0f * (xz - wy), 2.0f * (yz + wx), 1.0f - 2.0f * (x2 + y2), 0.0f,
            //		0.0f, 0.0f, 0.0f, 1.0f)
            //	}
        }

        internal Vector3 Xyz
        {
            get
            {
                return new Vector3(X, Y, Z);
            }

            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }


    }
}

