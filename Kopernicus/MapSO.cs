/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 *
 * This code has been ported from Kopernicus MapSOOnDemand
 */

using System;
using PQS.Unity;
using Bitmap = System.Drawing.Bitmap;

namespace PQS
{
    namespace KSP
    {
        /// <summary>
        /// Standalone implementation of KSP's MapSO, ported from Kopernicus
        /// </summary>
        public class MapSO
        {
            /// <summary>
            /// A class to create a relationship between Height and alpha
            /// </summary>
            public class HeightAlpha
            {
                public Single height;
                public Single alpha;

                /// <summary>
                /// Initializes a new instance of the <see cref="HeightAlpha"/> class.
                /// </summary>
                public HeightAlpha()
                {
                    height = 0f;
                    alpha = 0f;
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="HeightAlpha"/> class.
                /// </summary>
                /// <param name="height">The height.</param>
                /// <param name="alpha">The alpha.</param>
                public HeightAlpha(Single height, Single alpha)
                {
                    this.height = height;
                    this.alpha = alpha;
                }

                /// <summary>
                /// Lerps two HeightAlphas
                /// </summary>
                /// <returns></returns>
                public static HeightAlpha Lerp(HeightAlpha a, HeightAlpha b, Single dt)
                {
                    return new HeightAlpha(a.height + (b.height - a.height) * dt, a.alpha + (b.alpha - a.alpha) * dt);
                }
            }

            /// <summary>
            /// The type of the map
            /// </summary>
            public enum MapDepth
            {
                Greyscale,
                HeightAlpha,
                RGB,
                RGBA
            }

            /// <summary>
            /// BitPerPixels is always 4
            /// </summary>
            protected const Int32 _bpp = 4;

            /// <summary>
            /// Representation of the map
            /// </summary>
            protected Bitmap _data { get; set; }

            /// <summary>
            /// The depth of this Map
            /// </summary>
            public MapDepth Depth { get; set; }

            /// <summary>
            /// The width of the texture
            /// </summary>
            protected Int32 _width { get; set; }

            /// <summary>
            /// The height of the texture
            /// </summary>
            protected Int32 _height { get; set; }

            /// <summary>
            /// The size of a row in the image
            /// </summary>
            protected Int32 _rowWidth { get; set; }

            /// <summary>
            /// Whether the map is compiled
            /// </summary>
            protected Boolean _isCompiled { get; set; }

            /// <summary>
            /// Converts a byte into a floating point number
            /// </summary>
            public const Single Byte2Float = 0.003921569f;

            /// <summary>
            /// Converts a floating point number into a byte
            /// </summary>
            public const Single Float2Byte = 255f;

            /// <summary>
            /// Create a map from a Texture
            /// </summary>
            public void CreateMap(MapDepth depth, Bitmap tex)
            {
                // If the Texture is null, abort
                if (tex == null)
                    throw new ArgumentNullException(nameof(tex));

                // Set _data
                _data = tex;

                // Variables
                _width = tex.Width;
                _height = tex.Height;
                Depth = depth;
                _rowWidth = _width * _bpp;

                // We're compiled
                _isCompiled = true;
            }

            /// GetPixelByte
            public Byte GetPixelByte(Int32 x, Int32 y)
            {
                return _data.GetPixel(x, y).R;
            }

