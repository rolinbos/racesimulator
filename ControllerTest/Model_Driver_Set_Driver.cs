using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Model_Driver_Set_Driver
    {
        [Test]
        public void Set_Driver()
        {
            Car car = new Car(0, 0, 0, false);
            Driver driver = new Driver("Rolin", 0, car, TeamColors.Red);

            Assert.AreEqual(car, driver.Equipment);
            Assert.AreEqual("Rolin", driver.Name);
            Assert.AreEqual(0, driver.Points);
            Assert.AreEqual(TeamColors.Red, driver.TeamColor);
        }
    }
}
