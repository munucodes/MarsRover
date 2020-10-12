using System;
namespace DenizMarsRoverNetCore
{
    public class Rover
    {
        private int xCoordinate;
        private int yCoordinate;
        private CardinalCompassPoint compassPoint;
        private string nasaCommand;
            

        public int XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public int YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }

        public CardinalCompassPoint CompassPoint
        {
            get { return compassPoint; }
            set { compassPoint = value; }
        }

        public string NasaCommand
        {
            get { return nasaCommand; }
            set { nasaCommand = value; }
        }




        //did not validate here again. 
        public Rover(string position, string commands)
        {
            Int32.TryParse(position.Split(" ")[0], out xCoordinate);
            Int32.TryParse(position.Split(" ")[1], out yCoordinate);
            string cardinalInitial = position.Split(" ")[2];
            switch(cardinalInitial)
            {
                case "N":
                    compassPoint = CardinalCompassPoint.North;
                    break;
                case "E":
                    compassPoint = CardinalCompassPoint.East;
                    break;
                case "S":
                    compassPoint = CardinalCompassPoint.South;
                    break;
                case "W":
                    compassPoint = CardinalCompassPoint.West;
                    break;
                default:
                    compassPoint = CardinalCompassPoint.Default;
                    break;


            }
            nasaCommand = commands;
        }

        //Actually don't like switch a lot since it is not scalable
        //was thinking about making a calculation such as
        //(4 + number value of enum +1(clockwise))%4
        //and parsing it into the enum but this seemed more easy to understand
        //To-Do: Further thinking about turning with different angle. Not 90 degrees is needed.
        public void TurnRight()
        {
            switch(compassPoint)
            {
                case CardinalCompassPoint.North:
                    compassPoint = CardinalCompassPoint.East;
                    break;

                case CardinalCompassPoint.East:
                    compassPoint = CardinalCompassPoint.South;
                    break;

                case CardinalCompassPoint.South:
                    compassPoint = CardinalCompassPoint.West;
                    break;

                case CardinalCompassPoint.West:
                    compassPoint = CardinalCompassPoint.North;
                    break;

                default:
                    break;

            }


        }

        //Same as TurnRight
        public void TurnLeft()
        {
            switch (compassPoint)
            {
                case CardinalCompassPoint.North:
                    compassPoint = CardinalCompassPoint.West;
                    break;

                case CardinalCompassPoint.West:
                    compassPoint = CardinalCompassPoint.South;
                    break;

                case CardinalCompassPoint.South:
                    compassPoint = CardinalCompassPoint.East;
                    break;

                case CardinalCompassPoint.East:
                    compassPoint = CardinalCompassPoint.North;
                    break;

                default:
                    break;
            }
        }

        //Same logic as turning, but this time adding to the coordinates. 
        public void Move()
        {
            switch (compassPoint)
            {
                case CardinalCompassPoint.North:
                    yCoordinate++;
                    break;

                case CardinalCompassPoint.West:
                    xCoordinate--; 
                    break;

                case CardinalCompassPoint.South:
                    yCoordinate--;
                    break;

                case CardinalCompassPoint.East:
                    xCoordinate++;
                    break;

                default:
                    break;

            }
        }

        //added this one, because plateu is a "real" place and cannot have negative coordinates.
        //If rover comes to the any edge of the plateu, it shouldn't move.
        public bool CanMove(Plateu plateu)
        {
            bool CanMove = false;

            switch (compassPoint)
            {
                case CardinalCompassPoint.North:
                    if(yCoordinate < plateu.UpperRightCoordinateY)
                        CanMove = true;
                    break;

                case CardinalCompassPoint.West:
                    if (xCoordinate > 0)
                        CanMove = true;
                    break;

                case CardinalCompassPoint.South:
                    if (yCoordinate > 0)
                        CanMove = true;
                    break;

                case CardinalCompassPoint.East:
                    if(xCoordinate < plateu.UpperRightCoordinateX)
                        CanMove = true;
                    break;
                default:
                    break;
            }

            return CanMove;
        }

        //This is where nasa command is turned into the steps of the rover.
        //This gives the final position of the rover in a given plateu and command
        public void ObeyCommand( Plateu plateu)
        {
            foreach(char command in this.nasaCommand)
            {
                switch(command)
                {
                    case 'L':
                        this.TurnLeft();
                        break;

                    case 'R':
                        this.TurnRight();
                        break;

                    case 'M':
                        if (this.CanMove(plateu)) //had to check if can move
                            this.Move();
                        break;

                    default:
                        break;
                }

            }
        }


    }
}
