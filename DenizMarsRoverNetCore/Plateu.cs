using System;
namespace DenizMarsRoverNetCore
{
    public class Plateu
    {
        private int upperRightCoordinateX;
        private int upperRightCoordinateY;


        public int UpperRightCoordinateX
        {
            get { return upperRightCoordinateX; }
            set { upperRightCoordinateX = value; }
        }

        public int UpperRightCoordinateY
        {
            get { return upperRightCoordinateY; }
            set { upperRightCoordinateY = value; }
        }

        public Plateu(string plateuUpperRightCoordinates)
        {
           
            Int32.TryParse(plateuUpperRightCoordinates.Split(" ")[0], out upperRightCoordinateX);
            Int32.TryParse(plateuUpperRightCoordinates.Split(" ")[1], out upperRightCoordinateY);




        }
    }
}
