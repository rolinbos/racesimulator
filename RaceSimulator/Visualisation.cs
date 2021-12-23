using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Controller;
using System.Diagnostics;

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
            Data.CurrentRace.DriverChanged += DriversChanged;
            Data.CurrentRace.NextRace += NextRace;
        }

        //Add the new events (intialize) to the new race when previous race ends
        public static void NextRace(Object source, RaceStartEventArgs e)
        {
            Console.Clear();

            if (e.Race != null)
            {
                Initialize();

                CurrentDirection = Direction.East;
                DrawTrack(Data.CurrentRace.Track);
            }
        }

        public static void DriversChanged(Object sender, DriversChangedEventArgs args)
        {
            DrawTrack(args.Track);
        }

        public static void DrawTrack(Track track)
        {
            left = 20;
            top = 20;
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.DarkRed;

            foreach (Section section in track.Sections)
            {
                DrawSectionType(section);
            }
        }

        private static string[] FillPlaceHolders(string[] sprite, SectionData sectionData)
        {
            string[] output = (string[])sprite.Clone();
            for (int i = 0; i < output.Length; i++)
            {
                if (sectionData.Left != null)
                {
                    if (sectionData.Left.Equipment.IsBroken)
                    {
                        output[i] = output[i].Replace('1', '*');
                    } else
                    {
                        output[i] = output[i].Replace('1', char.Parse(sectionData.Left.Name.Substring(0, 1)));
                    } 
                }
                else
                {
                    output[i] = output[i].Replace('1', ' ');
                }

                if (sectionData.Right != null)
                {
                    if (sectionData.Right.Equipment.IsBroken)
                    {
                        output[i] = output[i].Replace('2', '*');
                    }
                    else
                    {
                        output[i] = output[i].Replace('2', char.Parse(sectionData.Right.Name.Substring(0, 1)));
                    }
                }
                else
                {
                    output[i] = output[i].Replace('2', ' ');
                }
            }

            return output;
        }

        private static void DrawSectionType(Section section)
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
                if (section.SectionType == SectionTypes.Finish)
                    draw(_finishVertical, section);

                if (section.SectionType == SectionTypes.LeftCorner)
                {
                    if (CurrentDirection == Direction.South)
                    {
                        draw(_cornerLefVertical, section);
                    }
                    else if (CurrentDirection == Direction.North)
                    {
                        draw(_cornerRightHorinzontal, section);
                    }
                }

                if (section.SectionType == SectionTypes.RightCorner)
                {
                    if (CurrentDirection == Direction.South)
                    {
                        draw(_cornerRightVertical, section);
                    }
                    else if (CurrentDirection == Direction.North)
                    {
                        draw(_cornerLeftHorizontal, section);
                    }
                }

                if (section.SectionType == SectionTypes.StartGrid)
                {
                    draw(_startVertical, section);
                }

                if (section.SectionType == SectionTypes.Straight)
                {
                    draw(_trackVertical, section);
                }
            }

            // Get horizontal
            if (CurrentDirection == Direction.East || CurrentDirection == Direction.West)
            {
                if (section.SectionType == SectionTypes.Finish)
                {
                    draw(_finishHorizontal, section);
                }

                if (section.SectionType == SectionTypes.LeftCorner)
                {
                    if (CurrentDirection == Direction.East)
                    {
                        draw(_cornerRightVertical, section);
                    }
                    else if (CurrentDirection == Direction.West)
                    {
                        draw(_cornerLeftHorizontal, section);
                    }
                }

                if (section.SectionType == SectionTypes.RightCorner)
                {
                    if (CurrentDirection == Direction.East)
                    {
                        draw(_cornerRightHorinzontal, section);
                    } else if (CurrentDirection == Direction.West)
                    {
                        draw(_cornerLefVertical, section);
                    }
                }

                if (section.SectionType == SectionTypes.StartGrid)
                {
                    draw(_startHorizontal, section);
                }

                if (section.SectionType == SectionTypes.Straight)
                {
                    draw(_trackHorizontal, section);
                }
            }
            // Set the new direction
            setDirection(section.SectionType);
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

        private static void draw(string[] graphics, Section section)
        {
            int tmpLeft = left;
            int tmpTop = top;
            Console.SetCursorPosition(left, top);

            string[] output = FillPlaceHolders(graphics, Data.CurrentRace.GetSectionData(section));

            foreach(string graphic in output)
            {
                Console.WriteLine(graphic);
                tmpTop += 1;

                Console.SetCursorPosition(tmpLeft, tmpTop);
            }
        }
    }
}
