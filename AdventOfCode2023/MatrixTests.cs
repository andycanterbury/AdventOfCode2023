using NUnit.Framework;
using Utilities;

namespace AdventOfCode2023
{
    public class MatrixTests
    {
        public Matrix<int> testMatrix;

        [SetUp]
        public void Setup()
        {
            testMatrix = new Matrix<int>(3, 3);
        }

        [Test]
        public void ShouldPutValueInFirstPosition()
        {
            testMatrix[0, 0] = 1;
            Assert.AreEqual(1, testMatrix[0, 0]);
        }

        [Test]
        public void ShouldGetNeighborsOfEdgeItem()
        {
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[0, 2] = 3;
            testMatrix[1, 0] = 4;
            testMatrix[1, 1] = 5;
            testMatrix[1, 2] = 6;
            testMatrix[2, 0] = 7;
            testMatrix[2, 1] = 8;
            testMatrix[2, 2] = 9;

            var result = testMatrix.GetNeighbors(0, 1);
            Assert.AreEqual(3, result.Count);
            Assert.Contains(1, result);
            Assert.Contains(3, result);
            Assert.Contains(5, result);
        }

        [Test]
        public void ShouldGetNeighborsOfCenterItem()
        {
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[0, 2] = 3;
            testMatrix[1, 0] = 4;
            testMatrix[1, 1] = 5;
            testMatrix[1, 2] = 6;
            testMatrix[2, 0] = 7;
            testMatrix[2, 1] = 8;
            testMatrix[2, 2] = 9;

            var result = testMatrix.GetNeighbors(1, 1);
            Assert.AreEqual(4, result.Count);
            Assert.Contains(4, result);
            Assert.Contains(6, result);
            Assert.Contains(2, result);
            Assert.Contains(8, result);
        }


        [Test]
        public void ShouldGetNeighborsOfBottomEdgeItem()
        {
            //1 2 3
            //4 5 6
            //7 8 9
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[0, 2] = 3;
            testMatrix[1, 0] = 4;
            testMatrix[1, 1] = 5;
            testMatrix[1, 2] = 6;
            testMatrix[2, 0] = 7;
            testMatrix[2, 1] = 8;
            testMatrix[2, 2] = 9;

            var result = testMatrix.GetNeighbors(2, 1);
            Assert.AreEqual(3, result.Count);
            Assert.Contains(7, result);
            Assert.Contains(9, result);
            Assert.Contains(5, result);
        }

        [Test]
        public void ShouldGetNeighborsOfCornerItem()
        {
            //1 2 3
            //4 5 6
            //7 8 9
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[0, 2] = 3;
            testMatrix[1, 0] = 4;
            testMatrix[1, 1] = 5;
            testMatrix[1, 2] = 6;
            testMatrix[2, 0] = 7;
            testMatrix[2, 1] = 8;
            testMatrix[2, 2] = 9;

            var result = testMatrix.GetNeighbors(2, 2);
            Assert.AreEqual(2, result.Count);
            Assert.Contains(6, result);
            Assert.Contains(8, result);
        }
    }
}
