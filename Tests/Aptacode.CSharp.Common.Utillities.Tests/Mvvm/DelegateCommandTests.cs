using Aptacode.CSharp.Common.Utilities.Mvvm;
using Xunit;

namespace Aptacode.CSharp.Common.Utilities.Tests.Mvvm
{
    public class DelegateCommandTests
    {
        [Fact]
        public void DelegateCommandCanExecuteChangedCanFire()
        {
            //Arrange
            var canExecuteChangedFired = false;
            var sut = new DelegateCommand(_ => { }, _ => true);

            sut.CanExecuteChanged += (s, e) => { canExecuteChangedFired = true; };

            //Act
            sut.RaiseCanExecuteChanged();

            //Assert
            Assert.True(canExecuteChangedFired);
        }

        [Fact]
        public void DelegateCommandCanFireWhenCanExecuteIsNotSet()
        {
            //Arrange
            var commandFired = false;
            var sut = new DelegateCommand(_ => { commandFired = true; });

            //Act
            sut.Execute(null);

            //Assert
            Assert.True(commandFired);
        }

        [Fact]
        public void DelegateCommandCanFireWhenCanExecuteIsTrue()
        {
            //Arrange
            var commandFired = false;
            var sut = new DelegateCommand(_ => { commandFired = true; }, _ => true);

            //Act
            sut.Execute(null);

            //Assert
            Assert.True(commandFired);
        }

        [Fact]
        public void DelegateCommandDoesNotFireWhenCanExecuteIsFalse()
        {
            //Arrange
            var commandFired = false;
            var sut = new DelegateCommand(_ => { commandFired = true; }, _ => false);

            //Act
            sut.Execute(null);

            //Assert
            Assert.False(commandFired);
        }
    }
}