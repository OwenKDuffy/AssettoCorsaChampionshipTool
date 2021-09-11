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
    public class ChampionshipTests
    {
        [TestMethod()]
        public void EmptyChampionshipTest()
        {
            Championship emptyChamp = new Championship();
            Assert.AreEqual(emptyChamp.name, "Untitled Championship");
            Assert.AreEqual(emptyChamp.playerVehicle, "");
            Assert.AreEqual(emptyChamp.carSkin, "");
            //Assert.AreEqual(emptyChamp.events , new List<Event>());
            //Assert.AreEqual(emptyChamp.events,Add(new Event(0)));
            //Assert.AreEqual(emptyChamp.events,0].setPlayerVehicle(playerVehicle, ""));
            CollectionAssert.AreEqual(emptyChamp.pointsScale, new int[6] { 10, 6, 4, 3, 2, 1 });
            Assert.AreEqual(emptyChamp.pointsGoal, 50);
            //Assert.AreEqual(emptyChamp.opponents , new Opponents());
        }

        [TestMethod()]
        public void ChampionshipFromFileTest()
        {

        }

        [TestMethod()]
        public void getEventTest()
        {

        }

        [TestMethod()]
        public void SetPlayerCarTest()
        {
            Championship emptyChamp = new Championship();
            emptyChamp.SetPlayerCar("ks_ferrari_sf70h", "01_ferrari_5");


            Assert.AreEqual(emptyChamp.name, "Untitled Championship");
            Assert.AreEqual(emptyChamp.playerVehicle, "ks_ferrari_sf70h");
            Assert.AreEqual(emptyChamp.carSkin, "01_ferrari_5");
            //Assert.AreEqual(emptyChamp.events , new List<Event>());
            //Assert.AreEqual(emptyChamp.events,Add(new Event(0)));
            //Assert.AreEqual(emptyChamp.events,0].setPlayerVehicle(playerVehicle, ""));
            CollectionAssert.AreEqual(emptyChamp.pointsScale, new int[6] { 10, 6, 4, 3, 2, 1 });
            Assert.AreEqual(emptyChamp.pointsGoal, 50);
            //Assert.AreEqual(emptyChamp.opponents , new Opponents());
        }

        [TestMethod()]
        public void ExportChampionshipToFileTest()
        {

        }

        [TestMethod()]
        public void PrettyPrintChampionshipDetailsTest()
        {
            var currentConsoleOut = Console.Out;

            Championship emptyChamp = new Championship();

            //Description: 
            //Vehicle: 
            string text = "Name: Untitled Championship\nNumber of Events: 1\nAchieve 50 Points\nP1: 10 points\nP2: 6 points\nP3: 4 points\nP4: 3 points\nP5: 2 points\nP6: 1 points\n";

            using (var consoleOutput = new ConsoleOutput())
            {
                emptyChamp.PrettyPrintChampionshipDetails();

                Assert.AreEqual(text, consoleOutput.GetOuput());
            }

            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}