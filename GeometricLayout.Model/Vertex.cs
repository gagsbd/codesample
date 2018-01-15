using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeometricLayout.Model
{
    public class Vertex
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vertex()
        { }

        public Vertex(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}