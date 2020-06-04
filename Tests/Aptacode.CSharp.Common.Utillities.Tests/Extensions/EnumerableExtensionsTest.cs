using System.Collections.Generic;
using System.Linq;
using Aptacode.CSharp.Common.Utillities.Extensions;
using Xunit;

namespace Aptacode.CSharp.Common.Utillities.Tests.Extensions
{
    public class EnumerableExtensionsTest
    {
        [Fact]
        public void CanShuffle()
        {
            //Arrange
            var sut = new List<int>{1,2,3,4,5,6,7,8,9,10};

            //Act
            var result1 = sut.Shuffle().ToList();
            var result2 = sut.Shuffle().ToList();

            //Assert
            foreach (var i in sut)
            {
                Assert.Contains(i, result1);
                Assert.Contains(i, result2);
            }

            Assert.Equal(sut.Count, result1.Count());
            Assert.Equal(sut.Count, result2.Count());

            Assert.NotEqual(result1, result2);
            Assert.NotEqual(result2, result1);
        }

        [Fact]
        public void CanTakeLast0()
        {
            //Arrange
            var sut = new List<int> { 1,2,3,4,5,6,7,8,9,10 };

            //Act
            var emptyList = EnumerableExtensions.TakeLast(sut, 0);

            //Assert
            Assert.Empty(emptyList);
        }

        [Fact]
        public void CanTakeLast1()
        {
            //Arrange
            var sut = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Act
            var actual =  EnumerableExtensions.TakeLast(sut, 1);

            //Assert
            Assert.Equal(new List<int>(){10}, actual);
        }

        [Fact]
        public void CanTakeLast10()
        {
            //Arrange
            var sut = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Act
            var actual = EnumerableExtensions.TakeLast(sut, 10);

            //Assert
            Assert.Equal(sut, actual);
        }
    }
}
