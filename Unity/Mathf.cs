/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using System.ComponentModel;
using XnaGeometry;

namespace PQS
{
    namespace Unity
    {
        /// <summary>
        /// A collection of common math functions.
        /// </summary>
        public struct Mathf
        {
            /// <summary>
            /// The infamous 3.14159265358979... value (Read Only).
            /// </summary>
            public const Single PI = 3.14159274f;

            /// <summary>
            /// A representation of positive infinity (Read Only).
            /// </summary>
            public const Single Infinity = Single.PositiveInfinity;

            /// <summary>
            /// A representation of negative infinity (Read Only).
            /// </summary>
            public const Single NegativeInfinity = Single.NegativeInfinity;

            /// <summary>
            /// Degrees-to-radians conversion constant (Read Only).
            /// </summary>
            public const Single Deg2Rad = 0.0174532924f;

            /// <summary>
            /// Radians-to-degrees conversion constant (Read Only).
            /// </summary>
            public const Single Rad2Deg = 57.29578f;

            /// <summary>
            /// A tiny floating point value (Read Only).
            /// </summary>
            public const Single Epsilon = 1.401298E-45f;

            /// <summary>
            /// Returns the absolute value of f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Abs(Single f)
            {
                return (Single)Math.Abs(f);
            }

            /// <summary>
            /// Returns the absolute value of value.
            /// </summary>
            /// <param name="value"></param>
            public static Int32 Abs(Int32 value)
            {
                return Math.Abs(value);
            }

            /// <summary>
            /// Returns the arc-cosine of f - the angle in radians whose cosine is f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Acos(Single f)
            {
                return (Single)Math.Acos((Double)f);
            }

            /// <summary>
            /// Compares two floating point values if they are similar.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            public static Boolean Approximately(Single a, Single b)
            {
                return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
            }

            /// <summary>
            /// Returns the arc-sine of f - the angle in radians whose sine is f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Asin(Single f)
            {
                return (Single)Math.Asin((Double)f);
            }

            /// <summary>
            /// Returns the arc-tangent of f - the angle in radians whose tangent is f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Atan(Single f)
            {
                return (Single)Math.Atan((Double)f);
            }

            /// <summary>
            /// Returns the angle in radians whose Tan is y/x.
            /// </summary>
            /// <param name="y"></param>
            /// <param name="x"></param>
            public static Single Atan2(Single y, Single x)
            {
                return (Single)Math.Atan2((Double)y, (Double)x);
            }

            /// <summary>
            /// Returns the smallest integer greater to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Ceil(Single f)
            {
                return (Single)Math.Ceiling((Double)f);
            }

            /// <summary>
            /// Returns the smallest integer greater to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static Int32 CeilToInt(Single f)
            {
                return (Int32)Math.Ceiling((Double)f);
            }

            /// <summary>
            /// Clamps a value between a minimum float and maximum float value.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="min"></param>
            /// <param name="max"></param>
            public static Single Clamp(Single value, Single min, Single max)
            {
                if (value < min)
                {
                    value = min;
                }
                else if (value > max)
                {
                    value = max;
                }
                return value;
            }

            /// <summary>
            /// Clamps value between min and max and returns value.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="min"></param>
            /// <param name="max"></param>
            public static Int32 Clamp(Int32 value, Int32 min, Int32 max)
            {
                if (value < min)
                {
                    value = min;
                }
                else if (value > max)
                {
                    value = max;
                }
                return value;
            }

            /// <summary>
            /// Clamps value between 0 and 1 and returns value.
            /// </summary>
            /// <param name="value"></param>
            public static Single Clamp01(Single value)
            {
                if (value < 0f)
                {
                    return 0f;
                }
                if (value > 1f)
                {
                    return 1f;
                }
                return value;
            }

            /// <summary>
            /// Returns the cosine of angle f in radians.
            /// </summary>
            /// <param name="f"></param>
            public static Single Cos(Single f)
            {
                return (Single)Math.Cos((Double)f);
            }

