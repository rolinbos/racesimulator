using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace ControllerTest
{
    [TestFixture]
    class Model_Track_Name
    {
        [Test]
        public void Set_Name()
        {
            Track track = new Track("Zandvoort", new SectionTypes[] { });
            Assert.AreEqual("Zandvoort", track.Name);
        }
    }
}
