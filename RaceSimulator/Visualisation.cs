using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace RaceSimulator
{
    public enum Direction
    {
        North,
        East,
        South,
        West,
    }
    public static class Visualisation
    {
        public static int x = 20;
        public static int y = 20;
        public static Direction CurrentDirection = Direction.East;

        #region graphics
        private static string[] _startHorizontal = { "----", " 1> ", "2>  ", "----" };
        private static string[] _startVertical = { "|  |", "|^ |", "| ^|", "|  |" };

        private static string[] _finishHorizontal = { "----", " 1# ", "2 # ", "----" };
        private static string[] _finishVertical = { "|  |", "| ## |", "|  |", "|  |" };

        private static string[] _trackHorizontal = { "----", "  1 ", " 2  ", "----" };
        private static string[] _trackVertical = { "|  |", "|1 |", "| 2|", "|  |" };

        private static string[] _cornerRightHorinzontal = { "--\\ ", " 1 \\", "  2|", "\\  |" };
        private static string[] _cornerRightVertical = { "/  |", " 1 |", "  2/", "--/ " };


        private static string[] _cornerLeftHorizontal = { " /--", "/1  ", "|  2", "|  /" };
        private static string[] _cornerLefVertical = { "|  \\", "| 1 ", "\\  2", " \\--" };
        #endregion

        public static void Initialize()
        {

        }

        public static void DrawTrack(Track track)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ConsoleColor.DarkRed;

            foreach (Section section in track.Sections)
            {
                DrawSectionType(section.SectionType);
            }
        }

        private static void DrawSectionType(SectionTypes sectionType)
        {
            // Get vertical
            if (CurrentDirection == Direction.North || CurrentDirection == Direction.South)
            {
                if (CurrentDirection == Direction.North)
                {
                    y -= 4;
                }

                if (CurrentDirection == Direction.South)
                {
                    y += 4;
                }

                if (sectionType == SectionTypes.Finish)
                {
                    draw(_finishVertical);
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    draw(_cornerLefVertical);
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    draw(_cornerRightVertical);
                }

                if (sectionType == SectionTypes.StartGrid)
                {
                    draw(_startVertical);
                }

                if (sectionType == SectionTypes.Straight)
                {
                    draw(_trackVertical);
                }
            }

            // Get horizontal
            if (CurrentDirection == Direction.East || CurrentDirection == Direction.West)
            {
                if (CurrentDirection == Direction.West)
                {
                    x -= 4;
                }

                if (CurrentDirection == Direction.East)
                {
                    x += 4;
                }


                if (sectionType == SectionTypes.Finish)
                {
                    draw(_finishHorizontal);
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    draw(_cornerLefVertical);
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    draw(_cornerRightHorinzontal);
                }

                if (sectionType == SectionTypes.StartGrid)
                {
                    draw(_startHorizontal);
                }

                if (sectionType == SectionTypes.Straight)
                {
                    draw(_trackHorizontal);
                }
            }
            // Set the new direction
            setDirection(sectionType);
        }

        // Set the new direction. 
        private static void setDirection(SectionTypes sectionCorner)
        {
            int indexCurrentDirection = (int)CurrentDirection;

            if (sectionCorner == SectionTypes.RightCorner)
            {
                if (indexCurrentDirection == 4)
                {
                    CurrentDirection = (Direction)0;
                } else
                {
                    CurrentDirection = (Direction)indexCurrentDirection + 1;
                }
            }

            if (sectionCorner == SectionTypes.LeftCorner)
            {
                if (indexCurrentDirection == 0)
                {
                    CurrentDirection = (Direction)4;
                }
                else
                {
                    CurrentDirection = (Direction)indexCurrentDirection - 1;
                }
            }
        }

        private static void draw(string[] graphics)
        {
            int tmpX = x;
            int tmpY = y;
            Console.SetCursorPosition(x, y);

            foreach(string graphic in graphics)
            {
                Console.WriteLine(graphic);
                tmpY += 1;

                if (CurrentDirection == Direction.South || CurrentDirection == Direction.East)
                {
                      
                }

                if (CurrentDirection == Direction.South || CurrentDirection == Direction.East)
                {
                    //tmpX += 1;
                }

                //if (CurrentDirection == Direction.North || CurrentDirection == Direction.West)
                //{
                //    tmpY -= 1;
                //}

                Console.SetCursorPosition(tmpX, tmpY);
            }
        }
    }
}
