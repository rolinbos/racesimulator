using System;
using System.Collections.Generic;
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

        public static BitmapSource DrawTrack(Track track)
        {
            var h = BuildTrack.Map;
            if (BuildTrack.Map != null)
            {
                // Build drivers
            }
            else
            {
                (int width, int height) getWidthAndHeight = BuildTrack.GetWidthAndHeight(track);

                Canvas = Image.CreateEmptyBitmap(getWidthAndHeight.width * 256, getWidthAndHeight.height * 256);

                Bitmap[,] map = CreateMap(track, getWidthAndHeight.width, getWidthAndHeight.height);
                //Bitmap[,] map = BuildTrack.RemovingUnusedColOrRow(bigMap);
                Graphics g = Graphics.FromImage(Canvas);

                int x = 0;
                int y = 0;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] == null)
                        {
                            g.DrawImage(Image.GetImage(WaterTile), x, y, 256, 256);
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
            }

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

            RaceSimulator.Direction direction = RaceSimulator.Direction.East;

            foreach (var section in track.Sections)
            {
                stringMap[row, col] = section.SectionType.ToString();
                map[row, col] = SetBitmap(section, direction);

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

            return BuildTrack.RemovingUnusedColOrRow(map);
        }

        public static Bitmap SetBitmap(Section section, Direction direction)
        {
            if ((section.SectionType == SectionTypes.StartGrid || section.SectionType == SectionTypes.Straight) && (direction == Direction.East || direction == Direction.West))
            {
                return Image.GetImage(TrackHorizontal);
            } else if (section.SectionType == SectionTypes.RightCorner)
            {
                return Image.GetImage(CornerRightHorizontal);
            } else if (section.SectionType == SectionTypes.LeftCorner)
            {
                return Image.GetImage(CornerLeftHorizontal);
            } else if (section.SectionType == SectionTypes.Finish)
            {
                return Image.GetImage(Finish);
            }

            return Image.GetImage(Fire);
        }

        //public static void Build(Track track, int width, int height)
        //{
        //    // hor
        //    int x = 0;
        //    // ver
        //    int y = 0;

        //    Graphics g = Graphics.FromImage(Canvas);
        //    var direction = RaceSimulator.Direction.East;

        //    foreach (var section in track.Sections)
        //    {
        //       // g.DrawImage(Image.GetImage(CornerRightVertical), x, y, 256, 256);

        //        if (direction == RaceSimulator.Direction.East)
        //        {
        //            if (section.SectionType == SectionTypes.LeftCorner)
        //            {
        //                g.DrawImage(Image.GetImage(Finish), x, y, 256, 256);
        //                //g.DrawImage(Image.RotateImage(Image.GetImage(CornerRightVertical), direction), x, y, 256, 256);
        //                //draw(_cornerRightVertical, section);
        //            }

        //            if (section.SectionType == SectionTypes.RightCorner)
        //            {
        //                g.DrawImage(Image.GetImage(Finish), x, y, 256, 256);
        //                //g.DrawImage(Image.RotateImage(Image.GetImage(CornerRightVertical), direction), x, y, 256, 256);

        //                //g.DrawImage(Image.GetImage(CornerRightHorizontal), x, y, 256, 256);
        //                // draw(_cornerRightHorinzontal, section);
        //            }

        //            if (section.SectionType == SectionTypes.Finish)
        //            {
        //                g.DrawImage(Image.GetImage(Finish), x, y, 256, 256);
        //                // g.DrawImage(Image.GetImage(Finish), x, y, 256, 256);
        //                // draw(_finishhorizontal, section);
        //            }

        //            if (section.SectionType == SectionTypes.Straight || section.SectionType == SectionTypes.StartGrid)
        //            {
        //              g.DrawImage(Image.GetImage(TrackHorizontal), x, y, 256, 256);
        //                // draw(_trackHorizontal, section);
        //            }

        //            x += 256;
        //        }

        //        if (direction == RaceSimulator.Direction.South)
        //        {
        //            g.DrawImage(Image.GetImage(Finish), x, y, 256, 256);
        //            y += 256;
        //        }

        //        if (direction == RaceSimulator.Direction.West)
        //        {
        //            g.DrawImage(Image.GetImage(Finish), x, y, 256, 256);
        //            if (section.SectionType == SectionTypes.Finish)
        //            {

        //                // draw(_finishhorizontal, section);
        //            }

        //            if (section.SectionType == SectionTypes.Straight || section.SectionType == SectionTypes.StartGrid)
        //            {
        //                // draw(_trackHorizontal, section);
        //            }

        //            x -= 256;
        //        }

        //        if (direction == RaceSimulator.Direction.North)
        //        {
        //            g.DrawImage(Image.GetImage(Finish), x, y, 256, 256);
        //            y -= 256;
        //        }

        //        // Zet nieuwe directie
        //        direction = BuildTrack.SetDirection(section.SectionType, (int)direction);
        //    }
        //}
    }
}
