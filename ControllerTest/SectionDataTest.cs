using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    class SectionDataTest
    {
        [Test]
        public void Set_SectionData()
        {
            SectionData sectionData = new SectionData();
            Driver driver = new Driver("Rolin", 0, null, TeamColors.Red, 0);
            sectionData.Left = driver;
            sectionData.Right = driver;
            sectionData.DistanceLeft = 0;
            sectionData.DistanceRight = 100;

            Assert.AreEqual(sectionData.Left, driver);
            Assert.AreEqual(sectionData.Right, driver);
            Assert.AreEqual(sectionData.DistanceLeft, 0);
            Assert.AreEqual(sectionData.DistanceRight, 100);
            Assert.AreEqual(driver.Laps, 0);
        }
    }
}
