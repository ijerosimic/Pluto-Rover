using System;
using FluentAssertions;
using Moq;
using RoverCore;
using RoverCore.Rover;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace RoverTests;

public class AppTests
{
    private readonly Mock<IOutputPrinter> _mockPrinter = new();
    private readonly Mock<IRover> _mockRover = new();

    [Fact]
    public void It_Returns_True_When_Passed_Valid_Arguments_String()
    {
        const string validArgument = "FFFR";
        var mockArgs = new Arguments(new[] { validArgument });
        _mockRover.Setup(x => x.Move(validArgument));

        var sut = new App(mockArgs, _mockPrinter.Object, _mockRover.Object);
        var result = sut.Run();

        _mockPrinter.Verify(x => x.PrintSuccessMessage("Successfully moved the rover."), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void It_Returns_False_When_Passed_Invalid_Arguments_Length()
    {
        var invalidLengthArgs = new[] { "FFF", "FFR" };
        var mockArgs = new Arguments(invalidLengthArgs);

        var sut = new App(mockArgs, _mockPrinter.Object, _mockRover.Object);
        var result = sut.Run();

        _mockPrinter.Verify(x => x.PrintInvalidArgumentsLength(), Times.Once);
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void It_Returns_False_When_Passed_An_Invalid_Argument(string invalidArgument)
    {
        var mockArgs = new Arguments(new[] { invalidArgument });

        var sut = new App(mockArgs, _mockPrinter.Object, _mockRover.Object);
        var result = sut.Run();

        _mockPrinter.Verify(x => x.PrintInvalidArgument(), Times.Once);
        result.Should().BeFalse();
    }

    [Fact]
    public void It_Returns_False_When_Rover_Throws_An_Exception()
    {
        const string invalidArgs = "X*!";
        var mockArgs = new Arguments(new[] { invalidArgs });
        var mockException = new ArgumentOutOfRangeException(nameof(invalidArgs), "Unrecognized rover move command");
        _mockRover.Setup(x => x.Move(invalidArgs)).Throws(mockException);

        var sut = new App(mockArgs, _mockPrinter.Object, _mockRover.Object);
        var result = sut.Run();

        _mockPrinter.Verify(x => x.PrintErrorMessage(mockException), Times.Once);
        result.Should().BeFalse();
    }
}