using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    class Maze
    {
        public Point Dimensions { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public string[,] Map { get; set; }
    }

    class MazeSolver
    {

        public Maze ParseFile(string filename)
        {

            string[] input = File.ReadAllLines(filename);

            var dimensionsStr = input[0].Split(' ');
            Point dimensions = new Point(dimensionsStr[0], dimensionsStr[1]);

            var startPos = input[1].Split(' ');
            Point start = new Point(startPos[0], startPos[1]);

            var endPos = input[2].Split(' ');
            Point end = new Point(endPos[0], endPos[1]);

            string[,] map = new string[dimensions.X, dimensions.Y];

            for (int i = 3; i < input.Length; i++)
            {
                string[] line = input[i].Split(' ');
                for (int mapX = 0; mapX < line.Length; mapX++)
                {
                    string c = line[mapX];
                    int mapY = i - 3;

                    if (c == "1")
                        map[mapX, mapY] = "#";
                    else if (c == "0")
                        map[mapX, mapY] = " ";

                    if (mapX == start.X && mapY == start.Y)
                        map[mapX, mapY] = "S";
                    else if (mapX == end.X && mapY == end.Y)
                        map[mapX, mapY] = "E";
                }
            }

            return new Maze { Dimensions = dimensions, Start = start, End = end, Map = map };
        }

        public bool CanMove(Point curPoint, Maze maze, int xDir = 0, int yDir = 0)
        {
            string character = "";
        //    if (xDir > 0 || yDir > 0 && curPoint.X + xDir < maze.Dimensions.X && curPoint.Y + yDir < maze.Dimensions.Y)
         //   {
                character = maze.Map[curPoint.X + xDir, curPoint.Y + yDir];
           // }
          //  else if (xDir < 0 || yDir < 0 && curPoint.X + )

            if (character == " " || character == "E")
                return true;
            return false;
        }

        public void RenderMaze(Maze maze)
        {
            for (int y = 0; y < maze.Dimensions.Y; y++)
            {
                for (int x = 0; x < maze.Dimensions.X; x++)
                {
                    switch(maze.Map[x,y])
                    {
                        case "X":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case "#":
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                            

                    }
                    Console.Write(maze.Map[x, y]);
                }
                Console.WriteLine("");
            
            }
            Console.WriteLine("");

        }

        public void SolveMaze(string filename)
        {
            Maze maze = ParseFile(filename);

            Point curPoint = maze.Start;

            Stack<Point> decisionPoints = new Stack<Point>();
            Stack<Point> path = new Stack<Point>();
            while (true)
            {

                bool canMoveRight = CanMove(curPoint, maze, 1);
                bool canMoveDown = CanMove(curPoint, maze, 0, 1);
                bool canMoveLeft = CanMove(curPoint, maze, -1);
                bool canMoveUp = CanMove(curPoint, maze, 0, -1);

                if ((canMoveRight ? 1 : 0) + (canMoveDown ? 1 : 0) +
                   (canMoveLeft ? 1 : 0) + (canMoveUp ? 1 : 0) > 1)
                {
                    decisionPoints.Push(new Point() { X = curPoint.X, Y = curPoint.Y });
                }

                if (!canMoveRight && !canMoveDown && !canMoveLeft && !canMoveUp)
                {
                    Point lastDecisionPoint = decisionPoints.Pop();
                    
                    while (!(curPoint.X == lastDecisionPoint.X && curPoint.Y == lastDecisionPoint.Y))
                    {
                        curPoint = path.Pop();
                        maze.Map[curPoint.X, curPoint.Y] = ".";
                    }
                    continue;
                }

                path.Push(new Point() { X = curPoint.X, Y = curPoint.Y });

                if (canMoveRight)
                    curPoint.X++;
                else if (canMoveDown)
                    curPoint.Y++;
                else if (canMoveLeft)
                    curPoint.X--;
                else if (canMoveUp)
                    curPoint.Y--;
                RenderMaze(maze);
                if (maze.Map[curPoint.X, curPoint.Y] == "E")
                    break;
                maze.Map[curPoint.X, curPoint.Y] = "X";

          

            }

        }
    }


    class Program
    {



        static void Main(string[] args)
        {
            MazeSolver mazeSolver = new MazeSolver();
            mazeSolver.SolveMaze("Mazes/medium_input.txt");

            while (true) { }


        }


    }

    internal class Point
    {
        private int v1;
        private int v2;

        public int X { get { return v1; } set { v1 = value; } }
        public int Y { get { return v2; } set { v2 = value; } }


        public Point(string v1, string v2)
        {
            this.v1 = Int32.Parse(v1);
            this.v2 = Int32.Parse(v2);
        }
        public Point() { }


    }
}
