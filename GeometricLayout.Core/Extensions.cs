using GeometricLayout.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricLayout.Core
{
    internal static class Extensions
    {
        public static bool IsRightAngled(this Triangle triangle,double sideLength)
        {
            var side1Length = triangle.R.DistanceFrom(triangle.A);
            var side2Length = triangle.R.DistanceFrom(triangle.B);
            if (side1Length != sideLength && side2Length != sideLength)
            {
                return false;
            }
            return Math.Round(Math.Pow(triangle.A.DistanceFrom(triangle.B), 2),2) ==
                Math.Pow(side1Length,2) + Math.Pow(side2Length, 2);
        }

        public static bool AreValidVertex(this Triangle triangle, double sideLength)
        {
            return (triangle.A.IsValidVertex(sideLength) && triangle.B.IsValidVertex(sideLength) && triangle.R.IsValidVertex(sideLength));
           
        }

        public static double DistanceFrom(this Vertex to, Vertex from)
        {
           return Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        }

        public static bool IsValidVertex(this Vertex vertex, double lenght)
        {
            return (vertex.X >= 0 && vertex.X % lenght == 0 && vertex.Y >= 0 && vertex.Y % lenght == 0);
        }
    }

  
}
