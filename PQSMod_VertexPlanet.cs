/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using System.Linq;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;
using ProceduralQuadSphere.KSP;
using ProceduralQuadSphere.Unity;
using XnaGeometry;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that can make a planet from scratch
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VertexPlanet : PQSMod
    {
        #region I won't document this
        public Int32 seed;
        public Double deformity;
        public Double colorDeformity;
        public Double oceanLevel;
        public Double oceanStep;
        public Double oceanDepth;
        public Boolean oceanSnap;
        public Double terrainSmoothing = 0.25;
        public Double terrainShapeStart = 2;
        public Double terrainShapeEnd = -2;
        public Double terrainRidgesMin = 0.4;
        public Double terrainRidgesMax = 1;
        public SimplexWrapper continental;
        public SimplexWrapper continentalRuggedness;
        private SimplexWrapper continentalSmoothing;
        public SimplexWrapper continentalSharpnessMap;
        public NoiseModWrapper continentalSharpness;
        public SimplexWrapper terrainType;
        public Boolean buildHeightColors;
        public LandClass[] landClasses;
        public Double terrainRidgeBalance = 0.1;
        private Int32 lcCount;
        private Double continentialHeightPreSmooth;
        #endregion

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            lcCount = landClasses.Length;
            Int32 i = 0;
            while (i < lcCount)
            {
                landClasses[i].colorNoiseMap.Setup(seed + i + 10);
                i++;
            }
            continental.Setup(seed);
            continentalRuggedness.Setup(seed + 1);
            continentalSmoothing = new SimplexWrapper(continental);
            continentalSmoothing.Setup(seed);
            continentalSmoothing.persistance = continental.persistance * terrainSmoothing;
            continentalSmoothing.frequency = continental.frequency;
            terrainType.Setup(seed + 2);
            continentalSharpness.Setup(new RidgedMultifractal(continentalSharpness.frequency, continentalSharpness.persistance, continentalSharpness.octaves, seed + 3, QualityMode.High));
            continentalSharpnessMap.Setup(seed + 4);
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            if (!buildHeightColors)
            {
                Double d1 = terrainType.simplex.noiseNormalized(data.directionFromCenter);
                Double tHeight = Mathf.Clamp01((Single)((continentialHeightPreSmooth + d1 * terrainType.deformity) * (Single)((data.vertHeight - sphere.radius) / colorDeformity)));
                LandClass lcSelected = SelectLandClassByHeight(tHeight);
                Color c1 = Color.Lerp(lcSelected.baseColor, lcSelected.colorNoise, (Single)(lcSelected.colorNoiseAmount * lcSelected.colorNoiseMap.simplex.noiseNormalized(data.directionFromCenter)));
                if (lcSelected.lerpToNext)
                {
                    LandClass lcLerp = landClasses[landClasses.ToList().IndexOf(lcSelected) + 1];
                    Color c2 = Color.Lerp(lcLerp.baseColor, lcLerp.colorNoise, (Single)(lcLerp.colorNoiseAmount * lcLerp.colorNoiseMap.simplex.noiseNormalized(data.directionFromCenter)));
                    c1 = Color.Lerp(c1, c2, (Single)((tHeight - lcSelected.fractalStart) / (lcSelected.fractalEnd - lcSelected.fractalStart)));
                }
                data.vertColor = c1;
                data.vertColor.a = (Single)continentialHeightPreSmooth;
            }
            else
            {
                data.vertColor.r = data.vertColor.g = data.vertColor.b = (Single)((data.vertHeight - sphere.radius) / colorDeformity);
            }
        }

        /// <summary>
        /// Selects the land class responsible for the given height
        /// </summary>
        public LandClass SelectLandClassByHeight(double height)
        {
            for (Int32 i = 0; i < lcCount; i++)
            {
                if (height >= landClasses[i].fractalStart && height <= landClasses[i].fractalEnd)
                {
                    return landClasses[i];
                }
            }
            return landClasses[0];
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            Double continentalDeformity = 1;
            Double continental2Height = continentalSmoothing.simplex.noiseNormalized(data.directionFromCenter);
            continental.simplex.persistence = continental.persistance - continentalSmoothing.persistance * continental2Height;
            Double continentialHeight = continental.simplex.noiseNormalized(data.directionFromCenter);
            Double continentialSharpnessValue = (continentalSharpness.noise.GetValue(data.directionFromCenter) + 1) * 0.5;
            continentialSharpnessValue *= MathHelper.Lerp(continentalSharpness.deformity, continentalSharpness.deformity * terrainRidgeBalance, (continental2Height + continentialSharpnessValue) * 0.5);
            Double continentialSharpnessMapValue = MathHelper.Clamp((continentalSharpnessMap.simplex.noise(data.directionFromCenter) + 1) * 0.5, terrainRidgesMin, terrainRidgesMax);
            continentialSharpnessMapValue = (continentialSharpnessMapValue - terrainRidgesMin) / (terrainRidgesMax - terrainRidgesMin) * continentalSharpnessMap.deformity;
            continentialSharpnessValue = continentialSharpnessValue + MathHelper.Lerp(0, continentialSharpnessValue, continentialSharpnessMapValue);
            continentialHeight += continentialSharpnessValue;
            continentalDeformity = continentalDeformity + continentalSharpness.deformity * continentalSharpnessMap.deformity;
            continentialHeight = continentialHeight / continentalDeformity;
            Double continentalDelta = (continentialHeight - oceanLevel) / (1 - oceanLevel);
            Double vHeight;
            if (continentialHeight >= oceanLevel)
            {
                continentalRuggedness.simplex.persistence = continentalRuggedness.persistance*continentalDelta;
                Double continentalRHeight = continentalRuggedness.simplex.noiseNormalized(data.directionFromCenter)*continentalDelta*continentalDelta;
                continentialHeight = continentalDelta*continental.deformity + continentalRHeight*this.continentalRuggedness.deformity;
                continentialHeight /= (continental.deformity + continentalRuggedness.deformity);
                continentialHeightPreSmooth = continentialHeight;
                continentialHeight = MathHelper.Hermite(0, terrainShapeStart, 1, terrainShapeEnd, continentialHeight);
                vHeight = continentialHeight;
            }
            else
            {
                if (!oceanSnap)
                    vHeight = continentalDelta*this.oceanDepth - this.oceanStep;
                else
                    vHeight = -oceanStep;
                continentialHeightPreSmooth = vHeight;
            }
            data.vertHeight += Math.Round(vHeight, 5) * deformity;
        }

        /// <summary>
        /// Gets the maximum height of the mod.
        /// </summary>
        public override Double GetVertexMaxHeight()
        {
            return deformity;
        }

        /// <summary>
        /// Gets the minimum height of the mod.
        /// </summary>
        public override Double GetVertexMinHeight()
        {
            return 0;
        }

        /// <summary>
        /// An object that connects an area on the planet with a color
        /// </summary>
        public class LandClass
        {
            /// <summary>
            /// The name of the class
            /// </summary>
            public String name;

            /// <summary>
            /// Where the class starts
            /// </summary>
            public Double fractalStart;

            /// <summary>
            /// Where the class ends
            /// </summary>
            public Double fractalEnd;

            /// <summary>
            /// The base color
            /// </summary>
            public Color baseColor;

            /// <summary>
            /// The color that is applied together with the noise
            /// </summary>
            public Color colorNoise;

            /// <summary>
            /// How much of the noise color should get taken into account
            /// </summary>
            public Double colorNoiseAmount;

            /// <summary>
            /// The color noise
            /// </summary>
            public SimplexWrapper colorNoiseMap;

            /// <summary>
            /// Whether the color should get lerped into the color of the next class
            /// </summary>
            public Boolean lerpToNext;

            /// <summary>
            /// The difference between the two fractal borders
            /// </summary>
            public Double fractalDelta => fractalEnd - fractalStart;

            public LandClass(String name, Double fractalStart, Double fractalEnd, Color baseColor, Color colorNoise, Double colorNoiseAmount)
            {
                this.name = name;
                this.fractalStart = fractalStart;
                this.fractalEnd = fractalEnd;
                this.baseColor = baseColor;
                this.colorNoise = colorNoise;
                this.colorNoiseAmount = colorNoiseAmount;
            }
        }

        /// <summary>
        /// A wrapper for a custom Noise
        /// </summary>
        public class NoiseModWrapper
        {
            /// <summary>
            /// The deformity of the noise
            /// </summary>
            public Double deformity;

            /// <summary>
            /// The octaves for the noise
            /// </summary>
            public Int32 octaves;

            /// <summary>
            /// The persistance of the noise
            /// </summary>
            public Double persistance;

            /// <summary>
            /// The frequency of the noise
            /// </summary>
            public Double frequency;

            /// <summary>
            /// The final noise
            /// </summary>
            public ModuleBase noise;

            /// <summary>
            /// The seed for the noise
            /// </summary>
            public Int32 seed;

            public NoiseModWrapper(NoiseModWrapper copyFrom)
            {
                deformity = copyFrom.deformity;
                octaves = copyFrom.octaves;
                persistance = copyFrom.persistance;
                frequency = copyFrom.frequency;
            }

            public NoiseModWrapper(Double deformity, Int32 octaves, Double persistance, Double frequency)
            {
                this.deformity = deformity;
                this.octaves = octaves;
                this.persistance = persistance;
                this.frequency = frequency;
            }

            public void Setup(ModuleBase mod)
            {
                noise = mod;
            }
        }

        /// <summary>
        /// A wrapper for a custom Simplex
        /// </summary>
        public class SimplexWrapper
        {
            /// <summary>
            /// The deformity of the simplex
            /// </summary>
            public Double deformity;

            /// <summary>
            /// The octaves for the simplex
            /// </summary>
            public Double octaves;

            /// <summary>
            /// The persistance of the simplex
            /// </summary>
            public Double persistance;

            /// <summary>
            /// The frequency of the simplex
            /// </summary>
            public Double frequency;

            /// <summary>
            /// The final simplex
            /// </summary>
            public Simplex simplex;

            public SimplexWrapper(SimplexWrapper copyFrom)
            {
                deformity = copyFrom.deformity;
                octaves = copyFrom.octaves;
                persistance = copyFrom.persistance;
                frequency = copyFrom.frequency;
            }

            public SimplexWrapper(Double deformity, Double octaves, Double persistance, Double frequency)
            {
                this.deformity = deformity;
                this.octaves = octaves;
                this.persistance = persistance;
                this.frequency = frequency;
            }

            public void Setup(Int32 seed)
            {
                simplex = new Simplex(seed, octaves, persistance, frequency);
            }
        }
    }
}