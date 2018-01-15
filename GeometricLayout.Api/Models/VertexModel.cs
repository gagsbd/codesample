using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeometricayoutApi.Models
{
    public class VertexModel
    {
        [Required,Range(0,float.MaxValue,ErrorMessage ="Must non negative number.")]
        public float X { get; set; }
        [Required, Range(0, float.MaxValue, ErrorMessage = "Must non negative number.")]
        public float Y { get; set; }
    }
}