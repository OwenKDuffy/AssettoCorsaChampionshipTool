using System;
using System.IO;
using System.Linq;

namespace AssettoCorsaChampionshipEditor
{

    // static class to hold global variables, etc.
    static class Globals
    {
        // global int
        public static string acDir = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\assettocorsa";

    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(Globals.acDir);
            string[] cars = GetListOfCars(Globals.acDir);
            string[] tracks = GetListOfTracks(Globals.acDir);

            Championship emptyChamp = new Championship();
            emptyChamp.SetPlayerCar(cars[96]);
            var e = emptyChamp.getEvent(0);
            e.setTrack(tracks[6]);

            emptyChamp.addEvent();
            emptyChamp.getEvent(1).setTrack(tracks[13]);


            emptyChamp.PrettyPrintChampionshipDetails();
            emptyChamp.ExportChampionshipToFile(Globals.acDir);

            listAndNumber(tracks);

            //Championship c = new Championship(Globals.acDir + "\\content\\career\\series_dtm2020");
            ////c.getEvent(0)
            //c.PrettyPrintChampionshipDetails();
            //c.ExportChampionshipToFile(Globals.acDir);
            //Console.WriteLine("Exporting");
            //Console.WriteLine("\nCars:\n");
            //Array.ForEach(cars, Console.WriteLine);
            //prettyPrintList(cars);
            //Console.WriteLine("\n\nTracks:\n");
            //Array.ForEach(tracks, Console.WriteLine);
        }


        private static void listAndNumber(string[] list)
        {
            foreach (var item in list.Select((value, i) => (value, i)))

            {
                Console.WriteLine($"{item.i}: {item.value}");
            }            
        }

        private static void prettyPrintList(string[] list, int numPerLine = 3)
        {
            for (int i = 0; i < list.Length - numPerLine; i += numPerLine)
            {
                Console.WriteLine($"{list[i], 20}\t{list[i + 1], 20}\t{list[i + 2], 20}");
            }
        }
        private static string[] GetListOfCars(string gameDirectory)
        {
            return Directory.GetDirectories(gameDirectory + "\\content\\cars").Select(Path.GetFileName).ToArray();
        }
        private static string[] GetListOfTracks(string gameDirectory)
        {
            return Directory.GetDirectories(gameDirectory + "\\content\\tracks").Select(Path.GetFileName).ToArray();
        }
    }
}
