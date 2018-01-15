
using GeometricLayout.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricLayout.Core
{
    public class Plotter
    {
        const string ROWS = "ABCDEF";
        const int MIN_COL = 1;
        const int MAX_COL = 12;
        const int BASE_LENGHT = 10;
        /// <summary>
        /// Returns triangle given a row and colum.
        /// This method assumes co-ordinates of  the left top corner as 0,0
        /// </summary>
        /// <param name="row">Row of a triangle</param>
        /// <param name="column">Column of a triangl</param>
        /// <returns></returns>
        public Triangle GetTriangle(Ordinal ordinal)
        {
            return GetTriangle(ordinal, BASE_LENGHT);
        }


        //We can expose this if in case in future we decide to provide flexibiity for user to provide base length
        private  Triangle GetTriangle(Ordinal ordinal,int baseLenght)
        {
            Triangle result = null;

            if (String.IsNullOrEmpty(ordinal?.Row))
            {
                throw new ArgumentNullException("Missing a value for row.");
            }

            if (ordinal.Row.Length != 1)
            {
                throw new ArgumentException("Invalid value  for row.");
            }

            int rowIndex = ROWS.IndexOf(ordinal.Row.ToUpper());

            if (rowIndex < 0 || ordinal.Column < MIN_COL || ordinal.Column > MAX_COL)
            {
                throw new ArgumentOutOfRangeException($"Row {ordinal.Row} and column {ordinal.Column} is not in acceptable range.");
            }


            result = new Triangle() { A = new Vertex(), B = new Vertex() };
            var index_of_square_block = ((ordinal.Column + 1) / 2); // index of sqaue to which colum index belongs to
            //We can use column number(index)  decide the X of side vertex 
            result.A.X = (index_of_square_block - 1) * BASE_LENGHT; //0
            result.B.X = index_of_square_block * BASE_LENGHT;//10

            //We can use the Row index to easily decide the y co-ordinate of base vertex 
            result.A.Y = (rowIndex) * BASE_LENGHT; // 0
            result.B.Y = (rowIndex + 1) * BASE_LENGHT; //10


            //X and Y of right vertext would be decided based in row and colum both
            result.R = new Vertex { X = (ordinal.Column / 2) * BASE_LENGHT, Y = (rowIndex + (ordinal.Column % 2)) * BASE_LENGHT };
            // 0,10

            //(0,0)(10,10)(0,10)
            return result;

        }

        public Ordinal GetOrdinal(Triangle triangle)
        {
            if (triangle == null)
            {
                throw new ArgumentNullException(nameof(triangle));
            }

            if (!triangle.AreValidVertex(BASE_LENGHT))
            {
                throw new ArgumentException($"Specified {nameof(triangle)} does not belong to any roe or column.");
            }

            if (!triangle.IsRightAngled(BASE_LENGHT))
            {
                throw new   ArgumentException($"Specified {nameof(triangle)} is not right angled triangle with side length {BASE_LENGHT}.");
            }

            //Canculate centroid vertex
            var centroid = new Vertex(((triangle.R.X + triangle.A.X + triangle.B.X) / 3), ((triangle.R.Y + triangle.A.Y + triangle.B.Y) / 3));

            //Check the orientaton of the triangle
            //If the triangle is of the proper allowed orientation the cetroid will never be equidistance from X and Y with in its square
            
            if (Math.Round(centroid.X %  10,2)  == Math.Round(centroid.Y % 10,2))
            {
                throw new System.ArgumentException($"Specified {nameof(triangle)} is not of allowed orientation.");
            }

            //to find the column index dtermine in which half of the sqaure cetroid exists 
            
            var columIndex = Math.Floor(centroid.X /(BASE_LENGHT / 2)) + 1;
            var rowIndex = Math.Ceiling(centroid.Y / (BASE_LENGHT)) - 1;

            if (rowIndex < 0 || rowIndex > 5)
            {
                throw new ApplicationException($"Row index {rowIndex} is not valid.");
            }

            return new Ordinal(ROWS.Substring((int)rowIndex,1),(int)columIndex); 
           
        }


    }
}
