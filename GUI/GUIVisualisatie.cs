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
        
        
        public static BitmapSource DrawTrack(Track track)
        {
            // Get Width and height of track
            (int width, int height) getWidthAndHeight = BuildTrack.GetWidthAndHeight(track);
            List<SectionInformation> sectionInformation = BuildTrack.buildSectionInformation(getWidthAndHeight.width, getWidthAndHeight.height, track);
            // Create canvas
            Canvas = Image.CreateEmptyBitmap(getWidthAndHeight.width * 256, getWidthAndHeight.height * 256);
            
            Graphics g = Graphics.FromImage(Canvas);

            foreach (var sectionDetails in sectionInformation)
            {
                g.DrawImage(sectionDetails.Bitmap, sectionDetails.col, sectionDetails.row, 256, 256);
                PlaceParticipants(g, sectionDetails.Section, sectionDetails.col, sectionDetails.row);
            }

            return Image.CreateBitmapSourceFromGdiBitmap(Canvas);
        }

        public static void PlaceParticipants(Graphics g, Section section, int col, int row)
        {
            var sectiondata = Data.CurrentRace.GetSectionData(section);

            if (sectiondata.Left != null)
            {
                var image = ParticipantsImage(sectiondata.Left);
                if (sectiondata.Left.Equipment.IsBroken)
                {
                    image = Image.GetImage(Fire);
                }

                g.DrawImage(image, (col + 128), row, 128, 128);
            }
            if (sectiondata.Right != null)
            {
                var image = ParticipantsImage(sectiondata.Right);
                if (sectiondata.Right.Equipment.IsBroken)
                {
                    image = Image.GetImage(Fire);
                }

                g.DrawImage(image, (col), (row + 128), 128, 128);
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
    }
}