            /// <summary>
            /// Calculates the shortest difference between two given angles given in degrees.
            /// </summary>
            /// <param name="current"></param>
            /// <param name="target"></param>
            public static Single DeltaAngle(Single current, Single target)
            {
                Single single = Mathf.Repeat(target - current, 360f);
                if (single > 180f)
                {
                    single = single - 360f;
                }
                return single;
            }

            /// <summary>
            /// Returns e raised to the specified power.
            /// </summary>
            /// <param name="power"></param>
            public static Single Exp(Single power)
            {
                return (Single)Math.Exp((Double)power);
            }

            /// <summary>
            /// Returns the largest integer smaller to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Floor(Single f)
            {
                return (Single)Math.Floor((Double)f);
            }

            /// <summary>
            /// Returns the largest integer smaller to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static Int32 FloorToInt(Single f)
            {
                return (Int32)Math.Floor((Double)f);
            }

            public static Single Gamma(Single value, Single absmax, Single gamma)
            {
                Boolean flag = false;
                if (value < 0f)
                {
                    flag = true;
                }
                Single single = Mathf.Abs(value);
                if (single > absmax)
                {
                    return (!flag ? single : -single);
                }
                Single single1 = Mathf.Pow(single / absmax, gamma) * absmax;
                return (!flag ? single1 : -single1);
            }

            /// <summary>
            /// Calculates the linear parameter t that produces the interpolant value within the range [a, b].
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="value"></param>
            public static Single InverseLerp(Single a, Single b, Single value)
            {
                if (a == b)
                {
                    return 0f;
                }
                return Mathf.Clamp01((value - a) / (b - a));
            }

            /// <summary>
            /// Linearly interpolates between a and b by t.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static Single Lerp(Single a, Single b, Single t)
            {
                return a + (b - a) * Mathf.Clamp01(t);
            }

            /// <summary>
            /// Same as Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static Single LerpAngle(Single a, Single b, Single t)
            {
                Single single = Mathf.Repeat(b - a, 360f);
                if (single > 180f)
                {
                    single = single - 360f;
                }
                return a + single * Mathf.Clamp01(t);
            }

            /// <summary>
            /// Linearly interpolates between a and b by t.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static Single LerpUnclamped(Single a, Single b, Single t)
            {
                return a + (b - a) * t;
            }

            internal static Boolean LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
            {
                Single single = (Single)(p2.X - p1.X);
                Single single1 = (Single)(p2.Y - p1.Y);
                Single single2 = (Single)(p4.X - p3.X);
                Single single3 = (Single)(p4.Y - p3.Y);
                Single single4 = single * single3 - single1 * single2;
                if (single4 == 0f)
                {
                    return false;
                }
                Single single5 = (Single)(p3.X - p1.X);
                Single single6 = (Single)(p3.Y - p1.Y);
                Single single7 = (single5 * single3 - single6 * single2) / single4;
                result = new Vector2(p1.X + single7 * single, p1.Y + single7 * single1);
                return true;
            }

            internal static Boolean LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
            {
                Single single = (Single)(p2.X - p1.X);
                Single single1 = (Single)(p2.Y - p1.Y);
                Single single2 = (Single)(p4.X - p3.X);
                Single single3 = (Single)(p4.Y - p3.Y);
                Single single4 = single * single3 - single1 * single2;
                if (single4 == 0f)
                {
                    return false;
                }
                Single single5 = (Single)(p3.X - p1.X);
                Single single6 = (Single)(p3.Y - p1.Y);
                Single single7 = (single5 * single3 - single6 * single2) / single4;
                if (single7 < 0f || single7 > 1f)
                {
                    return false;
                }
                Single single8 = (single5 * single1 - single6 * single) / single4;
                if (single8 < 0f || single8 > 1f)
                {
                    return false;
                }
                result = new Vector2(p1.X + single7 * single, p1.Y + single7 * single1);
                return true;
            }

