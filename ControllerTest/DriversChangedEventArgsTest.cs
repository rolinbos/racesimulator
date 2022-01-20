using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    class DriversChangedEventArgsTest
    {
        [Test]
        public void setTrack()
        {
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

            DriversChangedEventArgs driversChangedEventArgs = new DriversChangedEventArgs(track);

            Assert.AreEqual(track, driversChangedEventArgs.Track);
        }
    }
}
