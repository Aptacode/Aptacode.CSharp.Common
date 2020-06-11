using Aptacode.CSharp.Common.Utilities.Mvvm;
using Xunit;

namespace Aptacode.CSharp.Common.Utilities.Tests.Mvvm
{
    public class BindableBaseTests
    {

        private class FakeBindableBase : BindableBase
        {
            private int _testProperty;

            public int TestProperty
            {
                get => _testProperty;
                set => SetProperty(ref _testProperty, value);
            }

        }

        [Fact]
        public void PropertyChangedFires()
        {
            //Arrange
            var propertyChangedFired = false;
            var sut = new FakeBindableBase();

            sut.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(sut.TestProperty))
                {
                    propertyChangedFired = true;
                }
            };

            //Act
            sut.TestProperty = 10;

            //Assert
            Assert.True(propertyChangedFired);
        }
    }
}
