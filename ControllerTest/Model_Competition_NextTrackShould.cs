using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            this._competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track track = new Track("Spa", new SectionTypes[] {
                SectionTypes.StartGrid,
            });
            
            _competition.Tracks.Enqueue(track);

            var result = _competition.NextTrack();

            Assert.AreEqual(result, track);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track track1 = new Track("Spa", new SectionTypes[] {
                SectionTypes.StartGrid,
            });

            _competition.Tracks.Enqueue(track1);

            var result =_competition.NextTrack();
            Assert.AreEqual(result, track1);

            result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track track1 = new Track("Spa", new SectionTypes[] {
                SectionTypes.StartGrid,
            });
            Track track2 = new Track("Spa", new SectionTypes[] {
                SectionTypes.Finish,
            });
            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);

            var result = _competition.NextTrack();
            Assert.AreEqual(result, track1);

            result = _competition.NextTrack();
            Assert.AreEqual(result, track2);

            result = _competition.NextTrack();
            Assert.IsNull(result);
        }
    }
}
