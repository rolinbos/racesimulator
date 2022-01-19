using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using Controller;
using Model;
using RaceSimulator;

namespace GUI
{
    public class BuildTrack
    {
        #region graphics
        const string CornerLeftHorizontal = @".\\..\\..\\..\\Images\\CornerLeftHorizontal.png";
        const string CornerLeftVertical = @".\\..\\..\\..\\Images\\CornerLeftVertical.png";
        const string CornerRightHorizontal = @".\\..\\..\\..\\Images\\CornerRightHorizontal.png";
        const string CornerRightVertical = @".\\..\\..\\..\\Images\\CornerRightVertical.png";
        const string Finish = @".\\..\\..\\..\\Images\\Finish.png";
        const string TrackHorizontal = @".\\..\\..\\..\\Images\\TrackHorizontal.png";
        const string TrackVertical = @".\\..\\..\\..\\Images\\TrackVertical.png";
        const string GrassTile = @".\\..\\..\\..\\Images\\Grass_Tile.png";
        const string WaterTile = @".\\..\\..\\..\\Images\\Water.png";

        const string Blue = @".\\..\\..\\..\\Images\\Blue.png";
        const string Grey = @".\\..\\..\\..\\Images\\Grey.png";
        const string Red = @".\\..\\..\\..\\Images\\Red.png";
        const string Yellow = @".\\..\\..\\..\\Images\\Yellow.png";
        const string Green = @".\\..\\..\\..\\Images\\Green.png";

        const string Fire = @".\\..\\..\\..\\Images\\Fire.png";
        #endregion
        public static List<SectionInformation> SectionInformations = new List<SectionInformation>();
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
        public static RaceSimulator.Direction SetDirection(SectionTypes sectionType, int direction)
        {
            int indexCurrentDirection = direction;
            RaceSimulator.Direction newDirection = (RaceSimulator.Direction)direction;
            if (sectionType == SectionTypes.RightCorner)
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

            if (sectionType == SectionTypes.LeftCorner)
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

        public static void ClearMap()
        {
            Map = null;
        }

        public static List<SectionInformation> buildSectionInformation(int width, int height, Track track)
        {
            SectionInformation[,] map = new SectionInformation[(width * 2), (height * 2)];
            RaceSimulator.Direction direction = RaceSimulator.Direction.East;

            int row = 0;
            // Zet the beginning of the column in the middle
            int col = width;
            foreach (var section in track.Sections)
            {
                map[row, col] = new SectionInformation(row, col, section, direction, SetBitmap(section, direction));

                direction = BuildTrack.SetDirection(section.SectionType, (int)direction);

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
            }

            int lowestCol = int.MaxValue;

            for (int rowIndex = 0; rowIndex < map.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < map.GetLength(1); colIndex++)
                {
                    if (map[rowIndex, colIndex] != null)
                    {
                        if (lowestCol > map[rowIndex, colIndex].col || lowestCol == int.MaxValue)
                        {
                            lowestCol = map[rowIndex, colIndex].col;
                        }

                        map[rowIndex, colIndex].row = map[rowIndex, colIndex].row * 256;
                        SectionInformations.Add(map[rowIndex, colIndex]);
                    }
                }
            }

            for (int i = 0; i < SectionInformations.Count; i++)
            {
                SectionInformations[i].col = (SectionInformations[i].col - lowestCol) * 256;
            }

            return SectionInformations;
        }

        private static Bitmap SetBitmap(Section section, Direction direction)
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);

            if (section.SectionType == SectionTypes.StartGrid || section.SectionType == SectionTypes.Straight)
            {
                if ((direction == Direction.East || direction == Direction.West))
                {
                    return Image.GetImage(TrackHorizontal);
                }

                return Image.GetImage(TrackVertical);
            }
            else if (section.SectionType == SectionTypes.RightCorner)
            {
                if (direction == Direction.South)
                {
                    return Image.GetImage(CornerLeftHorizontal);
                }

                if (direction == Direction.West)
                {
                    return Image.GetImage(CornerRightHorizontal);
                }

                if (direction == Direction.North)
                {
                    return Image.GetImage(CornerRightVertical);
                }

                return Image.GetImage(CornerLeftVertical);
            }
            else if (section.SectionType == SectionTypes.LeftCorner)
            {
                if (direction == Direction.East || direction == Direction.West)
                {
                    return Image.GetImage(CornerRightVertical);
                }

                return Image.GetImage(CornerRightHorizontal);
            }
            else if (section.SectionType == SectionTypes.Finish)
            {
                return Image.GetImage(Finish);
            }

            return Image.GetImage(GrassTile);
        }
    }
}
