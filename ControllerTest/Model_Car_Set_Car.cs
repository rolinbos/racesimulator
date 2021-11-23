using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Model_Car_Set_Car
    {
        [Test]
        public void Get_Performance()
        {
            Car car = new Car(0, 0, 0, false);
  
            Assert.AreEqual(0, car.Quantity);
            Assert.AreEqual(0, car.Performance);
            Assert.AreEqual(0, car.Speed);
            Assert.AreEqual(false, car.IsBroken);
        }

        [Test]
        public void Get_Performance_Dirrent_Values()
        {
            Car car = new Car(10, 5, 16, true);

            Assert.AreEqual(10, car.Quantity);
            Assert.AreEqual(5, car.Performance);
            Assert.AreEqual(16, car.Speed);
            Assert.AreEqual(true, car.IsBroken);
        }
    }
}
