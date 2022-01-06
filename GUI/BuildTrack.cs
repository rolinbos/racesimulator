using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Model;
using RaceSimulator;

namespace GUI
{
    public class BuildTrack
    {
        public static Bitmap[,] Map;

        // Krijg de breedte en hoogte van de track
        public static (int width, int height) GetWidthAndHeight(Track track)
        {
            int numberOfSections = track.Sections.Count();

            var direction = RaceSimulator.Direction.East;
            int width = 0;

            // Altijd vertical + 1 omdat de eerste er nooit bij op wordt geteld.
            int height = 1;
            for (int i = 0; i < (numberOfSections + 1); i++)
            {
                if (direction == RaceSimulator.Direction.East)
                {
                    width++;
                }

                if (direction == RaceSimulator.Direction.South)
                {
                    height++;
                }

                if (i < numberOfSections)
                    direction = SetDirection(track.Sections.ElementAt(i).SectionType, (int)direction);
            }

            return (width, height);
        }

        // Set the new direction. 
        public static RaceSimulator.Direction SetDirection(SectionTypes sectionCorner, int direction)
        {
            int indexCurrentDirection = direction;
            RaceSimulator.Direction newDirection = (RaceSimulator.Direction)direction;
            if (sectionCorner == SectionTypes.RightCorner)
            {
                if (indexCurrentDirection == 3)
                {
                    newDirection = (RaceSimulator.Direction)0;
                }
                else
                {
                    newDirection = (RaceSimulator.Direction)indexCurrentDirection + 1;
                }
            }

            if (sectionCorner == SectionTypes.LeftCorner)
            {
                if (indexCurrentDirection == 0)
                {
                    newDirection = (RaceSimulator.Direction)3;
                }
                else
                {
                    newDirection = (RaceSimulator.Direction)indexCurrentDirection - 1;
                }
            }

            return newDirection;
        }


        public static Bitmap[,] RemovingUnusedColOrRow(Bitmap[,] bigMap)
        {
            int row = bigMap.GetLength(0);
            int col = bigMap.GetLength(1);

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
                    if (bigMap[i, j] != null)
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
                    if (bigMap[j, i] != null)
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

            Bitmap[,] map = new Bitmap[(row - rowRemoving.Count), (col - colRemoving.Count)];
            for (int i = 0; i < row; i++)
            {
                newCol = 0;
                for (int j = 0; j < col; j++)
                {
                    if (!rowRemoving.Contains(i))
                    {
                        if (!colRemoving.Contains(j))
                        {
                            if (bigMap[i, j] != null)
                            {
                                map[newRow, newCol] = bigMap[i, j];
                            }
                            else
                            {
                                map[newRow, newCol] = bigMap[i, j];
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

            Map = map;
            return map;
        }

        public static string[,] RemovingUnusedColOrRowString(string[,] bigMap)
        {
            int row = bigMap.GetLength(0);
            int col = bigMap.GetLength(1);

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
                    if (bigMap[i, j] != null)
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
                    if (bigMap[j, i] != null)
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

            string[,] map = new string[(row - rowRemoving.Count), (col - colRemoving.Count)];
            for (int i = 0; i < row; i++)
            {
                newCol = 0;
                for (int j = 0; j < col; j++)
                {
                    if (!rowRemoving.Contains(i))
                    {
                        if (!colRemoving.Contains(j))
                        {
                            if (bigMap[i, j] != null)
                            {
                                map[newRow, newCol] = bigMap[i, j];
                            }
                            else
                            {
                                map[newRow, newCol] = bigMap[i, j];
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

            return map;
        }

        public static void ClearMap()
        {
            Map = null;
        }
    }
}