            /// <summary>
            /// Returns the logarithm of a specified number in a specified base.
            /// </summary>
            /// <param name="f"></param>
            /// <param name="p"></param>
            public static Single Log(Single f, Single p)
            {
                return (Single)Math.Log((Double)f, (Double)p);
            }

            /// <summary>
            /// Returns the natural (base e) logarithm of a specified number.
            /// </summary>
            /// <param name="f"></param>
            public static Single Log(Single f)
            {
                return (Single)Math.Log((Double)f);
            }

            /// <summary>
            /// Returns the base 10 logarithm of a specified number.
            /// </summary>
            /// <param name="f"></param>
            public static Single Log10(Single f)
            {
                return (Single)Math.Log10((Double)f);
            }

            /// <summary>
            /// Returns largest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Single Max(Single a, Single b)
            {
                return (a <= b ? b : a);
            }

            /// <summary>
            /// Returns largest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Single Max(params Single[] values)
            {
                Int32 length = (Int32)values.Length;
                if (length == 0)
                {
                    return 0f;
                }
                Single single = values[0];
                for (Int32 i = 1; i < length; i++)
                {
                    if (values[i] > single)
                    {
                        single = values[i];
                    }
                }
                return single;
            }

            /// <summary>
            /// Returns the largest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Int32 Max(Int32 a, Int32 b)
            {
                return (a <= b ? b : a);
            }

            /// <summary>
            /// Returns the largest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Int32 Max(params Int32[] values)
            {
                Int32 length = (Int32)values.Length;
                if (length == 0)
                {
                    return 0;
                }
                Int32 num = values[0];
                for (Int32 i = 1; i < length; i++)
                {
                    if (values[i] > num)
                    {
                        num = values[i];
                    }
                }
                return num;
            }

            /// <summary>
            /// Returns the smallest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Single Min(Single a, Single b)
            {
                return (a >= b ? b : a);
            }

            /// <summary>
            /// Returns the smallest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Single Min(params Single[] values)
            {
                Int32 length = (Int32)values.Length;
                if (length == 0)
                {
                    return 0f;
                }
                Single single = values[0];
                for (Int32 i = 1; i < length; i++)
                {
                    if (values[i] < single)
                    {
                        single = values[i];
                    }
                }
                return single;
            }

            /// <summary>
            /// Returns the smallest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Int32 Min(Int32 a, Int32 b)
            {
                return (a >= b ? b : a);
            }

            /// <summary>
            /// Returns the smallest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static Int32 Min(params Int32[] values)
            {
                Int32 length = (Int32)values.Length;
                if (length == 0)
                {
                    return 0;
                }
                Int32 num = values[0];
                for (Int32 i = 1; i < length; i++)
                {
                    if (values[i] < num)
                    {
                        num = values[i];
                    }
                }
                return num;
            }

            /// <summary>
            /// Moves a value current towards target.
            /// </summary>
            /// <param name="current">The current value.</param>
            /// <param name="target">The value to move towards.</param>
            /// <param name="maxDelta">The maximum change that should be applied to the value.</param>
            public static Single MoveTowards(Single current, Single target, Single maxDelta)
            {
                if (Mathf.Abs(target - current) <= maxDelta)
                {
                    return target;
                }
                return current + Mathf.Sign(target - current) * maxDelta;
            }

            /// <summary>
            /// Same as MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.
            /// </summary>
            /// <param name="current"></param>
            /// <param name="target"></param>
            /// <param name="maxDelta"></param>
            public static Single MoveTowardsAngle(Single current, Single target, Single maxDelta)
            {
                target = current + Mathf.DeltaAngle(current, target);
                return Mathf.MoveTowards(current, target, maxDelta);
            }

            /// <summary>
            /// PingPongs the value t, so that it is never larger than length and never smaller than 0.
            /// </summary>
            /// <param name="t"></param>
            /// <param name="length"></param>
            public static Single PingPong(Single t, Single length)
            {
                t = Mathf.Repeat(t, length * 2f);
                return length - Mathf.Abs(t - length);
            }

