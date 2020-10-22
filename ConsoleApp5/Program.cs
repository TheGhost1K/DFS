using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FirstTask
{
    class Program
    {
        public enum State
        {
            Empty,
            Wall,
            Visited
        };

        static Point startPoint = new Point();
        static Point endPoint = new Point();

        static void Main(string[] args)
        {
            string inPath = "./in.txt";
            string outPath = "./out.txt";

            // Заполнение входных данных            
            using (StreamReader sr = new StreamReader(inPath))
            {
                var n = int.Parse(sr.ReadLine());
                var m = int.Parse(sr.ReadLine());
                var array = new State[n, m];
                for (int i = 0; i < n; i++)
                {
                    var z = sr.ReadLine().Split(' ');
                    for (int j = 0; j < m; j++)
                    {
                        array[i, j] = int.Parse(z[j]) == 1 ? State.Wall : State.Empty;
                    }
                }
                var tempStart = sr.ReadLine().Split(' ');
                startPoint.Y = int.Parse(tempStart[0]) - 1;
                startPoint.X = int.Parse(tempStart[1]) - 1;

                var tempEnd = sr.ReadLine().Split(' ');
                endPoint.Y = int.Parse(tempEnd[0]) - 1;
                endPoint.X = int.Parse(tempEnd[1]) - 1;

                // начало поиска
                var queue = new Queue<Tuple<Point, Point>>();
                queue.Enqueue(new Tuple<Point, Point>(new Point(-1, -1), startPoint));
                List<Tuple<Point, Point>> visitedPoints = new List<Tuple<Point, Point>>();
                while (queue.Count != -1)
                {
                    var pointPair = queue.Dequeue();
                    if (array[pointPair.Item2.Y, pointPair.Item2.X] != State.Empty) continue;
                    array[pointPair.Item2.Y, pointPair.Item2.X] = State.Visited;
                    visitedPoints.Add(pointPair);
                    if (pointPair.Item2.X == endPoint.X & pointPair.Item2.Y == endPoint.Y)
                    {
                        SavePath(visitedPoints, pointPair.Item2);
                        return;
                    }
                    queue.Enqueue(new Tuple<Point, Point>(pointPair.Item2, new Point { X = pointPair.Item2.X-1, Y = pointPair.Item2.Y }));
                    queue.Enqueue(new Tuple<Point, Point>(pointPair.Item2, new Point { X = pointPair.Item2.X+1, Y = pointPair.Item2.Y }));
                    queue.Enqueue(new Tuple<Point, Point>(pointPair.Item2, new Point { X = pointPair.Item2.X, Y = pointPair.Item2.Y-1 }));
                    queue.Enqueue(new Tuple<Point, Point>(pointPair.Item2, new Point { X = pointPair.Item2.X, Y = pointPair.Item2.Y+1 }));
                }
            }
            // Закончили поиск пути в лабиринте
            using (StreamWriter wr = new StreamWriter(outPath))
            {
                wr.WriteLine('N');              
            }
            return;
            // Выводим результат
            void SavePath(List<Tuple<Point, Point>> tupleList, Point end)
            {
                using (StreamWriter wr = new StreamWriter(outPath))
                {
                    wr.WriteLine('Y');
                    List<string> list = new List<string>();
                    var tempPoint = end;                    
                    while (tupleList.Where(x => x.Item2 == tempPoint).First().Item1.X != -1)
                    {
                        list.Add(((tempPoint.Y + 1).ToString() + ' ' + (tempPoint.X + 1).ToString()));
                        tempPoint = tupleList.Where(x => x.Item2 == tempPoint).FirstOrDefault().Item1;
                    }
                    list.Reverse();
                    foreach (var e in list)
                    {
                        wr.WriteLine(e);
                    }
                }
            }
        }
    }
}
