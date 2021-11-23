using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Model_Section_SectionTypes
    {
        [Test]
        public void SectionTypes_Set_And_Get()
        {
            Section section = new Section(SectionTypes.Straight);
            Assert.AreEqual(SectionTypes.Straight, section.SectionType);
        }
    }
}