            /// <summary>
            /// Returns f raised to power p.
            /// </summary>
            /// <param name="f"></param>
            /// <param name="p"></param>
            public static Single Pow(Single f, Single p)
            {
                return (Single)Math.Pow((Double)f, (Double)p);
            }

            internal static Int64 RandomToLong(Random r)
            {
                Byte[] numArray = new Byte[8];
                r.NextBytes(numArray);
                return (Int64)(BitConverter.ToUInt64(numArray, 0) & 9223372036854775807L);
            }

            /// <summary>
            /// Loops the value t, so that it is never larger than length and never smaller than 0.
            /// </summary>
            /// <param name="t"></param>
            /// <param name="length"></param>
            public static Single Repeat(Single t, Single length)
            {
                return t - Mathf.Floor(t / length) * length;
            }

            /// <summary>
            /// Returns f rounded to the nearest integer.
            /// </summary>
            /// <param name="f"></param>
            public static Single Round(Single f)
            {
                return (Single)Math.Round((Double)f);
            }

            /// <summary>
            /// Returns f rounded to the nearest integer.
            /// </summary>
            /// <param name="f"></param>
            public static Int32 RoundToInt(Single f)
            {
                return (Int32)Math.Round((Double)f);
            }

            /// <summary>
            /// Returns the sign of f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Sign(Single f)
            {
                return (f < 0f ? -1f : 1f);
            }

            /// <summary>
            /// Returns the sine of angle f in radians.
            /// </summary>
            /// <param name="f"></param>
            public static Single Sin(Single f)
            {
                return (Single)Math.Sin((Double)f);
            }

            public static Single SmoothDamp(Single current, Single target, ref Single currentVelocity, Single smoothTime, [DefaultValue("Mathf.Infinity")] Single maxSpeed, [DefaultValue("Time.deltaTime")] Single deltaTime)
            {
                smoothTime = Mathf.Max(0.0001f, smoothTime);
                Single single = 2f / smoothTime;
                Single single1 = single * deltaTime;
                Single single2 = 1f / (1f + single1 + 0.48f * single1 * single1 + 0.235f * single1 * single1 * single1);
                Single single3 = current - target;
                Single single4 = target;
                Single single5 = maxSpeed * smoothTime;
                single3 = Mathf.Clamp(single3, -single5, single5);
                target = current - single3;
                Single single6 = (currentVelocity + single * single3) * deltaTime;
                currentVelocity = (currentVelocity - single * single6) * single2;
                Single single7 = target + (single3 + single6) * single2;
                if (single4 - current > 0f == single7 > single4)
                {
                    single7 = single4;
                    currentVelocity = (single7 - single4) / deltaTime;
                }
                return single7;
            }

            public static Single SmoothDampAngle(Single current, Single target, ref Single currentVelocity, Single smoothTime, [DefaultValue("Mathf.Infinity")] Single maxSpeed, [DefaultValue("Time.deltaTime")] Single deltaTime)
            {
                target = current + Mathf.DeltaAngle(current, target);
                return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
            }

            /// <summary>
            /// Interpolates between min and max with smoothing at the limits.
            /// </summary>
            /// <param name="from"></param>
            /// <param name="to"></param>
            /// <param name="t"></param>
            public static Single SmoothStep(Single from, Single to, Single t)
            {
                t = Mathf.Clamp01(t);
                t = -2f * t * t * t + 3f * t * t;
                return to * t + from * (1f - t);
            }

            /// <summary>
            /// Returns square root of f.
            /// </summary>
            /// <param name="f"></param>
            public static Single Sqrt(Single f)
            {
                return (Single)Math.Sqrt((Double)f);
            }

            /// <summary>
            /// Returns the tangent of angle f in radians.
            /// </summary>
            /// <param name="f"></param>
            public static Single Tan(Single f)
            {
                return (Single)Math.Tan((Double)f);
            }
        }
    }
}