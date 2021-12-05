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
        public static int left = 20;
        public static int top = 20;
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
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.DarkRed;

            foreach (Section section in track.Sections)
            {
                DrawSectionType(section.SectionType);
            }
        }

        private static void DrawSectionType(SectionTypes sectionType)
        {
            // Check which direction the current is
            switch (CurrentDirection)
            {
                case Direction.West:
                    left -= 4;
                    break;
                case Direction.East:
                    left += 4;
                    break;
                case Direction.North:
                    top -= 4;
                    break;
                case Direction.South:
                    top += 4;
                    break;
            }


            if (CurrentDirection == Direction.North || CurrentDirection == Direction.South)
            {
                if (sectionType == SectionTypes.Finish)
                    draw(_finishVertical);

                if (sectionType == SectionTypes.LeftCorner)
                {
                    if (CurrentDirection == Direction.South)
                    {
                        draw(_cornerLefVertical);
                    }
                    else if (CurrentDirection == Direction.North)
                    {
                        draw(_cornerRightHorinzontal);
                    }
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    if (CurrentDirection == Direction.South)
                    {
                        draw(_cornerRightVertical);
                    }
                    else if (CurrentDirection == Direction.North)
                    {
                        draw(_cornerLeftHorizontal);
                    }
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
                if (sectionType == SectionTypes.Finish)
                {
                    draw(_finishHorizontal);
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    if (CurrentDirection == Direction.East)
                    {
                        draw(_cornerRightVertical);
                    }
                    else if (CurrentDirection == Direction.West)
                    {
                        draw(_cornerLeftHorizontal);
                    }
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    if (CurrentDirection == Direction.East)
                    {
                        draw(_cornerRightHorinzontal);
                    } else if (CurrentDirection == Direction.West)
                    {
                        draw(_cornerLefVertical);
                    }
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
                if (indexCurrentDirection == 3)
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
                    CurrentDirection = (Direction)3;
                }
                else
                {
                    CurrentDirection = (Direction)indexCurrentDirection - 1;
                }
            }
        }

        private static void draw(string[] graphics)
        {
            int tmpLeft = left;
            int tmpTop = top;
            Console.SetCursorPosition(left, top);

            foreach(string graphic in graphics)
            {
                Console.WriteLine(graphic);
                tmpTop += 1;


                Console.SetCursorPosition(tmpLeft, tmpTop);
            }
        }
    }
}
