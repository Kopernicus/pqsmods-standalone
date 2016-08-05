/**
 * libpqsmods - A standalone implementation of KSP's PQSMods
 * Copyright (c) Thomas P. 2016
 * Licensed under the terms of the MIT license
 */

using System;
using LibNoise.Generator;
using Microsoft.Xna.Framework;
using ProceduralQuadSphere.KSP;
using ProceduralQuadSphere.Unity;

namespace ProceduralQuadSphere
{
    /// <summary>
    /// A mod that adds procedural craters
    /// </summary>
    /// <seealso cref="PQSMod" />
    public class PQSMod_VoronoiCraters : PQSMod
    {
        #region VoronoiFields
        private Voronoi voronoi;
        public Double deformation;
        public Int32 voronoiSeed;
        public Double voronoiDisplacement;
        public Double voronoiFrequency;
        public Curve craterCurve;
        public Curve jitterCurve;
        private Simplex simplex;
        public Int32 simplexSeed;
        public Double simplexOctaves;
        public Double simplexPersistence;
        public Double simplexFrequency;
        public Single jitter;
        public Single jitterHeight;
        public Gradient craterColourRamp;
        public Single rFactor;
        public Single rOffset;
        public Single colorOpacity;
        public Boolean DebugColorMapping;
        private Single r;
        #endregion

        /// <summary>
        /// Init the mod with default values
        /// </summary>
        public PQSMod_VoronoiCraters() : base()
        {
            // Init Mun Default
            colorOpacity = 0.7f;
            DebugColorMapping = false;
            deformation = 500;
            jitter = 0.1f;
            jitterHeight = 3;
            rFactor = 1;
            rOffset = 1;
            simplexFrequency = 120;
            simplexOctaves = 3;
            simplexPersistence = 0.5;
            simplexSeed = 123123;
            voronoiDisplacement = 0;
            voronoiFrequency = 22;
            voronoiSeed = 824;

            // Curves
            craterCurve = new Curve();
            craterCurve.Keys.Add(new CurveKey(-0.9982381f, -0.7411783f, -0.06500059f, -0.06500059f));
            craterCurve.Keys.Add(new CurveKey(-0.9332262f, -0.7678316f, -0.2176399f, -0.2176399f));
            craterCurve.Keys.Add(new CurveKey(-0.8990405f, -0.7433339f, -2.560626f, -2.560626f));
            craterCurve.Keys.Add(new CurveKey(-0.7445966f, -0.8581167f, 0.4436148f, 0.4436148f));
            craterCurve.Keys.Add(new CurveKey(-0.4499771f, -0.1392395f, 5.289535f, 5.289535f));
            craterCurve.Keys.Add(new CurveKey(-0.4015177f, 0.2551735f, 9.069458f, -2.149609f));
            craterCurve.Keys.Add(new CurveKey(-0.2297457f, 0.002857953f, -0.4453675f, -0.4453675f));
            craterCurve.Keys.Add(new CurveKey(0.2724952f, 0.00423781f, -0.01884932f, -0.01884932f));
            craterCurve.Keys.Add(new CurveKey(0.9998434f, -0.004090764f, 0.01397126f, 0.01397126f));
            jitterCurve = new Curve();
            jitterCurve.Keys.Add(new CurveKey(-1.000701f, 0.4278412f, 0.1577609f, 0.1577609f));
            jitterCurve.Keys.Add(new CurveKey(-0.7884969f, 0.09487452f, -0.7739663f, -0.7739663f));
            jitterCurve.Keys.Add(new CurveKey(-0.6091803f, 0.072019f, 0.123537f, 0.123537f));
            jitterCurve.Keys.Add(new CurveKey(-0.3930514f, 0.3903495f, 3.300831f, 3.300831f));
            jitterCurve.Keys.Add(new CurveKey(-0.3584836f, 0.8643304f, 0.07139917f, 0.07139917f));
            jitterCurve.Keys.Add(new CurveKey(-0.2988068f, 0.002564805f, -0.01814346f, -0.01814346f));
            jitterCurve.Keys.Add(new CurveKey(0.9970253f, 0.003401639f, 0f, 0f));

            // Color ramp
            craterColourRamp = new Gradient();
            craterColourRamp.SetKeys(new GradientColorKey[]
            {
                new GradientColorKey(new Color(0.271f, 0.271f, 0.271f, 1f), 0.005889982f),
                new GradientColorKey(new Color(0.188f, 0.188f, 0.188f, 1f), 0.123537f),
                new GradientColorKey(new Color(0.329f, 0.329f, 0.329f, 1f), 0.5294118f),
                new GradientColorKey(new Color(0.584f, 0.584f, 0.584f, 1f), 0.6411841f),
                new GradientColorKey(new Color(0.400f, 0.400f, 0.400f, 1f), 0.7911803f)
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1, 0),
                new GradientAlphaKey(1, 1)
            });
        }

        /// <summary>
        /// Initializes the base mod
        /// </summary>
        public override void OnSetup()
        {
            this.voronoi = new Voronoi(voronoiFrequency, voronoiDisplacement, voronoiSeed, true);
            this.simplex = new Simplex(simplexSeed, simplexOctaves, simplexPersistence, simplexFrequency);
        }

        /// <summary>
        /// Called when the parent sphere builds it's color
        /// </summary>
        public override void OnVertexBuild(VertexBuildData data)
        {
            r = r * rFactor + rOffset;
            data.vertColor = !DebugColorMapping ? Color.Lerp(data.vertColor, craterColourRamp.Evaluate(r), (1f - r) * colorOpacity) : Color.Lerp(Color.magenta, data.vertColor, r);
        }

        /// <summary>
        /// Called when the parent sphere builds it's height
        /// </summary>
        public override void OnVertexBuildHeight(VertexBuildData data)
        {
            Single vorH = (Single)voronoi.GetValue(data.directionFromCenter);
            Single spxH = (Single)simplex.noise(data.directionFromCenter);
            Single jtt = spxH * jitter * jitterCurve.Evaluate(vorH);
            r = vorH + jtt;
            Single h = craterCurve.Evaluate(r);
            data.vertHeight += ((Double)h + jitterHeight * jtt * h) * deformation;
        }
    }
}