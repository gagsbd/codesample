using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeometricayoutApi.Models
{
    public class TriangleModel
    {
        /// <summary>
        /// Right Angled vertext
        /// </summary>
        
        [Required]
        public VertexModel R { get; set; }
        /// <summary>
        /// Side angle
        /// </summary>

        [Required]
        public VertexModel A { get; set; }
        /// <summary>
        /// Side ange
        /// </summary>

        [Required]
        public VertexModel B { get; set; }
    }
}