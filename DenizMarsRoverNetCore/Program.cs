using System;
using System.Collections.Generic;

namespace DenizMarsRoverNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Greetings from the programmer
            AppGreeting();

            //put a while(true) to be able to repeat the program to ask new instructions from the console
            //until the user presses anyhting other than "y" or "Y" as an answer.
            while (true)
            {
                //Method to get plateu's upper right coordinates
                Plateu plateu = GetPlateuCoordinates();
                //method to get number of rovers to command
                int roverCount = GetRoverCount();


                List<Rover> roverList = new List<Rover>();

                for (int i = 0; i < roverCount; i++)
                {
                    //for each rover we get the initial position in format of example "1 2 N"
                    Rover rover = GetRoverInitialPosition(i);
                    //then we get the command in string format consisting of letters "R L M"
                    rover.NasaCommand = GetRoverCommands(i);
                    roverList.Add(rover);

                }

                foreach (Rover rover in roverList)
                {
                    int j = 1;
                    //Decided to give initial position and command of the rover to the user before changing them
                    Console.WriteLine("Obeying Commands for rover No."+ j
                        + " with initial Position:" + rover.XCoordinate + " " + rover.YCoordinate + " " + (rover.CompassPoint.ToString("F")[0])
                        + " and Command:"  + rover.NasaCommand ); 

                    //rover goes to the final position in the plateu. beep beep
                    rover.ObeyCommand(plateu);

                    //Final position shown in the console
                    Console.WriteLine( "Final Position of Rover No."+ j + "is:");
                    Console.WriteLine(rover.XCoordinate + " " + rover.YCoordinate + " " + (rover.CompassPoint.ToString("F")[0]));

                }

                //end of loop
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All orders are completed.");
                Console.ResetColor();

                //if user wants to start again 
                Console.WriteLine("Do you have any more orders for new rovers? [Y or N]:");
                string Answer = Console.ReadLine()[0].ToString().ToUpper();
                if(Answer.Equals("Y"))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            //end of the program
            Console.WriteLine("Have a good day officer!");
            Console.ReadKey();
        }



        private static void AppGreeting()
        {
            //Set app vars
            string appName = "NASA Mars Rover Orientation";
            string appAuthor = "Deniz Kertmen";
            //Change Text Color
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}: by {1}", appName, appAuthor);
            Console.ResetColor();
        }

        private static Plateu GetPlateuCoordinates()
        {
            //put a while(true) to get to ask to user again if the format is not appropriate
            while (true)
            {

                Console.WriteLine("Please Enter Coordinates of the Upper Right Corner of the Explored Plateu: ");
                string plateuCoordinates = Console.ReadLine();

                //TO-DO: use regex next time, it's better
                if(plateuCoordinates.Length < 3 || !plateuCoordinates.Contains(" "))
                {
                    Console.WriteLine("Please enter coordinates in the right format!");
                    continue;
                }
                Plateu plateu = new Plateu(plateuCoordinates);

                bool ParsedX, ParsedY = false;
                //using the same int isParsed just being able to out the value. Not checking the value just controlling if it is parsed.
                //TO-DO: try to make cleaner validations next time. (Validations need to be tidier)
                int isParsed = 0;

                ParsedX = Int32.TryParse(plateuCoordinates.Split(" ")[0], out isParsed);
                ParsedY = Int32.TryParse(plateuCoordinates.Split(" ")[1], out isParsed);

                if (ParsedX && ParsedY)
                {
                    Console.WriteLine("Plateu Upper Right Coordinates is successfully parsed.");
                    return plateu;
                }
                else
                {
                    Console.WriteLine("Please enter coordinates in the right format!");
                    continue;
                }
            }

        }


        //Getting the rover count with validations and logic very close to GetPlateuCoordinates()
        private static int GetRoverCount()
        {
            while (true)
            {
                Console.WriteLine("How Many Rovers are You Planning to Command Today?");

                string roverNumber = Console.ReadLine();
                int roverCount = 0;

                bool isNumber = false;

                isNumber = Int32.TryParse(roverNumber, out roverCount);

                if (isNumber && roverCount > 0)
                {
                    Console.WriteLine("Number of Rovers is successfully parsed. Rover Count: " + roverCount);
                    return roverCount;
                }
                else
                {
                    Console.WriteLine("Please enter a positive number for the rover count!");
                    continue;

                }
            }

        }

        //Getting the rover initial position with validations and logic very close to GetPlateuCoordinates()
        private static Rover GetRoverInitialPosition(int i)
        {
            while (true)
            {
                string initialPosition = String.Empty;
           
                Console.WriteLine("Please enter the initial position of rover No:" + (i + 1)); //I started with 0 in the original program main, hence the i+1 addition...
                Console.WriteLine("Position example:1 2 N");

                initialPosition = Console.ReadLine();

                //TO-DO: Use regex next time!
                if (initialPosition.Length < 5 || !initialPosition.Contains(""))
                {
                    Console.WriteLine("Please enter coordinates in the right format!");
                    continue;
                }

                bool xParsed, yParsed, CardinalParsed = false;

                //again, not taking the actual values but just parsing
                int canParse = 0;
                string CardinalInitial = String.Empty;


                xParsed = Int32.TryParse(initialPosition.Split(" ")[0], out canParse);
                yParsed = Int32.TryParse(initialPosition.Split(" ")[1], out canParse);
                CardinalInitial = initialPosition.Split(" ")[2];
                string allowableLetters = "NWSE";
                //checking if Cardinal Compass Letter is indeed in NWSE
                CardinalParsed = FormatValid(CardinalInitial, allowableLetters);


                if (xParsed && yParsed && CardinalParsed)
                {
                    Console.WriteLine("Plateu Upper Right Coordinates is successfully parsed.");
                    return new Rover(initialPosition, "");
                }
                else
                {
                    Console.WriteLine("Please enter coordinates in the right format!");
                    continue;
                }
            }

        }

        //getting the nasa commands for robot to move around the plateu
        private static string GetRoverCommands(int i)
        {
            while(true)
            {
                string commandString = String.Empty;
                Console.WriteLine("Please enter the command chain of movement for rover No:" + (i + 1));
                Console.WriteLine("command chain must consist only of Characters L for Left, R for Right and M for Move");
                Console.WriteLine("Command chain example:LMLMLMLMM");
                string allowableLetters = "LRM";


                commandString = Console.ReadLine();


                if (FormatValid(commandString, allowableLetters))
                {
                    Console.WriteLine("Command is valid. Nasa Command for the rover No." + (i + 1) + " is:" + commandString);
                    return commandString;
                }
                else
                {
                    Console.WriteLine("Please enter the command in the right format!");
                    continue;
                }


            }
        }

        //Used both for validating format of nasa command and cardinal compass letter
        private static bool FormatValid(string str, string allowableLetters)
        {

            foreach (char c in str)
            {
                if (!allowableLetters.Contains(c.ToString()))
                    return false;
            }

            return true;
        }


    }
}
