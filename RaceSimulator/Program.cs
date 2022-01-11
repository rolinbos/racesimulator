using System;
using System.Collections.Generic;
using System.Threading;
using Controller;
using Model;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] s = new string[5, 4]
            {
                { null, null, null, null },
                { null, "x", "x", "2" },
                { null, "1", null, "2" },
                { null, "1", "1", "2" },
                { null, null, null, null },
            };

            int row = s.GetLength(0);
            int col = s.GetLength(1);

            List<int> rowRemoving = new List<int>();
            List<int> colRemoving = new List<int>();

            bool removeColumn = false;
            bool removeRow = false;

            // Removing row
            for (int i = 0; i < row; i++)
            {
                removeRow = true;

                for (int j = 0; j < col; j++)
                {
                    if (s[i, j] != null)
                    {
                        removeRow = false;
                    }
                }

                if (removeRow)
                {
                    rowRemoving.Add(i);
                }
            }

            // Removing column
            for (int i = 0; i < col; i++)
            {
                removeColumn = true;

                for (int j = 0; j < row; j++)
                {
                    if (s[j, i] != null)
                    {
                        removeColumn = false;
                    }
                }

                if (removeColumn)
                {
                    colRemoving.Add(i);
                }
            }

            int newRow = 0;
            int newCol = 0;

            string[,] d = new string[(row - rowRemoving.Count),(col - colRemoving.Count)];
            for (int i = 0; i < row; i++)
            {
                newCol = 0;
                for (int j = 0; j < col; j++)
                {
                    if (!rowRemoving.Contains(i))
                    {
                        if (!colRemoving.Contains(j))
                        {
                            if (s[i, j] != null)
                            {
                                d[newRow, newCol] = s[i,j];
                               // Console.Write("x");
                            }
                            else
                            {
                                d[newRow, newCol] = s[i,j];
                               // Console.Write(" ");
                            }
                            newCol++;
                        }
                    }
                }

                if (!rowRemoving.Contains(i))
                {
                    newRow++;
                }
            }

            for (int i = 0; i < d.GetLength(0); i++)
            {
                for (int j = 0; j < d.GetLength(1); j++)
                {
                    if (d[i,j] != null)
                    {
                        Console.Write(d[i, j]);

                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }



            //Competition competition = new Competition();
            //Data.Initialize(competition);
            //Data.NextRace();
            //Visualisation.Initialize();
            //Visualisation.DrawTrack(Data.CurrentRace.Track);

            //for (; ; )
            //{
            //    Thread.Sleep(100);
            //}
        }
    }
}
