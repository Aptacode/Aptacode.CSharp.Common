using System;
using System.Collections.Generic;
using Xunit;

namespace Aptacode.CSharp.Common.Utillities.Tests.Extensions
{
    public class CollectionExtensionsTest
    {
        [Fact]
        public void CanAddTwoLists()
        {
            //Arrange
            var sut = new List<int> {1, 2, 3};

            //Act
            sut.AddRange(new List<int> {4, 5, 6});

            //Assert
            Assert.Equal(new List<int> {1, 2, 3, 4, 5, 6}, sut);
        }

        [Fact]
        public void ThrowsArgumentNullException()
        {
            //Arrange
            var sut = new List<int> {1, 2, 3};

            //Assert
            Assert.Throws<ArgumentNullException>(() => sut.AddRange(null));
        }
    }
}