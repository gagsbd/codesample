using System;
using ms = Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using GeometricLayout.Core;

namespace GeometricLayout.Core.Test
{

    public class PlotterTest
    {
        [Theory]
        [InlineData("A", 1, 0, 0, 10, 10, 0, 10)] //row,colum,a.x,a.y,b.x,b.y,r.x,r.y
        [InlineData("A", 4, 10, 0, 20, 10, 20, 0)] //row,colum,a.x,a.y,b.x,b.y,r.x,r.y
        [InlineData("A", 12, 50, 0, 60, 10, 60, 0)]
        [InlineData("F", 1, 0, 50, 10, 60, 0, 60)]
        [InlineData("F", 6, 20, 50, 30, 60, 30, 50)]
        [InlineData("F", 12, 50, 50, 60, 60, 60, 50)]
        public void GetTriangleTest_ValidInput(string row, int col, int ax, int ay, int bx, int by, int rx, int ry)
        {


            var actual = new Plotter().GetTriangle(new Model.Ordinal(row, col));
            ms.Assert.AreEqual(ax, actual.A.X, $"Ax is incorrect, ({row},{col}).");
            ms.Assert.AreEqual(ay, actual.A.Y, $"Ay is incorrect, ({row},{col}).");
            ms.Assert.AreEqual(bx, actual.B.X, $"Bx is incorrect, ({row},{col}).");
            ms.Assert.AreEqual(by, actual.B.Y, $"By is incorrect, ({row},{col}).");
            ms.Assert.AreEqual(rx, actual.R.X, $"Rx is incorrect, ({row},{col}).");
            ms.Assert.AreEqual(ry, actual.R.Y, $"Ry is incorrect, ({row},{col}).");
        }

        [Theory]
        [InlineData("",1)]
        [InlineData(null, 1)]
        public void GetTriangleTest_EmptyValueRow(string row, int col)
        {
            Assert.Throws<ArgumentNullException>(()=> new Plotter().GetTriangle(new Model.Ordinal(row, col)));
        }

        [Theory]
        [InlineData("AB", 1)]
        public void GetTriangleTest_InValidValueForRow(string row, int col)
        {
            Assert.Throws<ArgumentException>(() => new Plotter().GetTriangle(new Model.Ordinal(row, col)));
        }

        [Theory]
        [InlineData("G", 1)]
        [InlineData("0", 1)]
        [InlineData("A", -1)]
        [InlineData("A", 0)]
        [InlineData("A", 13)]
        public void GetTriangleTest_InValidRange(string row, int col)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Plotter().GetTriangle(new Model.Ordinal(row, col)));
        }

        [Theory]
        [InlineData(0, 0, 10, 10, 0, 10, "A", 1)] //row,colum,a.x,a.y,b.x,b.y,r.x,r.y
        [InlineData( 10, 0, 20, 10, 20, 0,"A", 4)] //row,colum,a.x,a.y,b.x,b.y,r.x,r.y
        [InlineData( 50, 0, 60, 10, 60, 0, "A", 12)]
        [InlineData( 0, 50, 10, 60, 0, 60, "F", 1)]
        [InlineData( 20, 50, 30, 60, 30, 50, "F", 6)]
        [InlineData( 50, 50, 60, 60, 60, 50, "F", 12)]
        public void GetOrdinalTest(int ax, int ay, int bx, int by, int rx, int ry, string row, int col)
        {
            var actual = new Plotter().GetOrdinal(new Model.Triangle()
            {
                A = new Model.Vertex(ax, ay),
                B = new Model.Vertex(bx, by),
                R = new Model.Vertex(rx, ry)
            }
            );

            ms.Assert.AreEqual(actual.Row, row, $"Row {row} is incorrect.");
            ms.Assert.AreEqual(actual.Column, col, $"Column {col} is incorrect.");

        }

        [Theory]
        [InlineData(-1, 0, 10, 10, 0, 10)] //invalid vertex
        [InlineData(10, -1, 20, 10, 20, 0)] //invalid vertex
        [InlineData(-10, 0, 20, 10, 20, 0)] //invalid vertex
        [InlineData(10, 0, 20, -10, 20, 0)] //invalid vertex
        [InlineData(45, 0, 60, 10, 60, 0)]  //not right angled triangle
        [InlineData(60, 0, 60, 5, 60, 0)]  //not right angled triangle
        [InlineData(15, 0, 20, 5, 20, 0)] //right angled triange of lenght less than 10
        [InlineData(20, 0, 35, 15, 20, 15)] //right angled triange of lenght more than 10
       // [InlineData(20, 50, 30, 60, 30, 50)]
       // [InlineData(50, 50, 60, 60, 60, 50)]
        public void GetOrdinalTest_InvalidInput(int ax, int ay, int bx, int by, int rx, int ry)
        {
           Assert.Throws<ArgumentException>(()=> new Plotter().GetOrdinal(new Model.Triangle()
            {
                A = new Model.Vertex(ax, ay),
                B = new Model.Vertex(bx, by),
                R = new Model.Vertex(rx, ry)
            }
            ));

        }

        [Theory]
        [InlineData(0, 10, 10, 0, 0, 0)] //invalid orientatio
        [InlineData(10, 10, 20, 0, 20, 10)] //invalid orientatio
        [InlineData(0, 30, 10, 20, 0, 20)] //invalid orientatio
        [InlineData(10, 20, 20, 10, 20, 20)] //invalid orientatio

        public void GetOrdinalTest_InvalidOrientation(int ax, int ay, int bx, int by, int rx, int ry)
        {
            Assert.Throws<ArgumentException>(() => new Plotter().GetOrdinal(new Model.Triangle()
            {
                A = new Model.Vertex(ax, ay),
                B = new Model.Vertex(bx, by),
                R = new Model.Vertex(rx, ry)
            }
             ));

        }
    }
}
