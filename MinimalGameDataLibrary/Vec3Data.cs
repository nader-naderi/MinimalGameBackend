﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalGameDataLibrary
{
    /// <summary>
    /// Legacy code: since we're using strings as positions for ease of db integrations, we're no longer using this DataType.
    /// </summary>
    public class Vec3Data
    {
        public Vec3Data(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [Range(float.MinValue, float.MaxValue, ErrorMessage = "X value is out of valid range.")]
        public float X { get; set; }

        [Range(float.MinValue, float.MaxValue, ErrorMessage = "Y value is out of valid range.")]
        public float Y { get; set; }

        [Range(float.MinValue, float.MaxValue, ErrorMessage = "Z value is out of valid range.")]
        public float Z { get; set; }
    }
}
