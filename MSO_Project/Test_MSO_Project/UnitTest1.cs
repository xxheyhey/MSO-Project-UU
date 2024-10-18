using System;
using System.Collections.Generic;
using Xunit;
using MSO_Project;

namespace Test_MSO_Project
{
    public class UnitTest1
    {
        [Fact]
        public void TurnCommand_ShouldChangeOrientationCorrectly()
        {
            // Arrange
            var character = new Character();
            var turnCommand = new Turn("right");

            // Act
            turnCommand.Execute(character);

            // Assert
            Assert.Equal("south", character.Orientation); //Since character is initially facing east, the should be facing south after the turn "right"
        }

        [Fact]
        public void RepeatCommand_ShouldExecuteNestedCommandsCorrectly()
        {
            // Arrange
            var character = new Character();
            var moveCommand = new Move(2);
            var repeatCommand = new Repeat(3, new List<Command> { moveCommand });

            // Act
            repeatCommand.Execute(character);

            // Assert
            Assert.Equal((6, 0), character.Position); // Character should have moved 6 steps (2 * 3)
        }
    }
}
