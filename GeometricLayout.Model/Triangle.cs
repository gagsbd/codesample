using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeometricLayout.Model
{
    public class Triangle
    {
        /// <summary>
        /// Right Angled vertext
        /// </summary>
        public Vertex R { get; set; }
        /// <summary>
        /// Side angle
        /// </summary>
        public Vertex A { get; set; }
        /// <summary>
        /// Side ange
        /// </summary>
        public Vertex B { get; set; }
    }
}