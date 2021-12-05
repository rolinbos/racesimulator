using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace;

        public static void Initialize(Competition competition)
        {
            Competition = competition;
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Rolin", 0, new Car(0, 0, 0, false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("Patrick", 0, new Car(0, 0, 0, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Sjoerd", 0, new Car(0, 0, 0, false), TeamColors.Green));
            //Competition.Participants.Add(new Driver("Amber", 0, new Car(0, 0, 0, false), TeamColors.Yellow));
        }

        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("Zandvoort", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
            }));
            //Competition.Tracks.Enqueue(new Track("Spa", new SectionTypes[]
            //{
            //    SectionTypes.StartGrid,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.Straight,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.Straight,
            //    SectionTypes.Straight,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.Straight,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.Finish,
            //}));
            Competition.Tracks.Enqueue(new Track("Difficult", new SectionTypes[]
{
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
            }));
        }

        /**
         * Opgave 2.5
         */
        public static void NextRace()
        {
            var track = Competition.NextTrack();
            if (track != null)
            {
                CurrentRace = new Race(track, Competition.Participants);
            }
        }
    }
}
