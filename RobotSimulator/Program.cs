using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            RobotBaseClass robot = new RobotBaseClass();
            bool isPlaceCommandExeuted = false;
            Console.WriteLine("Instructions Below.");
            Console.WriteLine("");
            Console.WriteLine("Type the following commands.");
            Console.WriteLine("1. Type 'Exit' to end the application");
            Console.WriteLine("2. Type 'PLACE X,Y,F'");
            Console.WriteLine("     - PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST");
            Console.WriteLine("3. Type 'Move'");
            Console.WriteLine("     - This will move the toy robot one unit forward in the direction it is currently facing.");
            Console.WriteLine("4. Type 'Left or 'Right'");
            Console.WriteLine("     - This will rotate the robot 90 degrees in the specified direction without changing the position of the robot");
            Console.WriteLine("5. Type 'Report'");
            Console.WriteLine("     - This will announce the X,Y and F of the robot.");
            Console.WriteLine("");
            Console.WriteLine("IMPORTANT!!!");
            Console.WriteLine("- A 'PLACE' command must be executed first!");
           
            do
            {
                string command = Console.ReadLine().ToUpper();
                if (command.ToUpper() == "EXIT") break;

                /*Valid command type*/
                if (command.Length >= 4)
                {
                    string commandValue = command.Substring(0, 4).ToUpper();
                    if (commandValue != "PLAC" && !isPlaceCommandExeuted)
                    {
                        ShowInvalidText();
                        Console.WriteLine("'PLACE' command must be executed first!");
                        robot.LogCommand("PLACE", "Not yet set first");
                    }

                    switch (commandValue)
                    {
                        case "PLAC":
                            isPlaceCommandExeuted = true;
                            string[] parameters = command.Split(',');
                            if (parameters.Count() == 3)
                            {
                                /*Get the x value*/
                                string[] xparam = parameters[0].Split(' ');
                                if (xparam.Count() == 0)
                                {
                                    ShowInvalidText();
                                    robot.LogCommand("PLACE", "Invalid command");
                                    break;
                                }

                                /*Check if the x and y are integers*/
                                if (!int.TryParse(xparam[1], out int xvalue))
                                {
                                    ShowInvalidText();
                                    robot.LogCommand("PLACE", "Invalid command");
                                    break;
                                }
                                if (!int.TryParse(parameters[1], out int yvalue))
                                {
                                    ShowInvalidText();
                                    robot.LogCommand("PLACE", "Invalid command");
                                    break;
                                }

                                string direc = CheckDirectionValue(parameters[2]);
                                if (!string.IsNullOrEmpty(direc))
                                {
                                    robot.Place(xvalue, yvalue, direc);
                                    robot.LogCommand("PLACE", "");
                                }
                                else
                                {
                                    ShowInvalidText();
                                    robot.LogCommand("PLACE", "Invalid command");
                                }
                            }
                            else
                            {
                                ShowInvalidText();
                            }
                                break;
                        case "MOVE":
                            robot.Move();
                            robot.LogCommand("MOVE", "");
                            break;
                        case "LEFT":
                            robot.MoveLeft();
                            robot.LogCommand("LEFT", "");
                            break;
                        case "RIGH":
                            robot.MoveRight();
                            robot.LogCommand("RIGHT", "");
                            break;
                        case "REPO":
                            robot.Report();
                            robot.LogCommand("REPORT", "");
                            break;
                        default:
                            ShowInvalidText();
                            break;
                    }
                }
                else
                {
                    ShowInvalidText();
                    robot.LogCommand("INVALID", "Invalid command");
                }
            } while (true);
        }

        public static void ShowInvalidText()
        {
            Console.WriteLine("Invalid command!!!");
        }

        public static string CheckDirectionValue(string direction)
        {
            string returnValue = "";
            switch (direction)
            {
                case "NORTH":
                    returnValue = direction;
                    break;
                case "WEST":
                    returnValue = direction;
                    break;
                case "SOUTH":
                    returnValue = direction;
                    break;
                case "EAST":
                    returnValue = direction;
                    break;
            }
            return returnValue;
        }
    }
}
