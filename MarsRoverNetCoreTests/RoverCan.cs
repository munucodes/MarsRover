using System;
using DenizMarsRoverNetCore;
using Xunit;

namespace MarsRoverNetCoreTests
{
    public class RoverCan
    {
        [Fact]
        public void TurnRight()
        {
            //arrange
            Rover rover = new Rover("1 3 E", "");
            //act
            rover.TurnRight();
            //assert
            Assert.Equal(CardinalCompassPoint.South, rover.CompassPoint );
        }

        [Fact]
        public void TurnLeft()
        {
            //arrange
            Rover rover = new Rover("1 3 E", "");
            //act
            rover.TurnLeft();
            //assert
            Assert.Equal(CardinalCompassPoint.North, rover.CompassPoint);
        }

        [Fact]
        public void Move()
        {
            //arrange
            Rover rover = new Rover("1 3 E", "");
            //act
            rover.Move();
            //assert
            Assert.Equal(2, rover.XCoordinate);
        }

        [Fact]
        public void CanMove()
        {
            //arrange
            Rover rover = new Rover("5 1 E", "");
            Plateu plateu = new Plateu("5 5");
            //act
            bool act = rover.CanMove(plateu);
            //assert
            Assert.False(act);
        }

        [Fact]
        public void ObeyCommand()
        {
            //arrange
            Rover rover = new Rover("3 3 E", "MMRMMRMRRM");
            Plateu plateu = new Plateu("5 5");
            //act
            rover.ObeyCommand(plateu);
            //assert
            Assert.Equal("5 1 E", (rover.XCoordinate
                        + " "
                        + rover.YCoordinate
                        + " "
                        + (rover.CompassPoint.ToString("F")[0])));
        }
    }

}
