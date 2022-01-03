using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using RaceSimulator;

namespace Virtualisatie
{
    public static class Visualisation
    {
        #region graphics
        const string CornerLeftHorizontal = ".\\Assets\\CornerLeftHorizontal.png";
        const string CornerLeftVertical = ".\\Assets\\CornerLeftVertical.png";
        const string CornerRightHorizontal = ".\\Assets\\CornerRightHorizontal.png";
        const string CornerRightVertical = ".\\Assets\\CornerRightVertical.png";
        const string Finish = ".\\Assets\\Finish.png";
        const string TrackHorizontal = ".\\Assets\\TrackHorizontal.png";
        const string TrackVertical = ".\\Assets\\TrackVertical.png";
        const string GrassTile = ".\\Assets\\Grass_Tile.png";
        const string WaterTile = ".\\Assets\\Water.png";

        const string Blue = ".\\Assets\\Blue.png";
        const string Grey = ".\\Assets\\Grey.png";
        const string Red = ".\\Assets\\Red.png";
        const string Yellow = ".\\Assets\\Yellow.png";
        const string Green = ".\\Assets\\Green.png";

        const string Fire = ".\\Assets\\Fire.png";
        #endregion

        public static int MaxHorizontal = 0;
        public static int MaxVertical = 0;
        public static Bitmap[,] CompleteTrack;
        public static Bitmap canvas;

        public static BitmapSource DrawTrack(Track track)
        {
            GetHorAndVertValues(track);

            canvas = CacheImages.CreateBitmap(MaxHorizontal * 256, MaxVertical * 256);

            BuildTrack(track);
            var h = CompleteTrack;
            
            PlaceTrack(canvas);          

            return CacheImages.CreateBitmapSourceFromGdiBitmap(canvas);
        }

        //Place the track on the canvas
        public static void PlaceTrack(Bitmap canvas)
        {
            int x = 0;
            int y = 0;

            Graphics g = Graphics.FromImage(canvas);

            for (int i = 0; i < CompleteTrack.GetLength(0); i++)
            {
                for (int j = 0; j < CompleteTrack.GetLength(1); j++)
                {
                    if (CompleteTrack[i, j] != null)
                    {
                        g.DrawImage(CompleteTrack[i, j], x, y, 256, 256);
                    }

                    x += 256;
                }
                x = 0;
                y += 256;
            }
        }

        public static void BuildTrack(Track track)
        {
            int hor = MaxHorizontal * 3;
            int vert = MaxVertical * 3;
         
            Bitmap[,] map = new Bitmap[hor, vert];

            int row = 0;
            int col = hor / 2;
            RaceSimulator.Direction direction = RaceSimulator.Direction.East;

            foreach (var section in track.Sections)
            {
                map[row, col] = CacheImages.GetUrl(TrackHorizontal);

                if (direction == RaceSimulator.Direction.North)
                {
                    row--;
                }

                if (direction == RaceSimulator.Direction.East)
                {
                    col++;
                }

                if (direction == RaceSimulator.Direction.South)
                {
                    row++;
                }

                if (direction == RaceSimulator.Direction.West)
                {
                    col--;
                }

                direction = setDirection(section.SectionType, (int)direction);
            }

            CompleteTrack = map;

            //CleanUpMap(map, hor, vert);
        }

        public static void CleanUpMap(string[,] map, int hor, int vert)
        {
            List<int> removeColumn = new List<int>();

            string[,] test = new string[3, 4]
            {
                { null, "1", "2", null },
                { null, "1", "2", null },
                { null, null, null, null },
            };

            int columnLength = test.GetLength(1);
            int rowLength = test.GetLength(0);
            bool rowIsEmpty = false;

            for (int i = 0; i < columnLength; i++)
            {
                rowIsEmpty = false;

                for (int j = 0; j < rowLength; j++)
                {
                    if (test[j, i] == null)
                    {
                        rowIsEmpty = true;
                    }
                    else
                    {
                        rowIsEmpty = false;
                    }
                }

                if (rowIsEmpty)
                {
                    removeColumn.Add(i);
                }
            }

            Console.WriteLine("h");

            //string[,] tmpMap = new string[hor, vert];
            //int startHor = 0;
            //// Column
            //for (int vertI = 0; vertI < vert; vertI++)
            //{
            //    bool isEmptyColumn = true;

            //    // Row
            //    for (int horI = 0; horI < hor; horI++)
            //    {
            //        if (map[horI, vertI] != null)
            //        {
            //            isEmptyColumn = false;
            //        }
            //    }

            //    if (isEmptyColumn)
            //    {
            //        //for (int i = 0; i < map.get; i++)
            //        //{

            //        //}
            //        // Column is empty remove column
            //    }
            //}
        }

        public static void GetHorAndVertValues(Track track)
        {
            int numberOfSections = track.Sections.Count();

            var direction = RaceSimulator.Direction.East;
            int hor = 0;

            // Altijd vertical + 1 omdat de eerste er nooit bij op wordt geteld.
            int ver = 1;
            for (int i = 0; i < (numberOfSections + 1); i++)
            {
                if (direction == RaceSimulator.Direction.East)
                {
                    hor++;
                }

                if (direction == RaceSimulator.Direction.South)
                {
                    ver++;
                }

                if (i < numberOfSections)
                    direction = setDirection(track.Sections.ElementAt(i).SectionType, (int)direction);
            }

            MaxHorizontal = hor;
            MaxVertical = ver;
        }

        // Set the new direction. 
        private static RaceSimulator.Direction setDirection(SectionTypes sectionCorner, int direction)
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

        //Method to rotate a bitmap
        public static Bitmap RotateImage(Bitmap b, Direction direction)
        {
            int maxside = (int)(Math.Sqrt(b.Width * b.Width + b.Height * b.Height));

            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(maxside, maxside);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);

            //move rotation point to center of image

            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);

            switch (direction)
            {
                case Direction.South:
                    g.RotateTransform(90);
                    break;
                case Direction.West:
                    g.RotateTransform(180);
                    break;
                case Direction.North:
                    g.RotateTransform(270);
                    break;
            }

            //move image back
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);

            g.DrawImage(b, 0, 0, 128, 128);
            return returnBitmap;
        }
    }
}
