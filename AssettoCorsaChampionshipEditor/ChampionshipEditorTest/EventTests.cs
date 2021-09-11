using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssettoCorsaChampionshipEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssettoCorsaChampionshipEditor.Tests
{
    [TestClass()]
    public class EventTests
    {
        [TestMethod()]
        public void EventTest()
        {
            Event e = new Event(0);

            Assert.AreEqual(0, e.eventIndex);
            Assert.AreEqual("Untitled Event", e.name);
        }

        [TestMethod()]
        public void EventTest1()
        {

        }

        [TestMethod()]
        public void setPlayerVehicleTest()
        {
            Event e = new Event(0);
            e.setPlayerVehicle("ks_ferrari_sf70h", "01_ferrari_5");
            Assert.AreEqual(0, e.eventIndex);
            Assert.AreEqual("Untitled Event", e.name);
            Assert.AreEqual("ks_ferrari_sf70h", e.vehicle);
        }
    }
}