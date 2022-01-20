using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Controller;
namespace ControllerTest
{
    [TestFixture]
    class RaceSimulatorVisualisationTest
    {
        [Test]
        public void testDataParticipants()
        {
            Competition competition = new Competition();

            Data.Initialize(competition);
            Assert.AreEqual(Data.Competition, competition);

            competition.Participants.Add(new Driver("Rolin", 0, new Car(0, 0, 0, false), TeamColors.Blue));
            competition.Participants.Add(new Driver("Patrick", 0, new Car(0, 0, 0, false), TeamColors.Red));
            competition.Participants.Add(new Driver("Sjoerd", 0, new Car(0, 0, 0, false), TeamColors.Green));
            competition.Participants.Add(new Driver("Amber", 0, new Car(0, 0, 0, false), TeamColors.Yellow));

            Data.AddParticipants();
            Assert.AreEqual(Data.Competition, Data.Competition);

            competition.Tracks.Enqueue(new Track("Zandvoort", new SectionTypes[]
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
            competition.Tracks.Enqueue(new Track("Difficult", new SectionTypes[]
{
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
}));
            Data.AddTracks();
            Assert.AreEqual(Data.Competition, Data.Competition);
        }

        [Test]
        public void RaceStartEvent()
        {
            Competition competition = new Competition();
            competition.Participants.Add(new Driver("Rolin", 0, new Car(0, 0, 0, false), TeamColors.Blue));

            Track track = new Track("Zandvoort", new SectionTypes[]
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
            });
           
            Race race = new Race(track, competition.Participants);

            RaceStartEventArgs raceStartEventArgs = new RaceStartEventArgs(race);

            Assert.AreEqual(race, raceStartEventArgs.Race);
        }

        [Test]
        public void RaceGivePoints()
        {
            Competition competition = new Competition();
            Data.Initialize(competition);
            Track track = new Track("Zandvoort", new SectionTypes[]
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
            });
           
            Race race = new Race(track, competition.Participants);

            IParticipant participant = race.Participants[0];
            race.GivePoints(participant);

            Assert.AreEqual(4, race.Participants[0].Points);
        }
    }
}