            /// GetPixelColor - double
            public Color GetPixelColor(Double x, Double y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return Color.Lerp(
                    Color.Lerp(
                        this.GetPixelColor(coords.xFloor, coords.yFloor), 
                        this.GetPixelColor(coords.xCeiling, coords.yFloor), 
                        coords.u), 
                    Color.Lerp(
                        this.GetPixelColor(coords.xFloor, coords.yCeiling), 
                        this.GetPixelColor(coords.xCeiling, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelColor - Float
            public Color GetPixelColor(Single x, Single y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return Color.Lerp(
                    Color.Lerp(
                        this.GetPixelColor(coords.xFloor, coords.yFloor),
                        this.GetPixelColor(coords.xCeiling, coords.yFloor),
                        coords.u),
                    Color.Lerp(
                        this.GetPixelColor(coords.xFloor, coords.yCeiling),
                        this.GetPixelColor(coords.xCeiling, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelColor - Int
            public Color GetPixelColor(Int32 x, Int32 y)
            {
                return _data.GetPixel(x, y);
            }

            /// GetPixelColor32 - double
            public Color GetPixelColor32(Double x, Double y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return Color32.Lerp(
                    Color32.Lerp(
                        this.GetPixelColor32(coords.xFloor, coords.yFloor),
                        this.GetPixelColor32(coords.xCeiling, coords.yFloor),
                        coords.u),
                    Color32.Lerp(
                        this.GetPixelColor32(coords.xFloor, coords.yCeiling),
                        this.GetPixelColor32(coords.xCeiling, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelColor32 - Float - Honestly Squad, why are they named GetPixelColor32, but return normal Colors instead of Color32?
            public Color GetPixelColor32(Single x, Single y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return Color32.Lerp(
                    Color32.Lerp(
                        this.GetPixelColor32(coords.xFloor, coords.yFloor),
                        this.GetPixelColor32(coords.xCeiling, coords.yFloor),
                        coords.u),
                    Color32.Lerp(
                        this.GetPixelColor32(coords.xFloor, coords.yCeiling),
                        this.GetPixelColor32(coords.xCeiling, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelColor32 - Int
            public Color32 GetPixelColor32(Int32 x, Int32 y)
            {
                return _data.GetPixel(x, y);
            }

            /// GetPixelFloat - double
            public Single GetPixelFloat(Double x, Double y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return Mathf.Lerp(
                    Mathf.Lerp(
                        GetPixelFloat(coords.xFloor, coords.yFloor), 
                        GetPixelFloat(coords.xCeiling, coords.yFloor), 
                        coords.u), 
                    Mathf.Lerp(
                        GetPixelFloat(coords.xFloor, coords.yCeiling), 
                        GetPixelFloat(coords.xCeiling, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelFloat - Float
            public Single GetPixelFloat(Single x, Single y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return Mathf.Lerp(
                    Mathf.Lerp(
                        GetPixelFloat(coords.xFloor, coords.yFloor),
                        GetPixelFloat(coords.xCeiling, coords.yFloor),
                        coords.u),
                    Mathf.Lerp(
                        GetPixelFloat(coords.xFloor, coords.yCeiling),
                        GetPixelFloat(coords.xCeiling, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelFloat - Integer
            public Single GetPixelFloat(Int32 x, Int32 y)
            {
                Color pixel = _data.GetPixel(x, y);
                Single value = 0f;
                if (Depth == MapDepth.Greyscale)
                    value = pixel.r;
                else if (Depth == MapDepth.HeightAlpha)
                    value = pixel.r + pixel.a;
                else if (Depth == MapDepth.RGB)
                    value = pixel.r + pixel.g + pixel.b;
                else if (Depth == MapDepth.RGBA)
                    value = pixel.r + pixel.g + pixel.b + pixel.a;

                return value / (Int32)Depth;
            }

            /// GetPixelHeightAlpha - double
            public HeightAlpha GetPixelHeightAlpha(Double x, Double y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return HeightAlpha.Lerp(
                    HeightAlpha.Lerp(
                        GetPixelHeightAlpha(coords.xFloor, coords.yFloor), 
                        GetPixelHeightAlpha(coords.xCeiling, coords.yFloor), 
                        coords.u), 
                    HeightAlpha.Lerp(
                        GetPixelHeightAlpha(coords.xFloor, coords.yCeiling), 
                        GetPixelHeightAlpha(coords.xFloor, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelHeightAlpha - Float
            public HeightAlpha GetPixelHeightAlpha(Single x, Single y)
            {
                BilinearCoords coords = ConstructBilinearCoords(x, y);
                return HeightAlpha.Lerp(
                    HeightAlpha.Lerp(
                        GetPixelHeightAlpha(coords.xFloor, coords.yFloor),
                        GetPixelHeightAlpha(coords.xCeiling, coords.yFloor),
                        coords.u),
                    HeightAlpha.Lerp(
                        GetPixelHeightAlpha(coords.xFloor, coords.yCeiling),
                        GetPixelHeightAlpha(coords.xFloor, coords.yCeiling),
                        coords.u),
                    coords.v);
            }

            /// GetPixelHeightAlpha - Int
            public HeightAlpha GetPixelHeightAlpha(Int32 x, Int32 y)
            {
                Color pixel = _data.GetPixel(x, y);
                if (Depth == (MapDepth.HeightAlpha | MapDepth.RGBA))
                    return new HeightAlpha(pixel.r, pixel.a);
                else
                    return new HeightAlpha(pixel.r, 1f);
            }

            /// GreyByte
            public Byte GreyByte(Int32 x, Int32 y)
            {
                return (Byte)(Float2Byte * ((Color)_data.GetPixel(x, y)).r);
            }

            /// GreyFloat
            public Single GreyFloat(Int32 x, Int32 y)
            {
                return ((Color)_data.GetPixel(x, y)).grayscale;
            }

            /// PixelByte
            public Byte[] PixelByte(Int32 x, Int32 y)
            {

                Color c = _data.GetPixel(x, y);
                if (Depth == MapDepth.Greyscale)
                    return new [] { (Byte)c.r };
                if (Depth == MapDepth.HeightAlpha)
                    return new [] { (Byte)c.r, (Byte)c.a };
                if (Depth == MapDepth.RGB)
                    return new [] { (Byte)c.r, (Byte)c.g, (Byte)c.b };
                return new [] { (Byte)c.r, (Byte)c.g, (Byte)c.b, (Byte)c.a };
            }

            /// <summary>
            /// ConstructBilinearCoords from double
            /// </summary>
            protected BilinearCoords ConstructBilinearCoords(Double x, Double y)
            {
                // Create the struct
                BilinearCoords coords = new BilinearCoords();

                // Floor
                x = Math.Abs(x - Math.Floor(x));
                y = Math.Abs(y - Math.Floor(y));

                // X to U
                coords.x = x * _width;
                coords.xFloor = (Int32)Math.Floor(coords.x);
                coords.xCeiling = (Int32)Math.Ceiling(coords.x);
                coords.u = (Single)(coords.x - coords.xFloor);
                if (coords.xCeiling == _width) coords.xCeiling = 0;

                // Y to V
                coords.y = y * _height;
                coords.yFloor = (Int32)Math.Floor(coords.y);
                coords.yCeiling = (Int32)Math.Ceiling(coords.y);
                coords.v = (Single)(coords.y - coords.yFloor);
                if (coords.yCeiling == _height) coords.yCeiling = 0;

                // We're done
                return coords;
            }

            /// <summary>
            /// ConstructBilinearCoords from float
            /// </summary>
            protected BilinearCoords ConstructBilinearCoords(Single x, Single y)
            {
                return ConstructBilinearCoords((Double)x, (Double)y);
            }

            /// BilinearCoords
            public struct BilinearCoords
            {
                public Double x, y;
                public Int32 xCeiling, xFloor, yCeiling, yFloor;
                public Single u, v;
            }
        }
    }
}
