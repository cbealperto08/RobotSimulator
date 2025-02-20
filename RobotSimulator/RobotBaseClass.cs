using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RobotSimulator
{
    public class RobotBaseClass
    {
        int facingValue = 0;
        int yMaxValue = 5;
        int yMinValue = 0;
        int xMaxValue = 5;
        int xMinValue = 0;
        public RobotBaseClass()
        {
            faceDirection = "NORTH";
            facingValue = 0;
        }

        public void MoveLeft()
        {
            if (facingValue == 0) facingValue = 360;
            facingValue -= 90;
            SetfaceCardinalDirection(facingValue);
        }
        public void MoveRight()
        {
            if (facingValue == 360) facingValue = 0;
            facingValue += 90;
            SetfaceCardinalDirection(facingValue);
        }  

        public void Move()
        {
            switch (faceDirection)
            {
                case "NORTH":
                    if (Y + 1 <= yMaxValue)
                    {
                        Y++;
                    }
                    break;
                case "WEST":
                    if (x -1  >= xMinValue)
                    {
                        x--;
                    }
                    break;
                case "SOUTH":
                    if (y - 1 >= yMinValue)
                    {
                        y--;
                    }
                    break;
                case "EAST":
                    if (x + 1 >= xMaxValue)
                    {
                        x++;
                    }
                    break;
            }
        }

        public void Place(int xpos,int ypos,string direction)
        {
            try
            {
                /*Do not allow negative value*/
                /*Do not allow the robot to fall out of the table*/
                if (xpos < xMinValue || ypos < yMinValue) return;
                if (xpos > xMaxValue || ypos > yMaxValue) return;

                
                x = xpos;
                y = ypos;
                faceDirection = direction;

                switch (faceDirection)
                {
                    case "NORTH":
                        facingValue = 0;
                        break;
                    case "WEST":
                        facingValue = 270;
                        break;
                    case "SOUTH":
                        facingValue = 180;
                        break;
                    case "EAST":
                        facingValue = 90;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void Report()
        {
            Console.WriteLine(string.Format("{0},{1},{2}", x, y, faceDirection));
        }
        
        private void SetfaceCardinalDirection(int degree)
        {
            string[] caridnals = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };
            var facing = caridnals[(int)Math.Round(((double)degree * 10 % 3600) / 225)];
            switch (facing)
            {
                case "N":
                    faceDirection = "NORTH";
                    break;
                case "W":
                    faceDirection = "WEST";
                    break;
                case "S":
                    faceDirection = "SOUTH";
                    break;
                case "E":
                    faceDirection = "EAST";
                    break;
            }
        }

      
        private string faceDirection;

        public string FaceDirection 
        {
            get { return faceDirection; }
            set { faceDirection = value; }
        }

        private int x;

        public int  X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public  void LogCommand(string command,string message)
        {
            string position = string.Format("{0},{1},{2}", x, y, faceDirection);
            using (var fileStream = new FileStream("commands.txt", FileMode.Append))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(DateTime.Now + "," + command +"," + position +"," + message);
            }
        }
    }
}
