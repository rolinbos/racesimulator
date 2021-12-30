using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Virtualisatie
{
    public static class Visualisation
    {
        public static int MaxHorizontal = 0;
        public static int MaxVertical = 0;
        public static Bitmap canvas;
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

        public static BitmapSource DrawTrack(Track track)
        {
            GetHorAndVertValues(track);
            
            CreateMap(track);

            canvas = CacheImages.CreateBitmap(MaxHorizontal * 256, MaxVertical * 256);

            return CacheImages.CreateBitmapSourceFromGdiBitmap(canvas);
        }

        public static void CreateMap(Track track)
        {
            int hor = MaxHorizontal * 3;
            int vert = MaxVertical * 3;
            string[,] map = new string[hor, vert];

            int row = 0;
            int col = hor / 2;
            RaceSimulator.Direction direction = RaceSimulator.Direction.East;

            foreach (var section in track.Sections)
            {
                map[row, col] = section.SectionType.ToString();

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
    }
}
