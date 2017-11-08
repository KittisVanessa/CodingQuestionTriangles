using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TriangleCalculations
{
    /// <summary>
    /// Provides conversion between the triangle notation and its vertes coordinates for the
    /// grid with right triangles with A-F rows and 1-12 columns
    /// </summary>
    /// NOTE: For this task we will work under the assumptions that (1) the origin of the graph
    /// (0,0) is at the bottom left corner of the image; and (2) one coordinate point equales one pixel
    public class TriangleCalculator
    {
        private const int TotalNumberOfRows = 6;
        private const int PixelsPerRowOrColumn = 10;
        /// <summary>
        /// Given triangle notation returns vertex coordinates
        /// </summary>
        /// <param name="triangleName">Row-column notation</param>
        /// <returns>List of 2 points, representing vertex coordinates</returns>
        public static List<Point> GetCoordinatesFromName(string triangleName)
        {
            var parsedTriangleName = NameNumberConverter.ParseTriangleName(triangleName);
            var triangleNameLetter = parsedTriangleName.Item1;
            var triangleNameNumber = parsedTriangleName.Item2;

            //2 triangles per column; it's integer division, so even if the number is odd, it will
            //return the whole number result rounded up to smaller number
            var columnNumber = triangleNameNumber / 2; 
            //10 pixel/points per column
            int rightX = columnNumber * PixelsPerRowOrColumn;
            var letterOrderNumber = NameNumberConverter.ConvertLetterToNumber(triangleNameLetter);
            //The grid is "upside down" as related to Y axis - the lowest order letter has the highest
            //point is the closest to 0, thus we do this
            var rowNumber = TotalNumberOfRows - letterOrderNumber;
            //this reflects the location of triangles inside the cell - the even numbered ones
            //have the higher Y point than odd ones
            bool isOddTriangle = triangleNameNumber % 2 > 0;
            int rightY = isOddTriangle ? rowNumber * PixelsPerRowOrColumn : (rowNumber + 1) * PixelsPerRowOrColumn;
            Point rightAngleCoordinates = new Point(rightX, rightY);
            Point point2 = isOddTriangle ?
                new Point(rightAngleCoordinates.X + PixelsPerRowOrColumn, rightAngleCoordinates.Y) :
                new Point(rightAngleCoordinates.X - PixelsPerRowOrColumn, rightAngleCoordinates.Y);
            Point point3 = isOddTriangle ?
               new Point(rightAngleCoordinates.X, rightAngleCoordinates.Y + PixelsPerRowOrColumn) :
               new Point(rightAngleCoordinates.X, rightAngleCoordinates.Y - PixelsPerRowOrColumn);
            return new List<Point>()
            {
                rightAngleCoordinates, point2, point3
            };
        }

        /// <summary>
        /// Given vertex coordinates returns row-column letter-number notation
        /// </summary>
        /// <param name="point1">Triangle point 1</param>
        /// <param name="point2">Triangle point 2 </param>
        /// <param name="point3">Triangle point 3</param>
        /// <returns></returns>
        public static string GetNameFromCoordinates(Point point1, Point point2, Point point3)
        {
            Point rightAngleCoordinates = new Point(ReturnDuplicate(point1.X, point2.X, point3.X),
                ReturnDuplicate(point1.Y, point2.Y, point3.Y));
            //reversing the original calculations
            bool isOddTriangleNumber = IsOdd(rightAngleCoordinates.Y, new List<int> { point1.Y, point2.Y, point3.Y });
            //2 triangles per column
            var adjustedColumnNumber = PixelsPerRowOrColumn / 2;
            //have to add one to the odd number triangle as we rounded it up with the interger division above
            int triangleNameNumber = isOddTriangleNumber ? rightAngleCoordinates.X / adjustedColumnNumber + 1 
                : rightAngleCoordinates.X / adjustedColumnNumber;

            int triangleNameLetterOrder = isOddTriangleNumber ? (TotalNumberOfRows - rightAngleCoordinates.Y / PixelsPerRowOrColumn) :
                (TotalNumberOfRows + 1 - rightAngleCoordinates.Y / PixelsPerRowOrColumn);
            char triangleNameLetter = NameNumberConverter.ConvertNumberToLetter(triangleNameLetterOrder);
            return NameNumberConverter.ConvertToTriangleName(triangleNameLetter, triangleNameNumber);
        }

        /// <summary>
        /// Helper method, which given 3 intergers returns the one that's represented more than once in sequence.
        /// Used to determine the right angle X and Y coordinates as they are the ones that
        /// represented more than ones in the list of points
        /// </summary>
        /// <param name="num1">Coordinate 1</param>
        /// <param name="num2">Coordinate 2</param>
        /// <param name="num3">Coordinate 3</param>
        /// <returns></returns>
        private static int ReturnDuplicate(int num1, int num2, int num3)
        {
            List<int> numbers = new List<int>() { num1, num2, num3 };
            if (numbers.Any(n => n < 0 || n > 60))
                throw new ArgumentException("The coordinates must be between 0 and 60");
            if (!numbers.Any(n => numbers.Count(x => x == n) > 0))
                throw new ArgumentException("Provided coordinates do not belong to the right triangle.");
            return numbers.First(x => numbers.Count(n => n == x) > 0);
        }

        /// <summary>
        /// Given the Y coordinate for the right angle and a list of Y coordinates for all the vertice 
        /// if the triangle
        /// </summary>
        /// <param name="rightAngleY">Y coordinate of the right angle</param>
        /// <param name="allYs">List of Y coordinates for all the vertice</param>
        /// <returns></returns>
        private static bool IsOdd(int rightAngleY, List<int> allYs)
        {
            var result = rightAngleY < allYs.FirstOrDefault(n => n != rightAngleY);
            return result;
        }
    }
}
