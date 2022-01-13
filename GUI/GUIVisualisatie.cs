using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Model;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Controller;
using RaceSimulator;

namespace GUI
{
    public class GUIVisualisatie
    {
        //Keep track of all the image variables
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

        public static Bitmap Canvas;
        private static Dictionary<int, int> CoordinationX = new Dictionary<int, int>();
        private static Dictionary<int, int> CoordinationY = new Dictionary<int, int>();

        public static BitmapSource DrawTrack(Track track)
        {
            CoordinationX = new Dictionary<int, int>();
            CoordinationY = new Dictionary<int, int>();

            var h = BuildTrack.Map;


            //if (BuildTrack.Map == null)
            //{
                (int width, int height) getWidthAndHeight = BuildTrack.GetWidthAndHeight(track);

                Canvas = Image.CreateEmptyBitmap(getWidthAndHeight.width * 256, getWidthAndHeight.height * 256);
                Graphics g = Graphics.FromImage(Canvas);
                Bitmap[,] map = CreateMap(track, getWidthAndHeight.width, getWidthAndHeight.height);

                int x = 0;
                int y = 0;
                int index = 0;

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] == null)
                        {
                            g.DrawImage(Image.GetImage(GrassTile), x, y, 256, 256);
                        }
                        else
                        {
                            g.DrawImage(map[i, j], x, y, 256, 256);
                        }

                        x += 256;
                    }
                    y += 256;
                    x = 0;
                }

                PlaceParticipants(g, track);
            //}
            //else
            //{
            //    Graphics g = Graphics.FromImage(Canvas);
            //    PlaceParticipants(g, track);
            //    Debug.WriteLine("Hij komt hier");
            //}

            return Image.CreateBitmapSourceFromGdiBitmap(Canvas);
        }

        public static Bitmap[,] CreateMap(Track track, int horizontal, int vertical)
        {
            int hor = horizontal * 3;
            int vert = vertical * 3;
            Bitmap[,] map = new Bitmap[hor, vert];
            string[,] stringMap = new string[hor, vert];

            int row = 0;
            int col = hor / 2;
            int index = 0;
            RaceSimulator.Direction direction = RaceSimulator.Direction.East;

            foreach (var section in track.Sections)
            {
                stringMap[row, col] = section.SectionType.ToString();
                map[row, col] = SetBitmap(section, direction);
                CoordinationX.Add(index, row);
                CoordinationY.Add(index, Rounding(col, 3));

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

                index += 1;
            }
            

            return BuildTrack.RemovingUnusedColOrRow(map);
        }

        public static Bitmap SetBitmap(Section section, Direction direction)
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);

            if (section.SectionType == SectionTypes.StartGrid || section.SectionType == SectionTypes.Straight)
            {
                if ((direction == Direction.East || direction == Direction.West))
                {
                    return Image.GetImage(TrackHorizontal);
                }

                return Image.GetImage(TrackVertical);
            } else if (section.SectionType == SectionTypes.RightCorner)
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
            } else if (section.SectionType == SectionTypes.LeftCorner)
            {
                if (direction == Direction.East || direction == Direction.West)
                {
                    return Image.GetImage(CornerRightVertical);
                }

                return Image.GetImage(CornerRightHorizontal); 
            } else if (section.SectionType == SectionTypes.Finish)
            {
                return Image.GetImage(Finish);
            }

            return Image.GetImage(Fire);
        }

        public static void PlaceParticipants(Graphics g, Track track)
        {
            Debug.WriteLine("Pas driver aan");
            int index = 0;
            foreach (var section in track.Sections)
            {
                int x = CoordinationX[index];
                int y = CoordinationY[index] - 1;

                var sectiondata = Data.CurrentRace.GetSectionData(section);

                if (sectiondata.Left != null)
                {
                    if (sectiondata.Left.Equipment.IsBroken)
                    {
                        g.DrawImage(Image.GetImage(Fire), x * 256 + 128, y * 256, 128, 128);
                    }
                    Bitmap carleft = ParticipantsImage(sectiondata.Left);
                    g.DrawImage(carleft, ((y * 256 + 128)), (x * 256), 128, 128);
                }
                if (sectiondata.Right != null)
                {
                    if (sectiondata.Right.Equipment.IsBroken)
                    {
                        g.DrawImage(Image.GetImage(Fire), x * 256, y * 256 + 128, 128, 128);
                    }
                    Bitmap carRight = ParticipantsImage(sectiondata.Right);
                    g.DrawImage(carRight, ((y * 256)), ((x * 256 + 128)), 128, 128);

                }
            }
        }

        public static Bitmap ParticipantsImage(IParticipant participant)
        {
            switch (participant.TeamColor)
            {
                case TeamColors.Blue:
                    return Image.GetImage(Blue);
                case TeamColors.Grey:
                    return Image.GetImage(Grey);
                case TeamColors.Green:
                    return Image.GetImage(Green);
                case TeamColors.Red:
                    return Image.GetImage(Red);
                case TeamColors.Yellow:
                    return Image.GetImage(Yellow);
                default:
                    throw new ArgumentOutOfRangeException("Color null");
            }
        }

        public static int Rounding(int number, int splitter)
        {
            double output = (double)number / splitter;

            return Convert.ToInt32(Math.Round(output));
        }
    }
}
