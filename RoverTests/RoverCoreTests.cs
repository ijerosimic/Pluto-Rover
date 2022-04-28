using System;
using RoverCore;
using RoverCore.Rover;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace RoverTests;

public class RoverCoreTests
{
    [Theory]
    [InlineData("FFRF", "(1, 2, E)")]
    [InlineData("FFRRBBB", "(0, 5, S)")]
    [InlineData("RFFFLFF", "(3, 2, N)")]
    [InlineData("FFFFFBBBBB", "(0, 0, N)")]
    [InlineData("FFBBFFBBFFBB", "(0, 0, N)")]
    [InlineData("RRRRRRRR", "(0, 0, N)")]
    [InlineData("LLLLLLL", "(0, 0, E)")]
    [InlineData("F", "(0, 1, N)")]
    [InlineData("L", "(0, 0, W)")]
    [InlineData("LL", "(0, 0, S)")]
    [InlineData("LLL", "(0, 0, E)")]
    [InlineData("LLLL", "(0, 0, N)")]
    [InlineData("R", "(0, 0, E)")]
    [InlineData("RR", "(0, 0, S)")]
    [InlineData("RRR", "(0, 0, W)")]
    [InlineData("RRRR", "(0, 0, N)")]
    public void It_Moves_The_Rover_To_A_Correct_Position_When_Passed_A_Valid_Command(string validCommand, string expectedCoordinates)
    {
        var sut = new Rover();
        sut.Move(validCommand);
        var actualCoordinates = sut.GetCurrentCoordinates();

        Assert.Equal(expectedCoordinates, actualCoordinates);
    }

    [Theory]
    [InlineData("f")]
    [InlineData("b")]
    [InlineData("r")]
    [InlineData("l")]
    [InlineData("X")]
    [InlineData("D")]
    [InlineData("G")]
    [InlineData("SDSDDS")]
    [InlineData("244242")]
    [InlineData("1")]
    [InlineData("0")]
    [InlineData(" ")]
    [InlineData("*")]
    [InlineData("\\")]
    [InlineData("//")]
    [InlineData("-")]
    [InlineData("_")]
    [InlineData("o")]
    [InlineData("d")]
    public void It_Throws_ArgumentOutOfRangeException_When_Passed_An_Invalid_Command(string invalidCommand)
    {
        var sut = new Rover();

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Move(invalidCommand));
        Assert.Equal("Unrecognized rover move command. (Parameter 'command')", ex.Message);
    }

    [Theory]
    [InlineData("B")]
    [InlineData("BB")]
    [InlineData("FBB")]
    [InlineData("FFFBBBB")]
    [InlineData("LF")]
    [InlineData("RB")]
    [InlineData("RFFBBB")]
    [InlineData("LBBBFFFF")]
    [InlineData("LLF")]
    [InlineData("RRF")]
    [InlineData("LLLLB")]
    [InlineData("RRRRB")]
    public void It_Throws_ArgumentOutOfRangeException_When_Passed_A_Valid_Command_That_Would_Move_Rover_Out_Of_Bounds(string invalidCommand)
    {
        var sut = new Rover();

        var ex = Assert.Throws<ArgumentException>(() => sut.Move(invalidCommand));
        Assert.Equal("Command would move rover out of bounds.", ex.Message);
    }

    [Fact]
    public void It_Returns_The_Starting_Position_When_Rover_Is_In_Initial_State()
    {
        var sut = new Rover();
        
        Assert.Equal("(0, 0, N)", sut.GetCurrentCoordinates());
    }
}