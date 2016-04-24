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
            public const float PI = 3.14159274f;

            /// <summary>
            /// A representation of positive infinity (Read Only).
            /// </summary>
            public const float Infinity = float.PositiveInfinity;

            /// <summary>
            /// A representation of negative infinity (Read Only).
            /// </summary>
            public const float NegativeInfinity = float.NegativeInfinity;

            /// <summary>
            /// Degrees-to-radians conversion constant (Read Only).
            /// </summary>
            public const float Deg2Rad = 0.0174532924f;

            /// <summary>
            /// Radians-to-degrees conversion constant (Read Only).
            /// </summary>
            public const float Rad2Deg = 57.29578f;

            /// <summary>
            /// A tiny floating point value (Read Only).
            /// </summary>
            public const float Epsilon = 1.401298E-45f;

            /// <summary>
            /// Returns the absolute value of f.
            /// </summary>
            /// <param name="f"></param>
            public static float Abs(float f)
            {
                return (float)Math.Abs(f);
            }

            /// <summary>
            /// Returns the absolute value of value.
            /// </summary>
            /// <param name="value"></param>
            public static int Abs(int value)
            {
                return Math.Abs(value);
            }

            /// <summary>
            /// Returns the arc-cosine of f - the angle in radians whose cosine is f.
            /// </summary>
            /// <param name="f"></param>
            public static float Acos(float f)
            {
                return (float)Math.Acos((double)f);
            }

            /// <summary>
            /// Compares two floating point values if they are similar.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            public static bool Approximately(float a, float b)
            {
                return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
            }

            /// <summary>
            /// Returns the arc-sine of f - the angle in radians whose sine is f.
            /// </summary>
            /// <param name="f"></param>
            public static float Asin(float f)
            {
                return (float)Math.Asin((double)f);
            }

            /// <summary>
            /// Returns the arc-tangent of f - the angle in radians whose tangent is f.
            /// </summary>
            /// <param name="f"></param>
            public static float Atan(float f)
            {
                return (float)Math.Atan((double)f);
            }

            /// <summary>
            /// Returns the angle in radians whose Tan is y/x.
            /// </summary>
            /// <param name="y"></param>
            /// <param name="x"></param>
            public static float Atan2(float y, float x)
            {
                return (float)Math.Atan2((double)y, (double)x);
            }

            /// <summary>
            /// Returns the smallest integer greater to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static float Ceil(float f)
            {
                return (float)Math.Ceiling((double)f);
            }

            /// <summary>
            /// Returns the smallest integer greater to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static int CeilToInt(float f)
            {
                return (int)Math.Ceiling((double)f);
            }

            /// <summary>
            /// Clamps a value between a minimum float and maximum float value.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="min"></param>
            /// <param name="max"></param>
            public static float Clamp(float value, float min, float max)
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
            public static int Clamp(int value, int min, int max)
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
            public static float Clamp01(float value)
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
            public static float Cos(float f)
            {
                return (float)Math.Cos((double)f);
            }

            /// <summary>
            /// Calculates the shortest difference between two given angles given in degrees.
            /// </summary>
            /// <param name="current"></param>
            /// <param name="target"></param>
            public static float DeltaAngle(float current, float target)
            {
                float single = Mathf.Repeat(target - current, 360f);
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
            public static float Exp(float power)
            {
                return (float)Math.Exp((double)power);
            }

            /// <summary>
            /// Returns the largest integer smaller to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static float Floor(float f)
            {
                return (float)Math.Floor((double)f);
            }

            /// <summary>
            /// Returns the largest integer smaller to or equal to f.
            /// </summary>
            /// <param name="f"></param>
            public static int FloorToInt(float f)
            {
                return (int)Math.Floor((double)f);
            }

            public static float Gamma(float value, float absmax, float gamma)
            {
                bool flag = false;
                if (value < 0f)
                {
                    flag = true;
                }
                float single = Mathf.Abs(value);
                if (single > absmax)
                {
                    return (!flag ? single : -single);
                }
                float single1 = Mathf.Pow(single / absmax, gamma) * absmax;
                return (!flag ? single1 : -single1);
            }

            /// <summary>
            /// Calculates the linear parameter t that produces the interpolant value within the range [a, b].
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="value"></param>
            public static float InverseLerp(float a, float b, float value)
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
            public static float Lerp(float a, float b, float t)
            {
                return a + (b - a) * Mathf.Clamp01(t);
            }

            /// <summary>
            /// Same as Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="t"></param>
            public static float LerpAngle(float a, float b, float t)
            {
                float single = Mathf.Repeat(b - a, 360f);
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
            public static float LerpUnclamped(float a, float b, float t)
            {
                return a + (b - a) * t;
            }

            internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
            {
                float single = (float)(p2.X - p1.X);
                float single1 = (float)(p2.Y - p1.Y);
                float single2 = (float)(p4.X - p3.X);
                float single3 = (float)(p4.Y - p3.Y);
                float single4 = single * single3 - single1 * single2;
                if (single4 == 0f)
                {
                    return false;
                }
                float single5 = (float)(p3.X - p1.X);
                float single6 = (float)(p3.Y - p1.Y);
                float single7 = (single5 * single3 - single6 * single2) / single4;
                result = new Vector2(p1.X + single7 * single, p1.Y + single7 * single1);
                return true;
            }

            internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
            {
                float single = (float)(p2.X - p1.X);
                float single1 = (float)(p2.Y - p1.Y);
                float single2 = (float)(p4.X - p3.X);
                float single3 = (float)(p4.Y - p3.Y);
                float single4 = single * single3 - single1 * single2;
                if (single4 == 0f)
                {
                    return false;
                }
                float single5 = (float)(p3.X - p1.X);
                float single6 = (float)(p3.Y - p1.Y);
                float single7 = (single5 * single3 - single6 * single2) / single4;
                if (single7 < 0f || single7 > 1f)
                {
                    return false;
                }
                float single8 = (single5 * single1 - single6 * single) / single4;
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
            public static float Log(float f, float p)
            {
                return (float)Math.Log((double)f, (double)p);
            }

            /// <summary>
            /// Returns the natural (base e) logarithm of a specified number.
            /// </summary>
            /// <param name="f"></param>
            public static float Log(float f)
            {
                return (float)Math.Log((double)f);
            }

            /// <summary>
            /// Returns the base 10 logarithm of a specified number.
            /// </summary>
            /// <param name="f"></param>
            public static float Log10(float f)
            {
                return (float)Math.Log10((double)f);
            }

            /// <summary>
            /// Returns largest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static float Max(float a, float b)
            {
                return (a <= b ? b : a);
            }

            /// <summary>
            /// Returns largest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static float Max(params float[] values)
            {
                int length = (int)values.Length;
                if (length == 0)
                {
                    return 0f;
                }
                float single = values[0];
                for (int i = 1; i < length; i++)
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
            public static int Max(int a, int b)
            {
                return (a <= b ? b : a);
            }

            /// <summary>
            /// Returns the largest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static int Max(params int[] values)
            {
                int length = (int)values.Length;
                if (length == 0)
                {
                    return 0;
                }
                int num = values[0];
                for (int i = 1; i < length; i++)
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
            public static float Min(float a, float b)
            {
                return (a >= b ? b : a);
            }

            /// <summary>
            /// Returns the smallest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static float Min(params float[] values)
            {
                int length = (int)values.Length;
                if (length == 0)
                {
                    return 0f;
                }
                float single = values[0];
                for (int i = 1; i < length; i++)
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
            public static int Min(int a, int b)
            {
                return (a >= b ? b : a);
            }

            /// <summary>
            /// Returns the smallest of two or more values.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static int Min(params int[] values)
            {
                int length = (int)values.Length;
                if (length == 0)
                {
                    return 0;
                }
                int num = values[0];
                for (int i = 1; i < length; i++)
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
            public static float MoveTowards(float current, float target, float maxDelta)
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
            public static float MoveTowardsAngle(float current, float target, float maxDelta)
            {
                target = current + Mathf.DeltaAngle(current, target);
                return Mathf.MoveTowards(current, target, maxDelta);
            }

            /// <summary>
            /// PingPongs the value t, so that it is never larger than length and never smaller than 0.
            /// </summary>
            /// <param name="t"></param>
            /// <param name="length"></param>
            public static float PingPong(float t, float length)
            {
                t = Mathf.Repeat(t, length * 2f);
                return length - Mathf.Abs(t - length);
            }

            /// <summary>
            /// Returns f raised to power p.
            /// </summary>
            /// <param name="f"></param>
            /// <param name="p"></param>
            public static float Pow(float f, float p)
            {
                return (float)Math.Pow((double)f, (double)p);
            }

            internal static long RandomToLong(Random r)
            {
                byte[] numArray = new byte[8];
                r.NextBytes(numArray);
                return (long)(BitConverter.ToUInt64(numArray, 0) & 9223372036854775807L);
            }

            /// <summary>
            /// Loops the value t, so that it is never larger than length and never smaller than 0.
            /// </summary>
            /// <param name="t"></param>
            /// <param name="length"></param>
            public static float Repeat(float t, float length)
            {
                return t - Mathf.Floor(t / length) * length;
            }

            /// <summary>
            /// Returns f rounded to the nearest integer.
            /// </summary>
            /// <param name="f"></param>
            public static float Round(float f)
            {
                return (float)Math.Round((double)f);
            }

            /// <summary>
            /// Returns f rounded to the nearest integer.
            /// </summary>
            /// <param name="f"></param>
            public static int RoundToInt(float f)
            {
                return (int)Math.Round((double)f);
            }

            /// <summary>
            /// Returns the sign of f.
            /// </summary>
            /// <param name="f"></param>
            public static float Sign(float f)
            {
                return (f < 0f ? -1f : 1f);
            }

            /// <summary>
            /// Returns the sine of angle f in radians.
            /// </summary>
            /// <param name="f"></param>
            public static float Sin(float f)
            {
                return (float)Math.Sin((double)f);
            }

            public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
            {
                smoothTime = Mathf.Max(0.0001f, smoothTime);
                float single = 2f / smoothTime;
                float single1 = single * deltaTime;
                float single2 = 1f / (1f + single1 + 0.48f * single1 * single1 + 0.235f * single1 * single1 * single1);
                float single3 = current - target;
                float single4 = target;
                float single5 = maxSpeed * smoothTime;
                single3 = Mathf.Clamp(single3, -single5, single5);
                target = current - single3;
                float single6 = (currentVelocity + single * single3) * deltaTime;
                currentVelocity = (currentVelocity - single * single6) * single2;
                float single7 = target + (single3 + single6) * single2;
                if (single4 - current > 0f == single7 > single4)
                {
                    single7 = single4;
                    currentVelocity = (single7 - single4) / deltaTime;
                }
                return single7;
            }

            public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
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
            public static float SmoothStep(float from, float to, float t)
            {
                t = Mathf.Clamp01(t);
                t = -2f * t * t * t + 3f * t * t;
                return to * t + from * (1f - t);
            }

            /// <summary>
            /// Returns square root of f.
            /// </summary>
            /// <param name="f"></param>
            public static float Sqrt(float f)
            {
                return (float)Math.Sqrt((double)f);
            }

            /// <summary>
            /// Returns the tangent of angle f in radians.
            /// </summary>
            /// <param name="f"></param>
            public static float Tan(float f)
            {
                return (float)Math.Tan((double)f);
            }
        }
    }
}