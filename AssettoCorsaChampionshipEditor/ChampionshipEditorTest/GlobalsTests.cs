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
    public class GlobalsTests
    {
        [TestMethod()]
        public void PopulateDictionaryWithValuesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ListAndNumberTest()
        {
            string expectedOutput = @"0: ks_ferrari_812_superfast
1: ks_ferrari_f138
2: ks_ferrari_f2004
3: ks_ferrari_fxx_k
4: ks_ferrari_sf15t
5: ks_ferrari_sf70h
";
            string[] sample = new string[] { "ks_ferrari_812_superfast", "ks_ferrari_f138", "ks_ferrari_f2004", "ks_ferrari_fxx_k", "ks_ferrari_sf15t", "ks_ferrari_sf70h" };
            string output = Globals.ListAndNumber(sample);
            Assert.AreEqual(output, expectedOutput);
        }

        //[TestMethod()]
        //public void prettyPrintListTest()
        //{
        //    var currentConsoleOut = Console.Out;


        //    string text = "Hello";

        //    using (var consoleOutput = new ConsoleOutput())
        //    {
        //        target.WriteToConsole(text);

        //        Assert.AreEqual(text, consoleOutput.GetOuput());
        //    }

        //    Assert.AreEqual(currentConsoleOut, Console.Out);
        //}

        [TestMethod()]
        public void GetListOfCarsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetSkinsForCarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetListOfTracksTest()
        {
            Assert.Fail();
        }
    }
}