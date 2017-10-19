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
                for (int j = 0; j < line.Length; j++)
                {
                    string c = line[j];
                    int mapX = i - 3;

                    if (c == "1")
                        map[mapX, j] = "#";
                    else if (c == "0")
                        map[mapX, j] = " ";

                    if (mapX == start.X && j == start.Y)
                        map[mapX, j] = "S";
                    else if (mapX == end.X && j == end.Y)
                        map[mapX, j] = "E";
                }
            }

            return new Maze { Dimensions = dimensions, Start = start, End = end, Map = map };
        }

        public bool CanMove(Point curPoint, Maze maze, int xDir = 0, int yDir = 0)
        {
            string character = "";
            if (xDir > 0 || yDir > 0 && curPoint.X + xDir < maze.Dimensions.X && curPoint.Y + yDir < maze.Dimensions.Y)
            {
                character = maze.Map[curPoint.X + xDir, curPoint.Y + yDir];
            }

            if (character == " ")
                return true;
            return false;
        }

        public void SolveMaze(string filename)
        {
            Maze maze = ParseFile(filename);

            Point curPoint = maze.Start;

            List<Point> decisionPoints = new List<Point>();
            while (true)
            {
                bool canMoveRight = CanMove(curPoint, maze, 1);


            }

        }
    }


    class Program
    {



        static void Main(string[] args)
        {
            MazeSolver mazeSolver = new MazeSolver();
            mazeSolver.SolveMaze("Mazes/input.txt");




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
    }
}
